using FinalProject.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinalProject
{
    public partial class PreDetail : Form
    {
        private int prescriptionId;
        public PreDetail(int prescriptionId)
        {
            InitializeComponent();
            this.prescriptionId = prescriptionId;
        }

        private void PreDetail_Load(object sender, EventArgs e)
        {
            using (var context = new DbClinic())
            {
                // Truy vấn danh sách chi tiết của đơn thuốc, kèm theo thông tin thuốc nếu cần
                var details = context.PrescriptionDetails
                    .Include(pd => pd.Medicines) // nếu bạn muốn lấy thêm thông tin từ bảng Medicines
                    .Where(pd => pd.Prescription_ID == prescriptionId)
                    .ToList();

                // Xóa dữ liệu cũ trong ListView
                listView1.Items.Clear();

                // Duyệt qua từng chi tiết và thêm vào ListView
                foreach (var detail in details)
                {
                    // Tạo ListViewItem, cột đầu tiên là Medicine_ID
                    ListViewItem item = new ListViewItem(detail.Medicine_ID.ToString());

                    // Thêm thông tin tên thuốc nếu có (cột MedicineName)
                    string medicineName = detail.Medicines != null ? detail.Medicines.Medicine_Name : "N/A";
                    item.SubItems.Add(medicineName);

                    // Thêm các cột phụ: Quantity và Dosage
                    item.SubItems.Add(detail.Quantity.ToString());
                    item.SubItems.Add(detail.Dosage);

                    // Thêm item vào ListView
                    listView1.Items.Add(item);
                }
            }
        }
    }
}
