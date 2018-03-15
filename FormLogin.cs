using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Anchors
{
    public partial class FormLogin : Form
    {
        private string mConnStr;
        private bool mIsChangeConn = false;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void editUserCode_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = (editUserCode.Text.Length > 0);
        }

        private void ShowConn()
        {
            Type dataLinksClass = Type.GetTypeFromProgID("DataLinks");
            object udl = Activator.CreateInstance(dataLinksClass);

            // 设置窗体句柄，使其以模式窗体打开。
            dataLinksClass.InvokeMember("hWnd", BindingFlags.SetProperty, null, udl, new object[] { this.Handle.ToInt32() });

            // 创建新连接字符串
            object conn = dataLinksClass.InvokeMember("PromptNew", BindingFlags.InvokeMethod, null, udl, null);


            if (conn != null)
            {
                mConnStr = (string)conn.GetType().InvokeMember("ConnectionString", BindingFlags.GetProperty, null, conn, null);

                ConnStrMng.SaveToReg(ConnStrMng.DefaultKeyName, ConnStrMng.DefaultParamName, mConnStr, ConnStrMng.DefaultKeyVal);
                mIsChangeConn = true;
            }
            if (udl != null) Marshal.ReleaseComObject(udl);
            if (conn != null) Marshal.ReleaseComObject(conn);
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            ShowConn();
        }

        public bool IsChangeConn
        {
            get { return mIsChangeConn; }
            set { IsChangeConn = value; }
        }

        public string ConnStr
        {
            get { return mConnStr; }
            set { mConnStr = value; }
        }

        public string UserCode
        {
            get { return editUserCode.Text; }
        }

        public string Password
        {
            get { return EditPassword.Text; }
        }

        private void editUserCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditPassword.Focus();
            }
        }

        private void EditPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick() ;
            }

        }
    }
}