
namespace ApplicationTask1
{
    partial class ErrorsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rTxtTaffEorrorInfo = new System.Windows.Forms.RichTextBox();
            this.rTxtCffErrorInfo = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1372, 633);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rTxtTaffEorrorInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1364, 604);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "taff file eror";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rTxtCffErrorInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1072, 604);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "cff file error";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rTxtTaffEorrorInfo
            // 
            this.rTxtTaffEorrorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTxtTaffEorrorInfo.Location = new System.Drawing.Point(3, 3);
            this.rTxtTaffEorrorInfo.Name = "rTxtTaffEorrorInfo";
            this.rTxtTaffEorrorInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rTxtTaffEorrorInfo.Size = new System.Drawing.Size(1358, 598);
            this.rTxtTaffEorrorInfo.TabIndex = 0;
            this.rTxtTaffEorrorInfo.Text = "";
            // 
            // rTxtCffErrorInfo
            // 
            this.rTxtCffErrorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTxtCffErrorInfo.Location = new System.Drawing.Point(3, 3);
            this.rTxtCffErrorInfo.Name = "rTxtCffErrorInfo";
            this.rTxtCffErrorInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rTxtCffErrorInfo.Size = new System.Drawing.Size(1066, 598);
            this.rTxtCffErrorInfo.TabIndex = 0;
            this.rTxtCffErrorInfo.Text = "";
            // 
            // ErrorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 633);
            this.Controls.Add(this.tabControl1);
            this.Name = "ErrorsForm";
            this.Text = "ErrorsForm";
            this.Load += new System.EventHandler(this.ErrorsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rTxtTaffEorrorInfo;
        private System.Windows.Forms.RichTextBox rTxtCffErrorInfo;
    }
}