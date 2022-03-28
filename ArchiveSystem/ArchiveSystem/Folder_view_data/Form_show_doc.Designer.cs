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
            this.ImageList_add_viwe = new System.Windows.Forms.ImageList(this.components);
            this.ImageList_Extension = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ListView_show_doc
            // 
            this.ListView_show_doc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_show_doc.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListView_show_doc.LargeImageList = this.ImageList_add_viwe;
            this.ListView_show_doc.Location = new System.Drawing.Point(45, 220);
            this.ListView_show_doc.Name = "ListView_show_doc";
            this.ListView_show_doc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ListView_show_doc.Size = new System.Drawing.Size(1202, 353);
            this.ListView_show_doc.SmallImageList = this.ImageList_add_viwe;
            this.ListView_show_doc.TabIndex = 277;
            this.ListView_show_doc.UseCompatibleStateImageBehavior = false;
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
            // Form_show_doc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 818);
            this.Controls.Add(this.ListView_show_doc);
            this.Name = "Form_show_doc";
            this.Text = "Form_show_doc";
            this.Load += new System.EventHandler(this.Form_show_doc_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView ListView_show_doc;
        internal System.Windows.Forms.ImageList ImageList_Extension;
        internal System.Windows.Forms.ImageList ImageList_add_viwe;
    }
}