using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dechifr_client
{
    public partial class Form1 : Form
    {
        //for login button activation
        bool TB_username = false;
        bool TB_password = false;
        bool TB_appToken = false;

        String apptoken = "F5BwBkWzvL1hGT8zHk8bPlw975VHMz";

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGetFile_Click(object sender, EventArgs e)
        {
            //TODO : is logged in ?

            OpenFileDialog d = new OpenFileDialog();
            //only one selected file at once
            d.Filter = "Text files | *.txt";
            d.Multiselect = false;

            //when OK is clicked what do we do
            if (d.ShowDialog() == DialogResult.OK)
            {
                //TODO

                if(d.CheckFileExists)
                {
                    String path = d.FileName;
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Login button clicked");

            //Check if apptoken is valid
            if (string.Compare(textBox_appToken.Text, apptoken) == 0)
            {
                Console.WriteLine("App token valid");
            } else {
                Console.WriteLine("App token invalid");
            }

            //check if the 3 textBoxes are not empty
            if (textBox_username.TextLength != 0 &&
               textBox_password.TextLength != 0 &&
               textBox_appToken.TextLength != 0)
            {
   
            } else {
                //disable button else
                login_btn.Enabled = false;
            }
        }

        private void textBox_username_TextChanged(object sender, EventArgs e)
        {
            TB_username = true;

            if(TB_appToken == true && TB_password == true)
            {
                login_btn.Enabled = true;
            }
        }

        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            TB_password = true;

            if(TB_username == true && TB_appToken == true)
            {
                login_btn.Enabled = true;
            }
        }

        private void textBox_appToken_TextChanged(object sender, EventArgs e)
        {
            TB_appToken = true;

            if(TB_password == true && TB_username == true)
            {
                login_btn.Enabled = true;
            }
        }
    }
}
