using Lap04_3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Lap04_3
{
    public partial class frmSearch : Form
    {
        private readonly StudentDBContext db = new StudentDBContext();
        public frmSearch()
        {
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            try
            {
                using (var db = new StudentDBContext())
                {
                    var faculties = db.Faculties
                        .AsNoTracking()
                        .OrderBy(f => f.FacultyName)
                        .ToList();

                    //faculties.Insert(0, new Faculty
                    //{
                    //    FacultyID = 0,
                    //    FacultyName = "Tất cả"
                    //});

                    cmbFaculty.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmbFaculty.DisplayMember = "FacultyName";
                    cmbFaculty.ValueMember = "FacultyID";
                    cmbFaculty.DataSource = faculties;
                    cmbFaculty.SelectedIndex = -1;
                    txtResultCount.ReadOnly = true;
                    LoadAllStudents();
                }

                this.AcceptButton = btnSearch;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void LoadAllStudents()
        {
            using (var db = new StudentDBContext())
            {
                var data = db.Students
                .Include(s => s.Faculty)
                .AsNoTracking()
                .Select(s => new
                {
                    s.StudentID,
                    s.FullName,
                    FacultyName = s.Faculty.FacultyName,
                    s.AverageScore
                }).ToList();

                BindGrid(data);
            }
        }

        private void BindGrid(object data)
        {
            dgvResult.AutoGenerateColumns = true;
            dgvResult.DataSource = data;

            if (dgvResult.Columns.Count >= 4)
            {
                dgvResult.Columns[0].HeaderText = "Mã SV";
                dgvResult.Columns[1].HeaderText = "Họ tên";
                dgvResult.Columns[2].HeaderText = "Khoa";
                dgvResult.Columns[3].HeaderText = "Điểm TB";
                dgvResult.AutoResizeColumns();
            }

            txtResultCount.Text = dgvResult.Rows.Count.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtStudentID.Text.Trim();
                string name = txtFullName.Text.Trim();
                int selectedFacultyID = (cmbFaculty.SelectedValue is int v) ? v : 0;

                using (var db = new StudentDBContext())
                {
                    var query = db.Students
                        .Include(s => s.Faculty)
                        .AsNoTracking()
                        .AsQueryable();

                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        query = query.Where(s => s.StudentID.Contains(id));
                    }

                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        query = query.Where(s => s.FullName.Contains(name));
                    }

                    if (selectedFacultyID != 0) // 0 là Tất cả khoa
                    {
                        query = query.Where(s => s.FacultyID == selectedFacultyID);
                    }

                    var result = query.Select(s => new
                    {
                        s.StudentID,
                        s.FullName,
                        FacultyName = s.Faculty.FacultyName,
                        s.AverageScore
                    }).ToList();

                    BindGrid(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtStudentID.Clear();
            txtFullName.Clear();
            //if (cmbFaculty.Items.Count > 0)
            //{
            //    cmbFaculty.SelectedIndex = 0;
            //}
            cmbFaculty.SelectedIndex = -1;
            LoadAllStudents();
            //txtStudentID.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
