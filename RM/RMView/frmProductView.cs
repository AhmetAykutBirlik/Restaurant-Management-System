using Guna.UI2.WinForms;
using RM;
using RM.RMModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM.RMView
{
    public partial class frmProductView : Form
    {
        public frmProductView()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            string qry = "select pID , pName , pPrice , CategoryID , c.catId from products p inner join category c on c.catId = p.CategoryID  /*where pName like '%" + txtSearch.Text + "%'*/ ";
            ListBox lb = new ListBox();
            lb.Items.Add(dataGridViewTextBoxColumn2);
            lb.Items.Add(dataGridViewTextBoxColumn3);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvCat);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }
        public void frmCategoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }
   
        public void btnAdd_Click_1(object sender, EventArgs e)
        {
            //frmStaffAdd frm = new frmStaffAdd();
            //frm.ShowDialog();
            MainClass.BlurBackground(new frmProductAdd());
            GetData();
        }
        public void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        public void frmProductView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dataGridViewImageColumn1")
            {
                frmProductAdd frm = new frmProductAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn2"].Value);
                frm.cID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);
                //frm.txtPrice.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvPrice"].Value);
                //frm.cbCategory.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvCat"].Value);
                MainClass.BlurBackground(frm);
                //frm.ShowDialog();
                GetData();
            }

            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dataGridViewImageColumn2")
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn2"].Value);
                    string qry = "Delete from products where pID = " + id + "";
                    Hashtable ht = new Hashtable();
                    MainClass.SQl(qry, ht);

                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Deleted successfully");
                    GetData();
                }
            }
        }
    }
}