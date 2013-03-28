using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using System.Runtime.InteropServices;
using Check.sutherlandsmbpov_toolCreatingIncident;

namespace Check
{
    public partial class popUp : Form
    {
        private string impactCombotext = string.Empty;

        string _impactCombotext
        {
            get
            {
                return impactCombotext;
            }

            set
            {
                impactCombotext = value;
            }
        }


        public popUp()
        {
            InitializeComponent();
            this.Owner = new Check();
        }

        private void popUp_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void showMessage()
        {
            MessageBox.Show(
               "Some Fields are left empty." + "\n" + "Please fill them.",
               "Empty Fields Detected",
               MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //submit


            //gettext

            //short description
            string _shortDescription = string.Empty;
            _shortDescription = this.shortDescription.Text;

            //describe
            string _describe = string.Empty;
            _describe = this.describe.Text;

            //impact combo
            string IMPACTComboboxText = string.Empty;
            IMPACTComboboxText = _impactCombotext;



            if (_shortDescription.Equals(string.Empty) &&
                _describe.Equals(string.Empty) &&
                IMPACTComboboxText.Equals(string.Empty))
            {
                ////this Or 
                //NativeWindow nativeWindow = new NativeWindow();
                //nativeWindow.AssignHandle(this.Handle);

                ////this
                //IntPtr myWindowHandle = this.Handle;
                //IWin32Window win32WindowHandle = 
                //    Control.FromHandle(myWindowHandle);
                //MessageBox.Show(win32WindowHandle, 
                //    "Some Fields are left empty." + "\n" + "Please fill them.", 
                //    "Empty Fields Detected", 
                //    MessageBoxButtons.OK);
                System.Threading.Thread T = new System.Threading.Thread(showMessage);
                T.Name = "showMessage";
                T.Start();



            }

            else
            {

                string comboBoxRealText = string.Empty;
                switch (_impactCombotext)
                {
                    case "1 - High":
                        comboBoxRealText = "High";
                        break;
                    case "2 - Medium":
                        comboBoxRealText = "Medium";
                        break;
                    case "3 - Low":
                        comboBoxRealText = "Low";
                        break;
                }


                //call webservice


                //instantiate myService to webreference


                sutherlandsmbpov_toolCreatingIncident.ServiceNow_u_sgs_tool_creating_incident
                    _ServiceNow_u_sgs_tool_creating_incident = new ServiceNow_u_sgs_tool_creating_incident();

                //add login credentials

                System.Net.NetworkCredential _credentials =
                    new System.Net.NetworkCredential();

                _credentials.UserName = "admin";
                _credentials.Password = "admin";

                _ServiceNow_u_sgs_tool_creating_incident.Credentials =
                    _credentials;

                //instantiate the object to call which will be a
                //function in the webservice I intent
                //to use

                sutherlandsmbpov_toolCreatingIncident.insert _insertObjt =
                    new insert();

                //read name company name
                string data_name = string.Empty;
                string data2_company = string.Empty;

                data_name = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\name.txt");
                data2_company = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\CompanyName.txt");

                //combo
                _insertObjt.u_impact = comboBoxRealText;

                //multilinetextBox
                _insertObjt.u_issues_explained_in_detail = _describe;

                string dataScanID = string.Empty;
                dataScanID = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\scanID-Scan.txt");

                if (string.IsNullOrEmpty(dataScanID))
                {
                    Random _random = new Random();
                    dataScanID = _random.Next().ToString();
                }

                _insertObjt.u_scan_id = dataScanID;

                _insertObjt.u_short_description = _shortDescription;

                _insertObjt.u_user_name_re_ting_the_issue = data_name;

                _insertObjt.u_users_company_name = data2_company;


                //instantiate the insertResponse to upload

                sutherlandsmbpov_toolCreatingIncident.insertResponse _insertResponse =
                    new insertResponse();

                try
                {
                    _insertResponse = _ServiceNow_u_sgs_tool_creating_incident.insert(
                        _insertObjt);

                    MessageBox.Show("Upload Successful", "Success");


                    this.Dispose();
                    this.Close();



                }
                catch (Exception eXception)
                {
                    System.Diagnostics.Debug.WriteLine(eXception.Message
                        + Environment.NewLine
                        + eXception.StackTrace
                        );

                    MessageBox.Show("Upload Failed", "Failure");
                }

                finally
                {
                    if (_insertResponse != null)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            _insertResponse.status_message
                            + "\n"
                            + _insertResponse.sys_id
                            + "\n"
                            );
                    }



                }


            }

        }

        private void impactCombo_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void impactCombo_TextChanged(object sender, EventArgs e)
        {
            // Called whenever text changes.
            _impactCombotext = this.impactCombo.Text.ToString();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
                this.Close();
                this.Dispose();
        }

        private void exitBtn_MouseEnter(object sender, EventArgs e)
        {
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.exitBtn.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.exitBtn.FlatStyle = FlatStyle.Popup;
        }

        private void exitBtn_MouseLeave(object sender, EventArgs e)
        {
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Default;
            this.exitBtn.FlatStyle = FlatStyle.Flat;
            this.exitBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.exitBtn.ForeColor = System.Drawing.Color.Black;
        }

        #region moveTheFormAround
        //move the form around
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void popUp_MouseDown(object sender, MouseEventArgs e)
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

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = FlatStyle.Popup;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.FlatStyle = FlatStyle.Flat;
            
        }

    }
}
