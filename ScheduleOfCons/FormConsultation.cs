using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleOfCons
{
    public partial class FormConsultation : Form
    {
        int? id;

        void LoadData(Context db)
        {
            dataGridView1.DataSource = db.Consultations.Select(c => new
            {
                c.Id,
                TeacherId = c.Teacher.FullName,
                StudentId = c.Student.FullName,
                c.Date,
                c.Time,
                c.Auditorium
            }).ToList();
            Clear();
        }

        void Clear()
        {
            cmbBoxAudit.SelectedIndex = -1;
            cmbTime.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            dataGridView1.ClearSelection();
            cmbStudent.SelectedIndex = -1;
            cmbTeacher.SelectedIndex = -1;
            id = null;
        }
        
        bool CheckData(out string mess)
        {
            bool result = true;
            mess = "";

            if(cmbTime.SelectedIndex==-1 || cmbBoxAudit.SelectedIndex==-1 || cmbTeacher.SelectedIndex==-1 || cmbStudent.SelectedIndex == -1)
            {
                mess += "Вы заполнили не все поля!\n";
                return false;
            }
            if (dateTimePicker1.Value.Date < DateTime.Now.Date)
            {
                mess += "Вы не можете редактировать запись о консультации раньше сегодняшнего дня.";
                return false;
            }

            return result;
        }

        Consultation FindCons(Context db)
        {
            return db.Consultations.Where(c => c.Id==id).First();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Int32.Parse(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString());

            using (Context db = new Context())
            {
                Consultation cons = FindCons(db);

                dateTimePicker1.Value = cons.Date;
                cmbBoxAudit.Text = cons.Auditorium;
                cmbStudent.Text = cons.Student.FullName;
                cmbTeacher.Text = cons.Teacher.FullName;
                cmbTime.Text = cons.Time.ToString();

            }
        }
        public FormConsultation()
        {
            InitializeComponent();
        }

        private void FormConsultation_Load(object sender, EventArgs e)
        {
            using (Context db = new Context())
            {
                cmbStudent.DataSource = db.Students.ToList();
                cmbTeacher.DataSource = db.Teachers.ToList();
                LoadData(db);

                List<TimeSpan> times = new List<TimeSpan>();
                for (int h = 9; h <= 18; h++) 
                {
                    times.Add(new TimeSpan(h, 0, 0));
                    times.Add(new TimeSpan(h, 30, 0));
                }

                cmbTime.DataSource = times;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CheckData(out string mess))
                MessageBox.Show(mess);
            else
            {
                using(Context db = new Context())
                {
                        db.Consultations.Add(new Consultation
                        {
                            TeacherId = Int32.Parse(cmbTeacher.SelectedValue.ToString()),
                            StudentId = Int32.Parse(cmbStudent.SelectedValue.ToString()),
                            Date = dateTimePicker1.Value.Date,
                            Auditorium = cmbBoxAudit.Text,
                            Time = TimeSpan.Parse(cmbTime.Text)
                        });
                        db.SaveChanges();
                        LoadData(db);
                    
                }
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (id==null) 
                MessageBox.Show("Выберите строку для изменения!");
            else if (!CheckData(out string mess))
                MessageBox.Show(mess);
            else
                using (Context db = new Context())
                {
                    Consultation cons = FindCons(db);
                        cons.StudentId = Int32.Parse(cmbStudent.SelectedValue.ToString());
                        cons.TeacherId = Int32.Parse(cmbTeacher.SelectedValue.ToString());
                        cons.Auditorium = cmbBoxAudit.Text;
                        cons.Date = dateTimePicker1.Value.Date;
                        cons.Time = TimeSpan.Parse(cmbTime.Text);

                        db.SaveChanges();
                        LoadData(db);
                }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id==null)
                MessageBox.Show("Выберите строку для удаления!");
            else 
                using(Context db = new Context())
                {
                    Consultation cons = FindCons(db);

                    if (MessageBox.Show($"Вы уверены, что хотите удалить консультацию {cons.Date} у {cons.Student.FullName} и {cons.Teacher.FullName}?", "Подтверждение удаления",
                            MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        db.Consultations.Remove(cons);
                        db.SaveChanges();
                        LoadData(db);
                    }
                    else Clear();
                }
        }

        
    }
}
