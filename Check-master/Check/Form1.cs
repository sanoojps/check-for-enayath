using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using System.Runtime.InteropServices;
namespace Check
{
    public partial class Check : Form
    {

        private EmailAddressASk pipeClient;

        private string emailID = string.Empty;

        public string EmailID
        {
            get{return emailID;}
            set{this.emailID = value;}
        }



        

        Consume _consume = new Consume();
        public Check()
        {

            InitializeComponent();
            this.Owner = new EmailAddressASk();
            this.pipeClient = new EmailAddressASk();
            this.pipeClient.MessageReceived += pipeClient_MessageReceived;

        }

        void pipeClient_MessageReceived(string message)
        {
            this.Invoke(new EmailAddressASk.MessageReceivedHandler(DisplayReceivedMessage),
                new object[] { message });
        }

        
        void DisplayReceivedMessage(string message)
        {
            this.emailID = message;
        }


        void Scan_Click(object sender, EventArgs e)
        {
           
            System.Threading.Thread T = new Thread(_consume.JealousSCANServ);
            T.Name = "_consume.JealousSCANServ";
            T.Start();
            updateScanlabel();

        }

        private void Fix_Click(object sender, EventArgs e)
        {
           
            System.Threading.Thread T = new Thread(_consume.JealousFIXServ);
            T.Name = "_consume.JealousFIXServ";
            T.Start();

            updateFixlabel();


        }


