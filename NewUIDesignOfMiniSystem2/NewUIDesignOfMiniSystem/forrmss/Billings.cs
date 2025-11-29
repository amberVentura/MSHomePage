using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static NewUIDesignOfMiniSystem.forrmss.Appointment;

namespace NewUIDesignOfMiniSystem.forrmss
{
    public partial class Billings : Form
    {
        private Records recordsForm;
        private BindingList<Record> appointmentRecords;
        
        private Record currentRecord;


        Dictionary<string, decimal> procedurePrices = new Dictionary<string, decimal>()
        {
            { "Dental Cleaning", 800 },
            { "Dental Check Up", 500 },
            { "Dental Braces", 50000 },
            { "Tooth filling", 1200 },
            { "Dental fluoride", 600 }
        };

        public Billings(Records parentForm, BindingList<Record> records)
        {
            InitializeComponent();
            this.recordsForm = parentForm;
            this.appointmentRecords = records;

            
            Discount.KeyPress += Discount_KeyPress;
            Discount.Leave += Discount_Leave;

                        
        }
        private Record FindRecordByEmail(string emailInput)
        {
            if (string.IsNullOrWhiteSpace(emailInput))
                return null;

            emailInput = emailInput.Trim().ToLower();

            // Try exact match first
            var exact = appointmentRecords.FirstOrDefault(r =>
                string.Equals(r.Email?.Trim(), emailInput, StringComparison.OrdinalIgnoreCase));
            if (exact != null) return exact;

            // Fallback: contains (partial)
            return appointmentRecords.FirstOrDefault(r =>
                r.Email?.Trim().ToLower().Contains(emailInput) == true);
        }


        private void Discount_TextChanged(object sender, EventArgs e)
        {
            
        }

        
        private void Discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Allow digits, one decimal point, and backspace
            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                return;
            }

            // Prevent multiple decimals
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        
        // LIMIT DISCOUNT TO 100%
        
