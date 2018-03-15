using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Security.Cryptography;
using Microsoft.Win32;

namespace Anchors
{
    class ConnStrMng
    {
        public static string DefaultKeyName = "zzSoftware";
        public static string DefaultParamName = "fcConnection";
        public static string DefaultKeyVal = "Connection-String";


        public static string SimpEncode(string aData, string aKey)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StreamWriter sw = null;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            try
            {
                //des.KeySize = aKey.Length;
                string s8key = aKey.Substring(0, 8);


                byte[] lKey = ASCIIEncoding.ASCII.GetBytes(s8key);
                byte[] iv = ASCIIEncoding.ASCII.GetBytes(s8key);
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(lKey, iv), CryptoStreamMode.Write);
                sw = new StreamWriter(cs);
                sw.Write(aData);
                sw.Flush();
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            finally
            {
                if (sw != null) sw.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
        }

        public static string SimpDecode(string aData, string aKey)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StreamReader sr = null;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                string s8key = aKey.Substring(0, 8);
                byte[] key = ASCIIEncoding.ASCII.GetBytes(s8key);
                byte[] iv = ASCIIEncoding.ASCII.GetBytes(s8key);
                ms = new MemoryStream(Convert.FromBase64String(aData));
                cs = new CryptoStream(ms, des.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            finally
            {
                if (sr != null) sr.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
        }

        public static void SaveToReg(string KeyName,string parmName, string data,string eKey)
        {
            // 创建注册表项和命名值
            RegistryKey rk = Registry.LocalMachine.OpenSubKey("Software", true);
            RegistryKey ck = rk.OpenSubKey(KeyName, true);
            if (ck == null)
            {
                ck = rk.CreateSubKey(KeyName);
            }


            // 将加密字符串、初始化向量和密钥写入注册表
            ck.SetValue(parmName, SimpEncode(data,eKey));
            ck.Close();
            rk.Close();
        }

        public static string LoadFromReg(string KeyName, string parmName,  string eKey)
        {
            // 创建注册表项和命名值
            RegistryKey rk = Registry.LocalMachine.OpenSubKey("Software", true);
            RegistryKey ck = rk.OpenSubKey(KeyName);
            string edata = "";
            if (ck != null)
            {
                edata = ck.GetValue(parmName, "").ToString();
                ck.Close();
                edata=SimpDecode(edata, eKey);
            }
            rk.Close();
            // 将加密字符串、初始化向量和密钥写入注册表
            return edata;
        }
    }
}
