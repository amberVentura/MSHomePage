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
    public partial class Login : Form
    {
        Hashtable adminList = new Hashtable();
        public Login()
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
                MessageBox.Show("Incomplete username or password!", "Please Try Again!");
                return;
            }
            if (adminList.ContainsKey(user)) 
            {
                string saveHash = (string)adminList[user];
                String inputHash = GetHash(password);

                if (saveHash == inputHash)
                {
                    MessageBox.Show("Welcome, " + user + "!", "Login Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    HomePage main = new HomePage(); // Lalabas bigla yung homepage 
                    main.Show();
                    this.Hide();
                }
                else 
                {
                    MessageBox.Show("Incorrect password!", "Try Again!");
                    tbPassword.Clear();
                }
            }
            else 
            {
                MessageBox.Show("Incorrect username!", "Try Again!");
                tbUser.Clear();
                tbPassword.Clear();
            }
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
