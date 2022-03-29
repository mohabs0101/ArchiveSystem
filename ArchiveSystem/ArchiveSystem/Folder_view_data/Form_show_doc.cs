using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace ArchiveSystem.Folder_view_data
{



    public partial class Form_show_doc : MetroFramework.Forms.MetroForm
    {
        public Form_show_doc()
        {
            InitializeComponent();
        }
        public static implicit operator Form_show_doc(ScanDialog v)
        {
            throw new NotImplementedException();
        }



        String fn_g;

        void check_Extension_file()
        {

            var Extension_file = Path.GetExtension(fn_g);


            if (Extension_file == ".docx")
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[0]); }
            else if (Extension_file == ".pptx") //بور بوينت"
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[1]); }
            else if (Extension_file == ".xlsx") //ملف اكسل
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[2]); }
            else if (Extension_file == ".accdb") //'ملف اكسس
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[3]); }
            else if (Extension_file == ".txt") //'ملف نصي
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[4]); }
            else if (Extension_file == ".pdf") //'ملف pdf
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[5]); }
            else if (Extension_file == ".mp4") //'فيديو
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[6]); }
            else if (Extension_file == ".bnp" || Extension_file == ".bmp" || Extension_file == ".gif" || Extension_file == ".tif" || Extension_file == ".exe" || Extension_file == ".dll" || Extension_file == ".ico" || Extension_file == ".glp" || Extension_file == ".psd" || Extension_file == "." || Extension_file == ".xml" || Extension_file == ".html" || Extension_file == ".js" || Extension_file == ".css") //اخرى
            { ImageList_add_viwe.Images.Add(fn_g, ImageList_Extension.Images[7]); }
            else
            { ImageList_add_viwe.Images.Add(fn_g, Bitmap.FromFile(fn_g)); }

        }



        private void Form_show_doc_Load(object sender, EventArgs e)
        {
            ListView_show_doc.Columns.Add("الملف", 300);
            ListView_show_doc.View = View.LargeIcon;

            ListView_show_doc.Items.Clear();
            ImageList_add_viwe.Images.Clear();

            ImageList_add_viwe.ImageSize = new Size(100, 100);
            ListView_show_doc.Columns[0].Width = 120;

            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];

            foreach (string file in System.IO.Directory.GetFiles(path_folder_client_temp))
            {

                fn_g = file;

                check_Extension_file();


                FileInfo fi = new FileInfo(fn_g);

                //var files_n = new List<String>();
                //files_n.Add(fi.FullName);

                ListView_show_doc.Items.Add(fi.Name, ImageList_add_viwe.Images.Count - 1);

            }


        }
        string pic;
        private void ListView_show_doc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];

            ListView.SelectedListViewItemCollection breakfast = this.ListView_show_doc.SelectedItems;

            foreach (ListViewItem Item in breakfast)
            {

                try
                {
                    pic = path_folder_client_temp + @"\" + ListView_show_doc.Items[Item.Index].SubItems[0].Text;
                    pictureBox_show_doc.Load(pic);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Process.Start(pic);
                }

            }

        }

        void Rotat_img(RotateFlipType r90)
        {
            try
            {
                Image img = pictureBox_show_doc.Image;
                img.RotateFlip(r90);
                pictureBox_show_doc.Image = img;
            }
            catch (Exception ex)
            {

            }
        }
        private void btn_RotateR90_Click(object sender, EventArgs e)
        {
            Rotat_img(RotateFlipType.Rotate90FlipNone);
        }
        private void btn_RotateL90_Click(object sender, EventArgs e)
        {
            Rotat_img(RotateFlipType.Rotate180FlipNone);
        }

       

       

        private void ZoomInOut(bool zoom)
        {
            try
            { 
            //Zoom ratio by which the images will be zoomed by default
            int zoomRatio = 10;
            //Set the zoomed width and height
            int widthZoom = pictureBox_show_doc.Width * zoomRatio / 100;
            int heightZoom = pictureBox_show_doc.Height * zoomRatio / 100;
            //zoom = true --> zoom in
            //zoom = false --> zoom out
            if (!zoom)
            {
                widthZoom *= -1;
                heightZoom *= -1;
            }
            //Add the width and height to the picture box dimensions
            pictureBox_show_doc.Width += widthZoom;
            pictureBox_show_doc.Height += heightZoom;
            }
            catch (Exception ex)
            {

            }

        }
        private void btn_zoom_in_Click(object sender, EventArgs e)
        {
            ZoomInOut(true);
        }

        private void btn_zoom_out_Click(object sender, EventArgs e)
        {
            ZoomInOut(false);
        }

        private void cm_type_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cm_type_show.SelectedIndex == 0)
            {
                ListView_show_doc.View = View.Details;
                ListView_show_doc.GridLines = true;
            }
            else if (cm_type_show.SelectedIndex == 1)
            { ListView_show_doc.View = View.LargeIcon; }
            else if (cm_type_show.SelectedIndex == 2)
            { ListView_show_doc.View = View.List; }
            else if (cm_type_show.SelectedIndex == 3)
            { ListView_show_doc.View = View.SmallIcon; }
            else if (cm_type_show.SelectedIndex == 4)
            { //ListView_show_doc.View = View.Tile
            }

        }
        private void TSM_open_file_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(pic);
        }

        private void TSM_open_all_file_Click(object sender, EventArgs e)
        {
            string path_folder_client_temp = ConfigurationManager.AppSettings["Path_Folder_Client_Temp"];
            System.Diagnostics.Process.Start(path_folder_client_temp);
        }
    }
}
