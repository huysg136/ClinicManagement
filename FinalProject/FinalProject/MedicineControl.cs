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
    public partial class MedicineControl : UserControl
    {
        DbClinic db = new DbClinic();
        public MedicineControl()
        {
            InitializeComponent();
        }

        private void txtFinding_Enter(object sender, EventArgs e)
        {
            txtFinding.Text = "";
        }

        private void MedicineControl_Load(object sender, EventArgs e)
        {
            listViewMedicine.View = View.Details;
            db = new DbClinic();
            List<Medicines> medicines = db.Medicines.ToList();
            listViewMedicine.Items.Clear();
            foreach (var s in medicines)
            {
                ListViewItem item = new ListViewItem(s.Medicine_ID.ToString());
                item.SubItems.Add(s.Medicine_Name);
                item.SubItems.Add(s.Medicine_Unit);
                item.SubItems.Add(s.Medicine_Price.ToString());
                listViewMedicine.Items.Add(item);
            }
        }

        private void listViewMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewMedicine.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewMedicine.SelectedItems[0];
                txtID.Text = selectedItem.SubItems[0].Text;
                txtName.Text = selectedItem.SubItems[1].Text;
                txtUnit.Text = selectedItem.SubItems[2].Text;
                txtPrice.Text = selectedItem.SubItems[3].Text;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Medicines medicines = new Medicines
                {
                    Medicine_Name = txtName.Text,
                    Medicine_Unit = txtUnit.Text,
                    Medicine_Price = decimal.Parse(txtPrice.Text) 
                };

                db.Medicines.Add(medicines);
                db.SaveChanges();
                MessageBox.Show("Medicine added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MedicineControl_Load(sender, e);
                txtID.Text = "";
                txtName.Text = "";
                txtUnit.Text = "";
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
                int medicineID = int.Parse(txtID.Text);
                var medicineToEdit = db.Medicines.FirstOrDefault(s => s.Medicine_ID == medicineID);

                if (medicineToEdit != null)
                {
                    medicineToEdit.Medicine_Name = txtName.Text;
                    medicineToEdit.Medicine_Unit = txtUnit.Text;
                    medicineToEdit.Medicine_Price = decimal.Parse(txtPrice.Text);

                    db.SaveChanges();
                    MessageBox.Show("Medicine updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MedicineControl_Load(sender, e);
                    txtID.Text = "";
                    txtName.Text = "";
                    txtUnit.Text = "";
                    txtPrice.Text = "";
                }
                else
                {
                    MessageBox.Show("Medicine not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int serviceID = int.Parse(txtID.Text); // Get Service ID from the selected item
                var serviceToDelete = db.Medicines.FirstOrDefault(s => s.Medicine_ID == serviceID);

                if (serviceToDelete != null)
                {
                    db.Medicines.Remove(serviceToDelete);
                    db.SaveChanges();
                    MessageBox.Show("Medicine deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MedicineControl_Load(sender, e);
                    txtID.Text = "";
                    txtName.Text = "";
                    txtUnit.Text = "";
                    txtPrice.Text = "";
                }
                else
                {
                    MessageBox.Show("Medicine not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFinding_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtFinding.Text.ToLower();
            var filtered = db.Medicines
                                     .Where(s => s.Medicine_Name.ToLower().Contains(searchText))
                                     .ToList();

            listViewMedicine.Items.Clear();  
            foreach (var s in filtered)
            {
                ListViewItem item = new ListViewItem(s.Medicine_ID.ToString());
                item.SubItems.Add(s.Medicine_Name);
                item.SubItems.Add(s.Medicine_Unit);
                item.SubItems.Add(s.Medicine_Price.ToString());
                listViewMedicine.Items.Add(item);
            }
        }
    }
}
