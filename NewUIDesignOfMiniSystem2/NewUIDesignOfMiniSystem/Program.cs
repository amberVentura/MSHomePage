using NewUIDesignOfMiniSystem.forrmss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewUIDesignOfMiniSystem
{
    internal static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BindingList<Appointment.Record> appointmentRecords = new BindingList<Appointment.Record>();

            BindingList<Appointment.Record> deletedRecords = new BindingList<Appointment.Record>();

            Records recordsForm = new Records(appointmentRecords, deletedRecords);
            Billings billingsForm = new Billings(recordsForm, appointmentRecords);
            

            Application.Run(new Login(appointmentRecords,billingsForm,deletedRecords,recordsForm));
        }

    }
}
