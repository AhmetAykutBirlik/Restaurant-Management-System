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

namespace RM.RMModel
{
    public partial class frmStaffAdd : Form
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }
        public int id = 0;

        private void frmStaffAdd_Load(object sender, EventArgs e)
        {

        }


        public void btnSave_Click_1(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0) //insert
            {
                qry = "Insert into staff Values (@Name , @Phone , @Role)";
            }
            else //update 
            {
                qry = "Update staff set sName = @Name , sPhone=@Phone  , sRole=@Role where staffId=@id";
            }
            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@Phone", txtPhone.Text);
            ht.Add("@Role", cbRole.Text);
            if (MainClass.SQl(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Saved successfully..");
                id = 0;
                txtName.Text = "";
                txtPhone.Text = "";
                cbRole.SelectedIndex = -1;
                txtName.Focus();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
