namespace ArchiveSystem.Folder_view_data
{
    partial class Form_show_doc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_show_doc));
            this.ListView_show_doc = new System.Windows.Forms.ListView();
            this.ContextMenuStrip_right_click = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSM_open_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ImageList_add_viwe = new System.Windows.Forms.ImageList(this.components);
            this.ImageList_Extension = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TXT_notes = new System.Windows.Forms.TextBox();
            this.TXT_To = new System.Windows.Forms.TextBox();
            this.TXT_From = new System.Windows.Forms.TextBox();
            this.TXT_Subject = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.COM_bookStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.COM_priority = new System.Windows.Forms.ComboBox();
            this.COM_privicy = new System.Windows.Forms.ComboBox();
            this.COM_PaperType = new System.Windows.Forms.ComboBox();
            this.COM_bookType = new System.Windows.Forms.ComboBox();
            this.DT_bookRecive_date = new System.Windows.Forms.DateTimePicker();
            this.DT_bookDate = new System.Windows.Forms.DateTimePicker();
            this.TXT_SearchKEys = new System.Windows.Forms.TextBox();
            this.TXT_bookNumber = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.TXT_Book_recive_number = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cm_type_show = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_zoom_out = new System.Windows.Forms.Button();
            this.btn_zoom_in = new System.Windows.Forms.Button();
            this.btn_Rotate_180 = new System.Windows.Forms.Button();
            this.btn_Rotate_90 = new System.Windows.Forms.Button();
            this.pictureBox_show_doc = new System.Windows.Forms.PictureBox();
            this.TSM_open_all_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStrip_right_click.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_show_doc)).BeginInit();
            this.SuspendLayout();
            // 
            // ListView_show_doc
            // 
            this.ListView_show_doc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_show_doc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ListView_show_doc.ContextMenuStrip = this.ContextMenuStrip_right_click;
            this.ListView_show_doc.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListView_show_doc.LargeImageList = this.ImageList_add_viwe;
            this.ListView_show_doc.Location = new System.Drawing.Point(11, 54);
            this.ListView_show_doc.Name = "ListView_show_doc";
            this.ListView_show_doc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ListView_show_doc.Size = new System.Drawing.Size(160, 632);
            this.ListView_show_doc.SmallImageList = this.ImageList_add_viwe;
            this.ListView_show_doc.TabIndex = 277;
            this.ListView_show_doc.UseCompatibleStateImageBehavior = false;
            this.ListView_show_doc.SelectedIndexChanged += new System.EventHandler(this.ListView_show_doc_SelectedIndexChanged);
            // 
            // ContextMenuStrip_right_click
            // 
            this.ContextMenuStrip_right_click.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContextMenuStrip_right_click.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuStrip_right_click.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSM_open_file,
            this.ToolStripSeparator1,
            this.TSM_open_all_file});
            this.ContextMenuStrip_right_click.Name = "ContextMenuStrip_right_click";
            this.ContextMenuStrip_right_click.Size = new System.Drawing.Size(211, 94);
            // 
            // TSM_open_file
            // 
            this.TSM_open_file.Name = "TSM_open_file";
            this.TSM_open_file.Size = new System.Drawing.Size(210, 28);
            this.TSM_open_file.Text = "فتح";
            this.TSM_open_file.Click += new System.EventHandler(this.TSM_open_file_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // ImageList_add_viwe
            // 
            this.ImageList_add_viwe.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList_add_viwe.ImageSize = new System.Drawing.Size(100, 100);
            this.ImageList_add_viwe.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ImageList_Extension
            // 
            this.ImageList_Extension.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_Extension.ImageStream")));
            this.ImageList_Extension.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_Extension.Images.SetKeyName(0, "word.png");
            this.ImageList_Extension.Images.SetKeyName(1, "power point.png");
            this.ImageList_Extension.Images.SetKeyName(2, "Untitled.png");
            this.ImageList_Extension.Images.SetKeyName(3, "access.png");
            this.ImageList_Extension.Images.SetKeyName(4, "txt.png");
            this.ImageList_Extension.Images.SetKeyName(5, "pdf.png");
            this.ImageList_Extension.Images.SetKeyName(6, "vid.png");
            this.ImageList_Extension.Images.SetKeyName(7, "other.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(10, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.TXT_notes);
            this.splitContainer1.Panel1.Controls.Add(this.TXT_To);
            this.splitContainer1.Panel1.Controls.Add(this.TXT_From);
            this.splitContainer1.Panel1.Controls.Add(this.TXT_Subject);
            this.splitContainer1.Panel1.Controls.Add(this.label18);
            this.splitContainer1.Panel1.Controls.Add(this.COM_bookStatus);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.COM_priority);
            this.splitContainer1.Panel1.Controls.Add(this.COM_privicy);
            this.splitContainer1.Panel1.Controls.Add(this.COM_PaperType);
            this.splitContainer1.Panel1.Controls.Add(this.COM_bookType);
            this.splitContainer1.Panel1.Controls.Add(this.DT_bookRecive_date);
            this.splitContainer1.Panel1.Controls.Add(this.DT_bookDate);
            this.splitContainer1.Panel1.Controls.Add(this.TXT_SearchKEys);
            this.splitContainer1.Panel1.Controls.Add(this.TXT_bookNumber);
            this.splitContainer1.Panel1.Controls.Add(this.label16);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.label21);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.label14);
            this.splitContainer1.Panel1.Controls.Add(this.label15);
            this.splitContainer1.Panel1.Controls.Add(this.label17);
            this.splitContainer1.Panel1.Controls.Add(this.TXT_Book_recive_number);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer1.Size = new System.Drawing.Size(1482, 735);
            this.splitContainer1.SplitterDistance = 550;
            this.splitContainer1.TabIndex = 278;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(430, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 27);
            this.label7.TabIndex = 192;
            this.label7.Text = "رقم الكتاب";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(417, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 27);
            this.label8.TabIndex = 193;
            this.label8.Text = "تاريخ الكتاب";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(440, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 27);
            this.label9.TabIndex = 194;
            this.label9.Text = "رقم واردنا";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(428, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 27);
            this.label10.TabIndex = 195;
            this.label10.Text = "تاريخ واردنا";
            // 
            // TXT_notes
            // 
            this.TXT_notes.Location = new System.Drawing.Point(17, 261);
            this.TXT_notes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_notes.Multiline = true;
            this.TXT_notes.Name = "TXT_notes";
            this.TXT_notes.Size = new System.Drawing.Size(220, 82);
            this.TXT_notes.TabIndex = 190;
            // 
            // TXT_To
            // 
            this.TXT_To.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_To.Location = new System.Drawing.Point(17, 192);
            this.TXT_To.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_To.Multiline = true;
            this.TXT_To.Name = "TXT_To";
            this.TXT_To.Size = new System.Drawing.Size(220, 31);
            this.TXT_To.TabIndex = 189;
            // 
            // TXT_From
            // 
            this.TXT_From.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_From.Location = new System.Drawing.Point(17, 121);
            this.TXT_From.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_From.Multiline = true;
            this.TXT_From.Name = "TXT_From";
            this.TXT_From.Size = new System.Drawing.Size(220, 31);
            this.TXT_From.TabIndex = 188;
            // 
            // TXT_Subject
            // 
            this.TXT_Subject.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_Subject.Location = new System.Drawing.Point(17, 53);
            this.TXT_Subject.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_Subject.Multiline = true;
            this.TXT_Subject.Name = "TXT_Subject";
            this.TXT_Subject.Size = new System.Drawing.Size(220, 31);
            this.TXT_Subject.TabIndex = 187;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(164, 232);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 27);
            this.label18.TabIndex = 191;
            this.label18.Text = "ملاحضات";
            // 
            // COM_bookStatus
            // 
            this.COM_bookStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COM_bookStatus.FormattingEnabled = true;
            this.COM_bookStatus.Items.AddRange(new object[] {
            "منتهي",
            "قيد المتابعة"});
            this.COM_bookStatus.Location = new System.Drawing.Point(256, 332);
            this.COM_bookStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_bookStatus.Name = "COM_bookStatus";
            this.COM_bookStatus.Size = new System.Drawing.Size(130, 24);
            this.COM_bookStatus.TabIndex = 176;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(277, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 27);
            this.label2.TabIndex = 186;
            this.label2.Text = "حالة الكتاب";
            // 
            // COM_priority
            // 
            this.COM_priority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COM_priority.FormattingEnabled = true;
            this.COM_priority.Items.AddRange(new object[] {
            "عاجل",
            "فوري"});
            this.COM_priority.Location = new System.Drawing.Point(257, 189);
            this.COM_priority.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_priority.Name = "COM_priority";
            this.COM_priority.Size = new System.Drawing.Size(130, 24);
            this.COM_priority.TabIndex = 174;
            // 
            // COM_privicy
            // 
            this.COM_privicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COM_privicy.FormattingEnabled = true;
            this.COM_privicy.Items.AddRange(new object[] {
            "عام",
            "سري"});
            this.COM_privicy.Location = new System.Drawing.Point(257, 123);
            this.COM_privicy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_privicy.Name = "COM_privicy";
            this.COM_privicy.Size = new System.Drawing.Size(130, 24);
            this.COM_privicy.TabIndex = 173;
            // 
            // COM_PaperType
            // 
            this.COM_PaperType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COM_PaperType.FormattingEnabled = true;
            this.COM_PaperType.Items.AddRange(new object[] {
            "استنساخ",
            "اصلي"});
            this.COM_PaperType.Location = new System.Drawing.Point(257, 261);
            this.COM_PaperType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PaperType.Name = "COM_PaperType";
            this.COM_PaperType.Size = new System.Drawing.Size(130, 24);
            this.COM_PaperType.TabIndex = 175;
            // 
            // COM_bookType
            // 
            this.COM_bookType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COM_bookType.FormattingEnabled = true;
            this.COM_bookType.Location = new System.Drawing.Point(257, 53);
            this.COM_bookType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_bookType.Name = "COM_bookType";
            this.COM_bookType.Size = new System.Drawing.Size(130, 24);
            this.COM_bookType.TabIndex = 172;
            // 
            // DT_bookRecive_date
            // 
            this.DT_bookRecive_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DT_bookRecive_date.Location = new System.Drawing.Point(395, 261);
            this.DT_bookRecive_date.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DT_bookRecive_date.Name = "DT_bookRecive_date";
            this.DT_bookRecive_date.Size = new System.Drawing.Size(135, 24);
            this.DT_bookRecive_date.TabIndex = 171;
            // 
            // DT_bookDate
            // 
            this.DT_bookDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DT_bookDate.Location = new System.Drawing.Point(395, 123);
            this.DT_bookDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DT_bookDate.Name = "DT_bookDate";
            this.DT_bookDate.Size = new System.Drawing.Size(135, 24);
            this.DT_bookDate.TabIndex = 169;
            this.DT_bookDate.Value = new System.DateTime(2022, 3, 23, 0, 0, 0, 0);
            // 
            // TXT_SearchKEys
            // 
            this.TXT_SearchKEys.Location = new System.Drawing.Point(153, 391);
            this.TXT_SearchKEys.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_SearchKEys.Multiline = true;
            this.TXT_SearchKEys.Name = "TXT_SearchKEys";
            this.TXT_SearchKEys.Size = new System.Drawing.Size(380, 96);
            this.TXT_SearchKEys.TabIndex = 177;
            // 
            // TXT_bookNumber
            // 
            this.TXT_bookNumber.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_bookNumber.Location = new System.Drawing.Point(395, 50);
            this.TXT_bookNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_bookNumber.Multiline = true;
            this.TXT_bookNumber.Name = "TXT_bookNumber";
            this.TXT_bookNumber.Size = new System.Drawing.Size(135, 31);
            this.TXT_bookNumber.TabIndex = 168;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(308, 159);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 27);
            this.label16.TabIndex = 183;
            this.label16.Text = "الاولوية";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(156, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 27);
            this.label11.TabIndex = 178;
            this.label11.Text = "الموضوع";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(281, 92);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 27);
            this.label21.TabIndex = 185;
            this.label21.Text = "الخصوصية";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(284, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 27);
            this.label12.TabIndex = 179;
            this.label12.Text = "نوع الكتاب";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(194, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 27);
            this.label13.TabIndex = 180;
            this.label13.Text = "من";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(198, 159);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 27);
            this.label14.TabIndex = 181;
            this.label14.Text = "الى";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(434, 362);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(112, 27);
            this.label15.TabIndex = 182;
            this.label15.Text = "مفاتيح البحث";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(280, 228);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(95, 27);
            this.label17.TabIndex = 184;
            this.label17.Text = "نوع النسخة";
            // 
            // TXT_Book_recive_number
            // 
            this.TXT_Book_recive_number.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_Book_recive_number.Location = new System.Drawing.Point(395, 189);
            this.TXT_Book_recive_number.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_Book_recive_number.Multiline = true;
            this.TXT_Book_recive_number.Name = "TXT_Book_recive_number";
            this.TXT_Book_recive_number.Size = new System.Drawing.Size(135, 31);
            this.TXT_Book_recive_number.TabIndex = 170;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 735);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(13, 22);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer2.Panel1.Controls.Add(this.cm_type_show);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            this.splitContainer2.Panel1.Controls.Add(this.ListView_show_doc);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Silver;
            this.splitContainer2.Panel2.Controls.Add(this.btn_zoom_out);
            this.splitContainer2.Panel2.Controls.Add(this.btn_zoom_in);
            this.splitContainer2.Panel2.Controls.Add(this.btn_Rotate_180);
            this.splitContainer2.Panel2.Controls.Add(this.btn_Rotate_90);
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox_show_doc);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer2.Size = new System.Drawing.Size(903, 698);
            this.splitContainer2.SplitterDistance = 174;
            this.splitContainer2.TabIndex = 0;
            // 
            // cm_type_show
            // 
            this.cm_type_show.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.cm_type_show.FormattingEnabled = true;
            this.cm_type_show.Items.AddRange(new object[] {
            "تفاصيل",
            "ايقونة كبيرة",
            "لستة",
            "ايقونة صغيرة",
            "عناوين"});
            this.cm_type_show.Location = new System.Drawing.Point(10, 9);
            this.cm_type_show.Name = "cm_type_show";
            this.cm_type_show.Size = new System.Drawing.Size(160, 29);
            this.cm_type_show.TabIndex = 369;
            this.cm_type_show.Text = "ايقونة كبيرة";
            this.cm_type_show.SelectedIndexChanged += new System.EventHandler(this.cm_type_show_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 698);
            this.panel2.TabIndex = 278;
            // 
            // btn_zoom_out
            // 
            this.btn_zoom_out.Location = new System.Drawing.Point(186, 9);
            this.btn_zoom_out.Name = "btn_zoom_out";
            this.btn_zoom_out.Size = new System.Drawing.Size(95, 39);
            this.btn_zoom_out.TabIndex = 3;
            this.btn_zoom_out.Text = "Zoom OUT";
            this.btn_zoom_out.UseVisualStyleBackColor = true;
            this.btn_zoom_out.Click += new System.EventHandler(this.btn_zoom_out_Click);
            // 
            // btn_zoom_in
            // 
            this.btn_zoom_in.Location = new System.Drawing.Point(287, 9);
            this.btn_zoom_in.Name = "btn_zoom_in";
            this.btn_zoom_in.Size = new System.Drawing.Size(95, 39);
            this.btn_zoom_in.TabIndex = 2;
            this.btn_zoom_in.Text = "Zoom IN";
            this.btn_zoom_in.UseVisualStyleBackColor = true;
            this.btn_zoom_in.Click += new System.EventHandler(this.btn_zoom_in_Click);
            // 
            // btn_Rotate_180
            // 
            this.btn_Rotate_180.Location = new System.Drawing.Point(415, 9);
            this.btn_Rotate_180.Name = "btn_Rotate_180";
            this.btn_Rotate_180.Size = new System.Drawing.Size(95, 39);
            this.btn_Rotate_180.TabIndex = 1;
            this.btn_Rotate_180.Text = "R 180";
            this.btn_Rotate_180.UseVisualStyleBackColor = true;
            this.btn_Rotate_180.Click += new System.EventHandler(this.btn_RotateL90_Click);
            // 
            // btn_Rotate_90
            // 
            this.btn_Rotate_90.Location = new System.Drawing.Point(516, 9);
            this.btn_Rotate_90.Name = "btn_Rotate_90";
            this.btn_Rotate_90.Size = new System.Drawing.Size(95, 39);
            this.btn_Rotate_90.TabIndex = 1;
            this.btn_Rotate_90.Text = "R 90";
            this.btn_Rotate_90.UseVisualStyleBackColor = true;
            this.btn_Rotate_90.Click += new System.EventHandler(this.btn_RotateR90_Click);
            // 
            // pictureBox_show_doc
            // 
            this.pictureBox_show_doc.BackColor = System.Drawing.Color.White;
            this.pictureBox_show_doc.Location = new System.Drawing.Point(18, 54);
            this.pictureBox_show_doc.Name = "pictureBox_show_doc";
            this.pictureBox_show_doc.Size = new System.Drawing.Size(614, 632);
            this.pictureBox_show_doc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_show_doc.TabIndex = 0;
            this.pictureBox_show_doc.TabStop = false;
            // 
            // TSM_open_all_file
            // 
            this.TSM_open_all_file.Name = "TSM_open_all_file";
            this.TSM_open_all_file.Size = new System.Drawing.Size(210, 28);
            this.TSM_open_all_file.Text = "فتح الكل";
            this.TSM_open_all_file.Click += new System.EventHandler(this.TSM_open_all_file_Click);
            // 
            // Form_show_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 818);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_show_doc";
            this.Text = "Form_show_doc";
            this.Load += new System.EventHandler(this.Form_show_doc_Load);
            this.ContextMenuStrip_right_click.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_show_doc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView ListView_show_doc;
        internal System.Windows.Forms.ImageList ImageList_Extension;
        internal System.Windows.Forms.ImageList ImageList_add_viwe;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox COM_bookStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox COM_priority;
        private System.Windows.Forms.ComboBox COM_privicy;
        private System.Windows.Forms.ComboBox COM_PaperType;
        private System.Windows.Forms.ComboBox COM_bookType;
        private System.Windows.Forms.DateTimePicker DT_bookRecive_date;
        private System.Windows.Forms.DateTimePicker DT_bookDate;
        private System.Windows.Forms.TextBox TXT_SearchKEys;
        private System.Windows.Forms.TextBox TXT_bookNumber;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox TXT_Book_recive_number;
        private System.Windows.Forms.TextBox TXT_notes;
        private System.Windows.Forms.TextBox TXT_To;
        private System.Windows.Forms.TextBox TXT_From;
        private System.Windows.Forms.TextBox TXT_Subject;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox_show_doc;
        private System.Windows.Forms.Button btn_Rotate_90;
        private System.Windows.Forms.Button btn_Rotate_180;
        internal System.Windows.Forms.ComboBox cm_type_show;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip_right_click;
        internal System.Windows.Forms.ToolStripMenuItem TSM_open_file;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.Button btn_zoom_in;
        private System.Windows.Forms.Button btn_zoom_out;
        private System.Windows.Forms.ToolStripMenuItem TSM_open_all_file;
    }
}