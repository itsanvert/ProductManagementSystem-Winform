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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string query = "select * from tblCashia where username=N'"+txtUserName.Text+"' and password_ = N'"+txtPassword.Text+"'";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                //MessageBox.Show("Login Successfully");
                this.Hide();

                frmCashia frmCas = new frmCashia();
                frmCas.ShowDialog();
            }
            else
            {
                MessageBox.Show("User not found. Login fail.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();  
            //this.Close();
        }
    }
}
