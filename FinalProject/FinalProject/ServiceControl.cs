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
    public partial class ServiceControl : UserControl
    {
        DbClinic db = new DbClinic();
        public ServiceControl()
        {
            InitializeComponent();
        }

        private void ServiceControl_Load(object sender, EventArgs e)
        {
            listViewService.View = View.Details;
            db = new DbClinic();
            List<Services> services = db.Services.ToList();
            listViewService.Items.Clear();
            foreach (var s in services)
            {
                ListViewItem item = new ListViewItem(s.Service_ID.ToString());
                item.SubItems.Add(s.Service_Name);
                item.SubItems.Add(s.Service_Price.ToString());
                listViewService.Items.Add(item);
            }
        }

        private void listViewService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewService.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewService.SelectedItems[0];
                txtServiceID.Text = selectedItem.SubItems[0].Text;
                txtServiceName.Text = selectedItem.SubItems[1].Text;
                txtPrice.Text = selectedItem.SubItems[2].Text;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Services newService = new Services
                {
                    Service_Name = txtServiceName.Text,
                    Service_Price = decimal.Parse(txtPrice.Text) // Ensure the price is a valid decimal
                };

                db.Services.Add(newService);
                db.SaveChanges();
                MessageBox.Show("Service added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ServiceControl_Load(sender, e);
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
                var serviceToEdit = db.Services.FirstOrDefault(s => s.Service_ID == serviceID);

                if (serviceToEdit != null)
                {
                    serviceToEdit.Service_Name = txtServiceName.Text;
                    serviceToEdit.Service_Price = decimal.Parse(txtPrice.Text);

                    db.SaveChanges();
                    MessageBox.Show("Service updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ServiceControl_Load(sender, e);
                    txtServiceID.Text = "";
                    txtServiceName.Text = "";
                    txtPrice.Text = "";
                }
                else
                {
                    MessageBox.Show("Service not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var serviceToDelete = db.Services.FirstOrDefault(s => s.Service_ID == serviceID);

                if (serviceToDelete != null)
                {
                    db.Services.Remove(serviceToDelete);
                    db.SaveChanges();
                    MessageBox.Show("Service deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ServiceControl_Load(sender, e);
                    txtServiceID.Text = "";
                    txtServiceName.Text = "";
                    txtPrice.Text = "";
                }
                else
                {
                    MessageBox.Show("Service not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFinding_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtFinding.Text.ToLower();  // Lấy giá trị tìm kiếm và chuyển về chữ thường

            // Lọc danh sách dịch vụ dựa trên tên dịch vụ
            var filteredServices = db.Services
                                     .Where(s => s.Service_Name.ToLower().Contains(searchText))  // Tìm kiếm theo tên dịch vụ
                                     .ToList();

            listViewService.Items.Clear();  // Xóa các mục trong ListView

            // Thêm các dịch vụ đã lọc vào ListView
            foreach (var s in filteredServices)
            {
                ListViewItem item = new ListViewItem(s.Service_ID.ToString());
                item.SubItems.Add(s.Service_Name);
                item.SubItems.Add(s.Service_Price.ToString());
                listViewService.Items.Add(item);
            }
        }

        private void txtFinding_FontChanged(object sender, EventArgs e)
        {

        }

        private void txtFinding_Enter(object sender, EventArgs e)
        {
            txtFinding.Text = "";
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtServiceName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtServiceID_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
