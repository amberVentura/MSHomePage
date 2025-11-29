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

        BindingList<Record> appointmentRecords = new BindingList<Record>();
        private BindingList<Record> DeletedRecords;
        private Billings billingsForm;
        private Dictionary<string, decimal> procedurePrices;
        private Records recordsForm;
        private Form currentChildForm;

        public MainPage( Billings billingsForm, BindingList<Record> deletedRecords, Records parentForm, Dictionary<string, decimal> procedurePrices)
        {
            InitializeComponent();
            
            DeletedRecords = deletedRecords;
            this.billingsForm = billingsForm;
            this.recordsForm = parentForm;
            this.procedurePrices = procedurePrices;

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
            OpenChildForm(new forrmss.Appointment(appointmentRecords,billingsForm));

        }

        private void buttonRecords_Click(object sender, EventArgs e)
        {
            OpenChildForm(new forrmss.Records(appointmentRecords, DeletedRecords, procedurePrices));
        }

        private void buttonBillings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new forrmss.Billings(recordsForm,appointmentRecords));
        }

        private void MainPage_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
