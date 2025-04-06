using FinalProject.DB;
using iText.Kernel.Colors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace FinalProject
{
    public partial class Admin : Form
    {
        private Button lastClickedButton = null;

        public Admin()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            ReportControl home = new ReportControl();
            LoadControl(home);
            ChangeButtonColor(btnHome);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ReportControl home2 = new ReportControl();
            LoadControl(home2);
            ChangeButtonColor(btnHome);
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            PatientControl patient = new PatientControl();
            LoadControl(patient);
            ChangeButtonColor(btnPatients);
        }

        private void btnDT_Click(object sender, EventArgs e)
        {
            DiagnosisTreatmentControl diagnosisTreatment = new DiagnosisTreatmentControl();
            LoadControl(diagnosisTreatment);
            ChangeButtonColor(btnDT);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("d");
        }
        private void LoadControl(UserControl control)
        {
            mainPanel.Controls.Clear(); 
            control.Dock = DockStyle.Fill; 
            mainPanel.Controls.Add(control); 
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            DoctorControl doctorControl = new DoctorControl();
            LoadControl(doctorControl);
            ChangeButtonColor(btnDoctor);
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            ServiceControl service = new ServiceControl();
            LoadControl(service);
            ChangeButtonColor(btnService);
        }

        private void btnMedicine_Click(object sender, EventArgs e)
        {
            MedicineControl medicine = new MedicineControl();
            LoadControl(medicine);
            ChangeButtonColor(btnMedicine);
        }

        private void ChangeButtonColor(Button clickedButton)
        {
            if (lastClickedButton != null)
            {
                lastClickedButton.BackColor = Color.Red; 
                lastClickedButton.ForeColor = Color.White; 
            }
            clickedButton.BackColor = Color.Tomato; 
            clickedButton.ForeColor = Color.White;
            lastClickedButton = clickedButton;
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            BillControl bill = new BillControl();
            LoadControl(bill);
            ChangeButtonColor(btnBill);
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            PrescriptionControl prescription = new PrescriptionControl();
            LoadControl(prescription);
            ChangeButtonColor(btnPre);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            HomeControl home = new HomeControl();
            LoadControl(home);
            ChangeButtonColor(btnReport);
        }
    }
}
