﻿using System;
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
    public partial class frmPrint : Form
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            btnMax.PerformClick();
        }
    }
}
