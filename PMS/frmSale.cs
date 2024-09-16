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
    public partial class frmSale : Form
    {
        public frmSale()
        {
            InitializeComponent();
        }

        public int pubProductID;
        //=======get data from tblCashia and fill in cashia combobox===============
        private void getCashia() {
            string query = "select casID,casName from tblCashia";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                cboCashiaName.DisplayMember = "casName";
                cboCashiaName.ValueMember = "casID";
                cboCashiaName.DataSource = dt;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //=======get data from tblCashia and fill in cashia combobox===============
        private void getProduct()
        {
            string query = "select proID,proName from tblProduct";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                //DataRow row = dt.Rows[0];

                cboProductName.DisplayMember = "proName";
                cboProductName.ValueMember = "proID";
                cboProductName.DataSource = dt;
                //pubProductID = int.Parse(cboProductName.SelectedIndex());            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmSale_Load(object sender, EventArgs e)
        {
            getCashia();
            getProduct();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dgvInvoiceDetail.Rows.Add(cboProductName.SelectedValue, cboProductName.Text,txtQuantity.Text,txtUnitPrice.Text,txtTotal.Text);

        }

        private void cboProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getProduct();
            //MessageBox.Show(cboProductName.SelectedValue.ToString());
            string query = "select cast(ProPriceOut as real) as ProPriceOut from tblProduct where proID = " + cboProductName.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                DataRow row = dt.Rows[0];
                txtUnitPrice.Text = row["ProPriceOut"].ToString();
                calculateTotal();

                //cboProductName.DisplayMember = "proName";
                //cboProductName.ValueMember = "proID";
                //cboProductName.DataSource = dt;
                //pubProductID = int.Parse(cboProductName.SelectedIndex());            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void calculateTotal() {
            int quantity = 0;
            int unitPrice = 0;
            int total = 0;

            if (!string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                quantity = int.Parse(txtQuantity.Text);
                unitPrice = int.Parse(txtUnitPrice.Text);
                total = quantity * unitPrice;
                txtTotal.Text = total.ToString();
            }

            

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            calculateTotal();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvInvoiceDetail.CurrentCell != null)
            {
                dgvInvoiceDetail.Rows.RemoveAt(dgvInvoiceDetail.CurrentCell.RowIndex);
            }
        }

        private void dgvInvoiceDetail_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            int subtotal = 0;
            for (int i = 0; i <= dgvInvoiceDetail.Rows.Count - 1; i++)
            {
                subtotal += int.Parse(dgvInvoiceDetail.Rows[i].Cells[4].Value.ToString());
            }
            txtSubTotal.Text = subtotal.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(cboCashiaName.SelectedValue.ToString());
            
            try
            {
                //Insert data to tblInvoice
                cDataConn.conn.Open();
                string query = "INSERT INTO [dbo].[tblInvoice] ([invDate],[casID]) OUTPUT INSERTED.invID " +
                    "VALUES (N'" + dtpInvoiceDate.Text + "'," + cboCashiaName.SelectedValue + ")";
                SqlCommand cmd = new SqlCommand(query, cDataConn.conn);                
                int invID = (int)cmd.ExecuteScalar();
                
                //Insert data to tblInvoiceDetail
                for (int i = 0; i <= dgvInvoiceDetail.Rows.Count - 1; i++)
                {
                    string queryInv_Detail = "INSERT INTO [dbo].[tblInvoiceDetail] ([invID],[proID],[quantity],[unitPrice],[total]) " +
                     "VALUES (" + invID + "," + int.Parse(dgvInvoiceDetail.Rows[i].Cells[0].Value.ToString()) + ","+ int.Parse(dgvInvoiceDetail.Rows[i].Cells[2].Value.ToString()) + ","+ int.Parse(dgvInvoiceDetail.Rows[i].Cells[3].Value.ToString()) + ","+ int.Parse(dgvInvoiceDetail.Rows[i].Cells[4].Value.ToString()) + ") ";
                    SqlCommand cmd_invDetail = new SqlCommand(queryInv_Detail, cDataConn.conn);
                    cmd_invDetail.ExecuteNonQuery();
                }
                
                cDataConn.conn.Close();


                lblStatus.Text = "Your invoice No:"+invID.ToString()+" has been saved.";
                lblStatus.ForeColor = Color.Green;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
