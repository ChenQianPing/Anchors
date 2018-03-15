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
    public partial class FormUserList : Form
    {
        public FormUserList()
        {
            InitializeComponent();
        }

        private void FormUserList_Load(object sender, EventArgs e)
        {
            EditUserRole.SelectedIndex = 0;
        }

        private void EditUserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (EditUserRole.SelectedIndex == 0)
            {
                GetUserData();
            }
            else
            {
                GetRoleData();
            }
        }

        private void GetUserData()
        {
            IDataReader dr = dbData.getDataReader(dbConn.DBConn, "select F_User_Code ,F_USER_NAME from T_ACCOUNTS_USER_INFO");
            while (dr.Read())
            {
                int rno=dataGridView1.Rows.Add();
                dataGridView1.Rows[rno].Cells[0].Value = dr.GetValue(0).ToString();
                dataGridView1.Rows[rno].Cells[1].Value = dr.GetString(1);
            }
        }

        private void GetRoleData()
        {
            IDataReader dr = dbData.getDataReader(dbConn.DBConn, "select F_ROLE_ID ,F_ROLE_NAME from T_ACCOUNTS_ROLE");
            while (dr.Read())
            {
                int rno = dataGridView1.Rows.Add();
                dataGridView1.Rows[rno].Cells[0].Value = dr.GetValue(0).ToString();
                dataGridView1.Rows[rno].Cells[1].Value = dr.GetString(1);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = (dataGridView1.CurrentRow !=null);
        }

        public int UserType
        {
            get { return EditUserRole.SelectedIndex; }
        }

        public bool GetSelected(ref string Code, ref string Name)
        {
            if (dataGridView1.CurrentRow ==null)
                return false;
            else
            {
                Code = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                return true;
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex>0)
            {
                btnOK.PerformClick();
            }
        }
    }
}