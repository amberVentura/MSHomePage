using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace NewUIDesignOfMiniSystem.forrmss
{
    public partial class Appointment : Form
    {
        private Billings billingsForm;

        // Stores future time slots for ALL dates
        private Dictionary<string, List<string>> availableSlots = new Dictionary<string, List<string>>();

        // Shared appointment records across forms
        private BindingList<Record> AppoinmentRecords;

        // Record
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
        }

        // Constructor
        public Appointment(BindingList<Record> appoinmentRecords, Billings billings)
        {
            InitializeComponent();
            AppoinmentRecords = appoinmentRecords;
            billingsForm = billings;
        }

        // FORM LOAD
        private void Appointment_Load(object sender, EventArgs e)
        {
            cbTime.Text = "--Select Time--";

            // Default available time slots per day
            List<string> slots = new List<string>
            {
                "10:00 AM",
                "11:30 AM",
                "1:00 PM",
                "2:30 PM",
                "4:00 PM",
                "5:30 PM",
                "7:00 PM"
            };

            
            DateTime start = DateTime.Today.AddDays(1);
            DateTime end = start.AddYears(2);

            for (DateTime d = start; d <= end; d = d.AddDays(1))
            {
                string key = d.ToString("yyyy - MM - dd");
                availableSlots[key] = new List<string>(slots);
            }
           
            // Procedures
            List<string> procedure = new List<string>
            {
               "Dental Cleaning",
               "Dental Check Up",
               "Dental Braces",
               "Tooth filling",
               "Dental fluoride"
            };
            cbProcedures.DataSource = procedure;
            cbProcedures.SelectedIndex = -1;           
            cbProcedures.Text = "--Select Procedure--"; 

            // Doctors
            List<string> doctorNames = new List<string>
            {
               
                "Dr. Terick Malabon",
                "Dr. GianIscaliv Isorena"
            };
            cbDoctorNameAppoint.DataSource = doctorNames;
            cbDoctorNameAppoint.SelectedIndex = -1;          
            cbDoctorNameAppoint.Text = "--Select Doctor--"; 

        }

        // BOOK APPOINTMENT BUTTON
        private void bReserveAppoint_Click(object sender, EventArgs e)
        {
            try
            {
                // TIME REQUIRED
                if (cbTime.SelectedItem == null)
                {
                    MessageBox.Show("Please select a time slot for your appointment.");
                    return;
                }

                // DATE VALIDATION (no same-day or past booking)
                DateTime selectedDate = dateTimePicker1.Value.Date;
                if (selectedDate <= DateTime.Today)
                {
                    MessageBox.Show("You cannot book an appointment for today or any previous date.");
                    return;
                }

                string date = selectedDate.ToString("yyyy - MM - dd");
                string time = cbTime.SelectedItem.ToString();
                string lastName = tbLastNaneAppoint.Text.Trim();
                string firstName = tbFirstNameAppoint.Text.Trim();
                string contactNum = tbContactNoAppoint.Text.Trim();
                string email = tbEmailAppoint.Text.Trim();
                string DocName = cbDoctorNameAppoint.Text;
                string Procedure = cbProcedures.SelectedItem.ToString();

                string gender = bCircleMaleGenderAppoint.Checked ? "Male" :
                                bCircleFemaleGenderAppoint.Checked ? "Female" : "None";

                
                if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName) ||
                    string.IsNullOrEmpty(contactNum) || string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(DocName) || string.IsNullOrEmpty(Procedure) ||
                    string.IsNullOrEmpty(gender))
                {
                    MessageBox.Show("Please fill up all the required information.");
                    return;
                }

                
                if (!contactNum.StartsWith("09") || contactNum.Length != 11 || !contactNum.All(char.IsDigit))
                {
                    MessageBox.Show("Enter a valid 11-digit contact number starting with 09.");
                    return;
                }

                
                if (!email.Contains("@") ||
                    (!email.EndsWith(".com") && !email.EndsWith(".ph") && !email.EndsWith(".net")))
                {
                    MessageBox.Show("Please enter a valid email address.");
                    return;
                }

                
                bool duplicateFound = AppoinmentRecords.Any(r =>
                    r.Date == date &&
                    r.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                    r.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                    r.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
                    (r.Email.Equals(email, StringComparison.OrdinalIgnoreCase) ||
                     r.ContactNumber.Equals(contactNum))
                );

                if (duplicateFound)
                {
                    MessageBox.Show("This person already has an appointment at this date.");
                    return;
                }

                
                availableSlots[date].Remove(time);
                cbTime.Items.Remove(time);

                
                AppoinmentRecords.Add(new Record
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
                    PaymentStatus = billingsForm.PaymentStatusValue
                });

                MessageBox.Show("Appointment has been scheduled. See you!");
            }
            catch
            {
                MessageBox.Show("An unexpected error occurred while booking the appointment.");
            }
        }

        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string selectedDate = dateTimePicker1.Value.ToString("yyyy - MM - dd");
            cbTime.Items.Clear();

            if (availableSlots.ContainsKey(selectedDate))
            {
                foreach (string time in availableSlots[selectedDate])
                {
                    cbTime.Items.Add(time);
                }
                
            }
            
            else
            {
                MessageBox.Show("No available time slots for this date. Please pick another date.");
                return;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbEmailAppoint_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbFirstNameAppoint_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

