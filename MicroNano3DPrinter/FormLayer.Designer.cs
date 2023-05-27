
namespace MicroNano3DPrinter
{
    partial class FormLayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbMode = new Sunny.UI.UIGroupBox();
            this.ibGridMode = new Sunny.UI.UIImageButton();
            this.ibZigzagMode = new Sunny.UI.UIImageButton();
            this.ibSnailMode = new Sunny.UI.UIImageButton();
            this.gbPrintZero = new Sunny.UI.UIGroupBox();
            this.lblZ2 = new Sunny.UI.UILabel();
            this.lblY2 = new Sunny.UI.UILabel();
            this.txtZ = new Sunny.UI.UITextBox();
            this.lblZ1 = new Sunny.UI.UILabel();
            this.txtY = new Sunny.UI.UITextBox();
            this.lblY1 = new Sunny.UI.UILabel();
            this.lblX2 = new Sunny.UI.UILabel();
            this.txtX = new Sunny.UI.UITextBox();
            this.lblX1 = new Sunny.UI.UILabel();
            this.gbParameter = new Sunny.UI.UIGroupBox();
            this.lblDensity1 = new Sunny.UI.UILabel();
            this.lblWidth1 = new Sunny.UI.UILabel();
            this.lblDensity2 = new Sunny.UI.UILabel();
            this.lblWidth2 = new Sunny.UI.UILabel();
            this.txtDensity = new Sunny.UI.UITextBox();
            this.txtWidth = new Sunny.UI.UITextBox();
            this.lblLength1 = new Sunny.UI.UILabel();
            this.lblLength2 = new Sunny.UI.UILabel();
            this.txtLength = new Sunny.UI.UITextBox();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnConfirm = new Sunny.UI.UISymbolButton();
            this.uiStyleManager = new Sunny.UI.UIStyleManager(this.components);
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.txtSolidfyTime = new Sunny.UI.UITextBox();
            this.gbMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ibGridMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibZigzagMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibSnailMode)).BeginInit();
            this.gbPrintZero.SuspendLayout();
            this.gbParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMode
            // 
            this.gbMode.Controls.Add(this.ibGridMode);
            this.gbMode.Controls.Add(this.ibZigzagMode);
            this.gbMode.Controls.Add(this.ibSnailMode);
            this.gbMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbMode.Location = new System.Drawing.Point(25, 65);
            this.gbMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbMode.MinimumSize = new System.Drawing.Size(1, 1);
            this.gbMode.Name = "gbMode";
            this.gbMode.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.gbMode.Size = new System.Drawing.Size(750, 200);
            this.gbMode.TabIndex = 0;
            this.gbMode.Text = "打印方式";
            this.gbMode.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ibGridMode
            // 
            this.ibGridMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibGridMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibGridMode.Image = global::MicroNano3DPrinter.Properties.Resources.grid_shaped;
            this.ibGridMode.Location = new System.Drawing.Point(401, 35);
            this.ibGridMode.Name = "ibGridMode";
            this.ibGridMode.Size = new System.Drawing.Size(150, 150);
            this.ibGridMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ibGridMode.TabIndex = 3;
            this.ibGridMode.TabStop = false;
            this.ibGridMode.Text = null;
            this.ibGridMode.Click += new System.EventHandler(this.ibGridMode_Click);
            // 
            // ibZigzagMode
            // 
            this.ibZigzagMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibZigzagMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibZigzagMode.Image = global::MicroNano3DPrinter.Properties.Resources.looped;
            this.ibZigzagMode.Location = new System.Drawing.Point(209, 35);
            this.ibZigzagMode.Name = "ibZigzagMode";
            this.ibZigzagMode.Size = new System.Drawing.Size(150, 150);
            this.ibZigzagMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ibZigzagMode.TabIndex = 2;
            this.ibZigzagMode.TabStop = false;
            this.ibZigzagMode.Text = null;
            this.ibZigzagMode.Click += new System.EventHandler(this.ibZigzagMode_Click);
            // 
            // ibSnailMode
            // 
            this.ibSnailMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibSnailMode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ibSnailMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ibSnailMode.Image = global::MicroNano3DPrinter.Properties.Resources.s_shaped;
            this.ibSnailMode.Location = new System.Drawing.Point(17, 35);
            this.ibSnailMode.Name = "ibSnailMode";
            this.ibSnailMode.Selected = true;
            this.ibSnailMode.Size = new System.Drawing.Size(150, 150);
            this.ibSnailMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ibSnailMode.TabIndex = 1;
            this.ibSnailMode.TabStop = false;
            this.ibSnailMode.Text = null;
            this.ibSnailMode.Click += new System.EventHandler(this.ibSnailMode_Click);
            // 
            // gbPrintZero
            // 
            this.gbPrintZero.Controls.Add(this.lblZ2);
            this.gbPrintZero.Controls.Add(this.lblY2);
            this.gbPrintZero.Controls.Add(this.txtZ);
            this.gbPrintZero.Controls.Add(this.lblZ1);
            this.gbPrintZero.Controls.Add(this.txtY);
            this.gbPrintZero.Controls.Add(this.lblY1);
            this.gbPrintZero.Controls.Add(this.lblX2);
            this.gbPrintZero.Controls.Add(this.txtX);
            this.gbPrintZero.Controls.Add(this.lblX1);
            this.gbPrintZero.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbPrintZero.Location = new System.Drawing.Point(42, 288);
            this.gbPrintZero.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbPrintZero.MinimumSize = new System.Drawing.Size(1, 1);
            this.gbPrintZero.Name = "gbPrintZero";
            this.gbPrintZero.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.gbPrintZero.Size = new System.Drawing.Size(320, 199);
            this.gbPrintZero.TabIndex = 1;
            this.gbPrintZero.Text = "打印顶点";
            this.gbPrintZero.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZ2
            // 
            this.lblZ2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblZ2.Location = new System.Drawing.Point(246, 112);
            this.lblZ2.Name = "lblZ2";
            this.lblZ2.Size = new System.Drawing.Size(50, 30);
            this.lblZ2.TabIndex = 5;
            this.lblZ2.Text = "mm";
            this.lblZ2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblY2
            // 
            this.lblY2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblY2.Location = new System.Drawing.Point(246, 72);
            this.lblY2.Name = "lblY2";
            this.lblY2.Size = new System.Drawing.Size(50, 30);
            this.lblY2.TabIndex = 5;
            this.lblY2.Text = "mm";
            this.lblY2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtZ
            // 
            this.txtZ.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtZ.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtZ.Location = new System.Drawing.Point(84, 112);
            this.txtZ.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtZ.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtZ.Name = "txtZ";
            this.txtZ.ShowText = false;
            this.txtZ.Size = new System.Drawing.Size(150, 30);
            this.txtZ.TabIndex = 4;
            this.txtZ.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblZ1
            // 
            this.lblZ1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblZ1.Location = new System.Drawing.Point(27, 112);
            this.lblZ1.Name = "lblZ1";
            this.lblZ1.Size = new System.Drawing.Size(50, 30);
            this.lblZ1.TabIndex = 3;
            this.lblZ1.Text = "Z:";
            this.lblZ1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtY
            // 
            this.txtY.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtY.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtY.Location = new System.Drawing.Point(84, 72);
            this.txtY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtY.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtY.Name = "txtY";
            this.txtY.ShowText = false;
            this.txtY.Size = new System.Drawing.Size(150, 30);
            this.txtY.TabIndex = 4;
            this.txtY.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblY1
            // 
            this.lblY1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblY1.Location = new System.Drawing.Point(27, 72);
            this.lblY1.Name = "lblY1";
            this.lblY1.Size = new System.Drawing.Size(50, 30);
            this.lblY1.TabIndex = 3;
            this.lblY1.Text = "Y:";
            this.lblY1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblX2
            // 
            this.lblX2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblX2.Location = new System.Drawing.Point(246, 32);
            this.lblX2.Name = "lblX2";
            this.lblX2.Size = new System.Drawing.Size(50, 30);
            this.lblX2.TabIndex = 2;
            this.lblX2.Text = "mm";
            this.lblX2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtX
            // 
            this.txtX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtX.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtX.Location = new System.Drawing.Point(84, 32);
            this.txtX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtX.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtX.Name = "txtX";
            this.txtX.ShowText = false;
            this.txtX.Size = new System.Drawing.Size(150, 30);
            this.txtX.TabIndex = 1;
            this.txtX.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblX1
            // 
            this.lblX1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblX1.Location = new System.Drawing.Point(27, 32);
            this.lblX1.Name = "lblX1";
            this.lblX1.Size = new System.Drawing.Size(50, 30);
            this.lblX1.TabIndex = 0;
            this.lblX1.Text = "X:";
            this.lblX1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbParameter
            // 
            this.gbParameter.Controls.Add(this.uiLabel1);
            this.gbParameter.Controls.Add(this.uiLabel2);
            this.gbParameter.Controls.Add(this.txtSolidfyTime);
            this.gbParameter.Controls.Add(this.lblDensity1);
            this.gbParameter.Controls.Add(this.lblWidth1);
            this.gbParameter.Controls.Add(this.lblDensity2);
            this.gbParameter.Controls.Add(this.lblWidth2);
            this.gbParameter.Controls.Add(this.txtDensity);
            this.gbParameter.Controls.Add(this.txtWidth);
            this.gbParameter.Controls.Add(this.lblLength1);
            this.gbParameter.Controls.Add(this.lblLength2);
            this.gbParameter.Controls.Add(this.txtLength);
            this.gbParameter.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbParameter.Location = new System.Drawing.Point(426, 288);
            this.gbParameter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbParameter.MinimumSize = new System.Drawing.Size(1, 1);
            this.gbParameter.Name = "gbParameter";
            this.gbParameter.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.gbParameter.Size = new System.Drawing.Size(318, 199);
            this.gbParameter.TabIndex = 2;
            this.gbParameter.Text = "打印参数";
            this.gbParameter.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDensity1
            // 
            this.lblDensity1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDensity1.Location = new System.Drawing.Point(15, 112);
            this.lblDensity1.Name = "lblDensity1";
            this.lblDensity1.Size = new System.Drawing.Size(83, 30);
            this.lblDensity1.TabIndex = 12;
            this.lblDensity1.Text = "间隔:";
            this.lblDensity1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWidth1
            // 
            this.lblWidth1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWidth1.Location = new System.Drawing.Point(15, 32);
            this.lblWidth1.Name = "lblWidth1";
            this.lblWidth1.Size = new System.Drawing.Size(82, 30);
            this.lblWidth1.TabIndex = 12;
            this.lblWidth1.Text = "宽度:";
            this.lblWidth1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDensity2
            // 
            this.lblDensity2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDensity2.Location = new System.Drawing.Point(262, 112);
            this.lblDensity2.Name = "lblDensity2";
            this.lblDensity2.Size = new System.Drawing.Size(50, 30);
            this.lblDensity2.TabIndex = 11;
            this.lblDensity2.Text = "mm";
            this.lblDensity2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWidth2
            // 
            this.lblWidth2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWidth2.Location = new System.Drawing.Point(262, 32);
            this.lblWidth2.Name = "lblWidth2";
            this.lblWidth2.Size = new System.Drawing.Size(50, 30);
            this.lblWidth2.TabIndex = 11;
            this.lblWidth2.Text = "mm";
            this.lblWidth2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDensity
            // 
            this.txtDensity.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDensity.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDensity.Location = new System.Drawing.Point(105, 112);
            this.txtDensity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDensity.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtDensity.Name = "txtDensity";
            this.txtDensity.ShowText = false;
            this.txtDensity.Size = new System.Drawing.Size(150, 30);
            this.txtDensity.TabIndex = 10;
            this.txtDensity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWidth
            // 
            this.txtWidth.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtWidth.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWidth.Location = new System.Drawing.Point(105, 32);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtWidth.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.ShowText = false;
            this.txtWidth.Size = new System.Drawing.Size(150, 30);
            this.txtWidth.TabIndex = 10;
            this.txtWidth.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLength1
            // 
            this.lblLength1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLength1.Location = new System.Drawing.Point(15, 72);
            this.lblLength1.Name = "lblLength1";
            this.lblLength1.Size = new System.Drawing.Size(83, 30);
            this.lblLength1.TabIndex = 9;
            this.lblLength1.Text = "长度:";
            this.lblLength1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLength2
            // 
            this.lblLength2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLength2.Location = new System.Drawing.Point(262, 72);
            this.lblLength2.Name = "lblLength2";
            this.lblLength2.Size = new System.Drawing.Size(50, 30);
            this.lblLength2.TabIndex = 8;
            this.lblLength2.Text = "mm";
            this.lblLength2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLength
            // 
            this.txtLength.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLength.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLength.Location = new System.Drawing.Point(105, 72);
            this.txtLength.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLength.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtLength.Name = "txtLength";
            this.txtLength.ShowText = false;
            this.txtLength.Size = new System.Drawing.Size(150, 30);
            this.txtLength.TabIndex = 7;
            this.txtLength.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(454, 514);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(234, 514);
            this.btnConfirm.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 35);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(15, 152);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(82, 30);
            this.uiLabel1.TabIndex = 15;
            this.uiLabel1.Text = "固化时间:";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(262, 152);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(50, 30);
            this.uiLabel2.TabIndex = 14;
            this.uiLabel2.Text = "s";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSolidfyTime
            // 
            this.txtSolidfyTime.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSolidfyTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSolidfyTime.Location = new System.Drawing.Point(105, 152);
            this.txtSolidfyTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSolidfyTime.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtSolidfyTime.Name = "txtSolidfyTime";
            this.txtSolidfyTime.ShowText = false;
            this.txtSolidfyTime.Size = new System.Drawing.Size(150, 30);
            this.txtSolidfyTime.TabIndex = 13;
            this.txtSolidfyTime.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormLayer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbParameter);
            this.Controls.Add(this.gbPrintZero);
            this.Controls.Add(this.gbMode);
            this.Name = "FormLayer";
            this.Text = "层信息";
            this.gbMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ibGridMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibZigzagMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibSnailMode)).EndInit();
            this.gbPrintZero.ResumeLayout(false);
            this.gbParameter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox gbMode;
        private Sunny.UI.UIImageButton ibGridMode;
        private Sunny.UI.UIImageButton ibZigzagMode;
        private Sunny.UI.UIImageButton ibSnailMode;
        private Sunny.UI.UIGroupBox gbPrintZero;
        private Sunny.UI.UILabel lblZ2;
        private Sunny.UI.UILabel lblY2;
        private Sunny.UI.UITextBox txtZ;
        private Sunny.UI.UILabel lblZ1;
        private Sunny.UI.UITextBox txtY;
        private Sunny.UI.UILabel lblY1;
        private Sunny.UI.UILabel lblX2;
        private Sunny.UI.UITextBox txtX;
        private Sunny.UI.UILabel lblX1;
        private Sunny.UI.UIGroupBox gbParameter;
        private Sunny.UI.UILabel lblDensity1;
        private Sunny.UI.UILabel lblWidth1;
        private Sunny.UI.UILabel lblDensity2;
        private Sunny.UI.UILabel lblWidth2;
        private Sunny.UI.UITextBox txtDensity;
        private Sunny.UI.UITextBox txtWidth;
        private Sunny.UI.UILabel lblLength1;
        private Sunny.UI.UILabel lblLength2;
        private Sunny.UI.UITextBox txtLength;
        private Sunny.UI.UISymbolButton btnCancel;
        private Sunny.UI.UISymbolButton btnConfirm;
        private Sunny.UI.UIStyleManager uiStyleManager;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox txtSolidfyTime;
    }
}