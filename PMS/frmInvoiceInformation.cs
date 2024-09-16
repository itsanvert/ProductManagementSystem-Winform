using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PMS
{
    public partial class frmInvoiceInformation : Form
    {
        public frmInvoiceInformation()
        {
            InitializeComponent();
        }
        private void load_datagridview()
        {
            string query = "select INV.invID,INV.invDate,CAS.casName,PRO.proName,INVD.quantity,INVD.unitPrice,INVD.total from tblInvoiceDetail as INVD "+
                                "inner join tblInvoice as INV on INV.invID = INVD.invID "+
                                "inner join tblProduct as PRO on PRO.proID = INVD.proID "+
                                "inner join tblCashia as CAS on CAS.casID = INV.casID";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dgvInvoice.DataSource = bsource;
                sda.Update(dt);


                dgvInvoice.Columns[0].Width = 100;
                dgvInvoice.Columns[1].Width = 150;
                dgvInvoice.Columns[2].Width = 180;
                dgvInvoice.Columns[3].Width = 180;
                dgvInvoice.Columns[4].Width = 130;
                dgvInvoice.Columns[5].Width = 180;
                dgvInvoice.Columns[6].Width = 180;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void frmInvoiceInformation_Load(object sender, EventArgs e)
        {
            load_datagridview();
        }
    }
}
