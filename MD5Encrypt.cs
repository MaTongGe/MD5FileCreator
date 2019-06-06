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
            FileStream file = new FileStream(source, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x"));
            }
            return sb.ToString();
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
            MessageBox.Show("加密成功！", "MD5加密程序", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择要加密的文件";
            dialog.Filter = "所有文件类型|*.*";
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = dialog.FileName;
            }
        }

        private void TbPath_DragDrop(object sender, DragEventArgs e)
        {
            tbPath.Text = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

            if (tbPath.Text == "" || !File.Exists(tbPath.Text))
            {
                MessageBox.Show("文件地址不合法或文件不存在！", "MD5加密程序", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tbMD5.Text = EncryptWithMD5(tbPath.Text);
        }

        private void TbPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
