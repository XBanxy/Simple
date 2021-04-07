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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonTeach_Click(object sender, EventArgs e)
        {
            new FormTeacher().Show();
        }

        private void buttonStud_Click(object sender, EventArgs e)
        {
            new FormStudent().Show();
        }

        private void buttonCons_Click(object sender, EventArgs e)
        {
            new FormConsultation().Show();
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            new FormQuery().Show();
        }
    }
}
