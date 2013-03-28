using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;
using System.Runtime.InteropServices;
namespace Check
{

    public partial class EmailAddressASk : Form
    {
        private string _content = string.Empty;
        public delegate void MessageReceivedHandler(
            string message);
        public event MessageReceivedHandler MessageReceived;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        public EmailAddressASk()
        {
            InitializeComponent();
        }

        
        private void Proceed_Click(object sender, EventArgs e)
        {




           
            string textBoxText = string.Empty;
            textBoxText = this.txtBoxEmail.Text;
            //MessageBox.Show(textBoxText);
            _content = this.txtBoxEmail.Text;
            //MessageBox.Show(_content);



            // Compose a string that consists of three lines.
            string lines = _content;

            // Write the string to a file.
            System.IO.StreamWriter file =
                new System.IO.StreamWriter(System.IO.Path.GetTempPath() + "\\EmailID.txt", false);
            file.WriteLine(lines);

            file.Close();


            string data = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\EmailID.txt");

            //System.Windows.Forms.MessageBox.Show(data);  




            string _companyName = this.companyName.Text;

            // Write the string to a file.
            System.IO.StreamWriter fileCompany =
                new System.IO.StreamWriter(System.IO.Path.GetTempPath() + "\\CompanyName.txt", false);
            fileCompany.WriteLine(_companyName);

            fileCompany.Close();


            string _name = this.name.Text;

            // Write the string to a file.
            System.IO.StreamWriter file_name =
                new System.IO.StreamWriter(System.IO.Path.GetTempPath() + "\\name.txt", false);
            file_name.WriteLine(_name);

            file_name.Close();




            //fire message received event
            if (this.MessageReceived != null)
            {
              // MessageBox.Show(_content, "saasdsd");
               // this.MessageReceived(
                //    _content.ToString());
           }

           // else
           // {
                //MessageBox.Show(_content, "saasdsd");
           // }



            //Check theSchedule = new Check();

            System.Threading.Thread LaunchScanForm = new System.Threading.Thread(ScanForm); 
            LaunchScanForm.Name = "LaunchScanForm";
            LaunchScanForm.Start();

            //LaunchScanForm();

        }

        private void ScanForm()
        {
            LaunchScanForm();
        }


        private void LaunchScanForm()
        {
            Check thepop = new Check();


            if ((Application.OpenForms["Check"] as Check) != null)
            {
                //bring window to the front

                //MakeTopMost(theSchedule);


                ShowWindowAsTopMost Show = new ShowWindowAsTopMost();
                Show.setWindowPosition(thepop);

                //MessageBox.Show("Application.OpenForms[Check] as Check");

            }

            else
            {
                Application.EnableVisualStyles();
                Application.Run(thepop);
                //MessageBox.Show("pplication.Run(thepop)");
            }
        }





        private void name_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void exitBtn_MouseEnter(object sender, EventArgs e)
        {
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            
            this.exitBtn.FlatStyle = FlatStyle.Popup;
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void exitBtn_MouseLeave(object sender, EventArgs e)
        {
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.exitBtn.FlatStyle = FlatStyle.Flat;
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Default;
        }

        #region moveTheFormAround
        //move the form around
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Opacity = 0.5;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                this.Opacity = 1;
            }
        }

       
        private void EmailAddressASk_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Opacity = 0.5;
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                this.Opacity = 1;
            }
        }
        #endregion

        private void exitBtn_MouseDown(object sender, MouseEventArgs e)
        {
            this.exitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.exitBtn.ForeColor = System.Drawing.Color.White;
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Hide();
            
        }

        private void exitBtn_MouseUp(object sender, MouseEventArgs e)
        {
            this.exitBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.exitBtn.ForeColor = System.Drawing.Color.Black;
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Default;

        }

        private void Proceed_MouseEnter(object sender, EventArgs e)
        {
            this.Proceed.FlatStyle = FlatStyle.Popup;
            this.Proceed.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void Proceed_MouseLeave(object sender, EventArgs e)
        {
            this.Proceed.FlatStyle = FlatStyle.Flat;
            this.Proceed.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();

            Application.ExitThread();
        }



    }
}
