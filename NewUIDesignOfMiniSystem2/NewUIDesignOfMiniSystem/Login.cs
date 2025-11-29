using NewUIDesignOfMiniSystem.forrmss;
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
using static NewUIDesignOfMiniSystem.forrmss.Appointment;

namespace NewUIDesignOfMiniSystem
{
    public partial class Login : Form
    {
        
        private BindingList<Record> AppointmentRecords;
        private BindingList<Record> DeletedRecords;
        private Records recordsForm;
        private Dictionary<string, decimal> procedurePrices;
        private Billings billingsForm;
        Hashtable adminList = new Hashtable();
        
        public Login(BindingList<Appointment.Record> appointmentRecords, Billings billingsForm, BindingList<Record> deletedRecords, Records recordsForm, Dictionary<string, decimal> procedurePrices)
        {
            InitializeComponent();
            LoadAdmins();
            AppointmentRecords = appointmentRecords;
            DeletedRecords = deletedRecords;
            this.billingsForm = billingsForm;
            this.recordsForm = recordsForm;
            this.procedurePrices = procedurePrices;

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
        private void Form1_Load(object sender, EventArgs e)
        {
            tbPassword.UseSystemPasswordChar = true; // hindi makikita yung password
            tbUser.Focus();

            tbUser.KeyDown += tbUser_KeyDown;
            tbPassword.KeyDown += tbPassword_KeyDown;
        }
        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbPassword.Focus();           
                tbPassword.SelectAll();       
                e.SuppressKeyPress = true;    
                e.Handled = true;
            }
        }
        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bLogin.PerformClick();        
                e.SuppressKeyPress = true;    
                e.Handled = true;
            }
        }
        private void bLogin_Click(object sender, EventArgs e)
        {
            string user = tbUser.Text.Trim();
            string password = tbPassword.Text.Trim();

            if (user == "" || password == "")
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

                    MainPage main = new MainPage(billingsForm,DeletedRecords,recordsForm,procedurePrices); // Lalabas bigla yung homepage 
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
    }
}
