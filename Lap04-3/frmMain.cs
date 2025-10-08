using Lap04_3;
using Lap04_3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lap04_3
{
    public partial class frmMain : Form
    {
        private readonly StudentDBContext db = new StudentDBContext();
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                List<Faculty> listFalcultys = db.Faculties.ToList();
                List<Student> listStudent = db.Students.Include(s => s.Faculty).ToList();

                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
                cmbFaculty.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //keyshort
            mniDepartments.ShortcutKeys = Keys.F2;
            mniExit.ShortcutKeys = Keys.Alt | Keys.F4;
            mniSearch.ShortcutKeys = Keys.Control | Keys.F;
        }

        // Hàm binding combobox: tên hiển thị là tên khoa, giá trị là mã khoa
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cmbFaculty.DataSource = listFalcultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }

        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.AutoGenerateColumns = true;
            dgvStudent.DataSource = listStudent
                .Select(s => new
                {
                    s.StudentID,
                    s.FullName,
                    FacultyID = s.FacultyID,
                    Faculty = s.Faculty.FacultyName,
                    s.AverageScore
                })
                .ToList();

            dgvStudent.Columns[0].HeaderText = "MSSV";
            dgvStudent.Columns[1].HeaderText = "Họ và tên";
            dgvStudent.Columns[2].Visible = false;
            dgvStudent.Columns[3].HeaderText = "Khoa";
            dgvStudent.Columns[4].HeaderText = "Điểm TB";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // check nhap du thong tin
                if (string.IsNullOrWhiteSpace(txtStudentID.Text) ||
                    string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtAverageScore.Text) ||
                    cmbFaculty.SelectedValue == null)
                {
                    if (string.IsNullOrWhiteSpace(txtStudentID.Text)) txtStudentID.Focus();
                    else if (string.IsNullOrWhiteSpace(txtFullName.Text)) txtFullName.Focus();
                    else if (string.IsNullOrEmpty(txtAverageScore.Text)) txtAverageScore.Focus();

                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // mssv phai co 10 ky tu
                if (txtStudentID.Text.Length != 10)
                {
                    MessageBox.Show("Mã sinh viên phải gồm đúng 10 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStudentID.Focus();
                    return;
                }

                // check diem 0<= diem <=10
                if (!double.TryParse(txtAverageScore.Text, out double score) || score < 0 || score > 10)
                {
                    MessageBox.Show("Điểm trung bình phải là số trong khoảng từ 0 đến 10!", "Không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAverageScore.Focus();
                    return;
                }

                // check trung mssv
                if (db.Students.Any(s => s.StudentID == txtStudentID.Text))
                {
                    MessageBox.Show("MSSV đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStudentID.Focus();
                    return;
                }

                // add sv
                var newStudent = new Student
                {
                    StudentID = txtStudentID.Text.Trim(),
                    FullName = txtFullName.Text.Trim(),
                    FacultyID = Convert.ToInt32(cmbFaculty.SelectedValue),
                    AverageScore = score,
                };

                // them sv vao db va luu thay doi
                db.Students.Add(newStudent);
                db.SaveChanges();

                RefreshGridFromDb();

                // nap dl vao lai gridview
                BindGrid(db.Students.Include(s => s.Faculty).ToList());

                // notification thanh cong
                MessageBox.Show("Thêm sinh viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //StudentDBContext db = new StudentDBContext();
                List<Student> students = db.Students.ToList();
                var student = students.FirstOrDefault(s => s.StudentID == txtStudentID.Text);

                if (student != null)
                {
                    if (students.Any(s => s.StudentID == txtStudentID.Text && s.StudentID != student.StudentID))
                    {
                        MessageBox.Show("Mã SV đã tồn tại. Vui lòng nhập một mã khác.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    student.FullName = txtFullName.Text;
                    student.FacultyID = int.Parse(cmbFaculty.SelectedValue.ToString());
                    student.AverageScore = double.Parse(txtAverageScore.Text);

                    db.SaveChanges();
                    RefreshGridFromDb();

                    //BindGrid(db.Students.ToList());
                    BindGrid(db.Students.Include(s => s.Faculty).ToList());
                    MessageBox.Show("Chỉnh sửa thông tin sinh viên thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sinh viên không tìm thấy!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //StudentDBContext db = new StudentDBContext();
                List<Student> studentList = db.Students.ToList();

                // Tìm sinh viên có tồn tại trong CSDL hay không
                var student = studentList.FirstOrDefault(s => s.StudentID == txtStudentID.Text);

                if (student != null)
                {
                    var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?",
                        "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirm == DialogResult.Yes)
                    {
                        db.Students.Remove(student);
                        db.SaveChanges();
                        RefreshGridFromDb();

                        //BindGrid(db.Students.ToList());
                        BindGrid(db.Students.Include(s => s.Faculty).ToList());
                        MessageBox.Show("Sinh viên đã được xóa thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy MSSV cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var r = dgvStudent.Rows[e.RowIndex];
                DataGridViewRow selectedRow = dgvStudent.Rows[e.RowIndex];
                txtStudentID.Text = selectedRow.Cells[0].Value.ToString();
                txtFullName.Text = selectedRow.Cells[1].Value.ToString();
                // set SelectedValue theo FacultyID ẩn
                //cmbFaculty.Text = selectedRow.Cells[2].Value.ToString();
                cmbFaculty.SelectedValue = r.Cells[2].Value;
                txtAverageScore.Text = selectedRow.Cells[4].Value.ToString();
            }
        }

        private void mni_Departments_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (frmFaculty f = new frmFaculty())
            {
                f.ShowDialog();
            }

            this.Show();

            ReLoadAll();
        }

        private void ReLoadAll()
        {
            using (var fresh = new StudentDBContext())
            {
                var faculties = fresh.Faculties.ToList();
                var students = fresh.Students.Include(s => s.Faculty).ToList();

                FillFalcultyCombobox(faculties);
                BindGrid(students);
            }
        }

        private void RefreshGridFromDb()
        {
            using (var ctx = new StudentDBContext())
            {
                var students = ctx.Students
                                  .Include(s => s.Faculty)
                                  .AsNoTracking()        // không cache
                                  .ToList();
                BindGrid(students);
            }
        }

        private void mniExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mniSearch_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (frmSearch f = new frmSearch())
            {
                f.ShowDialog();
            }

            this.Show();
        }
    }
}
