using Lap04_4.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lap04_4
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //hiện thị ngày tháng năm hiện tại
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.CustomFormat = "dd/MM/yyyy";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.CustomFormat = "dd/MM/yyyy";

            dtpFrom.Value = DateTime.Today;
            dtpTo.Value = DateTime.Today;
            chkAllInMonth.Checked = false;
            txtTotal.ReadOnly = true;

            // Tự động tìm kiếm lúc mở form
            LoadGrid();

            // Khi người dùng thay đổi -> tự cập nhật
            dtpFrom.ValueChanged += (_, __) => LoadGrid();
            dtpTo.ValueChanged += (_, __) => LoadGrid();
            chkAllInMonth.CheckedChanged += (_, __) => LoadGrid();
        }

        private void LoadGrid()
        {
            // Nếu người dùng chọn sai khoảng thì hoán đổi cho hợp lệ
            if (!chkAllInMonth.Checked && dtpFrom.Value.Date > dtpTo.Value.Date)
            {
                var t = dtpFrom.Value;
                dtpFrom.Value = dtpTo.Value;
                dtpTo.Value = t;
            }

            using (var db = new ProductOrderDBContext())
            {
                IQueryable<Invoice> q = db.Invoices;

                if (chkAllInMonth.Checked)
                {
                    int y = dtpFrom.Value.Year;
                    int m = dtpFrom.Value.Month;
                    q = q.Where(i => i.DeliveryDate.Year == y && i.DeliveryDate.Month == m);
                }
                else
                {
                    DateTime from = dtpFrom.Value.Date;
                    DateTime toEx = dtpTo.Value.Date.AddDays(1); // < toEx để bao trọn ngày To
                    q = q.Where(i => i.DeliveryDate >= from && i.DeliveryDate < toEx);
                }

                var data = q
                    .Select(i => new
                    {
                        i.InvoiceNo,
                        i.OrderDate,
                        i.DeliveryDate,
                        Total = i.Orders.Sum(o => (decimal?)o.Price * o.Quantity) ?? 0
                    })
                    .OrderBy(x => x.DeliveryDate).ThenBy(x => x.InvoiceNo)
                    .ToList()
                    .Select((x, idx) => new
                    {
                        STT = idx + 1,
                        SốHĐ = x.InvoiceNo,
                        NgàyĐặtHàng = x.OrderDate,
                        NgàyGiaoHàng = x.DeliveryDate,
                        ThànhTiền = x.Total
                    })
                    .ToList();

                dgvOrders.AutoGenerateColumns = true; // hoặc map cột thủ công nếu bạn đã tạo sẵn
                dgvOrders.DataSource = data;

                txtTotal.Text = (data.Sum(r => r.ThànhTiền)).ToString("N0");
            }
        }

    }
}
