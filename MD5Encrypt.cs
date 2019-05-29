using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MD5FileCreator
{
    public partial class MD5Encrypt : Form
    {
        public MD5Encrypt()
        {
            InitializeComponent();
        }

        public static string EncryptWithMD5(string source)
        {
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                //加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
                strbul.Append(result[i].ToString("x4"));
            }
            return strbul.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            tbMD5.Text = "";
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (tbPath.Text == "" || !File.Exists(tbPath.Text))
            {
                MessageBox.Show("文件地址不合法或文件不存在！", "MD5加密程序", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tbMD5.Text = EncryptWithMD5(tbPath.Text);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择要加密的文件";
            dialog.Filter = "所有文件类型|*.*";
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = dialog.FileName;
                MessageBox.Show("加密成功！", "MD5加密程序", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
