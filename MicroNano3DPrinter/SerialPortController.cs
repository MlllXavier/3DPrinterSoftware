using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroNano3DPrinter
{
    public delegate void receiveDataDelegate(string value);
    public delegate void sendDataDelegate(string value);

    public class SerialPortController
    {
        public SerialPort serialPort = new SerialPort();
        public string portName = "";
        public int baudRate = 0;
        public int parity = 0;
        public int dataBits = 0;
        public int stopBits = 0;
        string receiveData = "";
        string sendData = "";

        public SerialPortController()
        {
            serialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);//绑定事件   数据接收缓存
        }

        public event receiveDataDelegate receiveDataEvent;

        StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
        private void dataReceived(object sender, SerialDataReceivedEventArgs e)//数据接收事件
        {
            int n = serialPort.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致  
            byte[] buf = new byte[n];//声明一个临时数组存  储当前来的串口数据  
            serialPort.Read(buf, 0, n);//读取缓冲数据
            builder.Clear();//清除字符串构造器的内容  
            int tempReceiveCount = 0;
            string tempStr = "";
            foreach (byte b in buf)
            {
                string temp = b.ToString("X2");
                //string temp = b.ToString();
                tempReceiveCount++;
                tempStr += temp + " ";
                builder.Append(temp + " ");
                if (tempReceiveCount == 30)//满一帧数据调用一次receive
                {
                    receiveData = builder.ToString();
                    receiveDataEvent(receiveData);
                }
            }
        }

        public event sendDataDelegate sendDataEvent;

        public int sendGCode(string gcode)//0：正确，-1：串口写入异常，1：串口未打开
        {
            sendData = gcode;
            byte[] data = Encoding.ASCII.GetBytes(gcode);
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Write(data, 0, data.Length);
                    sendDataEvent(sendData);
                    Thread.Sleep(100);//降低发送速度，以防速度太快，下位机收不到
                    return 0;
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}
