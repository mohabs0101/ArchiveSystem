using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveSystem
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'archiveDataset.DataTable1' table. You can move, or remove it, as needed.
            this.dataTable1TableAdapter.Fill(this.archiveDataset.DataTable1);
            // TODO: This line of code loads data into the 'archiveDataset.ArchiveBooks_TBL' table. You can move, or remove it, as needed.

            this.reportViewer1.RefreshReport();
        }
    }
}
