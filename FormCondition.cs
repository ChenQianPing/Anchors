using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Anchors
{
    public partial class FormCondition : Form
    {
        private int yy = 1;
        public FormCondition()
        {
            InitializeComponent();
        }



        private void FormCondition_Load(object sender, EventArgs e)
        {
            //FillTableSelect();
        }

        private void FillTableSelect()
        {
            /*DataTable dt= TableInfo.GetInstance().TableInfoSet;
            if (dt != null)
            {
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    comboBoxTables.Items.Add(dt.Rows[i]["F_DISPLAY"].ToString());
                }
            }

            dt = TableInfo.GetInstance().ColumnInfoSet;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listBox1.Items.Add(dt.Rows[i]["F_TABLENAME"].ToString()+"."+ dt.Rows[i]["F_DISPLAY"].ToString());
                }
            }
             * */
        }

        private void comboBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*int idx = comboBoxTables.SelectedIndex;
            if (idx > -1)
            {
                FillColumnSelect();
            }
             * */
        }

        private void FillColumnSelect()
        {
            /*
            string sFilter = comboBoxTables.Text;
            DataTable dt = TableInfo.GetInstance().ColumnInfoSet;
            listBox1.Items.Clear();
            if (dt != null)
            {
                DataRow[] rows=dt.Select("F_TABLENAME='" + sFilter.Replace("'", "''") + "'");
                for (int i = 0; i < rows.Length; i++)
                {
                    listBox1.Items.Add(rows[i]["F_TABLENAME"].ToString() + "." + rows[i]["F_DISPLAY"].ToString());
                }
            }
             * */

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
               
            
        }
    }
}