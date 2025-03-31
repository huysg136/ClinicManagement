using FinalProject.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class HomeControl : UserControl
    {
        DbClinic db = new DbClinic();
        public HomeControl()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HomeControl_Load(object sender, EventArgs e)
        {
            lblPatient.Text = db.Patients.Count().ToString();
            lblDoctor.Text = db.Doctors.Count().ToString();
            lblMedicine.Text = db.Medicines.Count().ToString();
            lblPre.Text = db.Prescriptions.Count().ToString();
            lblService.Text = db.Services.Count().ToString();
            lblBill.Text = db.Bills.Count().ToString();
        }
    }
}
