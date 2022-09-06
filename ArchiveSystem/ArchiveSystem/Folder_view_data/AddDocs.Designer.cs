namespace ArchiveSystem.EditeDocs
{
    partial class AddDocs
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.BTN_GobackToDetails = new System.Windows.Forms.Button();
            this.BTN_AddDocs = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.DGV_Folders = new System.Windows.Forms.DataGridView();
            this.panel14 = new System.Windows.Forms.Panel();
            this.TXT_addFolder = new System.Windows.Forms.TextBox();
            this.BTN_DELFolder = new System.Windows.Forms.Button();
            this.BTN_addfolder = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.Scanning_Folder = new System.Windows.Forms.Button();
            this.BTN_RefrshFolders = new System.Windows.Forms.Button();
            this.BTN_Scan = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.DGV_Files = new System.Windows.Forms.DataGridView();
            this.CHK_selectall = new System.Windows.Forms.CheckBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Folders)).BeginInit();
            this.panel14.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Files)).BeginInit();
            this.panel12.SuspendLayout();
            this.panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.BTN_GobackToDetails);
            this.panel4.Controls.Add(this.BTN_AddDocs);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(20, 443);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(921, 44);
            this.panel4.TabIndex = 3;
            // 
            // BTN_GobackToDetails
            // 
            this.BTN_GobackToDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BTN_GobackToDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Firebrick;
            this.BTN_GobackToDetails.Font = new System.Drawing.Font("Tahoma", 12F);
            this.BTN_GobackToDetails.Location = new System.Drawing.Point(702, 3);
            this.BTN_GobackToDetails.Name = "BTN_GobackToDetails";
            this.BTN_GobackToDetails.Size = new System.Drawing.Size(104, 37);
            this.BTN_GobackToDetails.TabIndex = 0;
            this.BTN_GobackToDetails.Text = "رجوع";
            this.BTN_GobackToDetails.UseVisualStyleBackColor = false;
            this.BTN_GobackToDetails.Click += new System.EventHandler(this.BTN_GobackToDetails_Click);
            // 
            // BTN_AddDocs
            // 
            this.BTN_AddDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_AddDocs.BackColor = System.Drawing.SystemColors.Highlight;
            this.BTN_AddDocs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.SeaGreen;
            this.BTN_AddDocs.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_AddDocs.ForeColor = System.Drawing.Color.White;
            this.BTN_AddDocs.Location = new System.Drawing.Point(812, 3);
            this.BTN_AddDocs.Name = "BTN_AddDocs";
            this.BTN_AddDocs.Size = new System.Drawing.Size(106, 37);
            this.BTN_AddDocs.TabIndex = 1;
            this.BTN_AddDocs.Text = "اضافة";
            this.BTN_AddDocs.UseVisualStyleBackColor = false;
            this.BTN_AddDocs.Click += new System.EventHandler(this.BTN_AddDocs_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Location = new System.Drawing.Point(392, 55);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(549, 381);
            this.panel8.TabIndex = 2;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel18);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(83, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(466, 381);
            this.panel10.TabIndex = 4;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.panel15);
            this.panel18.Controls.Add(this.panel14);
            this.panel18.Controls.Add(this.panel13);
            this.panel18.Controls.Add(this.panel16);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel18.Location = new System.Drawing.Point(244, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(222, 381);
            this.panel18.TabIndex = 3;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.DGV_Folders);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(0, 109);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(222, 272);
            this.panel15.TabIndex = 8;
            // 
            // DGV_Folders
            // 
            this.DGV_Folders.AllowUserToAddRows = false;
            this.DGV_Folders.AllowUserToDeleteRows = false;
            this.DGV_Folders.AllowUserToResizeColumns = false;
            this.DGV_Folders.AllowUserToResizeRows = false;
            this.DGV_Folders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_Folders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGV_Folders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Folders.Location = new System.Drawing.Point(0, 0);
            this.DGV_Folders.Name = "DGV_Folders";
            this.DGV_Folders.ReadOnly = true;
            this.DGV_Folders.RowHeadersVisible = false;
            this.DGV_Folders.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.DGV_Folders.Size = new System.Drawing.Size(222, 272);
            this.DGV_Folders.TabIndex = 5;
            this.DGV_Folders.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_Folders_CellMouseClick);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.TXT_addFolder);
            this.panel14.Controls.Add(this.BTN_DELFolder);
            this.panel14.Controls.Add(this.BTN_addfolder);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(0, 72);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(222, 37);
            this.panel14.TabIndex = 7;
            // 
            // TXT_addFolder
            // 
            this.TXT_addFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TXT_addFolder.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_addFolder.Location = new System.Drawing.Point(43, 0);
            this.TXT_addFolder.Multiline = true;
            this.TXT_addFolder.Name = "TXT_addFolder";
            this.TXT_addFolder.Size = new System.Drawing.Size(131, 37);
            this.TXT_addFolder.TabIndex = 172;
            this.TXT_addFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BTN_DELFolder
            // 
            this.BTN_DELFolder.BackColor = System.Drawing.Color.Red;
            this.BTN_DELFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.BTN_DELFolder.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_DELFolder.Location = new System.Drawing.Point(0, 0);
            this.BTN_DELFolder.Name = "BTN_DELFolder";
            this.BTN_DELFolder.Size = new System.Drawing.Size(43, 37);
            this.BTN_DELFolder.TabIndex = 171;
            this.BTN_DELFolder.Text = "-";
            this.BTN_DELFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BTN_DELFolder.UseVisualStyleBackColor = false;
            this.BTN_DELFolder.Click += new System.EventHandler(this.BTN_DELFolder_Click);
            // 
            // BTN_addfolder
            // 
            this.BTN_addfolder.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.BTN_addfolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.BTN_addfolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_addfolder.Location = new System.Drawing.Point(174, 0);
            this.BTN_addfolder.Name = "BTN_addfolder";
            this.BTN_addfolder.Size = new System.Drawing.Size(48, 37);
            this.BTN_addfolder.TabIndex = 169;
            this.BTN_addfolder.Text = "+";
            this.BTN_addfolder.UseVisualStyleBackColor = false;
            this.BTN_addfolder.Click += new System.EventHandler(this.BTN_addfolder_Click);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.Scanning_Folder);
            this.panel13.Controls.Add(this.BTN_RefrshFolders);
            this.panel13.Controls.Add(this.BTN_Scan);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(0, 36);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(222, 36);
            this.panel13.TabIndex = 6;
            // 
            // Scanning_Folder
            // 
            this.Scanning_Folder.BackColor = System.Drawing.Color.LightGray;
            this.Scanning_Folder.Dock = System.Windows.Forms.DockStyle.Left;
            this.Scanning_Folder.Font = new System.Drawing.Font("Nirmala UI Semilight", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Scanning_Folder.Location = new System.Drawing.Point(0, 0);
            this.Scanning_Folder.Name = "Scanning_Folder";
            this.Scanning_Folder.Size = new System.Drawing.Size(56, 36);
            this.Scanning_Folder.TabIndex = 167;
            this.Scanning_Folder.Text = "تحديث المصدر";
            this.Scanning_Folder.UseVisualStyleBackColor = false;
            this.Scanning_Folder.Click += new System.EventHandler(this.Scanning_Folder_Click);
            // 
            // BTN_RefrshFolders
            // 
            this.BTN_RefrshFolders.Dock = System.Windows.Forms.DockStyle.Right;
            this.BTN_RefrshFolders.Image = global::ArchiveSystem.Properties.Resources.icons8_refresh_16;
            this.BTN_RefrshFolders.Location = new System.Drawing.Point(94, 0);
            this.BTN_RefrshFolders.Name = "BTN_RefrshFolders";
            this.BTN_RefrshFolders.Size = new System.Drawing.Size(33, 36);
            this.BTN_RefrshFolders.TabIndex = 170;
            this.BTN_RefrshFolders.UseVisualStyleBackColor = true;
            // 
            // BTN_Scan
            // 
            this.BTN_Scan.Dock = System.Windows.Forms.DockStyle.Right;
            this.BTN_Scan.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Scan.Location = new System.Drawing.Point(127, 0);
            this.BTN_Scan.Name = "BTN_Scan";
            this.BTN_Scan.Size = new System.Drawing.Size(95, 36);
            this.BTN_Scan.TabIndex = 1;
            this.BTN_Scan.Text = "مسح ضؤي";
            this.BTN_Scan.UseVisualStyleBackColor = true;
            this.BTN_Scan.Click += new System.EventHandler(this.BTN_Scan_Click);
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel16.Controls.Add(this.label2);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(222, 36);
            this.panel16.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "ملفات النظام";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.DGV_Files);
            this.panel11.Controls.Add(this.CHK_selectall);
            this.panel11.Controls.Add(this.panel12);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(238, 381);
            this.panel11.TabIndex = 2;
            // 
            // DGV_Files
            // 
            this.DGV_Files.AllowUserToAddRows = false;
            this.DGV_Files.AllowUserToDeleteRows = false;
            this.DGV_Files.AllowUserToResizeColumns = false;
            this.DGV_Files.AllowUserToResizeRows = false;
            this.DGV_Files.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_Files.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV_Files.Location = new System.Drawing.Point(0, 59);
            this.DGV_Files.Name = "DGV_Files";
            this.DGV_Files.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DGV_Files.RowHeadersVisible = false;
            this.DGV_Files.Size = new System.Drawing.Size(238, 322);
            this.DGV_Files.TabIndex = 3;
            this.DGV_Files.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_Files_CellClick);
            // 
            // CHK_selectall
            // 
            this.CHK_selectall.AutoSize = true;
            this.CHK_selectall.BackColor = System.Drawing.Color.DarkGray;
            this.CHK_selectall.Dock = System.Windows.Forms.DockStyle.Top;
            this.CHK_selectall.Font = new System.Drawing.Font("Tahoma", 12F);
            this.CHK_selectall.Location = new System.Drawing.Point(0, 36);
            this.CHK_selectall.Name = "CHK_selectall";
            this.CHK_selectall.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CHK_selectall.Size = new System.Drawing.Size(238, 23);
            this.CHK_selectall.TabIndex = 2;
            this.CHK_selectall.Text = "اختيار الكل";
            this.CHK_selectall.UseVisualStyleBackColor = false;
            this.CHK_selectall.Visible = false;
            this.CHK_selectall.CheckedChanged += new System.EventHandler(this.CHK_selectall_CheckedChanged);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.panel17);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(238, 36);
            this.panel12.TabIndex = 1;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel17.Controls.Add(this.label1);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel17.Location = new System.Drawing.Point(0, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(238, 34);
            this.panel17.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "الفايلات";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(9, 55);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(377, 381);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btn_close
            // 
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LimeGreen;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.Location = new System.Drawing.Point(921, 6);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(33, 26);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "x";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // AddDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 507);
            this.ControlBox = false;
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel4);
            this.Name = "AddDocs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "ارفاق مستندات";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.AddDocs_Load);
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Folders)).EndInit();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Files)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button BTN_AddDocs;
        private System.Windows.Forms.Button BTN_GobackToDetails;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.DataGridView DGV_Folders;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.TextBox TXT_addFolder;
        private System.Windows.Forms.Button BTN_DELFolder;
        private System.Windows.Forms.Button BTN_addfolder;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Button Scanning_Folder;
        private System.Windows.Forms.Button BTN_RefrshFolders;
        private System.Windows.Forms.Button BTN_Scan;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.DataGridView DGV_Files;
        private System.Windows.Forms.CheckBox CHK_selectall;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_close;
    }
}