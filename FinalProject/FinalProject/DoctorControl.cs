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
    public partial class DoctorControl : UserControl
    {
        DbClinic db = new DbClinic();
        public DoctorControl()
        {
            InitializeComponent();
        }

        private void DoctorControl_Load(object sender, EventArgs e)
        {
            listViewDoctor.View = View.Details;
            db = new DbClinic();

            var doctors = db.Doctors
                            .Join(db.Departments,
                                  d => d.Department_ID,
                                  dept => dept.Department_ID,
                                  (d, dept) => new
                                  {
                                      d.Doctor_ID,
                                      d.Doctor_Full_Name,
                                      d.Doctor_Phone,
                                      DepartmentName = dept.Department_Name,
                                      Specialization = dept.Department_Specialization,
                                  })
                            .ToList();

            listViewDoctor.Items.Clear();

            foreach (var d in doctors)
            {
                ListViewItem item = new ListViewItem(d.Doctor_ID.ToString());
                item.SubItems.Add(d.Doctor_Full_Name);
                item.SubItems.Add(d.Specialization);
                item.SubItems.Add(d.Doctor_Phone);
                item.SubItems.Add(d.DepartmentName);
                listViewDoctor.Items.Add(item);
            }

            // Load departments into ComboBox
            cmbDept.DataSource = db.Departments
                        .Select(d => d.Department_Name)
                        .Distinct()
                        .ToList();
            var selectedDept = db.Departments.FirstOrDefault(d => d.Department_Name == cmbDept.SelectedItem.ToString());
            if (selectedDept != null)
            {
                txtSpe.Text = selectedDept.Department_Specialization;
            }
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listViewDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDoctor.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewDoctor.SelectedItems[0];
                txtDoctorID.Text = selectedItem.SubItems[0].Text;
                txtFullName.Text = selectedItem.SubItems[1].Text;
                txtSpe.Text = selectedItem.SubItems[2].Text;
                txtPhone.Text = selectedItem.SubItems[3].Text;
                cmbDept.Text = selectedItem.SubItems[4].Text;
            }
            else
            {
                txtDoctorID.Text = "";
                txtFullName.Text = "";
                cmbDept.Text = "";
                txtPhone.Text = "";
                txtSpe.Text = "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string fullName = txtFullName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string departmentName = cmbDept.Text.Trim();
                string specializationName = txtSpe.Text.Trim();

                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(departmentName) || string.IsNullOrEmpty(specializationName))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if phone number already exists
                bool phoneExists = db.Doctors.Any(d => d.Doctor_Phone == phone);
                if (phoneExists)
                {
                    MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập số khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get department ID based on the department name
                var department = db.Departments.FirstOrDefault(d => d.Department_Name == departmentName);
                if (department == null)
                {
                    MessageBox.Show("Khoa không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Add new doctor
                Doctors newDoctor = new Doctors
                {
                    Doctor_Full_Name = fullName,
                    Doctor_Phone = phone,
                    Department_ID = department.Department_ID,
                };

                db.Doctors.Add(newDoctor);
                db.SaveChanges();
                DoctorControl_Load(sender, e); // Reload doctor list

                // Reset form
                txtDoctorID.Text = "";
                txtFullName.Text = "";
                txtPhone.Text = "";
                cmbDept.SelectedIndex = -1; // Reset Department ComboBox
                txtSpe.Text = "";

                MessageBox.Show("Thêm bác sĩ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm bác sĩ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtDoctorID.Text);
                var doctor = db.Doctors.FirstOrDefault(d => d.Doctor_ID == id);
                if (doctor != null)
                {
                    doctor.Doctor_Full_Name = txtFullName.Text;
                    doctor.Doctor_Phone = txtPhone.Text;

                    // Update department and specialization
                    var department = db.Departments.FirstOrDefault(d => d.Department_Name == cmbDept.Text);
                    if (department != null)
                    {
                        doctor.Department_ID = department.Department_ID;
                    }

                    doctor.Departments.Department_Specialization = txtSpe.Text; // Update specialization
                }

                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin bác sĩ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DoctorControl_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật bác sĩ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtDoctorID.Text);
                var doctor = db.Doctors.FirstOrDefault(d => d.Doctor_ID == id);
                if (doctor != null)
                {
                    db.Doctors.Remove(doctor);
                    db.SaveChanges();
                    MessageBox.Show("Xóa bác sĩ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DoctorControl_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa bác sĩ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            
        }

        private void txtFinding_Enter(object sender, EventArgs e)
        {
            txtFinding.Text = "";
        }

        private void txtFinding_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchQuery = txtFinding.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(searchQuery))
                {
                    DoctorControl_Load(sender, e);
                    return;
                }

                var searchResults = db.Doctors
                    .Join(db.Departments,
                          d => d.Department_ID,
                          dept => dept.Department_ID,
                          (d, dept) => new
                          {
                              d.Doctor_ID,
                              d.Doctor_Full_Name,
                              d.Doctor_Phone,
                              DepartmentName = dept.Department_Name,
                              Specialization = dept.Department_Specialization,
                          })
                    .Where(d => d.Doctor_Full_Name.ToLower().Contains(searchQuery))
                    .ToList();

                listViewDoctor.Items.Clear();
                foreach (var d in searchResults)
                {
                    ListViewItem item = new ListViewItem(d.Doctor_ID.ToString());
                    item.SubItems.Add(d.Doctor_Full_Name);
                    item.SubItems.Add(d.Specialization);
                    item.SubItems.Add(d.Doctor_Phone);
                    item.SubItems.Add(d.DepartmentName);
                    listViewDoctor.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm bác sĩ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDept.SelectedIndex != -1)
            {
                var selectedDept = db.Departments.FirstOrDefault(d => d.Department_Name == cmbDept.SelectedItem.ToString());
                if (selectedDept != null)
                {
                    txtSpe.Text = selectedDept.Department_Specialization;
                }
            }
        }

    }
}