        #region updateScanlabel
        private void updateScanlabel()
        {
           
            this.progressBar1.Value = 35;
            this.progressBar1.Show();


            CheckServ servistatus = new CheckServ();

            string VSS_status = string.Empty;
            string wuauserv_status = string.Empty;
            string BITS_status = string.Empty;
            string CryptSvc_status = string.Empty;
            string PlugPlay_status = string.Empty;

            string Spooler_status = string.Empty;
            string srservice_status = string.Empty;

            string WerSvc_status = string.Empty;
            string TermService_status = string.Empty;
            string LanmanWorkstation_status = string.Empty;
            string NlaSvc_status = string.Empty;
            string LanmanServer_status = string.Empty;
            //calling service Check

            servistatus.CheckStatus(
                out VSS_status,
            out wuauserv_status,
           out BITS_status,
           out CryptSvc_status,
             out PlugPlay_status,
             out Spooler_status,
           out srservice_status,

           out WerSvc_status,
            out TermService_status,
            out LanmanWorkstation_status,
            out NlaSvc_status,
            out LanmanServer_status
           );



            //temp dir Size;

            DirSize nu = new DirSize();

            //for %temp%
            System.IO.DirectoryInfo path =
                new System.IO.DirectoryInfo(System.IO.Path.GetTempPath());
            nu.WalkDirectoryTree(path);

            // Console.WriteLine("Dir: " + System.IO.Path.GetTempPath() +
            //  Environment.NewLine + "Size: " + nu.Number / (1000 * 1000) + " MB ");

            long tempDirSize = nu.Number;

            //for Windows Temp
            nu.Number = 0;

            System.IO.DirectoryInfo NUpath =
                new System.IO.DirectoryInfo(System.IO.Path.GetPathRoot(
               Environment.SystemDirectory) + @"Windows\Temp");
            nu.WalkDirectoryTree(NUpath);

            //  Console.WriteLine("Dir: "
            //  + System.IO.Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\Temp"
            //     + Environment.NewLine
            //    + "Size: " + nu.Number / (1000 * 1000) + " MB ");

            long _tempDirSize = nu.Number;


            long TotalSize = (tempDirSize + _tempDirSize) / (1024 * 1024);

            //OS Version

            GetSystemInfo _GetSystemInfo =
                new GetSystemInfo();

            _GetSystemInfo.getOSVersion();

            string data = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\EmailID.txt");
            string data2 = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\scanID-Scan.txt");

            /*
            this.ResultLabel.Text =
                "EventType :                                    Scan" + Environment.NewLine
                + VSS_status + "\n"
            + wuauserv_status + "\n"
           + BITS_status + "\n"
            + CryptSvc_status + "\n"
             + PlugPlay_status + "\n"
            + Spooler_status + "\n"
           + srservice_status + "\n" +
           "Scan ID :                                       " + data2 
                //+ System.IO.Path.GetTempPath() + "    " + tempDirSize + "\n"
                //+ System.IO.Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\Temp" + "    " + _tempDirSize
            + "\n" + "Total Temp Dir Size :                                 " + TotalSize + " MB " + "\n"
            + "System Drive Free Space " + 
            "[" + System.IO.Path.GetPathRoot(Environment.SystemDirectory) + "]" 
            + " :                               " + nu.SystemDriveFreeSpace() + @" % " + "\n"
            + "IE Version :                                  " + nu.GetIEVersion() + Environment.NewLine
            + "OS Version :                                  " + Environment.NewLine + _GetSystemInfo.getOSVersion().ToString() + Environment.NewLine
            + "MAC Address :                                   " + _GetSystemInfo.GetMacAddress().ToString() + Environment.NewLine
            + "System Uptime :                                  " + _GetSystemInfo.getSystemUptime() + Environment.NewLine
            + "Date :                                              " + DateTime.Now + Environment.NewLine
            + "Email ID :                                     " + data 
           


            ;
             * 
             */

            this.label1.Text = "Scan ID";
            
            this.label2.Text = "Event Type";
            
            //this.label3.Text = VSS_status;
            //this.label4.Text = wuauserv_status;
            //this.label5.Text = BITS_status;
            //this.label6.Text = CryptSvc_status;
            //this.label7.Text = PlugPlay_status;
            //this.label8.Text = Spooler_status;
            //this.label9.Text = "System Restore Service";
            //this.label9.Text = srservice_status;
            //this.label10.Text = "Total Temp Dir Size";
            this.label11.Text = "Free Space " +
            "[" + System.IO.Path.GetPathRoot(Environment.SystemDirectory) + "]";
            
            this.label12.Text = "IE Version";
            
            this.label13.Text = "OS Version";
            
            this.label14.Text = "MAC Address";
            
            this.label15.Text = "System Uptime";
            
            this.label16.Text = "Date";
            
            this.label17.Text = "Email ID";
            

            //values


            this.label18.Text = ":       " + data2;
            
            this.label19.Text = ":       Scan";
            

            this.label20.Text = "";
            
            this.label21.Text = "";
            
            this.label22.Text = "";
            
            this.label23.Text = "";
            
            this.label24.Text = "";
            
            this.label25.Text = "";
            
            this.label26.Text = "";
            


            this.label27.Text = ":       " + TotalSize + " MB ";
            
            this.label28.Text = ":       " + nu.SystemDriveFreeSpace() + @" % ";
            
            this.label29.Text = ":       " + nu.GetIEVersion();
            
            this.label30.Text = ":       " + _GetSystemInfo.getOSVersion().ToString();
            
            this.label31.Text = ":       " + _GetSystemInfo.GetMacAddress().ToString();
            
            this.label32.Text = ":       " + _GetSystemInfo.getSystemUptime();
            
            this.label33.Text = ":       " + DateTime.Now;
            
            this.label34.Text = ":       " + data;

            this.progressBar1.Value = 75;

            string pcModel = string.Empty;
            string pcName = string.Empty;
            string pcTotalPhysicalMemory = string.Empty;
            string pcUserName = string.Empty;
            string CurrentClockSpeed = string.Empty;
            string processorManufacturer = string.Empty;
            string MaxClockSpeed = string.Empty;
            string processorName = string.Empty;
            string manufacturerName = string.Empty;

            manufacturerName = _GetSystemInfo.getSystemManufacturer(
            out  pcModel, out  pcName, out  pcTotalPhysicalMemory, out pcUserName);

            processorName = _GetSystemInfo.getProcessorInfo(
             out  CurrentClockSpeed, out  processorManufacturer, out  MaxClockSpeed);


            this.label44.Text = ":       " + manufacturerName;
            
            this.label45.Text = ":       " + pcModel;
            
            this.label46.Text = ":       " + pcName;
            
            this.label47.Text = ":       " + pcTotalPhysicalMemory;
            
            this.label48.Text = ":       " + pcUserName;
            
            this.label49.Text = ":       " + processorName;
            
            this.label50.Text = ":       " + Convert.ToDecimal(CurrentClockSpeed) / 1000 + " Ghz ";
            
            this.label51.Text = ":       " + processorManufacturer;
            
            this.label52.Text = ":       " + Convert.ToDecimal(MaxClockSpeed) / 1000 + " Ghz ";
            


            this.label59.Text = LanmanServer_status;
            
            this.label60.Text = NlaSvc_status;
            
            this.label61.Text = LanmanWorkstation_status;
            
            this.label62.Text = TermService_status;
            

            this.label64.Text = WerSvc_status;
            

            //query if IEScript Debugging is enabled

            uint readStatus;

            string scriptDebug = string.Empty;

            registryOPs _registryOPs = new registryOPs();

            scriptDebug = _registryOPs.keyRead(@"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main",
                "Disable Script Debugger",
                out readStatus
                );

            string IESCrlabelUpdate = string.Empty;

            if (readStatus == 0)
            {
                if (scriptDebug == "yes")
                {
                    IESCrlabelUpdate = "Disabled";
                }

                else
                {
                    IESCrlabelUpdate = "Enabled";
                }
            }
            else
            {
                IESCrlabelUpdate = "<Failed to retrieve the status>";
            }



            this.label63.Text = ":       " + IESCrlabelUpdate;
            

            this.label65.Text = VSS_status;
            
            this.label66.Text = wuauserv_status;
            
            this.label67.Text = BITS_status;
            
            this.label68.Text = CryptSvc_status;
            
            this.label69.Text = PlugPlay_status;
            
            this.label70.Text = Spooler_status;
            
            this.label71.Text = srservice_status;

            this.progressBar1.Value = 85;

            //read name company name
             string data_name = string.Empty;
             string data2_company = string.Empty;

             data_name = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\name.txt");
            data2_company = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\CompanyName.txt");


            this.label75.Text = ":       " + data2_company;
            
            this.label76.Text = ":       " + data_name;


            this.progressBar1.Value = 100;
            this.progressBar1.Hide();


        }
        #endregion

