using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void cashiaInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmCashia frmCas = new frmCashia();
            //frmCas.ShowDialog();

            frmCashia frmCas = new frmCashia();
            frmCas.TopLevel = false;
            frmCas.FormBorderStyle = FormBorderStyle.None;
            frmCas.Dock = DockStyle.Fill;
            this.Controls.Add(frmCas);
            frmCas.BringToFront();
            frmCas.Show();
            

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmCashia frmCas = new frmCashia();
            frmCas.TopLevel = false;
            frmCas.FormBorderStyle = FormBorderStyle.None;
            frmCas.Dock = DockStyle.Fill;
            this.Controls.Add(frmCas);
            frmCas.BringToFront();
            frmCas.Show();
        }

        private void productInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduct frmPro = new frmProduct();
            frmPro.TopLevel = false;
            frmPro.FormBorderStyle = FormBorderStyle.None;
            frmPro.Dock = DockStyle.Fill;
            this.Controls.Add(frmPro);
            frmPro.BringToFront();
            frmPro.Show();
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmProduct frmPro = new frmProduct();
            frmPro.TopLevel = false;
            frmPro.FormBorderStyle = FormBorderStyle.None;
            frmPro.Dock = DockStyle.Fill;
            this.Controls.Add(frmPro);
            frmPro.BringToFront(); 
            frmPro.Show();
        }

        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            frmSale frmSal = new frmSale();
            frmSal.TopLevel = false;
            frmSal.FormBorderStyle = FormBorderStyle.None;
            frmSal.Dock = DockStyle.Fill;
            this.Controls.Add(frmSal);
            frmSal.BringToFront();
            frmSal.Show();
        }

        private void invoiceInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            frmInvoiceInformation frmInv = new frmInvoiceInformation();
            frmInv.TopLevel = false;
            frmInv.FormBorderStyle = FormBorderStyle.None;
            frmInv.Dock = DockStyle.Fill;
            this.Controls.Add(frmInv);
            frmInv.BringToFront();
            frmInv.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }
    }
}
