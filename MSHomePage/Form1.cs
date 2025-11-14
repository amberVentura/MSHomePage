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

namespace MSHomePage
{
    public partial class Form1 : Form
    {
       Dictionary<string, List <string>> availableSlots = new Dictionary<string, List <string>> ();
        Dictionary<string, List<string>> availableProc = new Dictionary<string, List<string>> ();
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
            public string DoctorName {  get; set; }
            public string Procedures {  get; set; }
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
    }
}
