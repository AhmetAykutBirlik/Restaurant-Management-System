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
    public partial class frmSaleByCategory : Form
    {
        public frmSaleByCategory()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            string qry = @"SELECT * FROM tblMain m 
                   INNER JOIN tblDetails d ON m.MainID = d.MainID
                   INNER JOIN products p ON p.pID = d.proID
                   INNER JOIN category c ON c.catID = p.CategoryID
                   WHERE m.aDate BETWEEN @sdate AND @edate"; // Sorgunun WHERE kısmını doğru yere taşıdık.

            using (SqlCommand cmd = new SqlCommand(qry, MainClass.con))
            {
                cmd.Parameters.AddWithValue("@sdate", dateTimePicker1.Value.Date); // Convert.ToDateTime gereksiz
                cmd.Parameters.AddWithValue("@edate", dateTimePicker2.Value.Date); // Convert.ToDateTime gereksiz

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                try
                {
                    MainClass.con.Open();
                    da.Fill(dt); // Sadece DataAdapter kullanarak dolduruyoruz.
                }
                finally
                {
                    MainClass.con.Close();
                }

                frmPrint frm = new frmPrint();
                rptSaleByCategory cr = new rptSaleByCategory();
                cr.SetDatabaseLogon("aab", "123"); // Eğer gerekliyse bu bilgiyi kullanın.
                cr.SetDataSource(dt);

                frm.crystalReportViewer1.ReportSource = cr;
                frm.crystalReportViewer1.Refresh();
                frm.Show();
            }
        }

    }
}
