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
    public partial class Records : Form
    {
        
        private BindingList<Record> AppointmentRecords;
        private Dictionary<string, decimal> procedurePrices;
        private BindingList<Record> DeletedRecords;

        private Archives archives;
        private DeactiveSection deletion;
        public Records(BindingList<Record> appointmentRecords, BindingList<Record> deletedRecords, Dictionary<string, decimal> procedurePrices)
        {
            InitializeComponent();
            AppointmentRecords = appointmentRecords;
            DeletedRecords = deletedRecords;
            this.procedurePrices = procedurePrices;
        }
        private void Records_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = AppointmentRecords;

        }


        public void RefreshRecordsGrid()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke(new Action(RefreshRecordsGrid));
                return;
            }

            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (deletion == null || deletion.IsDisposed)
            {
                deletion = new DeactiveSection(AppointmentRecords, DeletedRecords,procedurePrices);
                deletion.Show();
            }
            else
            {
                deletion.Close();
                deletion = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (archives == null || archives.IsDisposed)
            {
                archives = new Archives(DeletedRecords);
                archives.Show();
            }
            else
            {
                archives.Close();
                archives = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
