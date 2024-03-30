namespace COOLEST_APP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dwnbtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menubtn = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.AccessibleDescription = "i forgor";
            this.button1.AccessibleName = "btn1";
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Click Me 😎";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox
            // 
            this.textBox.AutoSize = true;
            this.textBox.Location = new System.Drawing.Point(28, 38);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(35, 13);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "label1";
            this.textBox.Visible = false;
            this.textBox.Click += new System.EventHandler(this.textBox_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(175, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Change Installation Location";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please Select a Path.";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dwnbtn
            // 
            this.dwnbtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dwnbtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dwnbtn.Location = new System.Drawing.Point(612, 387);
            this.dwnbtn.Name = "dwnbtn";
            this.dwnbtn.Size = new System.Drawing.Size(116, 43);
            this.dwnbtn.TabIndex = 4;
            this.dwnbtn.Text = "Install";
            this.dwnbtn.UseVisualStyleBackColor = false;
            this.dwnbtn.Click += new System.EventHandler(this.dwnbtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 394);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(521, 30);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // menubtn
            // 
            this.menubtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menubtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.menubtn.Location = new System.Drawing.Point(723, 387);
            this.menubtn.Name = "menubtn";
            this.menubtn.Size = new System.Drawing.Size(38, 43);
            this.menubtn.TabIndex = 6;
            this.menubtn.Text = "☰";
            this.menubtn.UseVisualStyleBackColor = false;
            this.menubtn.Click += new System.EventHandler(this.menubtn_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel.Controls.Add(this.button4);
            this.panel.Controls.Add(this.button2);
            this.panel.Controls.Add(this.label2);
            this.panel.Location = new System.Drawing.Point(580, 281);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(181, 100);
            this.panel.TabIndex = 7;
            this.panel.Visible = false;
            this.panel.MouseLeave += new System.EventHandler(this.panel_Exit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 378);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(289, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Downloading Game Resources 34% (234.54MB/650.51MB)";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 427);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Download Speed 8.47MB/s";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 427);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Estimated remaining time: 0h 0m 0s";
            this.label4.Visible = false;
            // 
            // button3
            // 
            this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button3.Location = new System.Drawing.Point(542, 395);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(33, 27);
            this.button3.TabIndex = 11;
            this.button3.Text = "┃┃";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 45);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(175, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "Clear Downloaded Resources";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menubtn);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dwnbtn);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dwnbtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        protected System.Windows.Forms.Label textBox;
        private System.Windows.Forms.Button menubtn;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

