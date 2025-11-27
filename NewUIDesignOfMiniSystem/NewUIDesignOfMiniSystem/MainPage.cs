using NewUIDesignOfMiniSystem.forrmss;
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

namespace NewUIDesignOfMiniSystem
{
    public partial class MainPage : Form
    {

        private BindingList<Record> AppoinmentRecords;
        private BindingList<Record> DeletedRecords;
        private Billings billingsForm;
        private Form currentChildForm;

        public MainPage(BindingList<Record> appointmentRecords, Billings billingsForm, BindingList<Record> deletedRecords)
        {
            InitializeComponent();
            AppoinmentRecords = appointmentRecords;
            DeletedRecords = deletedRecords;
            this.billingsForm = billingsForm;

            typeof(Panel).InvokeMember("DoubleBuffered",
        System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
        null, this, new object[] { true });
        }

        
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
             this.SuspendLayout();  

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            this.Controls.Add(childForm); 
            
            this.Tag = childForm;
            childForm.BringToFront();
            this.ResumeLayout();
            childForm.Show();
        }

        private void buttonHomepage_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm = null;
            }
            

        }

        private void buttonAppointment_Click(object sender, EventArgs e)
        {
            OpenChildForm(new forrmss.Appointment(AppoinmentRecords));

        }

        private void buttonRecords_Click(object sender, EventArgs e)
        {
            OpenChildForm(new forrmss.Records(AppoinmentRecords,DeletedRecords));
        }

        private void buttonBillings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new forrmss.Billings());
        }
    }
}
