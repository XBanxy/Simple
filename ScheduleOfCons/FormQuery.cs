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
    public partial class FormQuery : Form
    {
        public FormQuery()
        {
            InitializeComponent();
        }

        private void btnExecQuery_Click(object sender, EventArgs e)
        {
            using(Context db = new Context())
            {
                //	Вывести список преподавателей отделения «Информационные технологии»;
                if (radioButton1.Checked) dataGridView1.DataSource =
                        db.Teachers.Where(t => t.Department == "Информационные технологии")
                        .Select(t => new { t.FullName }).ToList();

                if (radioButton2.Checked)
                {
                    //Вывести тему диплома определенного студента

                    dataGridView1.DataSource = 
                        db.Students.Where(s => s.FullName == cmbStudent.Text)
                        .Select(s => new {s.DiplomSubject }).ToList();
                    

                    cmbStudent.SelectedIndex = -1;
                }

                if (radioButton3.Checked)
                {
                    //Вывести следующую информацию о консультациях определенного преподавателя: ФИО студента, дата, время, аудитория!

                    int teachId = Int32.Parse(cmbTeacher.SelectedValue.ToString());
                    dataGridView1.DataSource = db.Consultations.Where(c => c.TeacherId == teachId)
                        .Select(c => new { c.Student.FullName, c.Date, c.Time, c.Auditorium }).ToList();

                }

                if (radioButton4.Checked)
                {
                    //Вывести список студентов, которые пишут диплом по теме, связанной с разработкой мобильного приложения, 
                    //т.е. в теме диплома фигурирует словосочетание «мобильного приложения»;

                    dataGridView1.DataSource = db.Students.Where(s => s.DiplomSubject.Contains("мобильного приложения"))
                        .Select(s => new { s.FullName }).ToList();
                }

                if (radioButton5.Checked)
                {
                    //Вывести расписание консультаций в следующем виде: 
                    //ФИО преподавателя, ФИО студента, дата, время, аудитория;

                    dataGridView1.DataSource=db.Consultations.Select(c => new 
                    { 
                        Teacher = c.Teacher.FullName,
                        c.Student.FullName, 
                        c.Date, 
                        c.Time, 
                        c.Auditorium 
                    }).ToList();
                }

                if (radioButton6.Checked)
                {
                    //Вывести расписание консультаций преподавателя Маховой Анны Николаевны: дата, время, аудитория;

                    dataGridView1.DataSource = db.Consultations.Where(c => c.Teacher.FullName == "Махова Анна Николаевна")
                        .Select(c => new { c.Date, c.Time, c.Auditorium }).ToList();
                }

                if (radioButton7.Checked)
                {
                    //Вывести расписание преподавателей кафедры «Информационные системы и программирование»: 
                    //ФИО преподавателя, дата, время, аудитория

                    dataGridView1.DataSource =
                        db.Consultations.Where(c => c.Teacher.Department == "Информационные технологии")
                        .Select(c => new { c.Teacher.FullName, c.Date, c.Time, c.Auditorium}).ToList();
                }

                if (radioButton8.Checked)
                {
                    //Вывести список преподавателей кафедры «Информационные системы и программирование», 
                    //отсортировав список в алфавитном порядке

                    dataGridView1.DataSource = db.Teachers.Where(t => t.Department == "Информационные технологии")
                        .Select(t => new {t.FullName }).OrderBy(t => t.FullName).ToList();
                }

                if (radioButton9.Checked)
                {
                    //Вывести список преподавателей с указанием числа студентов, руководителями которых они являются; 
                    //отсортировать список по убыванию числа студентов; в список включить только тех преподавателей, 
                    //у которых число студентов более 3.

                    dataGridView1.DataSource = db.Consultations.GroupBy(c => c.Teacher.FullName)
                        .Select(g => new { Teachers = g.Key, Students = g.Select(s=>s.StudentId).Distinct().Count()})
                        .Where(g => g.Students > 3)
                        .OrderByDescending(g => g.Students)
                        .ToList();
                }
            }

        }

        private void FormQuery_Load(object sender, EventArgs e)
        {
            using(Context db = new Context())
            {
                cmbStudent.DataSource = db.Students.ToList();
                cmbStudent.SelectedIndex = -1;

                cmbTeacher.DataSource = db.Teachers.ToList();
                cmbTeacher.SelectedIndex = -1;
            }
        }
    }
}
