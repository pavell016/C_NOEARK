namespace Scheduler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            submit = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            nom = new TextBox();
            cognom = new TextBox();
            curs = new TextBox();
            error = new Label();
            proces_actiu = new Label();
            label5 = new Label();
            label6 = new Label();
            totalq = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(177, 147);
            label1.Name = "label1";
            label1.Size = new Size(100, 20);
            label1.TabIndex = 0;
            label1.Text = "Nom Alumne:";
            // 
            // submit
            // 
            submit.Location = new Point(377, 294);
            submit.Name = "submit";
            submit.Size = new Size(148, 30);
            submit.TabIndex = 1;
            submit.Text = "Crear Bulleti";
            submit.UseVisualStyleBackColor = true;
            submit.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(177, 194);
            label2.Name = "label2";
            label2.Size = new Size(124, 20);
            label2.TabIndex = 2;
            label2.Text = "Cognom Alumne:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(177, 235);
            label3.Name = "label3";
            label3.Size = new Size(37, 20);
            label3.TabIndex = 3;
            label3.Text = "Curs";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(230, 26);
            label4.Name = "label4";
            label4.Size = new Size(341, 81);
            label4.TabIndex = 4;
            label4.Text = "Notes DAM";
            label4.Click += label4_Click;
            // 
            // nom
            // 
            nom.Location = new Point(326, 144);
            nom.Name = "nom";
            nom.Size = new Size(255, 27);
            nom.TabIndex = 5;
            nom.TextChanged += nom_TextChanged;
            // 
            // cognom
            // 
            cognom.Location = new Point(326, 187);
            cognom.Name = "cognom";
            cognom.Size = new Size(255, 27);
            cognom.TabIndex = 6;
            cognom.TextChanged += cognom_TextChanged;
            // 
            // curs
            // 
            curs.Location = new Point(326, 228);
            curs.Name = "curs";
            curs.Size = new Size(255, 27);
            curs.TabIndex = 7;
            curs.TextChanged += curs_TextChanged;
            // 
            // error
            // 
            error.AutoSize = true;
            error.Location = new Point(12, 351);
            error.Name = "error";
            error.Size = new Size(0, 20);
            error.TabIndex = 9;
            error.Click += label5_Click;
            // 
            // proces_actiu
            // 
            proces_actiu.AutoSize = true;
            proces_actiu.Location = new Point(703, 26);
            proces_actiu.Name = "proces_actiu";
            proces_actiu.Size = new Size(0, 20);
            proces_actiu.TabIndex = 10;
            proces_actiu.Click += label5_Click_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(594, 27);
            label5.Name = "label5";
            label5.Size = new Size(96, 20);
            label5.TabIndex = 11;
            label5.Text = "Executant-se:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(594, 59);
            label6.Name = "label6";
            label6.Size = new Size(92, 20);
            label6.TabIndex = 12;
            label6.Text = "Queue Total:";
            // 
            // totalq
            // 
            totalq.AutoSize = true;
            totalq.Location = new Point(703, 59);
            totalq.Name = "totalq";
            totalq.Size = new Size(0, 20);
            totalq.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(totalq);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(proces_actiu);
            Controls.Add(error);
            Controls.Add(curs);
            Controls.Add(cognom);
            Controls.Add(nom);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(submit);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button submit;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox nom;
        private TextBox cognom;
        private TextBox curs;
        private Label error;
        private Label proces_actiu;
        private Label label5;
        private Label label6;
        private Label totalq;
    }
}
