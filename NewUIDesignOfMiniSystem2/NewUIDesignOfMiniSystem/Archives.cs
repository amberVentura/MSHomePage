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
    public partial class Archives : Form
    {
        private BindingList<Record> DeletedRecords;
        public Archives(BindingList<Record> deletedRecords)
        {
            InitializeComponent();
            DeletedRecords = deletedRecords;
        }

        private void Archives_Load(object sender, EventArgs e)
        {
            dgvRemoved.Refresh();
            dgvRemoved.DataSource = DeletedRecords;
        }
    }
}
