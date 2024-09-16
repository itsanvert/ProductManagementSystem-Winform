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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }
        private void load_datagridview()
        {
            string query = "SELECT [proID] As ID,[proName] as [Product Name],[proType] as [Product Type],[proTotal] as Total,cast([proPriceIn] as real) as [Price In],cast([ProPriceOut] as real) as [Price Out] FROM [dbo].[tblProduct]";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dgvProduct.DataSource = bsource;
                sda.Update(dt);


                dgvProduct.Columns[0].Width = 70;
                dgvProduct.Columns[1].Width = 180;
                dgvProduct.Columns[2].Width = 180;
                dgvProduct.Columns[3].Width = 80;
                dgvProduct.Columns[4].Width = 100;
                dgvProduct.Columns[5].Width = 100;

                
                //dgvProduct.Columns[4].DefaultCellStyle.Format="N2";
                //dgvProduct.Columns[5].DefaultCellStyle.Format="N2";
                //dgvProduct.Columns[1].Visible = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void tsmInsert_Click(object sender, EventArgs e)
        {
            try
            {
                cDataConn.conn.Open();
                string query = "INSERT INTO [dbo].[tblProduct] ([proName],[proType],[proTotal],[proPriceIn],[ProPriceOut]) " +
                    "VALUES (N'" + txtProductName.Text + "',N'" + cboProductType.Text + "'," + txtTotal.Text + " " +
                    "," + txtPriceIn.Text + "," + txtPriceOut.Text + ") ";
                SqlCommand cmd = new SqlCommand(query, cDataConn.conn);
                cmd.ExecuteNonQuery();
                cDataConn.conn.Close();
                lblStatus.Text = "Your data has been saved.";
                lblStatus.ForeColor = Color.Green;
                load_datagridview();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                cDataConn.conn.Open();
                string query = "UPDATE [tblProduct] SET " +
                    "[proName] = N'" + txtProductName.Text + "', [proType] = N'" + cboProductType.Text + "'," +
                    "[proTotal] = " + txtTotal.Text + ",[proPriceIn]=" + txtPriceIn.Text + "," +
                    "[ProPriceOut] = " + txtPriceOut.Text + " " +
                    "WHERE proID = N'" + txtProductID.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, cDataConn.conn);
                cmd.ExecuteNonQuery();

                cDataConn.conn.Close();
                lblStatus.Text = "Your data has been Updated.";
                lblStatus.ForeColor = Color.Green;
                load_datagridview();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            load_datagridview();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvProduct.Rows[e.RowIndex];
                txtProductID.Text = row.Cells["ID"].Value.ToString();
                txtProductName.Text = row.Cells["Product Name"].Value.ToString();
                cboProductType.Text = row.Cells["Product Type"].Value.ToString();
                txtTotal.Text = row.Cells["Total"].Value.ToString();
                txtPriceIn.Text = row.Cells["Price In"].Value.ToString();
                txtPriceOut.Text = row.Cells["Price Out"].Value.ToString();
                
            }
        }

        private void txtPriceIn_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cDataConn.conn.Open();
                string query = "DELETE FROM tblProduct where proID = N'" + txtProductID.Text + "'";
                SqlCommand cmd = new SqlCommand(query, cDataConn.conn);
                cmd.ExecuteNonQuery();

                cDataConn.conn.Close();
                lblStatus.Text = "Your data has been deleted.";
                lblStatus.ForeColor = Color.Red;
                load_datagridview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = "Select [proID] As ID,[proName] as [Product Name],[proType] as [Product Type],[proTotal] as Total,[proPriceIn] as [Price In],cast([ProPriceOut] as real) as [Price Out] from tblProduct where proName like N'%" + txtSearchProductName.Text + "%'";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dgvProduct.DataSource = bsource;
                sda.Update(dt);

                dgvProduct.Columns[0].Width = 70;
                dgvProduct.Columns[1].Width = 180;
                dgvProduct.Columns[2].Width = 180;
                dgvProduct.Columns[3].Width = 80;
                dgvProduct.Columns[4].Width = 100;
                dgvProduct.Columns[5].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