        private void Discount_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(Discount.Text.Replace("%", ""), out decimal value))
            {
                if (value > 100)
                {
                    MessageBox.Show("Discount cannot exceed 100%.", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Discount.Text = "100%";
                }
                else
                {
                    Discount.Text = value.ToString("F2") + "%"; // format nicely
                }
            }
            else
            {
                Discount.Text = "0%";
            }
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
            string emailSearch = "";

            Control[] emailControls = this.Controls.Find("Email", true);
            if (emailControls.Length > 0 && emailControls[0] is ComboBox cb)
            {
                if (cb.SelectedItem != null)
                    emailSearch = cb.SelectedItem.ToString();   
                else
                    emailSearch = cb.Text; 
            }
            else
            {
                emailSearch = GetControlText("Email");
            }


            if (string.IsNullOrWhiteSpace(firstNameSearch) &&
                string.IsNullOrWhiteSpace(lastNameSearch) &&
                string.IsNullOrWhiteSpace(emailSearch))
            {
                MessageBox.Show("Please enter a first name, last name, or email.");
                return;
            }

            Record found = null;  // <--- THIS MUST BE HERE

            // ✔ FIRST: SEARCH BY EMAIL
            if (!string.IsNullOrWhiteSpace(emailSearch))
            {
                Record emailMatch = FindRecordByEmail(emailSearch);

                if (emailMatch != null)
                {
                    found = emailMatch;
                }
            }

            // ✔ ONLY search by name if no email match
            if (found == null)
            {
                if (!string.IsNullOrWhiteSpace(firstNameSearch) &&
                    !string.IsNullOrWhiteSpace(lastNameSearch))
                {
                    found = appointmentRecords.FirstOrDefault(r =>
                        r.FirstName?.Trim().Equals(firstNameSearch, StringComparison.OrdinalIgnoreCase) == true &&
                        r.LastName?.Trim().Equals(lastNameSearch, StringComparison.OrdinalIgnoreCase) == true &&
                        r.Email?.Trim().Equals(emailSearch, StringComparison.OrdinalIgnoreCase) == true);
                }
            }

            if (found == null)
            {
                found = appointmentRecords.FirstOrDefault(r =>
                    (!string.IsNullOrWhiteSpace(firstNameSearch) &&
                     r.FirstName?.Trim().Equals(firstNameSearch, StringComparison.OrdinalIgnoreCase) == true)
                    ||
                    (!string.IsNullOrWhiteSpace(lastNameSearch) &&
                     r.LastName?.Trim().Equals(lastNameSearch, StringComparison.OrdinalIgnoreCase) == true)
                     ||
                     (!string.IsNullOrWhiteSpace(emailSearch) &&
                     r.Email?.Trim().Equals(emailSearch, StringComparison.OrdinalIgnoreCase) == true)
                );
            }

            if (found == null)
            {
                MessageBox.Show("Patient not found.");
                return;
            }

            // Continue your code here…
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

            string text = discountBox.Text.Replace("%", "").Trim();

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

            string discountText = GetControlValue("Discount").Trim().Replace("%", "");
            decimal discount = 0;
            decimal.TryParse(discountText, out discount);

            if (discount > 100)
                discount = 100;

            
            decimal discountedPrice = originalPrice - (originalPrice * (discount / 100));

            
            Control[] fullPayControls = this.Controls.Find("FullPayment", true);
            Control[] downPayControls = this.Controls.Find("Downpayment", true);

            bool fullPay = fullPayControls.Length > 0 && ((RadioButton)fullPayControls[0]).Checked;
            bool downPay = downPayControls.Length > 0 && ((RadioButton)downPayControls[0]).Checked;

            string paymentStatus = "";

            if (fullPay)
            {
                paymentStatus = "FP"; // Full Payment
            }
            else if (downPay)
            {
                paymentStatus = "RF"; // Down Payment
                discountedPrice /= 2; 
            }
            else
            {
                
                paymentStatus = "N/A";
            }

            SetControlValue("TotalPrice", discountedPrice.ToString("F2"));

            
            if (this.Controls.Find("TotalPrice", true).FirstOrDefault() is Control totalPriceControl)
            {
                totalPriceControl.Tag = paymentStatus;
            }

            UpdateSelectedPatientPaymentStatus(GetControlValue("firstName"), GetControlValue("LastName"));
        }

        private void PrintPrice_Click_1(object sender, EventArgs e)
        {
            try
            {
                string firstName = GetControlValue("firstName");
                string lastName = GetControlValue("LastName");
                string doctor = GetControlValue("DoctorAssigned");
                string procedure = GetControlValue("ProcedureAssigned");
                string origPrice = GetControlValue("OrigPrice");
                string discount = GetControlValue("Discount");
                string total = GetControlValue("TotalPrice");

                 string selectedEmail = Email.Text.Trim();
               

                var recordToUpdate = appointmentRecords.FirstOrDefault(r =>
                    r.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                    r.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                    r.Email.Equals(selectedEmail, StringComparison.OrdinalIgnoreCase)
                );

                if (recordToUpdate == null)
                {
                    MessageBox.Show("Record not found!");
                    return;
                }

                // Get the computed payment status from TAG
                string paymentStatus = PaymentStatusValue;
                recordToUpdate.PaymentStatus = paymentStatus;

                MessageBox.Show("Payment status updated successfully!");

                // PRINT RECEIPT
                string receipt =
                    "----- BILLING RECEIPT -----\n" +
                    $"Date: {DateTime.Now.ToShortDateString()}\n" +
                    $"Patient: {firstName} {lastName}\n" +
                    $"Doctor: {doctor}\n" +
                    $"Procedure: {procedure}\n" +
                    $"Original Price: {origPrice}\n" +
                    $"Discount: {discount}\n" +
                    $"Total Due: {total}\n" +
                    $"Payment Status: {paymentStatus}\n" +
                    "----------------------------";

                MessageBox.Show(receipt, "Billing Receipt");

                ResetBillingFields();
                recordsForm?.RefreshRecordsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while printing: " + ex.Message);
            }
        }

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

        // Helper method to set text/value to controls
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
            string[] textBoxes = { "firstName", "LastName", "DoctorAssigned", "ProcedureAssigned", "OrigPrice", "Discount", "TotalPrice" };
            foreach (string name in textBoxes)
            {
                SetControlValue(name, "");
            }

            string[] radioButtons = { "FullPayment", "Downpayment" };
            foreach (string name in radioButtons)
            {
                SetControlValue(name, "");
            }

            // Manually reset the Tag property
            if (this.Controls.Find("TotalPrice", true).FirstOrDefault() is Control totalPriceControl)
            {
                totalPriceControl.Tag = null;
            }
        }

        // Public property to expose the payment status from the TotalPrice Tag
        public string PaymentStatusValue
        {
            get => this.Controls.Find("TotalPrice", true).FirstOrDefault()?.Tag?.ToString() ?? "N/A";
        }

        private void FullPayment_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Billings_Load(object sender, EventArgs e)
        {

        }

        private void firstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            string selectedEmail = Email.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedEmail))
                return;

            // Find EXACT record by this email
            var found = appointmentRecords.FirstOrDefault(r =>
                r.Email?.Trim().Equals(selectedEmail, StringComparison.OrdinalIgnoreCase) == true);

            if (found == null)
                return;

            // Update UI
            SetControlText("DoctorAssigned", found.DoctorName);
            SetControlText("ProcedureAssigned", found.Procedures);

            if (procedurePrices.TryGetValue(found.Procedures.Trim(), out decimal price))
                SetControlText("OrigPrice", price.ToString("F2"));
            else
                SetControlText("OrigPrice", "0.00");
        }

            
        

        private void LastName_TextChanged(object sender, EventArgs e)
        {
            string firstN = firstName.Text.Trim();
            string lastN = LastName.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstN) || string.IsNullOrWhiteSpace(lastN))
            {
                Email.Items.Clear();
                return;
            }

            var matches = appointmentRecords
                .Where(r =>
                    r.FirstName?.Trim().Equals(firstN, StringComparison.OrdinalIgnoreCase) == true &&
                    r.LastName?.Trim().Equals(lastN, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();

            Email.Items.Clear();

            if (matches.Count == 0)
                return;

            
            foreach (var r in matches)
                Email.Items.Add(r.Email);

            Email.SelectedIndex = 0; 
        }

        private void Downpayment_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Email_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
