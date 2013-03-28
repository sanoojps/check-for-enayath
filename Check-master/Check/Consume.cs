using System;
using System.Collections.Generic;
using System.Text;

using Check.sutherlandsmbpov;

namespace Check
{
    class Consume
    {
         private string _content = string.Empty;
       
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

        private EmailAddressASk pipeClient;
        public Consume()
        {
            this.pipeClient =
                new EmailAddressASk();



            this.pipeClient.MessageReceived +=pipeClient_MessageReceived;
            //System.Windows.Forms.MessageBox.Show("Consume called");   
        }

  void pipeClient_MessageReceived(string message)
        {
      System.Windows.Forms.MessageBox.Show(message);
      this._content = message;
        }




        #region JealousSCANServ
        public void JealousSCANServ()
        {
            insertObjectC _insertObjectC = new insertObjectC();


            ////get Service status 

            CheckServ _checkServ = new CheckServ();
            string VSS_status;
            string wuauserv_status;
            string BITS_status;
            string CryptSvc_status;
            string PlugPlay_status;

            string Spooler_status;
            string srservice_status;

            string WerSvc_status;
            string TermService_status;
            string LanmanWorkstation_status;
            string NlaSvc_status;
            string LanmanServer_status;


            //calling service Check

            _checkServ.CheckStatus(
                out VSS_status,
            out wuauserv_status,
           out BITS_status,
           out CryptSvc_status,
             out PlugPlay_status,
             out Spooler_status,
           out srservice_status,

           out  WerSvc_status,
             out  TermService_status,
             out  LanmanWorkstation_status,
             out  NlaSvc_status,
             out  LanmanServer_status
           );


            _insertObjectC.U_background_intelligent = BITS_status;
            _insertObjectC.U_system_restore_service = VSS_status;
            _insertObjectC.U_plug_and_play_service  = PlugPlay_status;
            _insertObjectC.U_print_spooler_service = Spooler_status;
            _insertObjectC.U_cryptographic_service = CryptSvc_status;
            _insertObjectC.U_windows_update_service = wuauserv_status;

            _insertObjectC.U_error_reporting_service = WerSvc_status;
            _insertObjectC.U_network_location_awareness = NlaSvc_status;
            _insertObjectC.U_remote_desktop_service = TermService_status;
            _insertObjectC.U_server_service = LanmanServer_status;
            _insertObjectC.U_workstation = LanmanWorkstation_status;

            //OS Version

            GetSystemInfo _GetSystemInfo =
                new GetSystemInfo();

            _insertObjectC.U_os_version_and_service_pack = 
                _GetSystemInfo.getOSVersion();


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

            _insertObjectC.U_temporary_files_folders_size =
                TotalSize.ToString();

            //Scan ID ---

            Random _random = new Random();

            _insertObjectC.U_scan_id = _random.Next().ToString();



            // Compose a string that consists of three lines.
            string line = _insertObjectC.U_scan_id;

            // Write the string to a file.
            System.IO.StreamWriter fileZ =
                new System.IO.StreamWriter(System.IO.Path.GetTempPath() + "\\ScanID-Scan.txt", false);
            fileZ.WriteLine(line);

            fileZ.Close();

          

            //MAC ID

            _insertObjectC.U_mac_id = _GetSystemInfo.GetMacAddress();

            //DriveFreeSpace



            _insertObjectC.U_hard_disk_space =
                _GetSystemInfo.SystemDriveFreeSpace().ToString();


            //IE Version

            _insertObjectC.U_ie_version = 
                _GetSystemInfo.GetIEVersion();

          //event Type

            _insertObjectC.U_event_type = "Scan";

            //system uptime

            _insertObjectC.U_system_uptime =
                _GetSystemInfo.getSystemUptime();






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


            _insertObjectC.U_pc_model = pcModel;
            _insertObjectC.U_pc_name = pcName;
            _insertObjectC.U_total_physical_memory = pcTotalPhysicalMemory;
            _insertObjectC.U_user_name = pcUserName;
            _insertObjectC.U_current_clock_speed = CurrentClockSpeed;
            _insertObjectC.U_processor_manufacturer = processorManufacturer;
            _insertObjectC.U_max_clock_speed = MaxClockSpeed;
            _insertObjectC.U_processor_name = processorName;
            _insertObjectC.U_system_manufacturer = manufacturerName;




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


            _insertObjectC.U_ie_script_debugging = IESCrlabelUpdate;







            //instantiate myService to webreference

            sutherlandsmbpov.ServiceNow_u_poc_snf
                _service = new sutherlandsmbpov.ServiceNow_u_poc_snf();


            //adding login credentials

            System.Net.NetworkCredential _credentials =
                new System.Net.NetworkCredential();

            _credentials.UserName = "admin";
            _credentials.Password = "admin";

            _service.Credentials = _credentials;

            //instantiate the object to call which will be a
            //function in the webservice I intent
            //to use
            sutherlandsmbpov.insert _objinsert = 
                new sutherlandsmbpov.insert();

            //System.Windows.Forms.MessageBox.Show(_objinsert.ToString());
            
            //add values to the object

            _objinsert.u_background_intelligent =
                _insertObjectC.U_background_intelligent;

            _objinsert.u_cryptographic_service =
                _insertObjectC.U_cryptographic_service;

            _objinsert.u_event_type =
                _insertObjectC.U_event_type;

            _objinsert.u_hard_disk_space =
                _insertObjectC.U_hard_disk_space;

            _objinsert.u_ie_version =
                _insertObjectC.U_ie_version;

            _objinsert.u_mac_id =
                _insertObjectC.U_mac_id;

            _objinsert.u_os_version_and_service_pack =
                _insertObjectC.U_os_version_and_service_pack;

            _objinsert.u_plug_and_play_service =
                _insertObjectC.U_plug_and_play_service;

            _objinsert.u_print_spooler_service =
                _insertObjectC.U_print_spooler_service;

            _objinsert.u_scan_id =
                _insertObjectC.U_scan_id;

            _objinsert.u_system_restore_service =
                _insertObjectC.U_system_restore_service;

            _objinsert.u_system_uptime =
                _insertObjectC.U_system_uptime;

            _objinsert.u_temporary_files_folders_size =
                _insertObjectC.U_temporary_files_folders_size;

            _objinsert.u_windows_update_service =
                _insertObjectC.U_windows_update_service;











            string data = System.IO.File.ReadAllText(System.IO.Path.GetTempPath() + "\\EmailID.txt");



            _objinsert.u_email_address = data;




            _objinsert.u_system_manufacturer =
                _insertObjectC.U_system_manufacturer;

            _objinsert.u_pc_model =
                _insertObjectC.U_pc_model;

            _objinsert.u_pc_name = 
                _insertObjectC.U_pc_name;

            _objinsert.u_total_physical_memory =
                _insertObjectC.U_total_physical_memory;

            _objinsert.u_user_name =
                _insertObjectC.U_user_name;

            _objinsert.u_processor_name =
                _insertObjectC.U_processor_name;

            _objinsert.u_current_clock_speed =
                _insertObjectC.U_current_clock_speed;

            _objinsert.u_processor_manufacturer =
                _insertObjectC.U_processor_manufacturer;

            _objinsert.u_max_clock_speed =
                _insertObjectC.U_max_clock_speed;

            _objinsert.u_server_service =
                _insertObjectC.U_server_service;

            _objinsert.u_network_location_awareness =
                _insertObjectC.U_network_location_awareness;

            _objinsert.u_workstation =
                _insertObjectC.U_workstation;

            _objinsert.u_remote_desktop_service =
                _insertObjectC.U_remote_desktop_service;

            _objinsert.u_ie_script_debugging =
                _insertObjectC.U_ie_script_debugging;

            _objinsert.u_error_reporting_service =
                _insertObjectC.U_error_reporting_service;

            

            //instantiate the insertResponse to upload

            sutherlandsmbpov.insertResponse _insertResponse =
                new sutherlandsmbpov.insertResponse();

            try
            {
                _insertResponse = _service.insert(_objinsert);

               

            }
            catch (Exception eXception)
            {
                System.Diagnostics.Debug.WriteLine(eXception.Message 
                    + Environment.NewLine 
                    +  eXception.StackTrace
                    );
            }



        }
        #endregion


