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

        Dictionary<string, List<string>> availableSlots = new Dictionary<string, List<string>>();
        private BindingList<Record> AppoinmentRecords;

        public Appointment(BindingList<Record> appoinmentRecords, Billings billings)
        {
            InitializeComponent();
            AppoinmentRecords = appoinmentRecords;
            this.billingsForm = billings;
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
        private void Appointment_Load(object sender, EventArgs e)
        {
            cbTime.Text = "--Select Time--";

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
            availableSlots["2025 - 11 - 01"] = new List<string>(slots); availableSlots["2025 - 11 - 02"] = new List<string>(slots);
            availableSlots["2025 - 11 - 03"] = new List<string>(slots); availableSlots["2025 - 11 - 04"] = new List<string>(slots);
            availableSlots["2025 - 11 - 05"] = new List<string>(slots); availableSlots["2025 - 11 - 06"] = new List<string>(slots);
            availableSlots["2025 - 11 - 07"] = new List<string>(slots); availableSlots["2025 - 11 - 08"] = new List<string>(slots);
            availableSlots["2025 - 11 - 09"] = new List<string>(slots); availableSlots["2025 - 11 - 10"] = new List<string>(slots);
            availableSlots["2025 - 11 - 11"] = new List<string>(slots); availableSlots["2025 - 11 - 12"] = new List<string>(slots);
            availableSlots["2025 - 11 - 13"] = new List<string>(slots); availableSlots["2025 - 11 - 14"] = new List<string>(slots);
            availableSlots["2025 - 11 - 15"] = new List<string>(slots); availableSlots["2025 - 11 - 16"] = new List<string>(slots);
            availableSlots["2025 - 11 - 17"] = new List<string>(slots); availableSlots["2025 - 11 - 18"] = new List<string>(slots);
            availableSlots["2025 - 11 - 19"] = new List<string>(slots); availableSlots["2025 - 11 - 20"] = new List<string>(slots);
            availableSlots["2025 - 11 - 21"] = new List<string>(slots); availableSlots["2025 - 11 - 22"] = new List<string>(slots);
            availableSlots["2025 - 11 - 23"] = new List<string>(slots); availableSlots["2025 - 11 - 24"] = new List<string>(slots);
            availableSlots["2025 - 11 - 25"] = new List<string>(slots); availableSlots["2025 - 11 - 26"] = new List<string>(slots);
            availableSlots["2025 - 11 - 27"] = new List<string>(slots); availableSlots["2025 - 11 - 28"] = new List<string>(slots);
            availableSlots["2025 - 11 - 29"] = new List<string>(slots); availableSlots["2025 - 11 - 30"] = new List<string>(slots);


            List<string> procedure = new List<string>
            {
               "Dental Cleaning",
               "Dental Check Up",
               "Dental Braces",
               "Tooth filling",
               "Dental flouride"
            };

            cbProcedures.DataSource = procedure;

            List<string> doctorNames = new List<string>
            {
                "Dr. Juan Dela Cruz",
                "Dr. Maria Santos",
                "Dr. Pedro Reyes",
                "Dr. Ana Lopez",
                "Dr. GianIscaliv Isorena"
            };
            cbDoctorNameAppoint.DataSource = doctorNames;

        }

        private void bReserveAppoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTime.SelectedItem == null)
                {
                    MessageBox.Show("Please select a timne slot for your appointment");
                    return;
                }
                string date = dateTimePicker1.Value.ToString("yyyy - MM - dd");
                string time = cbTime.SelectedItem.ToString();
                string lastName = tbLastNaneAppoint.Text;
                string firstName = tbFirstNameAppoint.Text;
                string contactNum = tbContactNoAppoint.Text.Trim();
                string email = tbEmailAppoint.Text;
                string DocName = cbDoctorNameAppoint.Text;
                string Procedure = cbProcedures.SelectedItem.ToString();
                string gender = bCircleMaleGenderAppoint.Checked ? "Male" :
                                bCircleFemaleGenderAppoint.Checked ? "Female" :
                                "None";


                if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName)
                || string.IsNullOrEmpty(contactNum) || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(DocName) || string.IsNullOrEmpty(Procedure) || string.IsNullOrEmpty(gender))
                {
                    MessageBox.Show("Please fill up all the required info, thank you!!");

                }
                if (!contactNum.StartsWith("09") || contactNum.Length != 11 || !contactNum.All(char.IsDigit))
                {
                    MessageBox.Show("Enter a valid 11 digit contact number. Contact number must start with 09.");
                    return;
                }
                if(!email.Contains("@") || !email.Contains(".com") || email.Contains(" .ph"))
                {
                    MessageBox.Show("Please enter a valid email address.");
                    return;
                }
                

                availableSlots[date].Remove(time);

                cbTime.Items.Remove(time);


                if (!AppoinmentRecords.Any(r => r.LastName == lastName))
                {

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
                        // here din para mag ka function ung sa data grid chinecheck kung fp orr dp ba
                    });

                    MessageBox.Show("Appointment has been scheduled. See you!");
                }

                else
                {
                    MessageBox.Show("The client has already booked an appointment", "Error", MessageBoxButtons.OK);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("You have not filled up all the required info. Please fill up all the required information, thank youu!!");

            }

            catch (FormatException)
            {
                MessageBox.Show("Invalid format, please fill up the form again!");
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
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
    }
}