        #region updateFixlabel
        private void updateFixlabel()
        {
            CheckServ servistatus = new CheckServ();

            string VSS_status;
            string wuauserv_status;
            string BITS_status;
            string CryptSvc_status;
            string PlugPlay_status;

            string Spooler_status;
            string srservice_status;

            //calling service Check

            servistatus.FixStatus(
                out VSS_status,
            out wuauserv_status,
           out BITS_status,
           out CryptSvc_status,
             out PlugPlay_status,
             out Spooler_status,
           out srservice_status
           );



            //temp dir Size;

            DirSize nu = new DirSize();

            //for %temp%
            System.IO.DirectoryInfo path =
                new System.IO.DirectoryInfo(System.IO.Path.GetTempPath());
            nu.WalkDirectoryTree(path);

            // Console.WriteLine("Dir: " + System.IO.Path.GetTempPath() +
            //  Environment.NewLine + "Size: " + nu.Number / (1000 * 1000) + " MB ");

            long tempDirSize = nu.Number;

            //for Windows Temp
            nu.Number = 0;

            System.IO.DirectoryInfo NUpath =
                new System.IO.DirectoryInfo(System.IO.Path.GetPathRoot(
               Environment.SystemDirectory) + @"Windows\Temp");
            nu.WalkDirectoryTree(NUpath);

            //  Console.WriteLine("Dir: "
            //  + System.IO.Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\Temp"
            //     + Environment.NewLine
            //    + "Size: " + nu.Number / (1000 * 1000) + " MB ");

            long _tempDirSize = nu.Number;


            long TotalSize = (tempDirSize + _tempDirSize) / (1024 * 1024);

            //OS Version

            GetSystemInfo _GetSystemInfo =
                new GetSystemInfo();

            _GetSystemInfo.getOSVersion();

            string data = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\EmailID.txt");
            string data2 = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\scanID-Fix.txt");

       //     this.Fixlabel.Text =
       //     "EventType : Fix " + Environment.NewLine
       //     + VSS_status + "\n"
       // + wuauserv_status + "\n"
       //+ BITS_status + "\n"
       // + CryptSvc_status + "\n"
       //  + PlugPlay_status + "\n"
       // + Spooler_status + "\n"
       //+ srservice_status + "\n" +
       //    "Scan ID : " + data2 
       //         //+ System.IO.Path.GetTempPath() + "    " + tempDirSize + "\n"
       //         //+ System.IO.Path.GetPathRoot(Environment.SystemDirectory) + @"Windows\Temp" + "    " + _tempDirSize
       // + "\n" + "Total Temp Dir Size : " + TotalSize + " MB " + "\n"
       // + "System Drive Free Space " + "[" + System.IO.Path.GetPathRoot(Environment.SystemDirectory) + "]" + " : " + nu.SystemDriveFreeSpace() + @" % " + "\n"
       // + "IE Version: " + nu.GetIEVersion() + Environment.NewLine
       // + "OS Version: " + Environment.NewLine + _GetSystemInfo.getOSVersion().ToString() + Environment.NewLine
       //     + "MAC Address : " + _GetSystemInfo.GetMacAddress().ToString() + Environment.NewLine
       //     + "System Uptime : " + _GetSystemInfo.getSystemUptime() + Environment.NewLine
       //     + "Date : " + DateTime.Now + Environment.NewLine
       //      + "Email ID : " + data 
       // ;
        }
        #endregion

        #region events

        private void Check_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void label69_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label63_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        #endregion



        private void button1_Click(object sender, EventArgs e)
        {

            Thread LaunchpopUpForm = new Thread(LaunchPopUPForm); 
                LaunchpopUpForm.Name = "LaunchpopUpForm";
                LaunchpopUpForm.Start();
        }

        private void LaunchPopUPForm()
        {
            LaunchpopUp();
        }


        private void LaunchpopUp()
        {
            popUp thepopUp = new popUp();


            if ((Application.OpenForms["popUp"] as popUp) != null)
            {
                //bring window to the front

                //MakeTopMost(theSchedule);


                ShowWindowAsTopMost Show = new ShowWindowAsTopMost();
                Show.setWindowPosition(thepopUp);

            }

            else
            {
                Application.EnableVisualStyles();
                Application.Run(thepopUp);
            }
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Scan_MouseEnter(object sender, EventArgs e)
        {
            this.Scan.FlatStyle = FlatStyle.Popup;
            this.Scan.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void Scan_MouseLeave(object sender, EventArgs e)
        {
            this.Scan.FlatStyle = FlatStyle.Flat;
            this.Scan.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void reportIssue_MouseEnter(object sender, EventArgs e)
        {
            this.reportIssue.FlatStyle = FlatStyle.Popup;
            this.reportIssue.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void reportIssue_MouseLeave(object sender, EventArgs e)
        {
            this.reportIssue.FlatStyle = FlatStyle.Flat;
            this.reportIssue.Cursor = System.Windows.Forms.Cursors.Default;
        }

        
    }
}
