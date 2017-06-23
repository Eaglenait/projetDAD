﻿namespace dechifr_client
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
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.fileselect_btn = new System.Windows.Forms.Button();
            this.username_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.textBox_appToken = new System.Windows.Forms.TextBox();
            this.app_token_label = new System.Windows.Forms.Label();
            this.status_label = new System.Windows.Forms.Label();
            this.textbox_connectionToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label_filePath = new System.Windows.Forms.Label();
            this.btn_sendFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // fileselect_btn
            // 
            this.fileselect_btn.Location = new System.Drawing.Point(15, 179);
            this.fileselect_btn.Name = "fileselect_btn";
            this.fileselect_btn.Size = new System.Drawing.Size(72, 21);
            this.fileselect_btn.TabIndex = 0;
            this.fileselect_btn.Text = "Select File";
            this.fileselect_btn.UseVisualStyleBackColor = true;
            this.fileselect_btn.Click += new System.EventHandler(this.buttonGetFile_Click);
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Location = new System.Drawing.Point(12, 9);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(55, 13);
            this.username_label.TabIndex = 1;
            this.username_label.Text = "Username";
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(12, 35);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(53, 13);
            this.password_label.TabIndex = 2;
            this.password_label.Text = "Password";
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(75, 6);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(173, 20);
            this.textBox_username.TabIndex = 3;
            this.textBox_username.TextChanged += new System.EventHandler(this.textBox_username_TextChanged);
            // 
            // textBox_password
            // 
            this.textBox_password.Location = new System.Drawing.Point(75, 32);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(173, 20);
            this.textBox_password.TabIndex = 0;
            this.textBox_password.TextChanged += new System.EventHandler(this.textBox_password_TextChanged);
            // 
            // login_btn
            // 
            this.login_btn.Enabled = false;
            this.login_btn.Location = new System.Drawing.Point(12, 88);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(46, 23);
            this.login_btn.TabIndex = 4;
            this.login_btn.Text = "Login";
            this.login_btn.UseVisualStyleBackColor = true;
            this.login_btn.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBox_appToken
            // 
            this.textBox_appToken.Location = new System.Drawing.Point(75, 58);
            this.textBox_appToken.Name = "textBox_appToken";
            this.textBox_appToken.Size = new System.Drawing.Size(173, 20);
            this.textBox_appToken.TabIndex = 5;
            this.textBox_appToken.TextChanged += new System.EventHandler(this.textBox_appToken_TextChanged);
            // 
            // app_token_label
            // 
            this.app_token_label.AutoSize = true;
            this.app_token_label.Location = new System.Drawing.Point(12, 61);
            this.app_token_label.Name = "app_token_label";
            this.app_token_label.Size = new System.Drawing.Size(60, 13);
            this.app_token_label.TabIndex = 6;
            this.app_token_label.Text = "App Token";
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Location = new System.Drawing.Point(290, 13);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(95, 13);
            this.status_label.TabIndex = 8;
            this.status_label.Text = "Please select a file";
            // 
            // textbox_connectionToken
            // 
            this.textbox_connectionToken.Location = new System.Drawing.Point(15, 153);
            this.textbox_connectionToken.Name = "textbox_connectionToken";
            this.textbox_connectionToken.Size = new System.Drawing.Size(134, 20);
            this.textbox_connectionToken.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Connection token";
            // 
            // label_filePath
            // 
            this.label_filePath.AutoSize = true;
            this.label_filePath.Location = new System.Drawing.Point(93, 183);
            this.label_filePath.Name = "label_filePath";
            this.label_filePath.Size = new System.Drawing.Size(85, 13);
            this.label_filePath.TabIndex = 13;
            this.label_filePath.Text = "please select file";
            // 
            // btn_sendFile
            // 
            this.btn_sendFile.Enabled = false;
            this.btn_sendFile.Location = new System.Drawing.Point(15, 219);
            this.btn_sendFile.Name = "btn_sendFile";
            this.btn_sendFile.Size = new System.Drawing.Size(72, 21);
            this.btn_sendFile.TabIndex = 14;
            this.btn_sendFile.Text = "Send File";
            this.btn_sendFile.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 479);
            this.Controls.Add(this.btn_sendFile);
            this.Controls.Add(this.label_filePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textbox_connectionToken);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.app_token_label);
            this.Controls.Add(this.textBox_appToken);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.textBox_password);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.username_label);
            this.Controls.Add(this.fileselect_btn);
            this.Name = "Form1";
            this.Text = "Decrypt client";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button fileselect_btn;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button login_btn;
        private System.Windows.Forms.TextBox textBox_appToken;
        private System.Windows.Forms.Label app_token_label;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.TextBox textbox_connectionToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_filePath;
        private System.Windows.Forms.Button btn_sendFile;
    }
}

