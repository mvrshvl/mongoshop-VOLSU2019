namespace dbuniver
{
    partial class Form2
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
            this.login = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.checkLogin = new System.Windows.Forms.Button();
            this.closeDialog = new System.Windows.Forms.Button();
            this.error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин";
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(122, 31);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(166, 20);
            this.login.TabIndex = 1;
            this.login.Text = "u1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Пароль";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(122, 69);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(166, 20);
            this.password.TabIndex = 3;
            this.password.Text = "12345678";
            // 
            // checkLogin
            // 
            this.checkLogin.Location = new System.Drawing.Point(213, 135);
            this.checkLogin.Name = "checkLogin";
            this.checkLogin.Size = new System.Drawing.Size(75, 23);
            this.checkLogin.TabIndex = 4;
            this.checkLogin.Text = "OK";
            this.checkLogin.UseVisualStyleBackColor = true;
            this.checkLogin.Click += new System.EventHandler(this.CheckLogin_Click);
            // 
            // closeDialog
            // 
            this.closeDialog.BackColor = System.Drawing.SystemColors.ControlLight;
            this.closeDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.closeDialog.Location = new System.Drawing.Point(122, 135);
            this.closeDialog.Name = "closeDialog";
            this.closeDialog.Size = new System.Drawing.Size(75, 23);
            this.closeDialog.TabIndex = 5;
            this.closeDialog.Text = "Отмена";
            this.closeDialog.UseVisualStyleBackColor = false;
            this.closeDialog.Click += new System.EventHandler(this.CloseDialog_Click);
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.ForeColor = System.Drawing.Color.Red;
            this.error.Location = new System.Drawing.Point(12, 109);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(0, 13);
            this.error.TabIndex = 6;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 170);
            this.Controls.Add(this.error);
            this.Controls.Add(this.closeDialog);
            this.Controls.Add(this.checkLogin);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.login);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.Text = "Авторизация";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button checkLogin;
        private System.Windows.Forms.Button closeDialog;
        private System.Windows.Forms.Label error;
    }
}