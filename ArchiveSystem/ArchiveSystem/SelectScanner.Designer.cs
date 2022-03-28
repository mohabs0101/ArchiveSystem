namespace ArchiveSystem
{
    partial class SelectScanner
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
            this.bt_cancell = new System.Windows.Forms.Button();
            this.bt_ok = new System.Windows.Forms.Button();
            this.listScan = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // bt_cancell
            // 
            this.bt_cancell.Location = new System.Drawing.Point(455, 323);
            this.bt_cancell.Name = "bt_cancell";
            this.bt_cancell.Size = new System.Drawing.Size(75, 23);
            this.bt_cancell.TabIndex = 8;
            this.bt_cancell.Text = "Cancel";
            this.bt_cancell.UseVisualStyleBackColor = true;
            this.bt_cancell.Click += new System.EventHandler(this.bt_cancell_Click);
            // 
            // bt_ok
            // 
            this.bt_ok.Location = new System.Drawing.Point(374, 323);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(75, 23);
            this.bt_ok.TabIndex = 7;
            this.bt_ok.Text = "OK";
            this.bt_ok.UseVisualStyleBackColor = true;
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // listScan
            // 
            this.listScan.FormattingEnabled = true;
            this.listScan.Location = new System.Drawing.Point(270, 105);
            this.listScan.Name = "listScan";
            this.listScan.Size = new System.Drawing.Size(260, 212);
            this.listScan.TabIndex = 6;
            this.listScan.SelectedIndexChanged += new System.EventHandler(this.listScan_SelectedIndexChanged);
            // 
            // SelectScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bt_cancell);
            this.Controls.Add(this.bt_ok);
            this.Controls.Add(this.listScan);
            this.Name = "SelectScanner";
            this.Text = "SelectScanner";
            this.Load += new System.EventHandler(this.SelectScanner_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_cancell;
        private System.Windows.Forms.Button bt_ok;
        private System.Windows.Forms.ListBox listScan;
    }
}