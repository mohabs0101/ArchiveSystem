namespace ArchiveSystem.Folder_Tracker
{
    partial class Form_Tracker_Procedure
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
            this.advanc_dgv_Assign_Comment = new Zuby.ADGV.AdvancedDataGridView();
            this.ContextMenuStrip_right_click = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.COMLIST_assination = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TXT_assignTitle = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.advanc_dgv_Assign_Comment)).BeginInit();
            this.ContextMenuStrip_right_click.SuspendLayout();
            this.SuspendLayout();
            // 
            // advanc_dgv_Assign_Comment
            // 
            this.advanc_dgv_Assign_Comment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advanc_dgv_Assign_Comment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advanc_dgv_Assign_Comment.ContextMenuStrip = this.ContextMenuStrip_right_click;
            this.advanc_dgv_Assign_Comment.FilterAndSortEnabled = true;
            this.advanc_dgv_Assign_Comment.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advanc_dgv_Assign_Comment.Location = new System.Drawing.Point(39, 438);
            this.advanc_dgv_Assign_Comment.MultiSelect = false;
            this.advanc_dgv_Assign_Comment.Name = "advanc_dgv_Assign_Comment";
            this.advanc_dgv_Assign_Comment.ReadOnly = true;
            this.advanc_dgv_Assign_Comment.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.advanc_dgv_Assign_Comment.RowTemplate.Height = 26;
            this.advanc_dgv_Assign_Comment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.advanc_dgv_Assign_Comment.Size = new System.Drawing.Size(764, 262);
            this.advanc_dgv_Assign_Comment.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advanc_dgv_Assign_Comment.TabIndex = 204;
            // 
            // ContextMenuStrip_right_click
            // 
            this.ContextMenuStrip_right_click.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContextMenuStrip_right_click.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ContextMenuStrip_right_click.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripTextBox1,
            this.ToolStripSeparator1,
            this.toolStripMenuItem2});
            this.ContextMenuStrip_right_click.Name = "ContextMenuStrip_right_click";
            this.ContextMenuStrip_right_click.Size = new System.Drawing.Size(182, 99);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "مكتمل",
            "قيد الانجاز",
            "متوقف"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 28);
            this.toolStripComboBox1.Text = "مكتمل";
            this.toolStripComboBox1.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(181, 28);
            this.toolStripMenuItem2.Text = "حفظ";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(686, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 27);
            this.label5.TabIndex = 206;
            this.label5.Text = "اشاره  الى قسم";
            // 
            // COMLIST_assination
            // 
            this.COMLIST_assination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.COMLIST_assination.BackColor = System.Drawing.Color.WhiteSmoke;
            this.COMLIST_assination.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.COMLIST_assination.ForeColor = System.Drawing.SystemColors.WindowText;
            this.COMLIST_assination.FormattingEnabled = true;
            this.COMLIST_assination.HorizontalScrollbar = true;
            this.COMLIST_assination.Location = new System.Drawing.Point(312, 211);
            this.COMLIST_assination.Margin = new System.Windows.Forms.Padding(3, 4, 7, 4);
            this.COMLIST_assination.MultiColumn = true;
            this.COMLIST_assination.Name = "COMLIST_assination";
            this.COMLIST_assination.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.COMLIST_assination.ScrollAlwaysVisible = true;
            this.COMLIST_assination.Size = new System.Drawing.Size(491, 139);
            this.COMLIST_assination.TabIndex = 205;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(676, 374);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 58);
            this.button1.TabIndex = 209;
            this.button1.Text = "اضافة مهمة جديدة";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(678, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 24);
            this.label3.TabIndex = 211;
            this.label3.Text = "عنوان المتابعة";
            // 
            // TXT_assignTitle
            // 
            this.TXT_assignTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT_assignTitle.Location = new System.Drawing.Point(491, 146);
            this.TXT_assignTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_assignTitle.Multiline = true;
            this.TXT_assignTitle.Name = "TXT_assignTitle";
            this.TXT_assignTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TXT_assignTitle.Size = new System.Drawing.Size(312, 30);
            this.TXT_assignTitle.TabIndex = 210;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(538, 374);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(132, 58);
            this.button2.TabIndex = 214;
            this.button2.Text = "تعديل مهمة";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(400, 374);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(132, 58);
            this.button3.TabIndex = 215;
            this.button3.Text = "حذف مهمة";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(202, 374);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(192, 58);
            this.button4.TabIndex = 216;
            this.button4.Text = "انهاء ك مكتمل نهائيا";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 27);
            this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form_Tracker_Procedure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 723);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TXT_assignTitle);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.COMLIST_assination);
            this.Controls.Add(this.advanc_dgv_Assign_Comment);
            this.Name = "Form_Tracker_Procedure";
            this.Text = "Form_Tracker_Procedure";
            this.Load += new System.EventHandler(this.Form_Tracker_Procedure_Load);
            ((System.ComponentModel.ISupportInitialize)(this.advanc_dgv_Assign_Comment)).EndInit();
            this.ContextMenuStrip_right_click.ResumeLayout(false);
            this.ContextMenuStrip_right_click.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView advanc_dgv_Assign_Comment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox COMLIST_assination;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TXT_assignTitle;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip_right_click;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
    }
}