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
    public partial class MedicalRecord : Form
    {
        private int patientID;
        public MedicalRecord(int patientID)
        {
            InitializeComponent();
            this.patientID = patientID;
        }

        private void MedicalRecord_Load(object sender, EventArgs e)
        {
            using (var context = new DbClinic())
            {
                // Lấy thông tin bệnh nhân theo ID
                var patient = context.Patients
                    .Where(p => p.Patient_ID == patientID)
                    .FirstOrDefault();

                if (patient != null)
                {
                    var medicalRecords = context.MedicalRecords
                        .Where(mr => mr.Patient_ID == patientID)
                        .Select(mr => new
                        {
                            RecordID = mr.Record_ID,
                            DoctorName = mr.Doctors.Doctor_Full_Name,
                            Diagnosis = mr.DiagnosisTreatment.Diagnosis_Name,
                            Treatment = mr.DiagnosisTreatment.Treatment_Details,
                            RecordDate = mr.Record_Date
                        })
                        .ToList();

                    // Hiển thị thông tin hồ sơ bệnh án vào ListView
                    foreach (var record in medicalRecords)
                    {
                        ListViewItem item = new ListViewItem(record.RecordID.ToString());
                        item.SubItems.Add(patient.Patient_Full_Name);
                        item.SubItems.Add(record.DoctorName);
                        item.SubItems.Add(record.Diagnosis);
                        item.SubItems.Add(record.Treatment);
                        item.SubItems.Add(record.RecordDate.ToShortDateString());

                        listViewRecord.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Bệnh nhân không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
