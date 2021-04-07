namespace ScheduleOfCons
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonTeach = new System.Windows.Forms.Button();
            this.buttonStud = new System.Windows.Forms.Button();
            this.buttonCons = new System.Windows.Forms.Button();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonTeach
            // 
            this.buttonTeach.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonTeach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonTeach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTeach.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonTeach.Location = new System.Drawing.Point(47, 38);
            this.buttonTeach.Name = "buttonTeach";
            this.buttonTeach.Size = new System.Drawing.Size(124, 121);
            this.buttonTeach.TabIndex = 0;
            this.buttonTeach.Text = "Teachers";
            this.buttonTeach.UseVisualStyleBackColor = false;
            this.buttonTeach.Click += new System.EventHandler(this.buttonTeach_Click);
            // 
            // buttonStud
            // 
            this.buttonStud.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonStud.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStud.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStud.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonStud.Location = new System.Drawing.Point(218, 39);
            this.buttonStud.Name = "buttonStud";
            this.buttonStud.Size = new System.Drawing.Size(124, 121);
            this.buttonStud.TabIndex = 1;
            this.buttonStud.Text = "Students";
            this.buttonStud.UseVisualStyleBackColor = false;
            this.buttonStud.Click += new System.EventHandler(this.buttonStud_Click);
            // 
            // buttonCons
            // 
            this.buttonCons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonCons.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCons.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCons.Location = new System.Drawing.Point(47, 204);
            this.buttonCons.Name = "buttonCons";
            this.buttonCons.Size = new System.Drawing.Size(124, 121);
            this.buttonCons.TabIndex = 2;
            this.buttonCons.Text = "Consultations";
            this.buttonCons.UseVisualStyleBackColor = false;
            this.buttonCons.Click += new System.EventHandler(this.buttonCons_Click);
            // 
            // buttonQuery
            // 
            this.buttonQuery.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonQuery.Location = new System.Drawing.Point(218, 204);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(124, 121);
            this.buttonQuery.TabIndex = 3;
            this.buttonQuery.Text = "Querys";
            this.buttonQuery.UseVisualStyleBackColor = false;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(401, 374);
            this.Controls.Add(this.buttonQuery);
            this.Controls.Add(this.buttonCons);
            this.Controls.Add(this.buttonStud);
            this.Controls.Add(this.buttonTeach);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonTeach;
        private System.Windows.Forms.Button buttonStud;
        private System.Windows.Forms.Button buttonCons;
        private System.Windows.Forms.Button buttonQuery;
    }
}

