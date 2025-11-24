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
    public partial class Archives : Form
    {
        public BindingList<Record> DeletedRecords = new BindingList<Record>();
        public Archives(BindingList<Record> deletedRecords)
        {
            InitializeComponent();

            dgvRemoved.DataSource = DeletedRecords;
        }


    }
}
