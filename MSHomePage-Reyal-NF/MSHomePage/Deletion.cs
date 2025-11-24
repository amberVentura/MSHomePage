using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MSHomePage.HomePage;

namespace MSHomePage
{
    public partial class Deletion : Form
    {

        private BindingList<Record> AppointmentRecords;
        private BindingList<Record> DeletedRecords;

        public Deletion(BindingList<Record> appointmentRecords, BindingList<Record> deletedRecords)
        {
            InitializeComponent();

            AppointmentRecords = appointmentRecords;
            DeletedRecords = deletedRecords;
        }

        private void bRSearch_Click(object sender, EventArgs e)
        {
            string RLastName = tbRLastName.Text;
            string RFirstName = tbRFirstName.Text;

            Record found = AppointmentRecords.FirstOrDefault(r =>
                r.FirstName.Equals(RFirstName, StringComparison.OrdinalIgnoreCase) &&
                r.LastName.Equals(RLastName, StringComparison.OrdinalIgnoreCase)
            );

            if (found == null)
            {
                found = AppointmentRecords.FirstOrDefault(r =>
                    r.FirstName.Equals(RFirstName, StringComparison.OrdinalIgnoreCase) ||
                    r.LastName.Equals(RLastName, StringComparison.OrdinalIgnoreCase)
                );
            }

            if (found == null)
            {
                MessageBox.Show("Patient not found. Check the spelling or pick from the appointment list.");
                return;
            }

            tbRProcedure.Text = found.Procedures;
            tbRDoctor.Text = found.DoctorName;
            tbRDate.Text = found.Date;
            tbRTime.Text = found.Time;
            tbRPaymentStat.Text = found.PaymentStatus;
        }

        private void bRDelete_Click(object sender, EventArgs e)
        {
            string RLastName = tbRLastName.Text;
            string RFirstName = tbRFirstName.Text;

            Record found = AppointmentRecords.FirstOrDefault(r =>
                r.FirstName.Equals(RFirstName, StringComparison.OrdinalIgnoreCase) &&
                r.LastName.Equals(RLastName, StringComparison.OrdinalIgnoreCase));

            if (found == null)
            {
                MessageBox.Show("No patient selected. Search for a patient first.");
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                DeletedRecords.Add(found);
                AppointmentRecords.Remove(found);

                MessageBox.Show("Record deleted successfully.");
                found = null;

                tbRProcedure.Clear();
                tbRDoctor.Clear();
                tbRDate.Clear();
                tbRTime.Clear();
                tbRPaymentStat.Clear();
            }
        }
    }
}