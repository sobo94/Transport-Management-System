using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Group2
{
    /// <summary>
    /// Interaction logic for PlannerDashBoard.xaml
    /// </summary>
    public partial class PlannerDashBoard : Page
    {

        DataTable DtOrderTable1 = new DataTable();
        OrderDetail order = new OrderDetail();


        /**
        *  \brief   PlannerDashBoard -- initialize components
        *  \details this method initialize components of PlannerDashBoard.xaml
        *  \param   NONE
        *  \returns NONE
        */
        public PlannerDashBoard()
        {
            InitializeComponent();
            AdminController.addLog("[Planner] Planner Dashboard Initialized");
        }


        // ----------------- Menu item hover effect [start] -------------------------------------------------

        /**
        *  \brief   planner_menu1_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color to accent colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void planner_menu1_MouseEnter(object sender, MouseEventArgs e)
        {
            planner_menu1.Background = new SolidColorBrush(Color.FromRgb(69, 177, 107));
            planner_menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(236, 255, 243));
        }


        /**
        *  \brief   planner_menu1_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void planner_menu1_MouseLeave(object sender, MouseEventArgs e)
        {
            planner_menu1.Background = new SolidColorBrush(Color.FromRgb(236, 255, 243));
            planner_menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(69, 177, 107));
        }


        
        // ----------------- Menu item hover effect [end] -------------------------------------------------



        /**
        *  \brief   PlannerBackToMain_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event and lead to ChooseRole.xaml
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void PlannerBackToMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ChooseRole.xaml", UriKind.Relative));

            AdminController.addLog("[Planner] Signed out - Good Job!");
        }



        /**
        *  \brief   planner_home_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event 
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void planner_home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            main_screen.Visibility = Visibility.Visible;

            // hide others
            menu1_screen.Visibility = Visibility.Collapsed;

            // Logic goes blow from here

        }



        // Render's UI  
        private void planner_menu1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menu1_screen.Visibility = Visibility.Visible;


            // hide others
            main_screen.Visibility = Visibility.Collapsed;

            // Logic goes blow from here
            // Rate Table Data Grid

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                foreach (System.Data.DataRowView SelectedRow in Planner_datagrid.SelectedItems)
                {

                    if (SelectedRow != null)
                    {
                        int jType = int.Parse(SelectedRow[1].ToString());
                        int VanType = int.Parse(SelectedRow[5].ToString());
                        string startCity = SelectedRow[3].ToString();
                        string endCity = SelectedRow[4].ToString();
                        string Carrier = SelectedRow[8].ToString();

                        int distance = order.calcDistance(startCity, endCity);
                        double time = order.calcTime(startCity, endCity, jType);
                        int CarrierCost = order.CalcCarrierRevenue(distance, time, jType, Carrier, VanType);
                        int OhstRevenue = order.CalcOSHTRevenue(CarrierCost, jType);


                        SelectedRow[9] = distance;
                        SelectedRow[10] = time;
                        SelectedRow[11] = CarrierCost;
                        SelectedRow[12] = OhstRevenue;
                        SelectedRow[7] = 9; //
                    }

                }
                AdminController.addLog("[Planner] Information Generated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Planner] Information Generating Failed");
            }






        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var connstr = AdminController.ConnectionStringForTMS;
                //var connstrSohaib = "Server=localhost;Uid=Group02;Pwd=group2password;database=tms_db;";

                // Clears the dataTable to avoid Double entry situation upon clicking.
                DtOrderTable1.Clear();



                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    // Shows Connection Accepted on UI
                    //market_status_bar.Content = $"Connected to MySql {conn.ServerVersion}";

                    // SQL Command
                    string sq1 = "SELECT * FROM tms_db.order";// where OrderStatus = '2';";
                    MySqlCommand selectAllRate = new MySqlCommand(sq1, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllRate);

                    // fills Data Table Object with All Contract Rows                   

                    reader.Fill(DtOrderTable1);

                    // Render the Columns and the rows 
                    Planner_datagrid.ItemsSource = DtOrderTable1.DefaultView;

                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Planner] Order is Loaded");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Planner] Order Loading Failed");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // MySql
            try
            {

                var connstr = AdminController.ConnectionStringForTMS;

                // [R] - Remove test connection string with Admin string for final Submit.
                //var connstr = AdminController.ConnectionStringForTMS;
                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    // Shows Connection Accepted on UI
                    //tms_status_bar.Content = $"Connected to MySql {conn.ServerVersion}";

                    int rowCounter = 0;

                    foreach (System.Data.DataRowView selectedInsertRows in Planner_datagrid.SelectedItems)
                    {

                        if (selectedInsertRows != null)
                        {

                            string sq1 = generateInsertString(selectedInsertRows);
                            MySqlCommand mySqlCommand = new MySqlCommand(sq1, conn);
                            mySqlCommand.ExecuteNonQuery();

                        }

                        rowCounter++;

                    }

                    //tms_status_bar.Content = $"Updated {rowCounter} selected rows";
                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Planner] Order is Updated");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Planner] Order Updating Failed");
            }
        }

        private string generateInsertString(System.Data.DataRowView selectedInsertRows)
        {

            if (string.Compare(selectedInsertRows[8].ToString(), $"Schooner's") == 0)
            {

                selectedInsertRows[8] = "Schooners";

            }

            string Order0 = "'" + selectedInsertRows[0].ToString() + "'";
            string Order1 = "'" + selectedInsertRows[1].ToString() + "'";
            string Order2 = "'" + selectedInsertRows[2].ToString() + "'";
            string Order3 = "'" + selectedInsertRows[3].ToString() + "'";
            string Order4 = "'" + selectedInsertRows[4].ToString() + "'";
            string Order5 = "'" + selectedInsertRows[5].ToString() + "'";
            string Order6 = "'" + selectedInsertRows[6].ToString() + "'";
            string Order7 = "'" + selectedInsertRows[7].ToString() + "'"; //order status
            string Order8 = "'" + selectedInsertRows[8].ToString() + "'";
            string Order9 = "'" + selectedInsertRows[9].ToString() + "'";
            string Order10 = "'" + selectedInsertRows[10].ToString() + "'";
            string Order11 = "'" + selectedInsertRows[11].ToString() + "'";
            string Order12 = "'" + selectedInsertRows[12].ToString() + "'";

            // SQL Command
            string sq1 = $" UPDATE tms_db.order SET Km = {Order9} , EstTime = {Order10} , CarrierFee = {Order11} , OSHTFee = {Order12}, OrderStatus = {Order7} WHERE OrderID = {Order6} ;";

            return sq1;
        }

    }
}

