using DevExpress.Utils.Behaviors.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace RM.RMModel
{
    public partial class frmProductAdd : Form
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public int cID = 0;
        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            //string qry = "select catID 'id' , catName 'name' from category ";
            //MainClass.CBFill(qry,cbCategory);
            //if(cbCategory > 0 )
            //{
            //    cbCategory.SelectedValue = cID;
            //}
            string qry = "select catID 'id', catName 'name' from category ";
            MainClass.CBFill(qry,cbCategory);
            if (cID > 0)
            {
                cbCategory.SelectedValue = cID;
            }
            if(id >0)
            {
                ForUpdateLoadData();
            }
        }
        string filepath;
        byte[] imageByteArray;
        public void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filepath = ofd.FileName;
                txtImage.Image = new Bitmap(filepath);
            }
        }


        public void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0) //insert
            {
                qry = "Insert into products Values (@Name , @Price , @cat , @img )";
            }
            else //update 
            {
                qry = "Update products set pName = @Name , pPrice=@price  , CategoryID=@cat , pImage=@img where pID=@id";
            }
            //for img 
            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
            ht.Add("@price", txtPrice.Text);
            ht.Add("@cat", Convert.ToInt32(cbCategory.SelectedValue));
            ht.Add("@img", imageByteArray);
            if (MainClass.SQl(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Saved successfully..");
                id = 0;
                cID= 0;
                txtName.Text = "";
                txtPrice.Text = "";
                cbCategory.SelectedIndex = 0;
                cbCategory.SelectedIndex = -1;
                txtImage.Image = RM.Properties.Resources.Product;
                txtName.Focus();
            }
        }
        private void ForUpdateLoadData()
        {
            string qry = @"Select * from products where pID = " + id + "";
            SqlCommand cmd = new SqlCommand(qry,MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["pName"].ToString();
                txtPrice.Text= dt.Rows[0]["pPrice"].ToString();

                byte[] imageArray = (byte[])(dt.Rows[0]["pImage"]);
                byte[] imageByteArray = imageArray;
                txtImage.Image = Image.FromStream(new MemoryStream(imageArray));
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close  ();
        }
            
    }
}