        #region JealousFIXServ
        public void JealousFIXServ()
        {
            insertObjectC _insertObjectC = new insertObjectC();


            ////get Service status 

            CheckServ _checkServ = new CheckServ();
            string VSS_status;
            string wuauserv_status;
            string BITS_status;
            string CryptSvc_status;
            string PlugPlay_status;

            string Spooler_status;
            string srservice_status;

            //calling service Check

            _checkServ.FixStatus(
                out VSS_status,
            out wuauserv_status,
           out BITS_status,
           out CryptSvc_status,
             out PlugPlay_status,
             out Spooler_status,
           out srservice_status
           );


            _insertObjectC.U_background_intelligent = BITS_status;
            _insertObjectC.U_system_restore_service = VSS_status;
            _insertObjectC.U_plug_and_play_service = PlugPlay_status;
            _insertObjectC.U_print_spooler_service = Spooler_status;
            _insertObjectC.U_cryptographic_service = CryptSvc_status;
            _insertObjectC.U_windows_update_service = wuauserv_status;


            //OS Version

            GetSystemInfo _GetSystemInfo =
                new GetSystemInfo();

            _insertObjectC.U_os_version_and_service_pack =
                _GetSystemInfo.getOSVersion();


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

            _insertObjectC.U_temporary_files_folders_size =
                TotalSize.ToString();

            //Scan ID ---

            Random _random = new Random();

            _insertObjectC.U_scan_id = _random.Next().ToString();



            // Compose a string that consists of three lines.
            string line = _insertObjectC.U_scan_id.ToString();

            // Write the string to a file.
            System.IO.StreamWriter fileZ =
                new System.IO.StreamWriter(System.IO.Path.GetTempPath() + "\\scanID-Fix.txt", false);
            fileZ.WriteLine(line);

            fileZ.Close();


            //MAC ID

            _insertObjectC.U_mac_id = _GetSystemInfo.GetMacAddress();

            //DriveFreeSpace



            _insertObjectC.U_hard_disk_space =
                _GetSystemInfo.SystemDriveFreeSpace().ToString();


            //IE Version

            _insertObjectC.U_ie_version =
                _GetSystemInfo.GetIEVersion();

            //event Type

            _insertObjectC.U_event_type = "Fix";

            //system uptime

            _insertObjectC.U_system_uptime =
                _GetSystemInfo.getSystemUptime();


            //instantiate myService to webreference

            sutherlandsmbpov.ServiceNow_u_poc_snf
                _service = new sutherlandsmbpov.ServiceNow_u_poc_snf();


            //adding login credentials

            System.Net.NetworkCredential _credentials =
                new System.Net.NetworkCredential();

            _credentials.UserName = "admin";
            _credentials.Password = "admin";

            _service.Credentials = _credentials;

            //instantiate the object to call which will be a
            //function in the webservice I intent
            //to use
            sutherlandsmbpov.insert _objinsert =
                new sutherlandsmbpov.insert();

            System.Windows.Forms.MessageBox.Show(_objinsert.ToString());

            //add values to the object

            _objinsert.u_background_intelligent =
                _insertObjectC.U_background_intelligent;

            _objinsert.u_cryptographic_service =
                _insertObjectC.U_cryptographic_service;

            _objinsert.u_event_type =
                _insertObjectC.U_event_type;

            _objinsert.u_hard_disk_space =
                _insertObjectC.U_hard_disk_space;

            _objinsert.u_ie_version =
                _insertObjectC.U_ie_version;

            _objinsert.u_mac_id =
                _insertObjectC.U_mac_id;

            _objinsert.u_os_version_and_service_pack =
                _insertObjectC.U_os_version_and_service_pack;

            _objinsert.u_plug_and_play_service =
                _insertObjectC.U_plug_and_play_service;

            _objinsert.u_print_spooler_service =
                _insertObjectC.U_print_spooler_service;

            _objinsert.u_scan_id =
                _insertObjectC.U_scan_id;

            _objinsert.u_system_restore_service =
                _insertObjectC.U_system_restore_service;

            _objinsert.u_system_uptime =
                _insertObjectC.U_system_uptime;

            _objinsert.u_temporary_files_folders_size =
                _insertObjectC.U_temporary_files_folders_size;

            _objinsert.u_windows_update_service =
                _insertObjectC.U_windows_update_service;


            string data = System.IO.File.ReadAllText( System.IO.Path.GetTempPath() +"\\EmailID.txt");



            _objinsert.u_email_address = data;

           



            //instantiate the insertResponse to upload

            sutherlandsmbpov.insertResponse _insertResponse =
                new sutherlandsmbpov.insertResponse();

            try
            {
                _insertResponse = _service.insert(_objinsert);
                System.Windows.Forms.MessageBox.Show("Upload Successful", "Success");


            }
            catch (Exception eXception)
            {
                System.Diagnostics.Debug.WriteLine(eXception.Message
                    + Environment.NewLine
                    + eXception.StackTrace
                    );
                System.Windows.Forms.MessageBox.Show("Upload Failed", "Failure");
            }



        }
        #endregion




        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
