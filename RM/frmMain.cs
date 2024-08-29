using RM.RMModel;
using RM.RMView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RM
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        // Method to add controls in Main Form
        public  void AddControls(Form f)
        {
            ControlsPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            ControlsPanel.Controls.Add(f);
            f.Show();

        }

        //for accessing frm
        static frmMain _obj;
        public static frmMain Instance
        {
            get { if( _obj == null) { _obj = new frmMain(); } return _obj; }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = MainClass.USER;
            _obj = this;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome());
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView());

        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            AddControls(new FrmTableV());

        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffView());

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            AddControls(new frmProductView());

        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            //AddControls(new frmPOS());
            frmPOS frm = new frmPOS();
            frm.Show();
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            AddControls(new frmKitchen());

        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            AddControls(new frmReportss());

        }
    }
}
