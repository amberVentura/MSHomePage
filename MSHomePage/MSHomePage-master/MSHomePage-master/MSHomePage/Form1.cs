using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// MIEL BISAYAWA
namespace MSHomePage
{
    public partial class Form1 : Form
    {
       Dictionary<string, List <string>> availableSlots = new Dictionary<string, List <string>> ();
        BindingList<Record> appointmentRecords = new BindingList<Record> ();
        public Form1()
        {
            InitializeComponent();
        }
        public class Record
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Gender { get; set; }
            public string ContactNumber { get; set; }
            public string Email { get; set; }
            public string DoctorName { get; set; }
            public string Procedures { get; set; }
            public string PaymentStatus { get; set; }
            // nag dagdag lng get set para sa update status :)
        }
        private void UpdateSelectedPatientPaymentStatus()
        {
            
            string firstName = tbFirstNameAppoint?.Text.Trim();
            string lastName = tbLastNaneAppoint?.Text.Trim();
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return;

           
            string paymentStatus = TotalPrice?.Tag?.ToString() ?? "N/A";

           
            var record = appointmentRecords.FirstOrDefault(r =>
                r.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                r.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (record != null)
            {
                record.PaymentStatus = paymentStatus;
                dataGridView1.Invoke(new Action(() => dataGridView1.Refresh())); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = appointmentRecords;
           List<string> slots = new List<string> 
            {
                "10:00 AM ", 
                "11:30 AM ",
                "1:00 PM ",
                "2:30 PM ",
                "4:00 PM ",
                "5:30 PM ",
                "7:00 PM "
            };
            availableSlots["2025 - 11 - 01"] = new List<string> (slots);            availableSlots["2025 - 11 - 02"] = new List<string>(slots);
            availableSlots["2025 - 11 - 03"] = new List<string>(slots);             availableSlots["2025 - 11 - 04"] = new List<string>(slots);
            availableSlots["2025 - 11 - 05"] = new List<string>(slots);             availableSlots["2025 - 11 - 06"] = new List<string>(slots);
            availableSlots["2025 - 11 - 07"] = new List<string>(slots);             availableSlots["2025 - 11 - 08"] = new List<string>(slots);
            availableSlots["2025 - 11 - 09"] = new List<string>(slots);             availableSlots["2025 - 11 - 10"] = new List<string>(slots);
            availableSlots["2025 - 11 - 11"] = new List<string>(slots);             availableSlots["2025 - 11 - 12"] = new List<string>(slots);
            availableSlots["2025 - 11 - 13"] = new List<string>(slots);             availableSlots["2025 - 11 - 14"] = new List<string>(slots);
            availableSlots["2025 - 11 - 15"] = new List<string>(slots);             availableSlots["2025 - 11 - 16"] = new List<string>(slots);
            availableSlots["2025 - 11 - 17"] = new List<string>(slots);             availableSlots["2025 - 11 - 18"] = new List<string>(slots);
            availableSlots["2025 - 11 - 19"] = new List<string>(slots);             availableSlots["2025 - 11 - 20"] = new List<string>(slots);
            availableSlots["2025 - 11 - 21"] = new List<string>(slots);             availableSlots["2025 - 11 - 22"] = new List<string>(slots);
            availableSlots["2025 - 11 - 23"] = new List<string>(slots);             availableSlots["2025 - 11 - 24"] = new List<string>(slots);
            availableSlots["2025 - 11 - 25"] = new List<string>(slots);             availableSlots["2025 - 11 - 26"] = new List<string>(slots);
            availableSlots["2025 - 11 - 27"] = new List<string>(slots);             availableSlots["2025 - 11 - 28"] = new List<string>(slots);
            availableSlots["2025 - 11 - 29"] = new List<string>(slots);             availableSlots["2025 - 11 - 30"] = new List<string>(slots);

            List<string> procedure = new List<string>
            {
               "Dental Cleaning",
               "Dental Check Up",
               "Dental Braces",
               "Tooth filling",
               "Dental flouride"
            };
            cbProcedures.DataSource = procedure;
       
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string SelectedDate = dateTimePicker1.Value.ToString("yyyy - MM - dd");
            cbTime.Items.Clear();

            if (availableSlots.ContainsKey(SelectedDate))
            {
                foreach (string time in (List<string>)availableSlots[SelectedDate])
                {
                    cbTime.Items.Add(time);
                    
                }
            }
            else
            {
                MessageBox.Show("No available time slots for this date, please pick another date. Thank You");
            }
          
        }

        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bReserveAppoint_Click(object sender, EventArgs e)
        {

            string date = dateTimePicker1.Value.ToString("yyyy - MM - dd");
            string time = cbTime.SelectedItem.ToString();
            string lastName = tbLastNaneAppoint.Text;
            string firstName = tbFirstNameAppoint.Text;
            string contactNum = tbContactNoAppoint.Text;
            string email = tbEmailAppoint.Text;
            string DocName = tbDoctorNameAppoint.Text;
            string Procedure = cbProcedures.SelectedItem.ToString();
            string gender = bCircleMaleGenderAppoint.Checked ? "Male" :
                            bCircleFemaleGenderAppoint.Checked ? "Female" :
                            "None";
            if (lastName == null)
            {
                MessageBox.Show("Please enter patient last name. ");
            }
            if (firstName == null)
            {
                MessageBox.Show("Please enter patient first name. ");
            }
            if (contactNum == null)
            {
                MessageBox.Show("Please enter patient contact number. ");
            }
            if (email == null)
            {
                MessageBox.Show("Please enter patient Email. ");
            }
            if (DocName == null)
            {
                MessageBox.Show("Please enter the name of the patients doctor. ");
            }
            if (Procedure == null)
            {
                MessageBox.Show("Please select the procedure for the patient. ");
            }
            if (time == null)
            {
                MessageBox.Show("Select a Time for your appointment. ");
                return;
            }
            availableSlots[date].Remove(time);

            cbTime.Items.Remove(time);

            appointmentRecords.Add(new Record
            {
                LastName = lastName,
                FirstName = firstName,
                ContactNumber = contactNum,
                Email = email,
                Gender = gender,
                DoctorName = DocName,
                Procedures = Procedure,
                Date = date,
                Time = time,
                PaymentStatus = TotalPrice.Tag?.ToString() ?? "N/A"
                // here din para mag ka function ung sa data grid chinecheck kung fp orr dp ba nigga
            });
            
            MessageBox.Show("Appointment has been scheduled. See you!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbLastNaneAppoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbFirstNameAppoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void bCircleMaleGenderAppoint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void bCircleFemaleGenderAppoint_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tbContactNoAppoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbEmailAppoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbDoctorNameAppoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbProcedures_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LastName_TextChanged(object sender, EventArgs e)
        {

        }
       
        private string GetControlText(string controlName)
        {
            var matches = this.Controls.Find(controlName, true);
            if (matches != null && matches.Length > 0)
                return matches[0].Text ?? string.Empty;
            return string.Empty;
        }

       
        private void SetControlText(string controlName, string value)
        {
            var matches = this.Controls.Find(controlName, true);
            if (matches != null && matches.Length > 0)
                matches[0].Text = value ?? string.Empty;
        }

        private void SearchPatient_Click(object sender, EventArgs e)
        {
            
            string firstNameSearch = GetControlText("LastName");     
            string lastNameSearch = GetControlText("TextLabel1");   

            
            Record found = appointmentRecords.FirstOrDefault(r =>
                r.FirstName.Equals(firstNameSearch, StringComparison.OrdinalIgnoreCase) &&
                r.LastName.Equals(lastNameSearch, StringComparison.OrdinalIgnoreCase)
            );

            
            if (found == null)
            {
                found = appointmentRecords.FirstOrDefault(r =>
                    r.FirstName.Equals(firstNameSearch, StringComparison.OrdinalIgnoreCase) ||
                    r.LastName.Equals(lastNameSearch, StringComparison.OrdinalIgnoreCase)
                );
            }

            if (found == null)
            {
                MessageBox.Show("Patient not found. Check the spelling or pick from the appointment list.");
                return;
            }

            
            SetControlText("DoctorAssigned", found.DoctorName);
            SetControlText("ProcedureAssigned", found.Procedures);

            
            if (procedurePrices.TryGetValue(found.Procedures.Trim(), out decimal price))
                SetControlText("OrigPrice", price.ToString("F2"));
            else
                SetControlText("OrigPrice", "0.00");

            MessageBox.Show($"Patient found: {found.FirstName} {found.LastName}");
        }


        private void OrigPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void DoctorAssigned_Click(object sender, EventArgs e)
        {

        }

        private void ProcedureAssigned_Click(object sender, EventArgs e)
        {
            string proc = GetControlValue("ProcedureAssigned").Trim();

            if (procedurePrices.TryGetValue(proc, out decimal price))
            {
                SetControlValue("OrigPrice", price.ToString("F2"));
            }
        }

        private void Discount_TextChanged(object sender, EventArgs e)
        {
            // wag nyo galawin to pang pa estitik lng sa dsicount box manigga
            TextBox discountBox = sender as TextBox;
            if (discountBox == null) return;

            string text = discountBox.Text;

         
            text = text.Replace("%", "").Trim();

            
            if (decimal.TryParse(text, out decimal number))
            {
                discountBox.Text = number.ToString() + "%";
                discountBox.SelectionStart = discountBox.Text.Length - 1; 
            }
        }


        private void TotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void FullPayment_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Downpayment_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CalculatePrice_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(GetControlValue("OrigPrice"), out decimal originalPrice))
            {
                MessageBox.Show("Enter a valid Original Price.");
                return;
            }
            decimal discount = 0;
            string discountText = GetControlValue("Discount").Trim().Replace("%", "");
            decimal.TryParse(discountText, out discount);


            decimal discountedPrice = originalPrice - (originalPrice * (discount / 100));

            bool fullPay = ((RadioButton)this.Controls.Find("FullPayment", true)[0]).Checked;
            bool downPay = ((RadioButton)this.Controls.Find("Downpayment", true)[0]).Checked;



            string paymentStatus = "";

            if (fullPay)
            {
                paymentStatus = "FP";
            }
            else if (downPay)
            {
                paymentStatus = "DP";
                discountedPrice /= 2;
            }

           
            SetControlValue("TotalPrice", discountedPrice.ToString("F2"));

            TotalPrice.Tag = paymentStatus;
            UpdateSelectedPatientPaymentStatus();

        }



        private void PrintPrice_Click(object sender, EventArgs e)
        {
            string receipt =
                "----- BILLING RECEIPT -----\n" +
                $"Patient: {GetControlValue("textLabel1")} {GetControlValue("LastName")}\n" +
                $"Doctor: {GetControlValue("DoctorAssigned")}\n" +
                $"Procedure: {GetControlValue("ProcedureAssigned")}\n" +
                $"Original Price: {GetControlValue("OrigPrice")}\n" +
                $"Discount: {GetControlValue("Discount")}%\n" +
                $"Total Due: {GetControlValue("TotalPrice")}\n" +
                "----------------------------";
            // ai lng to nigga para maganda nman tignan ung resibo wtf
            MessageBox.Show(receipt, "Billing Receipt");
        }

        Dictionary<string, decimal> procedurePrices = new Dictionary<string, decimal>()
{
    { "Dental Cleaning", 800 },
    { "Dental Check Up", 500 },
    { "Dental Braces", 1500000000000 },
    { "Tooth filling", 1200 },
    { "Dental flouride", 600 }
};// palitan nyo kung gusto nyo sorry kung pang mayaman

        private string GetControlValue(string controlName)
        {
            Control[] found = this.Controls.Find(controlName, true);
            if (found.Length > 0)
                return found[0].Text;
            return "";
        }
        // nanigga
     
        private void SetControlValue(string controlName, string value)
        {
            Control[] found = this.Controls.Find(controlName, true);
            if (found.Length > 0)
                found[0].Text = value;
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
