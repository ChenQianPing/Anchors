using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Anchors
{
    public partial class FormProcessProp : Form
    {
        public FormProcessProp()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnOK.Enabled = (EditName.Text.Trim() != "" && EditDesc.Text.Trim() != "");
        }

        public string NameValue
        {
            get { return EditName.Text; }
            set { EditName.Text = value; }
        }

        public string DescValue
        {
            get { return EditDesc.Text; }
            set { EditDesc.Text = value; }
        }

        public string ChangeTimeValue
        {
            set { EditChangeTime.Text = value; }
        }

        public string ChangeUserValue
        {
            set { EditChangeUser.Text = value; }
        }
    }
}