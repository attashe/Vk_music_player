namespace Vk_Music_Player
{
    partial class Authority
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Login = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.StartAuth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пароль";
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(82, 30);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(119, 20);
            this.Login.TabIndex = 2;
            this.Login.Text = "mihailov_@bk.ru";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(83, 56);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(118, 20);
            this.Password.TabIndex = 3;
            this.Password.Text = "q1w2e3r4t5y6";
            this.Password.UseSystemPasswordChar = true;
            // 
            // StartAuth
            // 
            this.StartAuth.Location = new System.Drawing.Point(18, 108);
            this.StartAuth.Name = "StartAuth";
            this.StartAuth.Size = new System.Drawing.Size(120, 33);
            this.StartAuth.TabIndex = 4;
            this.StartAuth.Text = "OK";
            this.StartAuth.UseVisualStyleBackColor = true;
            this.StartAuth.Click += new System.EventHandler(this.button1_Click);
            // 
            // Authority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 162);
            this.Controls.Add(this.StartAuth);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Authority";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authority";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button StartAuth;
    }
}