using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Anchors
{
    public partial class CtrConditionItem : UserControl
    {
        private string sFilter;

        public CtrConditionItem()
        {
            InitializeComponent();
        }

        private void CtrConditionItem_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = this.ClientRectangle;
            r.Width = r.Width - 1;
            r.Height = r.Height - 1;
            e.Graphics.DrawRectangle(Pens.Gray, r);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Dispose(true);

        }

        private void CtrConditionItem_Load(object sender, EventArgs e)
        {
            FillTableSelect();
        }

        private void FillTableSelect()
        {
            /*DataTable dt = TableInfo.GetInstance().TableInfoSet;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxTables.Items.Add(dt.Rows[i]["F_DISPLAY"].ToString());
                }
            }
             */

            DataTable dt = TableInfo.GetInstance().ColumnInfoSet;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxField.Items.Add(dt.Rows[i]["F_TABLENAME"].ToString() + "." + dt.Rows[i]["F_DISPLAY"].ToString());
                }
            }
        }

 
        public void FillColumnSelect()
        {
            //string sFilter = comboBoxTables.Text;
            DataTable dt = TableInfo.GetInstance().ColumnInfoSet;
            comboBoxField.Items.Clear();
            if (dt != null)
            {
                DataRow[] rows = dt.Select("F_TABLENAME='" + sFilter.Replace("'", "''") + "'");
                for (int i = 0; i < rows.Length; i++)
                {
                    comboBoxField.Items.Add(rows[i]["F_TABLENAME"].ToString() + "." + rows[i]["F_DISPLAY"].ToString());
                }
            }
        }

        public string Filter
        {
            get { return sFilter; }
            set { sFilter = value; }
        }
 
    }
}
