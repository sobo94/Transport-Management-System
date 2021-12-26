using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;



namespace Group2
{
    /// <summary>
    /// Static class for Admin
    /// </summary>
    /// 
    /// \class  AdminController
    /// 
    /// \brief  The purpose of this class is to allow the admin specific controls
    ///         to be able to review log record log and connect to the Database
    /// 
    /// Attributes:
    ///     -Record                       : Attribute for storing the event log
    ///     -LogFileDirectory             : Attribute to store the local location
    ///                                   : of the log file
    ///     -ConnectionStringForTMS       : Stores the connection string for the 
    ///                                   : TMS database
    /// 
    /// Methods:
    ///     -addLog(string msg)
    ///     -readLog(string msg)
    ///     -connectToDb()
    ///     -changeCarrierData()
    ///     -changeRouteTable()
    ///     -changeRateFeeTable()
    ///     
    /// \author <i>Colby Taylor & Sohaib Sheikh & Seungjae Lee & Parichehr Moghanloo</i>
    ///         
    /// </summary>
    /// 

    public static class AdminController
    {

        public static string PresetLogFile = AppDomain.CurrentDomain.BaseDirectory + "\\App_Log_File.log";
        public static string LogFileDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string LogFileName;
        public static string LogWithDateTime;   // variable to store eventlogs
        public static string ConnectionStringForTMS;  // connection string for TMS Database

        // Backup
        public static string BackupFile;


        /**
        *  \brief   addLog -- add logs to a log file
        *  \details this method add log to a log file by appending
        *  \param   logMsg string
        *  \returns NONE
        */

        public static void addLog(string logMsg)
        {
            DateTime tsmTime = DateTime.Now;
            LogWithDateTime = $"[ {tsmTime} ] - {logMsg}";

            if (!File.Exists(LogFileName))
            {
                LogFileName = PresetLogFile;
               
                using (StreamWriter sw = File.CreateText(LogFileName))
                {
                    DateTime createFileTime = DateTime.Now;
                    sw.WriteLine($"[ {createFileTime} ] - Log File Created at App Default Directory");
                }
            }

            using (StreamWriter sw = File.AppendText(LogFileName))
            {
                sw.WriteLine(LogWithDateTime);
            }

        }



        /**
        *  \brief   InitialConnectToDB -- connect to TMS DB initially 
        *  \details this method connects TMS DB initially
        *  \param   NONE
        *  \returns NONE
        */

        public static string InitialConnectToDB()
        {
            string mySqlVersion = "";
            try
            {                
                // Connection String
                var connstr = ConnectionStringForTMS;                

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();
                    mySqlVersion =$"Connected to MySql {conn.ServerVersion}";

                    conn.Close(); // Close connection
                }         
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);               
            }
           
            return mySqlVersion;
        }



        /**
        *  \brief    ConnectToDB -- connect to TMS DB with sql command
        *  \details  this method connects TMS DB and execute sql command
        *  \param    mySqlCommand string
        *  \returns  NONE
        */
        public static void ConnectToDB(string mySqlCommand)
        {
            try
            {
                // Connection String
                var connstr = ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();                    

                    // Create a command
                    using (var cmd = conn.CreateCommand())
                    {
                        // This a command text
                        cmd.CommandText = mySqlCommand;

                        // execute
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close(); // Close connection
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        /**
        *  \brief    localBackup -- create a DB backup locally
        *  \details  this method create a DB backup file locally
        *  \param    ConnectionStringForTMS string
        *  \param    BackupFile string
        *  \returns  NONE
        */
        public static void localBackup(string ConnectionStringForTMS, string BackupFile)
        {

            // https://www.nuget.org/packages/MySqlBackup.NET/

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionStringForTMS))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(BackupFile);
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Planner] Database Backup Failed");
            }

        }


        public static void localRestore(string ConnectionStringForTMS, string BackupFile)
        {
            // https://www.nuget.org/packages/MySqlBackup.NET/

            try
            {
                using (MySqlConnection conn = new MySqlConnection(ConnectionStringForTMS))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(BackupFile);
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Planner] Backup Restore Failed");
            }
        }

    }

}

