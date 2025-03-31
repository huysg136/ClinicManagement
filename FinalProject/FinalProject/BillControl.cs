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
    public partial class BillControl : UserControl
    {
        DbClinic db = new DbClinic();
        public BillControl()
        {
            InitializeComponent();
        }

        private void BillControl_Load(object sender, EventArgs e)
        {
            listViewBill.View = View.Details;
            db = new DbClinic();
            List<Bills> bills = db.Bills.ToList();
            listViewBill.Items.Clear();
            foreach (var s in bills)
            {
                ListViewItem item = new ListViewItem(s.Bill_ID.ToString());
                item.SubItems.Add(s.Patient_ID.ToString());
                item.SubItems.Add(s.Total_Amount.ToString());
                item.SubItems.Add(s.Bill_Date.ToString("yyyy-MM-dd"));
                listViewBill.Items.Add(item);
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            
        }
    }
}
