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
using System.Xml.Linq;

namespace FinalProject
{
    public partial class PatientControl : UserControl
    {
        DbClinic db = new DbClinic();
        public PatientControl()
        {
            InitializeComponent();
        }

        private void txtFindingPatientByName_Enter(object sender, EventArgs e)
        {
            txtFindingPatientByName.Text = "";
        }

        private void txtFindingProductByName_Leave(object sender, EventArgs e)
        {
        }

        private void pnProductControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PatientControl_Load(object sender, EventArgs e)
        {
            listViewPatient.View = View.Details;
            db = new DbClinic();
            List<Patients> patients = db.Patients.ToList();
            listViewPatient.Items.Clear();
            foreach (var p in patients)
            {
                ListViewItem item = new ListViewItem(p.Patient_ID.ToString());
                item.SubItems.Add(p.Patient_Full_Name);
                item.SubItems.Add(p.Patient_DOB.ToString("yyyy-MM-dd"));
                item.SubItems.Add(p.Patient_Gender);
                item.SubItems.Add(p.Patient_Phone);
                item.SubItems.Add(p.Patient_Address);
                listViewPatient.Items.Add(item);
            }
            cmbGender.Items.Clear();
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.SelectedIndex = -1;
        }

        private void listViewPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPatient.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewPatient.SelectedItems[0];
                txtPatientID.Text = selectedItem.SubItems[0].Text;
                txtName.Text = selectedItem.SubItems[1].Text;
                txtDOB.Text = selectedItem.SubItems[2].Text;
                cmbGender.Text = selectedItem.SubItems[3].Text;
                txtPhone.Text = selectedItem.SubItems[4].Text;
                txtAddress.Text = selectedItem.SubItems[5].Text;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường nhập liệu có trống không
                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtDOB.Text) ||
                    string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra định dạng ngày tháng hợp lệ
                DateTime dob;
                if (!DateTime.TryParse(txtDOB.Text, out dob))
                {
                    MessageBox.Show("Ngày sinh không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra số điện thoại đã tồn tại trong cơ sở dữ liệu chưa
                if (db.Patients.Any(p => p.Patient_Phone == txtPhone.Text))
                {
                    MessageBox.Show("Số điện thoại đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm bệnh nhân mới
                Patients newPatient = new Patients
                {
                    Patient_Full_Name = txtName.Text,
                    Patient_DOB = dob,
                    Patient_Gender = cmbGender.Text,
                    Patient_Phone = txtPhone.Text,
                    Patient_Address = txtAddress.Text,
                };

                db.Patients.Add(newPatient);
                db.SaveChanges();

                MessageBox.Show("Patient added successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tải lại danh sách bệnh nhân
                PatientControl_Load(sender, e);

                // Xóa các trường nhập liệu
                txtName.Clear();
                txtDOB.Clear();
                txtPhone.Clear();
                txtAddress.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtPatientID.Text);
                Patients patient = db.Patients.FirstOrDefault(p => p.Patient_ID == id);
                if (patient != null)
                {
                    patient.Patient_Full_Name = txtName.Text;
                    patient.Patient_DOB = DateTime.Parse(txtDOB.Text);
                    patient.Patient_Gender = cmbGender.Text;
                    patient.Patient_Phone = txtPhone.Text;
                    patient.Patient_Address = txtAddress.Text;

                    db.SaveChanges();
                    MessageBox.Show("Patient edited successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PatientControl_Load(sender, e); // Reload the patient list
                    txtPatientID.Clear();
                    txtName.Clear();
                    txtDOB.Clear();
                    txtPhone.Clear();
                    txtAddress.Clear();
                }
                else
                {
                    MessageBox.Show("Patient not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtPatientID.Text);
                Patients patient = db.Patients.FirstOrDefault(p => p.Patient_ID == id);
                if (patient != null)
                {
                    db.Patients.Remove(patient);
                    db.SaveChanges();
                    MessageBox.Show("Patient deleted successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PatientControl_Load(sender, e); // Reload the patient list
                    txtName.Clear();
                    txtDOB.Clear();
                    txtPhone.Clear();
                    txtAddress.Clear();
                }
                else
                {
                    MessageBox.Show("Patient not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnFind_Click(object sender, EventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtFindingPatientByName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchQuery = txtFindingPatientByName.Text.Trim();
                if (string.IsNullOrEmpty(searchQuery) || searchQuery == "Finding patient by name")
                {
                    PatientControl_Load(sender, e); // Load all patients
                    return;
                }

                List<Patients> patients;
                patients = db.Patients
                            .Where(p => p.Patient_Full_Name.Contains(searchQuery))
                            .ToList();

                listViewPatient.Items.Clear();
                foreach (var patient in patients)
                {
                    ListViewItem item = new ListViewItem(patient.Patient_ID.ToString());
                    item.SubItems.Add(patient.Patient_Full_Name);
                    item.SubItems.Add(patient.Patient_DOB.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(patient.Patient_Gender);
                    item.SubItems.Add(patient.Patient_Phone);
                    item.SubItems.Add(patient.Patient_Address);
                    listViewPatient.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProductQuantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtProductPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtProductID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbGender_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new DbClinic()) // Thay bằng DbContext của bạn
                {
                    var genders = context.Patients
                                         .Select(p => p.Patient_Gender) // Lấy dữ liệu cột Gender
                                         .Distinct() // Chỉ lấy các giá trị khác nhau
                                         .ToList();

                    cmbGender.Items.Clear();
                    cmbGender.Items.AddRange(genders.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                int patientID = int.Parse(txtPatientID.Text);
                MedicalRecord record = new MedicalRecord(patientID);
                record.Show();
            }
            catch 
            {
                MessageBox.Show("ID bệnh nhân rỗng !!!");
            }
        }
    }
}
