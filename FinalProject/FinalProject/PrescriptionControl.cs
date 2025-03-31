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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinalProject
{
    public partial class PrescriptionControl : UserControl
    {
        DbClinic db = new DbClinic();
        public PrescriptionControl()
        {
            InitializeComponent();
        }

        private void listViewService_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PrescriptionControl_Load(object sender, EventArgs e)
        {
            listView.View = View.Details;
            db = new DbClinic();
            List<Prescriptions> prescriptions = db.Prescriptions.ToList();
            listView.Items.Clear();
            foreach (var s in prescriptions)
            {
                ListViewItem item = new ListViewItem(s.Prescription_ID.ToString());
                item.SubItems.Add(s.Record_ID.ToString());
                item.SubItems.Add(s.Prescription_Date_Issued.ToString("yyyy-MM-dd"));
                listView.Items.Add(item);
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                // Lấy dòng được chọn (item đầu tiên)
                ListViewItem selectedItem = listView.SelectedItems[0];

                // Giả sử rằng cột đầu tiên chứa id và chuyển đổi sang kiểu số nguyên
                int selectedId = int.Parse(selectedItem.SubItems[0].Text);

                // Tạo đối tượng Form2 và truyền id vào constructor
                PreDetail formDetail = new PreDetail(selectedId);
                formDetail.Show();  // Hoặc .Show() nếu không cần dạng modal
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn thuốc để xem chi tiết.",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
    }
}
