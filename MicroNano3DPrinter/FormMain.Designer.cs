
namespace MicroNano3DPrinter
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTips = new Sunny.UI.UIPanel();
            this.pnlNowTime = new Sunny.UI.UIPanel();
            this.pnlHeader = new Sunny.UI.UIPanel();
            this.Footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Footer
            // 
            this.Footer.Controls.Add(this.pnlNowTime);
            this.Footer.Controls.Add(this.pnlTips);
            this.Footer.Location = new System.Drawing.Point(0, 915);
            this.Footer.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Top;
            this.Footer.Size = new System.Drawing.Size(1400, 45);
            this.Footer.Style = Sunny.UI.UIStyle.Custom;
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Header.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Header.Size = new System.Drawing.Size(1400, 80);
            this.Header.Style = Sunny.UI.UIStyle.Custom;
            // 
            // pnlTips
            // 
            this.pnlTips.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlTips.Location = new System.Drawing.Point(4, 5);
            this.pnlTips.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTips.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlTips.Name = "pnlTips";
            this.pnlTips.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.pnlTips.Size = new System.Drawing.Size(1200, 40);
            this.pnlTips.Style = Sunny.UI.UIStyle.Custom;
            this.pnlTips.TabIndex = 70;
            this.pnlTips.Text = "    提示：";
            this.pnlTips.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlNowTime
            // 
            this.pnlNowTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlNowTime.Location = new System.Drawing.Point(1212, 5);
            this.pnlNowTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlNowTime.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlNowTime.Name = "pnlNowTime";
            this.pnlNowTime.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.pnlNowTime.Size = new System.Drawing.Size(180, 40);
            this.pnlNowTime.Style = Sunny.UI.UIStyle.Custom;
            this.pnlNowTime.TabIndex = 39;
            this.pnlNowTime.Text = "—— 00:00:00";
            this.pnlNowTime.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlHeader.BackgroundImage = global::MicroNano3DPrinter.Properties.Resources.header;
            this.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlHeader.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlHeader.Location = new System.Drawing.Point(0, 35);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.pnlHeader.Size = new System.Drawing.Size(1400, 80);
            this.pnlHeader.Style = Sunny.UI.UIStyle.Custom;
            this.pnlHeader.TabIndex = 3;
            this.pnlHeader.Text = null;
            this.pnlHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1400, 960);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FormMain";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "微纳3D打印控制软件  (V 2.0.0)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.Controls.SetChildIndex(this.Footer, 0);
            this.Controls.SetChildIndex(this.Header, 0);
            this.Controls.SetChildIndex(this.pnlHeader, 0);
            this.Footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel pnlTips;
        private Sunny.UI.UIPanel pnlNowTime;
        private Sunny.UI.UIPanel pnlHeader;
    }
}

