using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using AForge.Controls;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;
using Sunny.UI;

namespace MicroNano3DPrinter
{
    public partial class FormBody : UIPage
    {
        public FormBody()
        {
            InitializeComponent();
        }

        public static Printer printer = new Printer(150, 130, 100);//创建静态打印机对象
        public static SerialPortController serial = new SerialPortController();//创建静态串口管理对象
        List<PrintLayer> printLayers = new List<PrintLayer>();//打印层
        List<PrintChannel> printChannels = new List<PrintChannel>();//打印通道
        int indexLayer = -1;//当前层索引
        int indexChannel = 0;//当前通道索引

        //用于提示的委托
        public delegate void TipsDelegate(string tip);
        public event TipsDelegate TipEvent;

        //画图要用到的
        PathDrawer pathDrawer;
        System.Timers.Timer timer = new System.Timers.Timer(interval: 2000);//用于检测是否是打印中空闲
        WpfControlLibrary3D.MainControl mainControl;

        //
        System.Timers.Timer timerIsStop = new System.Timers.Timer(interval: 100);

        //摄像头
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        VideoFileWriter videoWriter;
        int tickNum = 0;
        System.Timers.Timer timeCount;
        bool isRecord = false;

        private void FormBody_Load(object sender, EventArgs e)
        {
            //初始化串口
            cbbComList.Items.AddRange(SerialPort.GetPortNames());//串口设置选项卡中的端口选择框,选择框内容为GetPortNames()获取计算机的端口
            if (cbbComList.Items.Count > 0)
            {
                cbbComList.SelectedIndex = cbbComList.Items.Count - 1;//如果端口选择框可选项数目大于0，默认选择最后一项
            }
            cbbBaudRate.SelectedIndex = 3;          //..串口设置选项卡中的波特率选择框，默认选择第3项
            cbbDataBits.SelectedIndex = 0;
            cbbParity.SelectedIndex = 0;
            cbbStopBits.SelectedIndex = 0;
            //openSerial();
            //初始化通道
            rbChannel1.Checked = true;
            printChannels.Add(new PrintChannel(Color.Salmon, new Point(0, 0), 2000, 1600, 25.35));
            printChannels.Add(new PrintChannel(Color.ForestGreen, new Point(12, 0), 2000, 1600, 25.35));
            printChannels.Add(new PrintChannel(Color.MediumBlue, new Point(44, 70), 2000, 1600, 25.35));
            printChannels.Add(new PrintChannel(Color.Crimson, new Point(0, 0), 2000, 1600, 25.35));
            printChannels.Add(new PrintChannel(Color.Yellow, new Point(0, 0), 2000, 1600, 25.35));
            printer.printChannel = printChannels[indexChannel];
            //初始化位置显示区
            pathDrawer = new PathDrawer();
            Init3DControl();

            //初始化事件
            serial.receiveDataEvent += Serial_receiveDataEvent;
            serial.sendDataEvent += Serial_sendDataEvent;
            timer.Elapsed += Timer_Elapsed;
            timerIsStop.Elapsed += TimerIsStop_Elapsed;
            timerIsStop.AutoReset = true;//必须加，否则停不下来

            //初始化摄像头
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                cbbVideoDevice.Items.Add(device.Name);
            }
            timeCount = new System.Timers.Timer();
            timeCount.Interval = 1000;
            timeCount.AutoReset = true;
            timeCount.Elapsed += TimeCount_Elapsed;
            videoWriter = new VideoFileWriter();
        }

        private void TimeCount_Elapsed(object sender, ElapsedEventArgs e)
        {
            tickNum++;
            int temp = tickNum;
            int sec = temp % 60;
            int min = temp / 60;
            if (60 == min)
            {
                min = 0;
                min++;
            }
            int hour = min / 60;
            String tick = "";
            if (hour < 10)
            {
                tick += "0" + hour.ToString();
            } 
            else
            {
                tick += hour.ToString();
            }
            tick += ":";
            if (min < 10)
            {
                tick += "0" + min.ToString();
            }
            else
            {
                tick += min.ToString();
            }
            tick += ":";
            if (sec < 10)
            {
                tick += "0" + sec.ToString();
            }
            else
            {
                tick += sec.ToString();
            }
            Invoke(new EventHandler(delegate
            {
                lblRECTime.Text = tick;
            }));
        }

