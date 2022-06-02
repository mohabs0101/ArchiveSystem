namespace ArchiveSystem.FollowUp
{
    partial class Form_Manager_FollowUp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.advanc_dgv_FollowUp = new Zuby.ADGV.AdvancedDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.advanc_dgv_FollowUp)).BeginInit();
            this.SuspendLayout();
            // 
            // advanc_dgv_FollowUp
            // 
            this.advanc_dgv_FollowUp.AllowUserToAddRows = false;
            this.advanc_dgv_FollowUp.AllowUserToDeleteRows = false;
            this.advanc_dgv_FollowUp.AllowUserToOrderColumns = true;
            this.advanc_dgv_FollowUp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advanc_dgv_FollowUp.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.advanc_dgv_FollowUp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.advanc_dgv_FollowUp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.advanc_dgv_FollowUp.DefaultCellStyle = dataGridViewCellStyle2;
            this.advanc_dgv_FollowUp.EnableHeadersVisualStyles = false;
            this.advanc_dgv_FollowUp.FilterAndSortEnabled = true;
            this.advanc_dgv_FollowUp.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advanc_dgv_FollowUp.Location = new System.Drawing.Point(10, 84);
            this.advanc_dgv_FollowUp.Name = "advanc_dgv_FollowUp";
            this.advanc_dgv_FollowUp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.advanc_dgv_FollowUp.RowTemplate.Height = 26;
            this.advanc_dgv_FollowUp.Size = new System.Drawing.Size(776, 566);
            this.advanc_dgv_FollowUp.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advanc_dgv_FollowUp.TabIndex = 412;
            // 
            // Form_Manager_FollowUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 673);
            this.Controls.Add(this.advanc_dgv_FollowUp);
            this.Font = new System.Drawing.Font("Tahoma", 11F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_Manager_FollowUp";
            this.RightToLeftLayout = true;
            this.Text = "المتابعة والمهام";
            this.Load += new System.EventHandler(this.Form_Manager_FollowUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.advanc_dgv_FollowUp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView advanc_dgv_FollowUp;
    }
}