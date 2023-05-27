using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroNano3DPrinter
{
    public partial class FormMain : UIHeaderMainFooterFrame
    {
        public static FormBody formBody = new FormBody();

        public FormMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            int pageIndex = 1000;
            AddPage(formBody, ++pageIndex);
        }

        protected override CreateParams CreateParams//用于解决绘制控件闪烁的问题
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            formBody.TipEvent += FormBody_TipEvent;
            flushSize();
        }

        private void FormBody_TipEvent(string tip)
        {
            pnlTips.Text = "    提示：" + tip;
            pnlNowTime.Text = "—— " + DateTime.Now.ToLongTimeString().ToString();
            
            pnlHeader.BackColor = pnlTips.BackColor;
        }

        public void flushSize()
        {
            pnlHeader.Width = Width;
            pnlNowTime.Location = new Point(Width - 180, 5);
            formBody.flushSize(Width, Height - 160);

        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            flushSize();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            formBody.ShutCamera();
        }
    }
}