        public void Init3DControl()
        {
            mainControl = new WpfControlLibrary3D.MainControl();
            elementHost.Child = mainControl;
            pathDrawer.setMainControl(mainControl);
        }

        int tagTimer = 0;
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)//用于检测是否是打印中空闲
        {
            tagTimer++;
            if (tagTimer > 1)
            {
                Invoke(new EventHandler(delegate
                {
                    lightFree.State = UILightState.On;
                    lightPrinting.State = UILightState.Off;
                    lightPause.State = UILightState.Off;
                }));
                printer.printerState = 0;
                timer.Stop();
                tagTimer = 0;
            }
        }

        //-----------------------------------------打印层设置界面对应的代码-------------------------------------------

        private void flushDgv()//刷新dgv
        {
            dgvPrintLayer.Rows.Clear();
            for (int i = 0; i < printLayers.Count; i++)
            {
                dgvPrintLayer.Rows.Add();
                dgvPrintLayer.Rows[i].Cells[0].Value = i + 1;
                switch (printLayers[i].mode)
                {
                    case 0:
                        dgvPrintLayer.Rows[i].Cells[1].Value = "蛇形";
                        break;
                    case 1:
                        dgvPrintLayer.Rows[i].Cells[1].Value = "回形";
                        break;
                    case 2:
                        dgvPrintLayer.Rows[i].Cells[1].Value = "网格形";
                        break;
                }
                dgvPrintLayer.Rows[i].Cells[2].Value = printLayers[i].channel + 1;
                switch (printLayers[i].state)
                {
                    case 0:
                        dgvPrintLayer.Rows[i].Cells[3].Value = "就绪";
                        break;
                    case 1:
                        dgvPrintLayer.Rows[i].Cells[3].Value = "正在打印";
                        break;
                    case 2:
                        dgvPrintLayer.Rows[i].Cells[3].Value = "打印完毕";
                        break;
                    case 3:
                        dgvPrintLayer.Rows[i].Cells[3].Value = "等待打印";
                        break;
                }
            }
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            FormLayer addLayer = new FormLayer(new PrintLayer(0, 0, 10, 50, 50, 50, 40, 0, 0), uiStyleManager.Style);
            addLayer.AddLayerEvent += AddLayer_AddLayerEvent;
            addLayer.ShowDialog();
        }

        private void AddLayer_AddLayerEvent(PrintLayer layerChanged)
        {
            printLayers.Add(layerChanged);
            flushDgv();
            if (printLayers.Count == 1)
            {
                changeLayer(0);
            }
        }

        private void btnDelLayer_Click(object sender, EventArgs e)
        {
            if (printLayers.Count == 0)
            {
                TipEvent("无数据!");
                return;
            }
            DialogResult RSS = MessageBox.Show(this, "确定要删除选中行数据码？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (RSS)
            {
                case DialogResult.Yes:
                    printLayers.RemoveAt(indexLayer);
                    if (printLayers.Count == 0)
                    {
                        indexLayer = -1;
                    }
                    flushDgv();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void btnEditLayer_Click(object sender, EventArgs e)
        {
            if (printLayers.Count == 0)
            {
                TipEvent("无数据!");
                return;
            }
            FormLayer editLayer = new FormLayer(printLayers[indexLayer], uiStyleManager.Style);
            editLayer.AddLayerEvent += EditLayer_AddLayerEvent;
            editLayer.ShowDialog();
        }

        private void EditLayer_AddLayerEvent(PrintLayer layerChanged)
        {
            printLayers[indexLayer] = layerChanged;
            flushDgv();
        }

        private void btnLoadLayer_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(dialog.FileName, Encoding.UTF8);
                string mode = reader.ReadLine();
                string channel = reader.ReadLine();
                string times = reader.ReadLine();
                string x_size = reader.ReadLine();
                string y_size = reader.ReadLine();
                string x_start = reader.ReadLine();
                string y_start = reader.ReadLine();
                string z_start = reader.ReadLine();
                string solidfyTime = reader.ReadLine();
                printLayers.Add(new PrintLayer(int.Parse(mode), int.Parse(channel), int.Parse(times), double.Parse(x_size), double.Parse(y_size), double.Parse(x_start), double.Parse(y_start), double.Parse(z_start), int.Parse(solidfyTime)));
                flushDgv();
                if (printLayers.Count == 1)
                {
                    changeLayer(0);
                }
            }
        }

        private void btnSaveLayer_Click(object sender, EventArgs e)
        {
            if (printLayers.Count == 0)
            {
                TipEvent("无数据!");
                return;
            }
            string path = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择要保存的文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
            }
            else
            {
                return;
            }
            string filename = "gcode" + indexLayer + ".txt";
            TextWriter tw = new StreamWriter(Path.Combine(path, filename), false, Encoding.UTF8);
            try
            {
                tw.WriteLine(printLayers[indexLayer].mode);
                tw.WriteLine(printLayers[indexLayer].channel);
                tw.WriteLine(printLayers[indexLayer].interval);
                tw.WriteLine(printLayers[indexLayer].x_size);
                tw.WriteLine(printLayers[indexLayer].y_size);
                tw.WriteLine(printLayers[indexLayer].y_size);
                tw.WriteLine(printLayers[indexLayer].y_start);
                tw.WriteLine(printLayers[indexLayer].z_start);
                tw.WriteLine(printLayers[indexLayer].solidfyTime);
                TipEvent("保存成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存文件错误");
            }
            finally
            {
                tw.Close();
            }
        }

        private void btnUpLayer_Click(object sender, EventArgs e)
        {
            if (printLayers.Count == 0)
            {
                TipEvent("无数据!");
                return;
            }
            if (indexLayer == 0)
            {
                TipEvent("已经是第一行了!");
                return;
            }
            printLayers.Insert(indexLayer - 1, printLayers[indexLayer]);
            printLayers.RemoveAt(indexLayer + 1);
            int temp = indexLayer;
            flushDgv();
            dgvPrintLayer.Rows[temp - 1].Selected = true;
            indexLayer = dgvPrintLayer.SelectedRows[0].Index;
        }

        private void btnDownLayer_Click(object sender, EventArgs e)
        {
            if (printLayers.Count == 0)
            {
                TipEvent("无数据!");
                return;
            }
            if (indexLayer == printLayers.Count - 1)
            {
                TipEvent("已经是最后一行了!");
                return;
            }
            printLayers.Insert(indexLayer + 2, printLayers[indexLayer]);
            printLayers.RemoveAt(indexLayer);
            int temp = indexLayer;
            flushDgv();
            dgvPrintLayer.Rows[temp + 1].Selected = true;
            indexLayer = dgvPrintLayer.SelectedRows[0].Index;
        }

        private void dgvPrintLayer_SelectIndexChange(object sender, int index)
        {
            if (printer.printerState != 0)
            {
                TipEvent("正在打印中，不能切换层!");
                return;
            }
            if (index >= printLayers.Count)
            {
                return;
            }
            changeLayer(index);
        }

        private void changeLayer(int index)
        {
            indexLayer = index;
            printer.PrintLayer = printLayers[indexLayer];
            label_Set();
            pathDrawer.initPath(printLayers[indexLayer].points, printer.printChannel, this);
        }

        private void label_Set()
        {
            if (dgvPrintLayer.SelectedRows.Count > 0)
            {
                //由于此线程(非主线程)会改变界面外观，需要用Invoke来避免程序出错。另一例见btnSend_Click()
                Invoke(new EventHandler(delegate
                {
                    lblLayerNow.Text = "当前层：" + (1 + indexLayer);
                    lblLayerSize.Text = "尺寸：" + printLayers[indexLayer].x_size + " mm *" + printLayers[indexLayer].y_size + " mm ";
                    lblLayerInterval.Text = "间隔：" + printLayers[indexLayer].interval + " mm";
                    lblLayerStart.Text = "起点：" + printLayers[indexLayer].x_start + " mm *" + printLayers[indexLayer].y_start + " mm ";
                }));
            }
            else
            {
                Invoke(new EventHandler(delegate
                {
                    lblLayerNow.Text = "当前层：";
                    lblLayerSize.Text = "尺寸：";
                    lblLayerInterval.Text = "间隔：";
                    lblLayerStart.Text = "起点：";
                }));
            }
        }



        //------------------------------------------通道设置界面对应的代码--------------------------------------------

        private void changeChannel(int index)
        {
            if (indexChannel == index)
            {
                return;
            }
            indexChannel = index;
            printer.printChannel = printChannels[indexChannel];
            int re = PrintChannel.setChannel(indexChannel);
            if (re == 0)
            {
                //printChannels[indexChannel].setPulseWidth(printChannels[indexChannel].OpenTime);
                //printChannels[indexChannel].setResolution(printChannels[indexChannel].PrintDistance);
                //printChannels[indexChannel].setAirPress(printChannels[indexChannel].AirPress);
                switch (indexChannel)
                {
                    case 0:
                        //penRealTime = new Pen(Color.Salmon, 2);
                        break;
                    case 1:
                        //penRealTime = new Pen(Color.ForestGreen, 2);
                        break;
                    case 2:
                        //penRealTime = new Pen(Color.MediumBlue, 2);
                        break;
                    case 3:
                        //penRealTime = new Pen(Color.Crimson, 2);
                        break;
                    case 4:
                        //penRealTime = new Pen(Color.Yellow, 2);
                        break;
                }
            }
        }

        private void rbChannel1_CheckedChanged(object sender, EventArgs e)
        {
            changeChannel(0);
        }

        private void rbChannel2_CheckedChanged(object sender, EventArgs e)
        {
            changeChannel(1);
        }

        private void rbChannel3_CheckedChanged(object sender, EventArgs e)
        {
            changeChannel(2);
        }

        private void rbChannel4_CheckedChanged(object sender, EventArgs e)
        {
            changeChannel(3);
        }

        private void rbChannel5_CheckedChanged(object sender, EventArgs e)
        {
            changeChannel(4);
        }

        private void rbChannel6_CheckedChanged(object sender, EventArgs e)
        {
            //changeChannel(5);
        }

        private void btnAirPress_Click(object sender, EventArgs e)
        {
            printChannels[indexChannel].setAirPress(double.Parse(txtAirPress.Text));
        }

        private void btnTemperature_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintDistance_Click(object sender, EventArgs e)
        {
            printChannels[indexChannel].setResolution(double.Parse(txtPrintDistance.Text));
        }

        private void btnOpenTime_Click(object sender, EventArgs e)
        {
            printChannels[indexChannel].setPulseWidth(double.Parse(txtOpenTime.Text));
        }

        //------------------------------------------串口设置界面对应的代码--------------------------------------------

        private void Serial_sendDataEvent(string value)//串口发送数据调用此事件
        {
            this.Invoke((EventHandler)(delegate
            {
                rtbSendData.SelectionStart = rtbSendData.Text.Length;
                rtbSendData.ScrollToCaret();
                rtbSendData.Text += value + '\n';
            }));
        }

        Point3D begin = new Point3D(0, 0, 0, 0);
        Point3D next = new Point3D(0, 0, 0, 0);
        private void Serial_receiveDataEvent(string value)//串口收到数据调用此事件
        {
            UploadInfo info = Utilities.hexStrToInfo(value);
            printer.X = info.xLocation / 1000;
            printer.Y = info.yLocation / 1000;
            printer.Z = info.zLocation / 1000;
            //因为要访问ui资源，所以需要使用invoke方式同步ui。  
            this.Invoke((EventHandler)delegate
            {

                switch (info.sysStatus)
                {
                    case "80":
                        if (printer.printerState == 0)
                        {
                            break;
                        }
                        timer.Start();
                        break;
                    case "82":
                        tagTimer = 0;
                        if (printer.printerState == 1)
                        {
                            break;
                        }
                        printer.printerState = 1;
                        lightFree.State = UILightState.Off;
                        lightPrinting.State = UILightState.On;
                        lightPause.State = UILightState.Off;
                        break;
                    case "83":
                        timer.Stop();
                        if (printer.printerState == 2)
                        {
                            break;
                        }
                        printer.printerState = 2;
                        lightFree.State = UILightState.Off;
                        lightPrinting.State = UILightState.Off;
                        lightPause.State = UILightState.On;
                        break;
                }
                rtbReceiveData.SelectionStart = rtbReceiveData.Text.Length;
                rtbReceiveData.ScrollToCaret();
                rtbReceiveData.Text += value + '\n';
                //显示状态
                ledX.Text = "X:" + string.Format("{0:F2}", printer.X);
                ledY.Text = "Y:" + string.Format("{0:F2}", printer.Y);
                ledZ.Text = "Z:" + string.Format("{0:F2}", printer.Z);
                metChannel.Angle = (info.CurChannelNum - 1) * 60;
                metAirPress.Value = Utilities.currentToPressure(info.AirPress);
                metTemperature.Value = info.Temperature;
                metPrintDistance.Value = info.PrintDistance;
                metOpenTime.Value = info.OpenTime;
                lblAirPressValue.Text = metAirPress.Value + "";
                lblTemperatureValue.Text = metTemperature.Value + "";
                lblPrintDistanceValue.Text = metPrintDistance.Value + "";
                lblOpenTimeValue.Text = metOpenTime.Value + "";
                //画实时路径
                pathDrawer.changeHead(printer.X, printer.Y, printer.Z);
                next.X = printer.X + printChannels[indexChannel].position.X;
                next.Y = printer.Y + printChannels[indexChannel].position.Y;
                next.Z = printer.Z;
                pathDrawer.draw(begin, next);
                begin.X = next.X;
                begin.Y = next.Y;
                begin.Z = next.Z;
            });
        }

        private void cbbComList_DropDown(object sender, EventArgs e)
        {
            cbbComList.Text = null;
            cbbComList.Items.Clear();
            cbbComList.Items.AddRange(SerialPort.GetPortNames());
            if (cbbComList.Items.Count > 0)
            {
                cbbComList.SelectedIndex = 0;
            }
        }

        private void btnSerialSwitch_Click(object sender, EventArgs e)
        {
            if (cbbComList.Items.Count <= 0)
            {
                TipEvent("没有发现串口,请检查线路！");
                return;
            }
            openSerial();
        }

        public void openSerial()
        {
            if (serial.serialPort.IsOpen == false)  //..此时如果串口未开启，则打开串口
            {
                serial.serialPort.PortName = cbbComList.SelectedItem.ToString();           //..串口名：COM1等
                serial.serialPort.BaudRate = Convert.ToInt32(cbbBaudRate.SelectedItem.ToString());         //..比特率
                serial.serialPort.Parity = (Parity)Convert.ToInt32(cbbParity.SelectedIndex.ToString());        //..校验位
                serial.serialPort.DataBits = Convert.ToInt32(cbbDataBits.SelectedItem.ToString());         //..数据位
                serial.serialPort.StopBits = (StopBits)Convert.ToInt32(cbbStopBits.SelectedItem.ToString());           //..停止位
                try
                {
                    serial.serialPort.Open();
                    bubSerial.On = true;
                    bubSerial.Blink = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else                  //..如果此时串口已开启，则关闭串口
            {
                try
                {
                    serial.serialPort.Close();
                    bubSerial.Blink = false;
                    bubSerial.On = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            btnSend.Enabled = serial.serialPort.IsOpen;
            btnSerialSwitch.Text = serial.serialPort.IsOpen ? "关闭串口" : "打开串口";
            //..打开串口状态下，串口参数不可设置；否则可设置。可见，串口可设置性与串口状态是相反的关系
            cbbComList.Enabled = !serial.serialPort.IsOpen;
            cbbBaudRate.Enabled = !serial.serialPort.IsOpen;
            cbbParity.Enabled = !serial.serialPort.IsOpen;
            cbbDataBits.Enabled = !serial.serialPort.IsOpen;
            cbbStopBits.Enabled = !serial.serialPort.IsOpen;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            serial.sendGCode(txtGCode.Text);
        }

        public void changeColor()
        {
            tabPagePrintLayer.BackColor = dgvPrintLayer.BackgroundColor;
            tabPageChannel.BackColor = dgvPrintLayer.BackgroundColor;
            tabPageSerialPort.BackColor = dgvPrintLayer.BackgroundColor;
            tabPageSet.BackColor = dgvPrintLayer.BackgroundColor;
            TipEvent("主题颜色已变更 ^0^");
        }

        private void uiBtnBlue_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Blue;
            changeColor();
        }

        private void uiBtnGreen_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Green;
            changeColor();
        }

        private void uiBtnOrange_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Orange;
            changeColor();
        }

        private void uiBtnRed_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Red;
            changeColor();
        }

        private void uiBtnGray_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Gray;
            changeColor();
        }

        private void uiBtnPurple_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Purple;
            changeColor();
        }

        private void uiBtnLGreen_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.LayuiGreen;
            changeColor();
        }

        private void uiBtnLRed_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.LayuiRed;
            changeColor();
        }

        private void uiBtnLOrange_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.LayuiOrange;
            changeColor();
        }

        private void uiBtnDBlue_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.DarkBlue;
            changeColor();
        }

        private void uiBtnBlack_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Black;
            changeColor();
        }

        private void uiBtnColorful_Click(object sender, EventArgs e)
        {
            uiStyleManager.Style = UIStyle.Colorful;
            changeColor();
        }

        private void timerDraw_Tick(object sender, EventArgs e)
        {

        }

        private void btnBeginPrint_Click(object sender, EventArgs e)
        {
            if (!serial.serialPort.IsOpen)
            {
                TipEvent("串口未打开！");
                return;
            }
            if (indexLayer == -1)
            {
                TipEvent("无打印层！");
                return;
            }
            if (printer.printerState != 0)
            {
                TipEvent("正在打印，请稍后再试！");
                return;
            }
            probar.Value = 0;
            Thread thread = new Thread(new ThreadStart(LoadOneLayerData));
            thread.IsBackground = true;
            thread.Start();
        }

        private void LoadOneLayerData()
        {
            List<Point3D> points = printLayers[indexLayer].points;
            Invoke(new EventHandler(delegate
            {
                probar.Maximum = points.Count;
            }));
            for (int i = 0; i < points.Count; i++)
            {
                Invoke(new EventHandler(delegate
                {
                    probar.Value = i + 1;
                }));
                printer.toPoint(points[i]);
            }
            if (printLayers[indexLayer].solidfyTime != 0)
            {
                printer.moveToSolidifyZero();
            }
        }

        int tagPause = 0;
        private void btnEmergePause_Click(object sender, EventArgs e)
        {
            if (tagPause == 0)
            {
                tagPause = 1;
                printer.Pause();
                btnEmergePause.Text = "继续";
            }
            else if (tagPause == 1)
            {
                tagPause = 0;
                printer.Continue();
                btnEmergePause.Text = "暂停";
            }
        }

        private void btnToZero_Click(object sender, EventArgs e)
        {
            printer.moveToAbsolute(0, 0, 0);
        }

        private void btnToPrintZero_Click(object sender, EventArgs e)
        {
            if (printLayers.Count == 0)
            {
                TipEvent("请先选择一层！");
                return;
            }
            Point3D point = printLayers[indexLayer].points[0];
            printer.moveToRelative(point.X, point.Y, point.Z);
        }

        private void btnToSolidfyZero_Click(object sender, EventArgs e)
        {
            if (printLayers.Count == 0)
            {
                TipEvent("请先选择一层！");
                return;
            }
            printer.moveToSolidifyZero();
        }

        private void btnXAdd_Click(object sender, EventArgs e)
        {
            printer.x_moveUp(10);
        }

        private void btnXSub_Click(object sender, EventArgs e)
        {
            printer.x_moveDown(10);
        }

        private void btnYAdd_Click(object sender, EventArgs e)
        {
            printer.y_moveUp(10);
        }

        private void btnYSub_Click(object sender, EventArgs e)
        {
            printer.y_moveDown(10);
        }

        private void btnZAdd_Click(object sender, EventArgs e)
        {
            printer.z_moveUp(10);
        }

        private void btnZSub_Click(object sender, EventArgs e)
        {
            printer.z_moveDown(10);
        }

        bool solodfyLight = false;
        private void btnSolidfyLight_Click(object sender, EventArgs e)
        {
            if (solodfyLight)
            {
                printer.SendGCode("M251");
            }
            else
            {
                printer.SendGCode("M250");
            }
            solodfyLight = !solodfyLight;
        }

        private void btnPrintWall_Click(object sender, EventArgs e)
        {
            if (!serial.serialPort.IsOpen)
            {
                TipEvent("串口未打开！");
                return;
            }
            if (indexLayer == -1)
            {
                TipEvent("无打印层！");
                return;
            }
            if (printer.printerState != 0)
            {
                TipEvent("正在打印，请稍后再试！");
                return;
            }
            probar.Value = 0;
            Thread thread = new Thread(new ThreadStart(WallLayerData));
            thread.IsBackground = true;
            thread.Start();
        }

        private void WallLayerData()
        {
            List<Point3D> points = new List<Point3D>();
            PrintLayer printLayer = printLayers[indexLayer];
            points.Add(new Point3D(printLayer.x_start, printLayer.y_start, printLayer.z_start, 0));
            points.Add(new Point3D(printLayer.x_start + printLayer.x_size, printLayer.y_start, printLayer.z_start, 1));
            points.Add(new Point3D(printLayer.x_start + printLayer.x_size, printLayer.y_start + printLayer.y_size, printLayer.z_start, 1));
            points.Add(new Point3D(printLayer.x_start, printLayer.y_start + printLayer.y_size, printLayer.z_start, 1));
            points.Add(new Point3D(printLayer.x_start, printLayer.y_start, printLayer.z_start, 1));
            Invoke(new EventHandler(delegate
            {
                probar.Maximum = points.Count;
            }));
            for (int i = 0; i < points.Count; i++)
            {
                Invoke(new EventHandler(delegate
                {
                    probar.Value = i + 1;
                }));
                printer.toPoint(points[i]);
            }
        }

        private void btnSpread_Click(object sender, EventArgs e)
        {
            uiTabControlMenu.Size = new Size(200, mainHeight);
        }

        int mainWidth = 0;
        int mainHeight = 0;
        public void flushSize(int sw, int sh)
        {
            mainWidth = sw;
            mainHeight = sh;
            Invoke(new EventHandler(delegate
            {
                uiTabControlMenu.Size = new Size(200, mainHeight);
                pnlShow.Height = mainHeight;
                pnlShow.Location = new Point(mainWidth - 320, 0);
                uiLine1.Height = mainHeight;
                uiLine2.Height = mainHeight;
                uiLine3.Height = mainHeight;
                uiLine4.Height = mainHeight;
                uiLine5.Height = mainHeight;
                elementHost.Width = mainWidth - 600;
                elementHost.Height = mainHeight - 200;
                probar.Top = mainHeight - 35;
                btnSolidfyLight.Top = mainHeight - 40;
            }));
            Application.DoEvents();
        }

        private void uiTabControlMenu_Click(object sender, EventArgs e)
        {
            uiTabControlMenu.Size = new Size(1060, mainHeight);
        }

        private void btnBeginAll_Click(object sender, EventArgs e)
        {
            if (!serial.serialPort.IsOpen)
            {
                TipEvent("串口未打开！");
                return;
            }
            if (indexLayer == -1)
            {
                TipEvent("无打印层！");
                return;
            }
            if (printer.printerState != 0)
            {
                TipEvent("正在打印，请稍后再试！");
                return;
            }
            probar.Value = 0;
            indexLayer = 0;
            Thread thread = new Thread(new ThreadStart(LoadLayerData));
            thread.IsBackground = true;
            thread.Start();
        }

        private void LoadLayerData()
        {
            //changeLayer(indexLayer);
            List<Point3D> points = printLayers[indexLayer].points;
            Invoke(new EventHandler(delegate
            {
                probar.Maximum = points.Count;
            }));
            for (int j = 0; j < points.Count; j++)
            {
                Invoke(new EventHandler(delegate
                {
                    probar.Value = j + 1;
                }));
                printer.toPoint(points[j]);
            }
            if (printLayers[indexLayer].solidfyTime != 0)
            {
                printer.moveToSolidifyZero();
            }
            timerIsStop.Start();
        }

        private void TimerIsStop_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (printer.printerState == 0)
            {
                timerIsStop.Stop();
                Thread.Sleep(printLayers[indexLayer].solidfyTime * 1000);
                if (indexLayer < printLayers.Count - 1)
                {
                    indexLayer++;
                    Thread thread = new Thread(new ThreadStart(LoadLayerData));
                    thread.IsBackground = true;
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("全部打印完成");
                }
                return;
            }
        }

        private void cbbVideoDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShutCamera();
            videoSource = new VideoCaptureDevice(videoDevices[cbbVideoDevice.SelectedIndex].MonikerString);
            videoSourcePlayer.VideoSource = videoSource;
            videoSourcePlayer.NewFrame += VideoSourcePlayer_NewFrame;
            videoSourcePlayer.Start();
        }

        private void VideoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            try
            {
                if (isRecord && videoWriter.IsOpen)
                {
                    videoWriter.WriteVideoFrame(image);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("视频写入问题：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShutCamera()
        {
            if (videoSourcePlayer.VideoSource != null)
            {
                if (videoWriter.IsOpen)
                {
                    videoWriter.Close();
                    isRecord = false;
                    timeCount.Stop();
                    tickNum = 0;
                    lightIsREC.State = UILightState.Off;
                    lblRECTime.Text = "00:00:00";
                }
                videoSourcePlayer.SignalToStop();
                videoSourcePlayer.WaitForStop();
                videoSourcePlayer.VideoSource = null;
            }
        }

        Bitmap photo;
        private void btnSnap_Click(object sender, EventArgs e)
        {
            photo = videoSourcePlayer.GetCurrentVideoFrame();
            pictureBox.Image = photo;
        }

        private void btnSavePhoto_Click(object sender, EventArgs e)
        {
            try
            {
                string picName = GetImagePath() + "\\" + DateTime.Now.ToString("yyyyMMdd HH-mm-ss") + ".jpg";
                photo.Save(picName);
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetImagePath()
        {
            string path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)
                         + Path.DirectorySeparatorChar.ToString() + "MyPicture";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private void btnBeginRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer.IsRunning)
                {
                    Bitmap bitmap = videoSourcePlayer.GetCurrentVideoFrame();
                    if (bitmap == null)
                    {
                        MessageBox.Show("摄像头未准备好", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //创建一个视频文件
                    String picName = GetVideoPath() + "\\" + DateTime.Now.ToString("yyyyMMdd HH-mm-ss") + ".avi";
                    timeCount.Start();//是否执行System.Timers.Timer.Elapsed事件；
                    isRecord = true;
                    lightIsREC.State = UILightState.Blink;
                    videoWriter = new VideoFileWriter();
                    videoWriter.Open(picName, bitmap.Width, bitmap.Height, 10);
                }
                else
                {
                    MessageBox.Show("没有视频源输入，无法录制视频。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("摄像头异常：" + ex.Message);
            }
        }

        private String GetVideoPath()
        {
            String path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)
                         + Path.DirectorySeparatorChar.ToString() + "MyVideo";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private void btnPauseRecord_Click(object sender, EventArgs e)
        {
            if (btnPauseRecord.Text.Trim() == "暂停录像")
            {
                isRecord = false;
                btnPauseRecord.Text = "恢复录像";
                timeCount.Enabled = false;  //暂停计时
                lightIsREC.State = UILightState.Off;
                return;
            }
            if (btnPauseRecord.Text.Trim() == "恢复录像")
            {
                isRecord = true;
                btnPauseRecord.Text = "暂停录像";
                timeCount.Enabled = true;   //恢复计时
                lightIsREC.State = UILightState.Blink;
            }
        }

        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            try
            {
                videoWriter.Close();
                isRecord = false;
                lightIsREC.State = UILightState.Off;
                timeCount.Stop();
                tickNum = 0;
                lblRECTime.Text = "00:00:00";
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbVideoDevice_DropDown(object sender, EventArgs e)
        {
            cbbVideoDevice.Items.Clear();
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                cbbVideoDevice.Items.Add(device.Name);
            }
        }

        private void btnOpenMyVideo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(GetVideoPath());
        }

        private void btnOpenMyPhoto_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(GetImagePath());
        }
    }
}
