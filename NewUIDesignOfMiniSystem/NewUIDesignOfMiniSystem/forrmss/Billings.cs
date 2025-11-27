using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NewUIDesignOfMiniSystem.forrmss.Appointment;

namespace NewUIDesignOfMiniSystem.forrmss
{
    
    public partial class Billings : Form
    {
        private Records recordsForm;

        BindingList<Record> appointmentRecords = new BindingList<Record>();
        public Billings()
        {
            InitializeComponent();
            


        }

        private void UpdateSelectedPatientPaymentStatus(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return;

            string paymentStatus = TotalPrice?.Tag?.ToString() ?? "N/A";

            var record = appointmentRecords.FirstOrDefault(r =>
                r.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                r.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));

            if (record != null)
            {
                record.PaymentStatus = paymentStatus;
                recordsForm?.RefreshRecordsGrid();
            }
        }
        private void SetControlText(string controlName, string value)
        {
            var matches = this.Controls.Find(controlName, true);
            if (matches != null && matches.Length > 0)
                matches[0].Text = value ?? string.Empty;
        }
        private string GetControlText(string controlName)
        {
            var matches = this.Controls.Find(controlName, true);
            if (matches != null && matches.Length > 0)
                return matches[0].Text ?? string.Empty;
            return string.Empty;
        }
        
        private void SearchPatient_Click(object sender, EventArgs e)
        {
            string firstNameSearch = GetControlText("firstName");
            string lastNameSearch = GetControlText("LastName");


            Record found = appointmentRecords.FirstOrDefault(r =>
               r.FirstName?.Trim().Equals(firstNameSearch, StringComparison.OrdinalIgnoreCase) == true &&
               r.LastName?.Trim().Equals(lastNameSearch, StringComparison.OrdinalIgnoreCase) == true
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
        private void ProcedureAssigned_Click(object sender, EventArgs e)
        {
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

        

        private void Discount_TextChanged(object sender, EventArgs e)
        {
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
                paymentStatus = "FP";
            else if (downPay)
            {
                paymentStatus = "DP";
                discountedPrice /= 2;
            }

            SetControlValue("TotalPrice", discountedPrice.ToString("F2"));
            TotalPrice.Tag = paymentStatus;


            string billingFirstName = GetControlValue("textBox1");
            string billingLastName = GetControlValue("LastName");
            UpdateSelectedPatientPaymentStatus(billingFirstName, billingLastName);
        }
        private void PrintPrice_Click_1(object sender, EventArgs e)
        {
            try
            {
                string firstName = GetControlValue("textBox1");
                string lastName = GetControlValue("LastName");
                string doctor = GetControlValue("DoctorAssigned");
                string procedure = GetControlValue("ProcedureAssigned");
                string origPrice = GetControlValue("OrigPrice");
                string discount = GetControlValue("Discount");
                string total = GetControlValue("TotalPrice");
                string billingFirstName = GetControlValue("textBox1");
                string billingLastName = GetControlValue("LastName");
                UpdateSelectedPatientPaymentStatus(billingFirstName, billingLastName);

                if (string.IsNullOrWhiteSpace(firstName) ||
                    string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(procedure) ||
                    string.IsNullOrWhiteSpace(origPrice))
                {
                    MessageBox.Show("Missing billing information. Please search patient and calculate first.");
                    return;
                }

                string receipt =
                    "----- BILLING RECEIPT -----\n" +
                    $"Patient: {firstName} {lastName}\n" +
                    $"Doctor: {doctor}\n" +
                    $"Procedure: {procedure}\n" +
                    $"Original Price: {origPrice}\n" +
                    $"Discount: {discount}%\n" +
                    $"Total Due: {total}\n" +
                    "----------------------------";

                MessageBox.Show(receipt, "Billing Receipt");

                ResetBillingFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while printing: " + ex.Message);
            }
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
            if (found.Length == 0) return "";

            Control ctrl = found[0];

            if (ctrl is TextBox tb) return tb.Text;
            if (ctrl is Label lbl) return lbl.Text;
            if (ctrl is RadioButton rb) return rb.Checked ? "Checked" : "";
            return ctrl.Text;
        }

        private void SetControlValue(string controlName, string value)
        {
            Control[] found = this.Controls.Find(controlName, true);
            if (found.Length == 0) return;

            Control ctrl = found[0];

            if (ctrl is TextBox tb) tb.Text = value;
            else if (ctrl is Label lbl) lbl.Text = value;
            else if (ctrl is RadioButton rb) rb.Checked = value == "Checked";
            else ctrl.Text = value;
        }
        private void ResetBillingFields()
        {

            string[] textBoxes = { "textBox1", "LastName", "DoctorAssigned", "ProcedureAssigned", "OrigPrice", "Discount", "TotalPrice" };
            foreach (string name in textBoxes)
            {
                SetControlValue(name, "");
            }

            string[] radioButtons = { "FullPayment", "Downpayment" };
            foreach (string name in radioButtons)
            {
                SetControlValue(name, "");
            }


            TotalPrice.Tag = null;
        }
        public string PaymentStatusValue
        {
            get => TotalPrice.Tag?.ToString() ?? "N/A";
        
        }
        
        

        
    }
}
