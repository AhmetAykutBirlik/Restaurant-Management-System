using RM.NewFolder1;
using RM.RMModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ListBox = System.Windows.Forms.ListBox;

namespace RM.RMView
{
    public partial class frmBillList : Form
    {

        public frmBillList()
        {
            InitializeComponent();
        }
        public int MainID = 0;

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmBillList_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            string qry = @"SELECT MainID, TableName, WaiterName, orderType, status, total 
                   FROM tblMain WHERE status <>'Pending'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvtable);
            lb.Items.Add(dgvWaiter);
            lb.Items.Add(dgvType);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvTotal);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //#src no

            int count = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                this.Close();
            }
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                //print bill
                string qry = @"SELECT * FROM tblMain m 
                                        INNER JOIN tblDetails d ON m.MainID = d.MainID  
                                        INNER JOIN products p ON p.pID = d.proID
                                        where m.MainId = " + MainID + " ";
                SqlCommand cmd = new SqlCommand(qry, MainClass.con);
                MainClass.con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //dt.Load(cmd.ExecuteReader());
                da.Fill(dt);

                MainClass.con.Close();

                frmPrint frm = new frmPrint();
                CrystalReport1Receipr cr = new CrystalReport1Receipr();
                cr.SetDatabaseLogon("aab", "123");
                cr.SetDataSource(dt);

                frm.crystalReportViewer1.ReportSource = cr;
                frm.crystalReportViewer1.Refresh();
                frm.Show();
            }
        }
    }
}
