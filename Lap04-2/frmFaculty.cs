using Lap04_2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lap04_2
{
    public partial class frmFaculty : Form
    {
        private readonly StudentDBContext db = new StudentDBContext();

        public frmFaculty()
        {
            InitializeComponent();
        }

        private void frmFaculty_Load(object sender, EventArgs e)
        {
            try
            {
                List<Faculty> listFalcultys = db.Faculties.ToList();

                BindGird(listFalcultys);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindGird(List<Faculty> listFaculties)
        {
            dgvFaculty.AutoGenerateColumns = true;
            dgvFaculty.DataSource = listFaculties
                .Select(f => new
                {
                    f.FacultyID,
                    f.FacultyName,
                    f.TotalProfessor
                })
                .ToList();

            // Đặt lại tên cột
            dgvFaculty.Columns[0].HeaderText = "Mã khoa";
            dgvFaculty.Columns[1].HeaderText = "Tên khoa";
            dgvFaculty.Columns[2].HeaderText = "Số giáo sư";
        }

        private void dgvFaculty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvFaculty.Rows[e.RowIndex];
                txtFacultyID.Text = selectedRow.Cells[0].Value.ToString();
                txtFacultyName.Text = selectedRow.Cells[1].Value.ToString();
                txtTotalProfessor.Text = selectedRow.Cells[2].Value.ToString();

                txtFacultyID.ReadOnly = true; // Khóa không cho sửa mã khoa
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtFacultyID.Text, out int facultyId) || facultyId <= 0)
                {
                    MessageBox.Show("Mã khoa phải là số nguyên dương.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFacultyID.Focus();
                    return;
                }

                string name = txtFacultyName.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Tên khoa không được để trống.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFacultyName.Focus();
                    return;
                }

                int? totalProf = null;
                string totalStr = txtTotalProfessor.Text.Trim();
                if (!string.IsNullOrWhiteSpace(totalStr))
                {
                    if (!int.TryParse(totalStr, out int total) || total < 0)
                    {
                        MessageBox.Show("Số giáo sư phải là số nguyên không âm.", "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTotalProfessor.Focus();
                        return;
                    }
                    totalProf = total;
                }

                // add - update
                var faculty = db.Faculties.Find(facultyId);
                if (faculty == null)
                {
                    // add
                    faculty = new Faculty
                    {
                        FacultyID = facultyId,
                        FacultyName = name,
                        TotalProfessor = totalProf
                    };
                    db.Faculties.Add(faculty);
                    db.SaveChanges();
                    MessageBox.Show("Thêm khoa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // update
                    faculty.FacultyName = name;
                    faculty.TotalProfessor = totalProf;
                    db.SaveChanges();
                    MessageBox.Show("Cập nhật khoa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Refresh lưới
                BindGird(db.Faculties.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtFacultyID.Text, out int facultyId))
                {
                    MessageBox.Show("Vui lòng chọn khoa cần xóa.", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var faculty = db.Faculties.Find(facultyId); // find voi int
                if (faculty == null)
                {
                    MessageBox.Show("Không tìm thấy khoa cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // xác nhận xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa khoa '{faculty.FacultyName}'?",
                                         "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    db.Faculties.Remove(faculty);
                    db.SaveChanges();
                    // Refresh lưới
                    MessageBox.Show("Xóa khoa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    BindGird(db.Faculties.ToList());

                    // Xóa trắng các TextBox
                    txtFacultyID.Clear();
                    txtFacultyName.Clear();
                    txtTotalProfessor.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
