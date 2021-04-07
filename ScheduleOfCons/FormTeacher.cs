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
    public partial class FormTeacher : Form
    {
        int? id;

        void LoadData(Context db)
        {
            dataGridView1.DataSource = db.Teachers.ToList();
            Clear();
        }

        void Clear()
        {
            dataGridView1.ClearSelection();
            txtBoxTableNum.Clear();
            txtBoxFullName.Clear();
            cmbDepartment.SelectedIndex = -1;
            id = null;
        }


        Teacher FindTeacher(Context db)
        {
            return db.Teachers.Where(t => t.Id == id).First();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           id = Int32.Parse(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString());

            using (Context db = new Context())
            {
                Teacher t = FindTeacher(db);

                txtBoxTableNum.Text = t.TableNumber;
                txtBoxFullName.Text = t.FullName;
                cmbDepartment.Text = t.Department;
            }
        }

        public FormTeacher()
        {
            InitializeComponent();
        }


        bool CheckData(out string mess)
        {
            bool result = true;
            mess = "";

            if(cmbDepartment.SelectedIndex==-1 || txtBoxFullName.Text=="" || txtBoxTableNum.Text == "")
            {
                mess += "Не все поля заполнены!\n";
                return false;
            }
            if(!Regex.IsMatch(txtBoxTableNum.Text, @"^\d{6}$"))
            {
                mess += "Табельный номер должен содержать 6 цифр!\n";
                return false;
            }
            if(!Regex.IsMatch(txtBoxFullName.Text, @"^[А-Я|Ё][а-я|ё]+\s[А-Я|Ё][а-я|ё]+\s?[А-Я|Ё]?[а-я|ё]*$"))
            {
                mess += "Неправильный формат ФИО!\n";
                return false;
            }

            return result;
        }

        
        private void FormTeacher_Load(object sender, EventArgs e)
        {
            using(Context db = new Context())
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
                using (Context db = new Context())
                {
                    bool flag = true;
                    foreach (Teacher t in db.Teachers)
                    {
                        if (t.TableNumber == txtBoxTableNum.Text)
                        {
                            MessageBox.Show("Такой табельный номер уже существует у другого преподавателя!");
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        db.Teachers.Add(new Teacher
                        {
                            TableNumber = txtBoxTableNum.Text,
                            FullName = txtBoxFullName.Text,
                            Department = cmbDepartment.Text
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
            else if(!CheckData(out string mess))
                MessageBox.Show(mess);
            else
                using(Context db = new Context())
                {
                    Teacher t = FindTeacher(db);

                    if (t.TableNumber != txtBoxTableNum.Text)
                    {
                        MessageBox.Show("Нельзя поменять табельный номер преподавателя!");
                        Clear();
                    }
                    else
                    {
                        t.TableNumber = txtBoxTableNum.Text;
                        t.FullName = txtBoxFullName.Text;
                        t.Department = cmbDepartment.Text;

                        db.SaveChanges();
                        LoadData(db);
                    }
                    
                }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id == null)
                MessageBox.Show("Выберите строку для удаления!");
            else
                using(Context db = new Context())
                {
                    Teacher t = FindTeacher(db);

                    if (MessageBox.Show($"Вы уверены, что хотите удалить преподавателя {t.FullName}?", "Подтверждение удаления",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (t.Consultations.Count > 0) 
                        {
                            string mess = "У этого преподавателя будут удалены консультации:\n";
                             foreach (Consultation cons in t.Consultations)
                                mess += cons.Date.Date.ToString() + " со студентом " + cons.Student.FullName;

                            MessageBox.Show(mess);
                        }
                        db.Teachers.Remove(t);
                        db.SaveChanges();
                        LoadData(db);
                    }
                    else Clear();
                }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
