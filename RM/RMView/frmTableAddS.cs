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
using System.Xml.Linq;

namespace RM.RMView
{
    public partial class frmTableAddS : Form
    {
        public frmTableAddS()
        {
            InitializeComponent();
        }
        public int id =0;
        private void frmTableAddS_Load(object sender, EventArgs e)
        {

        }
       

        public  void btnSave_Click_1(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0) //insert
            {
                qry = "Insert into tables Values (@Name)";
            }
            else //update 
            {
                qry = "Update tables set tname = @Name where tid=@id";
            }
            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            if (MainClass.SQl(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Saved successfully..");
                id = 0;
                txtName.Text = "";
                txtName.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
