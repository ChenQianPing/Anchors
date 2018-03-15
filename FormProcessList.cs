using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using wfEnginer;

namespace Anchors
{
    public partial class FormProcessList : Form
    {
        public FormProcessList()
        {
            InitializeComponent();
        }

        private void FormProcessList_Load(object sender, EventArgs e)
        {
            LoadList();
            dataGridView1.Focus();
        }

        private void LoadList()
        {
            IDataReader dr = dbData.getDataReader(dbConn.DBConn, "select  F_ID, F_NAME, F_DESC, F_CHANGETIME, F_CHANGEUSER from WF_PROCESS");
            this.dataGridView1.Rows.Clear();
            while (dr.Read())
            {
                int rno=dataGridView1.Rows.Add();
                DataGridViewRow row= dataGridView1.Rows[rno];
                row.Cells[0].Value = dr.GetValue(0).ToString();
                row.Cells[1].Value = dr.GetString(1);
                row.Cells[2].Value = dr.GetDateTime(3).ToString();
                row.Cells[3].Value = dr.GetString(4);
                row.Cells[4].Value = dr.GetString(2);
            }
            dr.Dispose();
            dr = null;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rno = e.RowIndex;
            if (dataGridView1.Rows[rno].Cells[1].Value!=null)
                labelName.Text = dataGridView1.Rows[rno].Cells[1].Value.ToString();
            else
                labelName.Text = "Î´Ñ¡¶¨";
        }

        public string SelectRowID
        {
            get
            {
                if (dataGridView1.Rows.Count == 0)
                    return "";
                else
                    return dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
        }

    }
}