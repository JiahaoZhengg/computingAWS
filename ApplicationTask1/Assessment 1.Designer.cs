
namespace ApplicationTask1
{
    partial class PrismApiTest
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        
        /// <param name="disposing"></param>
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
            this.AllocationsMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rTxtResult = new System.Windows.Forms.RichTextBox();
            this.openTaffFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.AllocationsMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AllocationsMenuStrip
            // 
            this.AllocationsMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.AllocationsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.validateToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.AllocationsMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.AllocationsMenuStrip.Name = "AllocationsMenuStrip";
            this.AllocationsMenuStrip.Size = new System.Drawing.Size(800, 28);
            this.AllocationsMenuStrip.TabIndex = 0;
            this.AllocationsMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(132, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allocationsToolStripMenuItem});
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.validateToolStripMenuItem.Text = "Validate";
            // 
            // allocationsToolStripMenuItem
            // 
            this.allocationsToolStripMenuItem.Enabled = false;
            this.allocationsToolStripMenuItem.Name = "allocationsToolStripMenuItem";
            this.allocationsToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.allocationsToolStripMenuItem.Text = "Allocations";
            this.allocationsToolStripMenuItem.Click += new System.EventHandler(this.allocationsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.errorsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // errorsToolStripMenuItem
            // 
            this.errorsToolStripMenuItem.Enabled = false;
            this.errorsToolStripMenuItem.Name = "errorsToolStripMenuItem";
            this.errorsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.errorsToolStripMenuItem.Text = "Errors";
            this.errorsToolStripMenuItem.Click += new System.EventHandler(this.errorsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // rTxtResult
            // 
            this.rTxtResult.Location = new System.Drawing.Point(0, 74);
            this.rTxtResult.Name = "rTxtResult";
            this.rTxtResult.Size = new System.Drawing.Size(800, 376);
            this.rTxtResult.TabIndex = 1;
            this.rTxtResult.Text = "";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "https://sit323t122.blob.core.windows.net/at2-blob/TestSmall.cff",
            "https://sit323t122.blob.core.windows.net/at2-blob/TestLarge.cff",
            "https://sit323t122.blob.core.windows.net/at2-blob/TestExtraLarge.cff"});
            this.comboBox1.Location = new System.Drawing.Point(12, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(590, 23);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(608, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 32);
            this.button1.TabIndex = 3;
            this.button1.Text = "Generate allocation ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PrismApiTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.rTxtResult);
            this.Controls.Add(this.AllocationsMenuStrip);
            this.MainMenuStrip = this.AllocationsMenuStrip;
            this.Name = "Assessment 1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assessment 1-Task";
            this.Load += new System.EventHandler(this.AllocationsForm_Load);
            this.AllocationsMenuStrip.ResumeLayout(false);
            this.AllocationsMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip AllocationsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rTxtResult;
        private System.Windows.Forms.OpenFileDialog openTaffFileDialog;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
    }
}

