using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleOfCons
{
    public partial class FormStudent : Form
    {
        int? id;

        void LoadData(Context db)
        {
            dataGridView1.DataSource = db.Students.ToList();
            Clear();
        }

        void Clear()
        {
            txtBoxNumber.Clear();
            dataGridView1.ClearSelection();
            textBoxName.Clear();
            textBoxPhone.Clear();
            textBoxDiplom.Clear();
            id = null;
        }
        public FormStudent()
        {
            InitializeComponent();
        }

        Student FindStudent(Context db)
        {
            return db.Students.Where(s => s.Id == id).First();
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Int32.Parse(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString());

            using(Context db = new Context())
            {
                Student s = FindStudent(db);

                txtBoxNumber.Text = s.GradebookNum;
                textBoxName.Text = s.FullName;
                textBoxPhone.Text = s.Phone;
                textBoxDiplom.Text = s.DiplomSubject;
            }
        }

        bool CheckData(out string mess)
        {
            bool result = true;
            mess = "";

            if(textBoxDiplom.Text=="" || txtBoxNumber.Text=="" || textBoxName.Text=="" || textBoxPhone.Text == "")
            {
                mess += "Не все поля заполнены!\n";
                return false;
            }
            if (!Regex.IsMatch(txtBoxNumber.Text, @"^\d{4}$"))
            {
                mess += "Номер зачётной книжки должен состоять из 4 цифр!\n";
                return false;
            }
            if (!Regex.IsMatch(textBoxName.Text, @"^[А-Я|Ё][а-я|ё]+\s[А-Я|Ё][а-я|ё]+\s?[А-Я|Ё]?[а-я|ё]*$"))
            {
                mess += "Неправильный формат ФИО!\n";
                return false;
            }
            if (!Regex.IsMatch(textBoxPhone.Text, @"^8-9[0-9]{2}-[0-9]{3}-[0-9]{2}-[0-9]{2}$"))
            {
                mess += "Телефон должен быть записан в формате 8-9**-***-**-** !\n";
                return false;
            }

            return result;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {

        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            using (Context db = new Context())
            {
                LoadData(db);
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
                    bool flag = true;
                    foreach (Student s in db.Students)
                        if (s.GradebookNum == txtBoxNumber.Text)
                        {
                            MessageBox.Show("Такой номер зачётки уже существует у другого студента!");
                            flag = false;
                        }
                    if(flag)
                    {
                        db.Students.Add(new Student
                        {
                            GradebookNum = txtBoxNumber.Text,
                            FullName = textBoxName.Text,
                            Phone = textBoxPhone.Text,
                            DiplomSubject = textBoxDiplom.Text
                        });
                        db.SaveChanges();
                        LoadData(db);
                    }
                }
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (id == null)
                MessageBox.Show("Выберите строку для изменения!");
            else if (!CheckData(out string mess))
                MessageBox.Show(mess);
            else
            {
                using(Context db = new Context())
                {
                    Student s = FindStudent(db);
                    if (s.GradebookNum != txtBoxNumber.Text)
                    {
                        MessageBox.Show("Нельзя поменять номер зачётки студента!");
                        Clear();
                    }
                    else
                    {
                        s.GradebookNum = txtBoxNumber.Text;
                        s.FullName = textBoxName.Text;
                        s.Phone = textBoxPhone.Text;
                        s.DiplomSubject = textBoxDiplom.Text;

                        db.SaveChanges();
                        LoadData(db); 
                    }

                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id == null)
                MessageBox.Show("Выберите строку для удаления!");
            else
                using (Context db = new Context())
                {
                    Student s = FindStudent(db);

                    if (MessageBox.Show($"Вы уверены, что хотите удалить студента {s.FullName}?", "Подтверждение удаления",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (s.Consultations.Count > 0)
                        {
                            string mess = "У этого студента будут удалены консультации:\n";
                            foreach (Consultation cons in s.Consultations)
                                mess += cons.Date.Date + " с преподавателем " + cons.Teacher.FullName;

                            MessageBox.Show(mess);
                        }
                        db.Students.Remove(s);
                        db.SaveChanges();
                        LoadData(db);
                    }
                    else Clear();
                }
        }
    }
}
