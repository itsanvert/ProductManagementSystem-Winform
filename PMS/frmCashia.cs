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
    public partial class frmCashia : Form
    {
        public frmCashia()
        {
            InitializeComponent();
        }

        private void load_datagridview()
        {
            string query = "select ROW_NUMBER() OVER(Order BY casID) as No_,casID as ID,casName as [Name],casGender as Gender,casTel as [Telephone]," +
                "casAddress as [Address],username as [User Name],password_ as [Password] from tblCashia";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            try {
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dgvCashia.DataSource = bsource;
                sda.Update(dt);


                dgvCashia.Columns[0].Width = 70;
                dgvCashia.Columns[1].Width = 130;
                dgvCashia.Columns[2].Width = 130;
                dgvCashia.Columns[3].Width = 200;
                dgvCashia.Columns[4].Width = 200;
                dgvCashia.Columns[5].Width = 200;
                dgvCashia.Columns[1].Visible = false;


            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try {
                cDataConn.conn.Open();
                string query = "INSERT INTO tblCashia (casName, casGender,casTel,casAddress,username,password_) "+
                    "VALUES (N'"+txtCashiaName.Text+"',N'"+cboGender.Text+"',N'"+txtCashiaTelephone.Text+"' "+
                    ",N'"+txtAddress.Text+"',N'"+txtUserName.Text+"',N'"+txtPassword.Text+"') ";
                SqlCommand cmd = new SqlCommand(query, cDataConn.conn);
                cmd.ExecuteNonQuery();
                cDataConn.conn.Close();
                lblStatus.Text = "Your data has been saved.";
                load_datagridview();
                
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmCashia_Load(object sender, EventArgs e)
        {
            load_datagridview();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try {
                cDataConn.conn.Open();
                string query = "UPDATE tblCashia SET "+
                    "casName = N'"+txtCashiaName.Text+"', casGender = N'"+cboGender.Text+"',"+
                    "casTel = N'"+txtCashiaTelephone.Text+"',casAddress=N'"+txtAddress.Text+"',"+
                    "username = N'"+txtUserName.Text+"',password_ = N'"+txtPassword.Text+"' "+
                    "WHERE casID = N'"+txtCashiaID.Text+"' ";
                SqlCommand cmd = new SqlCommand(query, cDataConn.conn);
                cmd.ExecuteNonQuery();

                cDataConn.conn.Close();
                lblStatus.Text = "Your data has been Updated.";
                load_datagridview();

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCashia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                DataGridViewRow row = this.dgvCashia.Rows[e.RowIndex];
                txtCashiaID.Text = row.Cells["ID"].Value.ToString();
                txtCashiaName.Text = row.Cells["Name"].Value.ToString();
                cboGender.Text = row.Cells["Gender"].Value.ToString();
                txtCashiaTelephone.Text = row.Cells["Telephone"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtUserName.Text = row.Cells["User Name"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try {
                cDataConn.conn.Open();
                string query = "DELETE FROM tblCashia where casID = N'"+txtCashiaID.Text+"'";
                SqlCommand cmd = new SqlCommand(query, cDataConn.conn);
                cmd.ExecuteNonQuery();

                cDataConn.conn.Close();
                lblStatus.Text = "Your data has been deleted.";
                load_datagridview();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string query = "Select * from tblCashia where casName = N'" + txtCashiaNameSearch.Text + "'";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                BindingSource bsource = new BindingSource();
                bsource.DataSource = dt;
                dgvCashia.DataSource = bsource;
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
