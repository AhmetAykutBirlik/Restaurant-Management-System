using RM.NewFolder1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.RMView
{
    public partial class frmReportss : Form
    {
        public frmReportss()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT * FROM products ";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            MainClass.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //dt.Load(cmd.ExecuteReader());
            da.Fill(dt);

            MainClass.con.Close();

            frmPrint frm = new frmPrint();
            rptMenu cr = new rptMenu();
            cr.SetDatabaseLogon("aab", "123");
            cr.SetDataSource(dt);

            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT * FROM staff ";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            MainClass.con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //dt.Load(cmd.ExecuteReader());
            da.Fill(dt);

            MainClass.con.Close();

            frmPrint frm = new frmPrint();
            rptStaffList cr = new rptStaffList();
            cr.SetDatabaseLogon("aab", "123");
            cr.SetDataSource(dt);

            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnSaleCat_Click(object sender, EventArgs e)
        {
            frmSaleByCategory frm = new frmSaleByCategory();
            frm.ShowDialog();
            
        }
    }
}
