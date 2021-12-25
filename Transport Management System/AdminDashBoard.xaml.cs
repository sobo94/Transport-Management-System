using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using Path = System.IO.Path;
using MySql.Data.MySqlClient;
using System.Data;

namespace Group2
{
    /// <summary>
    /// Interaction logic for AdminDashBoard.xaml
    /// </summary>
    /// 

    

    public partial class AdminDashBoard : Page
    {
        // Declare Data tables
        DataTable DtRateTable = new DataTable();
        DataTable DtCarrierTable = new DataTable();
        DataTable DtRouteTable = new DataTable();

        // Declare IDs for MySql commands
        int SelectedRateID;
        int SelectedCarrierID;
        int SelectedRouteID;

        /**
        *  \brief   AdminDashBoard -- initialize components
        *  \details this method initialize components of AdminDashBoard.xaml
        *  \param   NONE
        *  \returns NONE
        */

        public AdminDashBoard()
        {
            InitializeComponent();
            AdminController.addLog("[Admin] Admin Dashboard Initialized");
        }


        // ------------------------- These are for hover effect [start] ---------------------


        /**
        *  \brief   menu1_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color to accent colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu1_MouseEnter(object sender, MouseEventArgs e)
        {
            menu1.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));
        }


        /**
        *  \brief   menu1_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu1_MouseLeave(object sender, MouseEventArgs e)
        {
            menu1.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));
        }


        /**
        *  \brief   menu2_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu2_MouseEnter(object sender, MouseEventArgs e)
        {
            menu2.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            menu2_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));
        }


        /**
        *  \brief   menu2_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu2_MouseLeave(object sender, MouseEventArgs e)
        {
            menu2.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            menu2_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));
        }


        /**
        *  \brief   menu3_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu3_MouseEnter(object sender, MouseEventArgs e)
        {
            menu3.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            menu3_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));
        }


        /**
        *  \brief   menu3_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu3_MouseLeave(object sender, MouseEventArgs e)
        {
            menu3.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            menu3_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));
        }



        /**
        *  \brief   menu4_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu4_MouseEnter_1(object sender, MouseEventArgs e)
        {
            menu4.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            menu4_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));
        }


        /**
        *  \brief   menu4_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu4_MouseLeave_1(object sender, MouseEventArgs e)
        {
            menu4.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            menu4_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));
        }


        /**
        *  \brief   sub_menu1_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void sub_menu1_MouseEnter(object sender, MouseEventArgs e)
        {
            // Parent menu keep the hover state
            menu4.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            menu4_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));

            // sub-menu hover effect
            sub_menu1.Background = new SolidColorBrush(Color.FromRgb(192, 128, 255));
            sub_menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));
        }



        /**
        *  \brief   sub_menu1_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void sub_menu1_MouseLeave(object sender, MouseEventArgs e)
        {
            // Parent menu keep the hover state
            menu4.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            menu4_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));

            // sub-menu hover effect
            sub_menu1.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            sub_menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));
        }



        /**
        *  \brief   sub_menu2_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void sub_menu2_MouseEnter(object sender, MouseEventArgs e)
        {
            // Parent menu keep the hover state
            menu4.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            menu4_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));

            // sub-menu hover effect
            sub_menu2.Background = new SolidColorBrush(Color.FromRgb(192, 128, 255));
            sub_menu2_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));
        }



        /**
        *  \brief   sub_menu2_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void sub_menu2_MouseLeave(object sender, MouseEventArgs e)
        {
            // Parent menu keep the hover state
            menu4.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            menu4_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));

            // sub-menu hover effect
            sub_menu2.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            sub_menu2_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));
        }


        /**
        *  \brief   sub_menu3_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void sub_menu3_MouseEnter(object sender, MouseEventArgs e)
        {
            // Parent menu keep the hover state
            menu4.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            menu4_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));

            // sub-menu hover effect
            sub_menu3.Background = new SolidColorBrush(Color.FromRgb(192, 128, 255));
            sub_menu3_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));
        }



        /**
        *  \brief   sub_menu2_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void sub_menu3_MouseLeave(object sender, MouseEventArgs e)
        {
            // Parent menu keep the hover state
            menu4.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            menu4_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));

            // sub-menu hover effect
            sub_menu3.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            sub_menu3_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));
        }


        /**
        *  \brief   menu5_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu5_MouseEnter(object sender, MouseEventArgs e)
        {
            menu5.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            menu5_label.Foreground = new SolidColorBrush(Color.FromRgb(247, 239, 255));
        }


        /**
        *  \brief   menu5_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void menu5_MouseLeave(object sender, MouseEventArgs e)
        {
            menu5.Background = new SolidColorBrush(Color.FromRgb(247, 239, 255));
            menu5_label.Foreground = new SolidColorBrush(Color.FromRgb(151, 86, 217));
        }

        // ------------------------- These are for hover effect [end] ---------------------



        // ------------------------- These are for Menu Items Click Events [start] ---------------------



        /**
        *  \brief   Label_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu which is main dashboard page and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        // Dashboard Home
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AdminDashBoardMain.Visibility = Visibility.Visible;

            // Hide the other parts
            AdminDashBoardDirectory.Visibility = Visibility.Collapsed;
            AdminDashBoardIpPort.Visibility = Visibility.Collapsed;
            Rate_Table.Visibility = Visibility.Collapsed;
            Carrier_Table.Visibility = Visibility.Collapsed;
            Route_Table.Visibility = Visibility.Collapsed;
            admin_dashboard_review_log.Visibility = Visibility.Collapsed;
            admin_dashboard_backup.Visibility = Visibility.Collapsed;
        }



        /**
        *  \brief   menu1_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu which is main dashboard page and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void menu1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AdminDashBoardDirectory.Visibility = Visibility.Visible;

            // Hide the other parts
            AdminDashBoardMain.Visibility = Visibility.Collapsed;
            AdminDashBoardIpPort.Visibility = Visibility.Collapsed;
            Rate_Table.Visibility = Visibility.Collapsed;
            Carrier_Table.Visibility = Visibility.Collapsed;
            Route_Table.Visibility = Visibility.Collapsed;
            admin_dashboard_review_log.Visibility = Visibility.Collapsed;
            admin_dashboard_backup.Visibility = Visibility.Collapsed;
        }


        /**
        *  \brief   menu2_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void menu2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AdminDashBoardIpPort.Visibility = Visibility.Visible;

            // Hide the other parts
            AdminDashBoardMain.Visibility = Visibility.Collapsed;
            AdminDashBoardDirectory.Visibility = Visibility.Collapsed;
            Rate_Table.Visibility = Visibility.Collapsed;
            Carrier_Table.Visibility = Visibility.Collapsed;
            Route_Table.Visibility = Visibility.Collapsed;
            admin_dashboard_review_log.Visibility = Visibility.Collapsed;
            admin_dashboard_backup.Visibility = Visibility.Collapsed;
        }


        /**
        *  \brief   menu3_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void menu3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            admin_log_listbox.Items.Clear();

            if (AdminController.LogFileName == null)
            {
                AdminController.LogFileName = AdminController.PresetLogFile;
            }

            using (StreamReader sr = File.OpenText(AdminController.LogFileName))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    admin_log_listbox.Items.Add(s);
                }
            }

            admin_dashboard_review_log.Visibility = Visibility.Visible;

            // Hide the other parts
            AdminDashBoardMain.Visibility = Visibility.Collapsed;
            AdminDashBoardIpPort.Visibility = Visibility.Collapsed;
            AdminDashBoardDirectory.Visibility = Visibility.Collapsed;
            Rate_Table.Visibility = Visibility.Collapsed;
            Carrier_Table.Visibility = Visibility.Collapsed;
            Route_Table.Visibility = Visibility.Collapsed;
            admin_dashboard_backup.Visibility = Visibility.Collapsed;
        }



        /**
        *  \brief   sub_menu1_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void sub_menu1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rate_Table.Visibility = Visibility.Visible;

            // Hide the other parts
            AdminDashBoardMain.Visibility = Visibility.Collapsed;
            AdminDashBoardIpPort.Visibility = Visibility.Collapsed;
            AdminDashBoardDirectory.Visibility = Visibility.Collapsed;            
            Carrier_Table.Visibility = Visibility.Collapsed;
            Route_Table.Visibility = Visibility.Collapsed;
            admin_dashboard_review_log.Visibility = Visibility.Collapsed;
            admin_dashboard_backup.Visibility = Visibility.Collapsed;

            // Rate Table Data Grid

            // Clear the data table to prevent duplicate data
            DtRateTable.Clear();

            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    // Shows Connection Accepted on UI
                    //market_status_bar.Content = $"Connected to MySql {conn.ServerVersion}";

                    // SQL Command
                    string sq1 = "SELECT * FROM Rate_Table;"; 
                    MySqlCommand selectAllRate = new MySqlCommand(sq1, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRate);

                    // fills Data Table Object with All Contract Rows                   

                    reader.Fill(DtRateTable);                   

                    // Render the Columns and the rows 
                    rate_table_datagrid.ItemsSource = DtRateTable.DefaultView;

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Rate Database Loaded");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Rate Database Loading Failed");
            }
        }


        /**
        *  \brief   sub_menu2_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void sub_menu2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Carrier_Table.Visibility = Visibility.Visible;

            // Hide the other parts
            AdminDashBoardMain.Visibility = Visibility.Collapsed;
            AdminDashBoardIpPort.Visibility = Visibility.Collapsed;
            AdminDashBoardDirectory.Visibility = Visibility.Collapsed;
            Rate_Table.Visibility = Visibility.Collapsed;            
            Route_Table.Visibility = Visibility.Collapsed;
            admin_dashboard_review_log.Visibility = Visibility.Collapsed;
            admin_dashboard_backup.Visibility = Visibility.Collapsed;

            // Clear the data table to prevent duplicate data
            DtCarrierTable.Clear();

            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    // Shows Connection Accepted on UI
                    //market_status_bar.Content = $"Connected to MySql {conn.ServerVersion}";

                    // SQL Command
                    string sq1 = "SELECT * FROM Carrier_Data;";
                    MySqlCommand selectAllCarrier = new MySqlCommand(sq1, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllCarrier);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtCarrierTable);

                    // Render the Columns and the rows
                    carrier_table_datagrid.ItemsSource = DtCarrierTable.DefaultView;

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Carrier Database Loaded");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Carrier Database Loading Failed");
            }

            

        }


        /**
        *  \brief   sub_menu3_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void sub_menu3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Route_Table.Visibility = Visibility.Visible;

            // Hide the other parts
            AdminDashBoardMain.Visibility = Visibility.Collapsed;
            AdminDashBoardIpPort.Visibility = Visibility.Collapsed;
            AdminDashBoardDirectory.Visibility = Visibility.Collapsed;
            Rate_Table.Visibility = Visibility.Collapsed;
            Carrier_Table.Visibility = Visibility.Collapsed;            
            admin_dashboard_review_log.Visibility = Visibility.Collapsed;
            admin_dashboard_backup.Visibility = Visibility.Collapsed;


            // Route Table Data Grid

            // Clear the data table to prevent duplicate data
            DtRouteTable.Clear();

            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    // Shows Connection Accepted on UI
                    //market_status_bar.Content = $"Connected to MySql {conn.ServerVersion}";

                    // SQL Command
                    string sq1 = "SELECT * FROM Route_Table;";
                    MySqlCommand selectAllRoute = new MySqlCommand(sq1, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRoute);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtRouteTable);

                    // Render the Columns and the rows 
                    route_table_datagrid.ItemsSource = DtRouteTable.DefaultView;

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Route Database Loaded");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Route Database Loading Failed");
            }

        }



        /**
        *  \brief   menu5_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void menu5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            admin_dashboard_backup.Visibility = Visibility.Visible;

            // Hide the other parts
            AdminDashBoardMain.Visibility = Visibility.Collapsed;
            AdminDashBoardIpPort.Visibility = Visibility.Collapsed;
            AdminDashBoardDirectory.Visibility = Visibility.Collapsed;
            Rate_Table.Visibility = Visibility.Collapsed;
            Carrier_Table.Visibility = Visibility.Collapsed;
            Route_Table.Visibility = Visibility.Collapsed;
            admin_dashboard_review_log.Visibility = Visibility.Collapsed;
        }


        /**
        *  \brief   AdminBackToMain_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event and lead to ChooseRole.xaml
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void AdminBackToMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ChooseRole.xaml", UriKind.Relative));

            // LOG
            AdminController.addLog("[Admin] Signed out - Good Job!");
        }



        // ------------------------- These are for Menu Items Click Events [end] ---------------------


        /**
        *  \brief   ChangeLogFilePath_Click -- event handling of change directory button click
        *  \details this method handles change directory button click event, display open file dialog
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void ChangeLogFilePath_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = AdminController.LogFileDirectory;
            sfd.Filter = "Log Files (*.log) | *.log";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == true)
            {
                if ((myStream = sfd.OpenFile()) != null)
                {
                    // Start with empty string
                    myStream.Close();
                }
                LogFilePath.Text = Path.GetDirectoryName(sfd.FileName);
                AdminController.LogFileName = sfd.FileName;
                AdminController.LogFileDirectory = LogFilePath.Text;

                // LOG
                AdminController.addLog($"[Admin] Changed a log file directory");
                AdminController.addLog($"[Admin] Started a new log file");
            }
        }


        /**
        *  \brief   TmsConnect_Click -- event handling of connect button click
        *  \details this method handles connect button click event and make an initial connection to MySql server
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void TmsConnect_Click(object sender, RoutedEventArgs e)
        {         
            AdminController.ConnectionStringForTMS = $"Server={TmsIpAddress.Text};Uid={TmsUserName.Text};Pwd={TmsDbPassword.Password};database=tms_db";

            connection_result.Content = AdminController.InitialConnectToDB();

            // log
            AdminController.addLog("[Admin] TMS Database Connected");
        }



        /**
        *  \brief   backup_button_Click -- event handling of backup button click
        *  \details this method handles backup button click event and exports a backup file
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void backup_button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "MySql Database (*.sql) | *.sql";
            bool? saveResult = sfd.ShowDialog();

            if (saveResult == true)
            {
                AdminController.BackupFile = sfd.FileName;
                backup_file_path.Text = AdminController.BackupFile;
                AdminController.localBackup(AdminController.ConnectionStringForTMS, AdminController.BackupFile);
            }

            // log
            AdminController.addLog("[Admin] Database Backup File Created : " + AdminController.BackupFile);
        }



        /**
        *  \brief   restore_button_Click -- event handling of restore button click
        *  \details this method handles restore button click event and imports a backup file
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void restore_button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MySql Database (*.sql) | *.sql";
            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                restore_file_path.Text = ofd.FileName;
                if(ofd.FileName == AdminController.BackupFile)
                {
                    AdminController.localRestore(AdminController.ConnectionStringForTMS, AdminController.BackupFile);
                }
                else
                {
                    MessageBox.Show("You chose the wrong file! - Only a backup file can be restored.", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            // log
            AdminController.addLog("[Admin] Database Backup File Restored : " + AdminController.BackupFile);
        }



        // ------------------ When Data Grid Selected, textboxes are filled ------------------------------------

        private void rate_table_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            try
            {
                if (rate_table_datagrid.SelectedItem != null)
                {
                    DataRowView dataRowView = (DataRowView)rate_table_datagrid.SelectedItem;
                    SelectedRateID = Int32.Parse(dataRowView[0].ToString());
                    Rate_Table_Message.Content = $"Carrier ID: {dataRowView[0].ToString()} - Selected";
                    ForSurcharge.Text = dataRowView[1].ToString();
                    ForFTL.Text = dataRowView[2].ToString();
                    ForLTL.Text = dataRowView[3].ToString();
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Rate Database Row Selection Failed");
            }
        }

        private void carrier_table_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (carrier_table_datagrid.SelectedItem != null)
                {
                    DataRowView dataRowView = (DataRowView)carrier_table_datagrid.SelectedItem;
                    SelectedCarrierID = Int32.Parse(dataRowView[0].ToString());
                    Carrier_Table_Message.Content = $"Carrier ID: {dataRowView[0].ToString()} - Selected";
                    ForCarrierName.Text = dataRowView[1].ToString();
                    ForDepotCity.Text = dataRowView[2].ToString();
                    ForFTLAvailability.Text = dataRowView[3].ToString();
                    ForLTLAvailability.Text = dataRowView[4].ToString();
                    ForFTLRate.Text = dataRowView[5].ToString();
                    ForLTLRate.Text = dataRowView[6].ToString();
                    ForReeferCharge.Text = dataRowView[7].ToString();
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Carrier Database Row Selection Failed");
            }           
        }

        private void route_table_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (route_table_datagrid.SelectedItem != null)
                {
                    DataRowView dataRowView = (DataRowView)route_table_datagrid.SelectedItem;
                    SelectedRouteID = Int32.Parse(dataRowView[0].ToString());
                    Route_Table_Message.Content = $"Route ID: {dataRowView[0].ToString()} - Selected";
                    ForDestination.Text = dataRowView[1].ToString();
                    ForKM.Text = dataRowView[2].ToString();
                    ForWest.Text = dataRowView[3].ToString();
                    ForEast.Text = dataRowView[4].ToString();
                    ForTime.Text = dataRowView[5].ToString();
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Route Database Row Selection Failed");
            }            
        }


        // ------------------------ Add Buttons ----------------------------------------------------------
        private void rate_add_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string sq1 = $"INSERT INTO Rate_Table (Surcharge, FTL, LTL) VALUES (0.00, 0.00, 0.00);";
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtRateTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Rate_Table;";
                    MySqlCommand selectAllRate = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRate);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtRateTable);

                    // Render the Columns and the rows
                    rate_table_datagrid.ItemsSource = DtRateTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Rate Database Data Added");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Rate Database Row Addition Failed");
            }
}

        private void carrier_add_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string sq1 = "INSERT INTO Carrier_Data (Carrier_Name, Depot_City, FTL_Availability, LTL_Availability, FTL_Rate, LTL_Rate, Reefer_Charge) VALUES ('New Item', 'None', 0, 0, 0.00, 0.00, 0);"; 
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtCarrierTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Carrier_Data;";
                    MySqlCommand selectAllCarrier = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllCarrier);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtCarrierTable);

                    // Render the Columns and the rows
                    carrier_table_datagrid.ItemsSource = DtCarrierTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Carrier Database Data Added");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Carrier Database Row Addition Failed");
            }
        }

        private void route_add_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string sq1 = "INSERT INTO Route_Table (Destination, Kilometer, West, East, Time) VALUES ('New Item', 0, 'None', 'None', 0.00)"; ; // ADJUSTMENT NAME REQUIRES
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtRouteTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Route_Table;";
                    MySqlCommand selectAllRoute = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRoute);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtRouteTable);

                    // Render the Columns and the rows
                    route_table_datagrid.ItemsSource = DtRouteTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Route Database Data Added");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Route Database Row Addition Failed");
            }
        }


        // ------------------------ Delete Buttons ----------------------------------------------------------

        private void rate_delete_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string sq1 = $"DELETE FROM Rate_Table WHERE Rate_Table_ID = {SelectedRateID}"; ; // ADJUSTMENT NAME REQUIRES
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtRateTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Rate_Table;";
                    MySqlCommand selectAllRate = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRate);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtRateTable);

                    // Render the Columns and the rows
                    rate_table_datagrid.ItemsSource = DtRateTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Rate Database Data Deleted");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Rate Database Row Delete Failed");
            }
        }

        private void carrier_delete_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string sq1 = $"DELETE FROM Carrier_Data WHERE Carrier_ID = {SelectedCarrierID};" ; // ADJUSTMENT NAME REQUIRES
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtCarrierTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Carrier_Data;";
                    MySqlCommand selectAllCarrier = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllCarrier);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtCarrierTable);

                    // Render the Columns and the rows
                    carrier_table_datagrid.ItemsSource = DtCarrierTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Carrier Database Data Deleted");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Carrier Database Row Delete Failed");
            }

        }

        private void route_delete_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string sq1 = $"DELETE FROM Route_Table WHERE Route_ID = {SelectedRouteID};"; 
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtRouteTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Route_Table;";
                    MySqlCommand selectAllRoute = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRoute);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtRouteTable);

                    // Render the Columns and the rows
                    route_table_datagrid.ItemsSource = DtRouteTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Route Database Data Deleted");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Route Database Row Delete Failed");
            }
        }


        // ------------------------ Update Buttons ----------------------------------------------------------

        private void rate_update_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string setCondition = $"Surcharge = '{ForSurcharge.Text}', FTL = {ForFTL.Text}, LTL = {ForLTL.Text}";
                    string sq1 = $"UPDATE Rate_Table SET {setCondition} WHERE Rate_Table_ID = {SelectedRateID};" ; 
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtRateTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Rate_Table;";
                    MySqlCommand selectAllRate = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRate);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtRateTable);

                    // Render the Columns and the rows
                    rate_table_datagrid.ItemsSource = DtRateTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Rate Database Data Updated");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Rate Database Row Updating Failed");
            }
        }


        private void carrier_update_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string setCondition = $"Carrier_Name = '{ForCarrierName.Text}', Depot_City = '{ForDepotCity.Text}', FTL_Availability = {ForFTLAvailability.Text}, LTL_Availability = {ForLTLAvailability.Text}, FTL_Rate = {ForFTLRate.Text}, LTL_Rate = {ForLTLRate.Text}, Reefer_Charge ={ForReeferCharge.Text}";
                    string sq1 = $"UPDATE Carrier_Data SET {setCondition} WHERE Carrier_ID = {SelectedCarrierID};"; // ADJUSTMENT NAME REQUIRES
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtCarrierTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Carrier_Data;";
                    MySqlCommand selectAllCarrier = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllCarrier);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtCarrierTable);

                    // Render the Columns and the rows
                    carrier_table_datagrid.ItemsSource = DtCarrierTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Carrier Database Data Updated");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Carrier Database Row Updating Failed");
            }
        }


        private void route_update_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    string setCondition = $"Destination = '{ForDestination.Text}', Kilometer = {ForKM.Text}, West = '{ForWest.Text}', East = '{ForEast.Text}', Time = {ForTime.Text}";
                    string sq1 = $"UPDATE Route_Table SET {setCondition} WHERE Route_ID = {SelectedRouteID};"; // ADJUSTMENT NAME REQUIRES
                    MySqlCommand cmd = new MySqlCommand(sq1, conn);
                    cmd.ExecuteNonQuery();

                    // ----------- Refresh [Start] -----------------
                    DtRouteTable.Clear();

                    // SQL Command
                    string sq2 = "SELECT * FROM Route_Table;";
                    MySqlCommand selectAllRoute = new MySqlCommand(sq2, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRoute);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtRouteTable);

                    // Render the Columns and the rows
                    route_table_datagrid.ItemsSource = DtRouteTable.DefaultView;

                    // ----------- Refresh [End] -----------------

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Admin] Route Database Data Updated");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Admin] Route Database Row Updating Failed");
            }
        }
    }
}
