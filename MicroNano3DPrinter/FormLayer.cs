using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroNano3DPrinter
{
    public partial class FormLayer : UIForm
    {
        int tag = 0;                //..用于存储选中的打印方式

        public FormLayer(PrintLayer layer, UIStyle uiStyle)
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            //设置默认值
            tag = layer.mode;
            txtX.Text = layer.x_start + "";
            txtY.Text = layer.y_start + "";
            txtZ.Text = layer.z_start + "";
            txtWidth.Text = layer.x_size + "";
            txtLength.Text = layer.y_size + "";
            txtDensity.Text = layer.interval + "";
            txtSolidfyTime.Text = layer.solidfyTime + "";
        }

        private void ibSnailMode_Click(object sender, EventArgs e)
        {
            tag = 0;
            ibSnailMode.BorderStyle = BorderStyle.FixedSingle;
            ibZigzagMode.BorderStyle = BorderStyle.None;
            ibGridMode.BorderStyle = BorderStyle.None;
        }

        private void ibZigzagMode_Click(object sender, EventArgs e)
        {
            tag = 1;
            ibSnailMode.BorderStyle = BorderStyle.None;
            ibZigzagMode.BorderStyle = BorderStyle.FixedSingle;
            ibGridMode.BorderStyle = BorderStyle.None;
        }

        private void ibGridMode_Click(object sender, EventArgs e)
        {
            tag = 2;
            ibSnailMode.BorderStyle = BorderStyle.None;
            ibZigzagMode.BorderStyle = BorderStyle.None;
            ibGridMode.BorderStyle = BorderStyle.FixedSingle;
        }

        //声明委托
        public delegate void AddLayerDelegate(PrintLayer layerChanged);
        //声明事件
        public event AddLayerDelegate AddLayerEvent;

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            AddLayerEvent(new PrintLayer(tag, 0, int.Parse(txtDensity.Text), double.Parse(txtWidth.Text), double.Parse(txtLength.Text), double.Parse(txtX.Text), double.Parse(txtY.Text), double.Parse(txtZ.Text), int.Parse(txtSolidfyTime.Text)));
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
