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
    public partial class DiagnosisTreatmentControl : UserControl
    {
        DbClinic db = new DbClinic();
        public DiagnosisTreatmentControl()
        {
            InitializeComponent();
        }

        private void DiagnosisTreatmentControl_Load(object sender, EventArgs e)
        {
            listView.View = View.Details;
            db = new DbClinic();
            List<DiagnosisTreatment> diagnosisTreatments = db.DiagnosisTreatment.ToList();
            listView.Items.Clear();
            foreach (var s in diagnosisTreatments)
            {
                ListViewItem item = new ListViewItem(s.Diagnosis_ID.ToString());
                item.SubItems.Add(s.Diagnosis_Name);
                item.SubItems.Add(s.Treatment_Details);
                listView.Items.Add(item);
            }
        }

        private void txtFinding_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtFinding.Text.ToLower();  // Lấy giá trị tìm kiếm và chuyển về chữ thường

            // Lọc danh sách dịch vụ dựa trên tên dịch vụ
            var filteredServices = db.DiagnosisTreatment
                                     .Where(s => s.Diagnosis_Name.ToLower().Contains(searchText))  // Tìm kiếm theo tên dịch vụ
                                     .ToList();

            listView.Items.Clear();  // Xóa các mục trong ListView

            // Thêm các dịch vụ đã lọc vào ListView
            foreach (var s in filteredServices)
            {
                ListViewItem item = new ListViewItem(s.Diagnosis_ID.ToString());
                item.SubItems.Add(s.Diagnosis_Name);
                item.SubItems.Add(s.Treatment_Details.ToString());
                listView.Items.Add(item);
            }
        }

        private void txtFinding_Enter(object sender, EventArgs e)
        {
            txtFinding.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DiagnosisTreatment diagnosisTreatment = new DiagnosisTreatment
                {
                    Diagnosis_Name = txtServiceName.Text,
                    Treatment_Details = txtPrice.Text 
                };

                db.DiagnosisTreatment.Add(diagnosisTreatment);
                db.SaveChanges();
                MessageBox.Show("Diagnosis Treatment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DiagnosisTreatmentControl_Load(sender, e);
                txtServiceID.Text = "";
                txtServiceName.Text = "";
                txtPrice.Text = "";
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
                int serviceID = int.Parse(txtServiceID.Text);
                var serviceToEdit = db.DiagnosisTreatment.FirstOrDefault(s => s.Diagnosis_ID == serviceID);

                if (serviceToEdit != null)
                {
                    serviceToEdit.Diagnosis_Name = txtServiceName.Text;
                    serviceToEdit.Treatment_Details = txtPrice.Text;

                    db.SaveChanges();
                    MessageBox.Show("Diagnosis Treatment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DiagnosisTreatmentControl_Load(sender, e);
                    txtServiceID.Text = "";
                    txtServiceName.Text = "";
                    txtPrice.Text = "";
                }
                else
                {
                    MessageBox.Show("Diagnosis Treatment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int serviceID = int.Parse(txtServiceID.Text); // Get Service ID from the selected item
                var serviceToDelete = db.DiagnosisTreatment.FirstOrDefault(s => s.Diagnosis_ID == serviceID);

                if (serviceToDelete != null)
                {
                    db.DiagnosisTreatment.Remove(serviceToDelete);
                    db.SaveChanges();
                    MessageBox.Show("Diagnosis Treatment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DiagnosisTreatmentControl_Load(sender, e);
                    txtServiceID.Text = "";
                    txtServiceName.Text = "";
                    txtPrice.Text = "";
                }
                else
                {
                    MessageBox.Show("Diagnosis Treatment not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView.SelectedItems[0];
                txtServiceID.Text = selectedItem.SubItems[0].Text;
                txtServiceName.Text = selectedItem.SubItems[1].Text;
                txtPrice.Text = selectedItem.SubItems[2].Text;
            }
        }
    }
}
