using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.ServiceModel;
using System.Windows.Forms;
using dechifr_client.BrutusControl;

namespace dechifr_client
{
    public partial class Form1 : Form
    {
        readonly BrutusControlClient bcc = new BrutusControlClient("projectEndpoint2");
        
        //index that is used to create the dynamic controls
        static int dyn_index = 1;

        //selected file stream
        string filepath;

        //for login button activation
        bool TB_username = false;
        bool TB_password = false;
        bool TB_appToken = false;

        bool TB_fileselected = false;
        bool TB_connectionTokenEntered = false;

        Authenticate t = new Authenticate();

        //const string apptoken = "F5BwBkWzvL1hGT8zHk8bPlw975VHMz";
        const string apptoken = "A";

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGetFile_Click(object sender, EventArgs e)
        {
            if (t.checkUserToken(textBox_username.Text, textbox_connectionToken.Text))
            {
                OpenFileDialog d = new OpenFileDialog();
                d.Filter = "Text files | *.txt";
                d.Multiselect = false;

                //when OK is clicked what do we do
                if (d.ShowDialog() == DialogResult.OK)
                {
                    if (d.CheckFileExists && d.CheckPathExists)
                    {
                        label_filePath.Text = d.FileName;
                        filepath = d.FileName;

                        //activate send file button
                        TB_fileselected = true;
                        if(TB_connectionTokenEntered)
                        {
                            btn_sendFile.Enabled = true;
                        }
                    }
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            //Check if apptoken is valid
            if (string.Compare(textBox_appToken.Text, apptoken) == 0)
            {
                Console.WriteLine("App token valid");

                //check if the 3 textBoxes are not empty
                if (textBox_username.TextLength != 0 &&
                   textBox_password.TextLength != 0 &&
                   textBox_appToken.TextLength != 0)
                {
                    //generate connection token
                    string connectionToken = t.connect(textBox_username.Text,
                                                       textBox_password.Text,
                                                       textBox_appToken.Text);

                    //if connectionToken is "invalid" then the connect failed
                    if (string.Compare(connectionToken, "invalid") == 0)
                    {
                        //connection failed
                        Console.WriteLine("Connection failed");
                        status_label.Text = "Connection failed";
                    } else
                    {

                        //if the connectionToken is valid we let the user know by displaying it and by putting it in the token textbox
                        Console.WriteLine("Connection successfull");
                        status_label.Text = "connectionToken : " + connectionToken;
                        textbox_connectionToken.Text = connectionToken;

                        TB_connectionTokenEntered = true;
                        if(TB_fileselected)
                        {
                            btn_sendFile.Enabled = true;
                        }

                    }
                }
            } else {//if app token is invalid
                status_label.Text = "App Token Invalid";
                Console.WriteLine("App token invalid");
            }
        }

        /*
         Button that sends the file to the brute force
             */
        private void btn_sendFile_Click(object sender, EventArgs e)
        {
            Console.WriteLine("file sended");

            string msg = File.ReadAllText(filepath);
            Console.WriteLine("File is {0}", msg);

            //create new form module
            BfControl bf = new BfControl();
            bf.Location = new Point(293, (dyn_index * 61));
            
            CancellationTokenSource cts = new CancellationTokenSource();
            bcc.startBrutus(msg, label_filePath.Text, cts);
            
            dyn_index++;
            bf.setLabel(label_filePath.Text);
            bf.button1.Click += (o, i) => {
                Console.WriteLine("Thread killed");
                cts.Cancel();
                bf.Dispose();
                --dyn_index;
            };
            mainFloatPanel.Controls.Add(bf);
        }

        /*
         Next three methods are used to activate the login button when the three previous fields are not empty
             */
        private void textBox_username_TextChanged(object sender, EventArgs e)
        {
            TB_username = true;

            if(TB_appToken && TB_password)
            {
                login_btn.Enabled = true;
            }
        }
        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            TB_password = true;

            if(TB_username && TB_appToken)
            {
                login_btn.Enabled = true;
            }
        }
        private void textBox_appToken_TextChanged(object sender, EventArgs e)
        {
            TB_appToken = true;

            if(TB_password && TB_username)
            {
                login_btn.Enabled = true;
            }
        }
    }
}
