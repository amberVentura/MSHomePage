using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSHomePage
{
    public partial class Form2 : Form
    {
        Hashtable adminList = new Hashtable();
        public Form2()
        {
            InitializeComponent();
            LoadAdmins();
        }
        //Ilagay ang GetHash para siguradong nakikita  siya
        private string GetHash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        // Dito nakalagay ang mga admin
        private void LoadAdmins()
        {
            adminList.Add("admin", GetHash("12345"));
            adminList.Add("dentist", GetHash("dentist"));
            adminList.Add("manager", GetHash("manager123"));
            adminList.Add("reciption", GetHash("hello123"));
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            tbPassword.UseSystemPasswordChar = true; // hindi makikita yung password
            tbUser.Focus(); 
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            string user = tbUser.Text.Trim();
            string password = tbPassword.Text.Trim();

            if  (user == "" || password == "") 
            {
                MessageBox.Show("Kulang po ang username o password!", "Error");
                return;
            }
            if (adminList.ContainsKey(user)) 
            {
                string saveHash = (string)adminList[user];
                String inputHash = GetHash(password);

                if (saveHash == inputHash)
                {
                    MessageBox.Show("Welcome po, " + user + "!", "Login Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form1 main = new Form1(); // Lalabas bigla yung homepage 
                    main.Show();
                    this.Hide();
                }
                else 
                {
                    MessageBox.Show("Mali ang password!", "Error");
                    tbPassword.Clear();
                }
            }
            else 
            {
                MessageBox.Show("Hindi po kilala ang username!", "Error");
                tbUser.Clear();
                tbPassword.Clear();
            }
        }
    }
}
