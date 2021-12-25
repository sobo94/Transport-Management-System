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
using System.Diagnostics;
using System.IO;

namespace Group2
{
    /// <summary>
    /// Interaction logic for BuyerDashBoard.xaml
    /// </summary>
    /// 

    public partial class BuyerDashBoard : Page
    {

        /**
        *  \brief   BuyerDashBoard -- initialize components
        *  \details this method initialize components of PlannerDashBoard.xaml
        *  \param   NONE
        *  \returns NONE
        */
        DataTable DtMarketPlace = new DataTable();                      // DataTable Defined as a Global variable.
        DataTable DtOrder = new DataTable();                            // DataTable Defined as a Global variable.
        DataTable DtCompletedOrder = new DataTable();                   // DataTable Defined as a Global variable.
        List<object> newList = new List<object>();


        public BuyerDashBoard()
        {

            InitializeComponent();
            Carrier_ComboBox.Items.Add("We Haul");
            Carrier_ComboBox.Items.Add("Planet Express");
            Carrier_ComboBox.Items.Add("Schooner's");
            Carrier_ComboBox.Items.Add("Tillman Transport");
            AdminController.addLog("[Buyer] Buyer Dashboard Initialized");
        }



        // ----------------------------------------------------------------------------------------------//
        // ----------------------------------   Sohaib functions   --------------------------------------//
        // ----------------------------------------------------------------------------------------------//

        private void buyer_dashboard_market_button_Click(object sender, RoutedEventArgs e)
        {
            buyer_market_db_signin.Visibility = Visibility.Collapsed;

            // MySql
            try
            {
                // Connection String - Test version. Missing ip address
                var connstr = $"Server={buyer_dashboard_market_ip.Text};Uid={buyer_dashboard_market_id.Text};Pwd={buyer_dashboard_market_password.Password};database={buyer_dashboard_market_ip_dbName.Text}";

                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    // Shows Connection Accepted on UI
                    market_status_bar.Content = $"Connected to MySql {conn.ServerVersion}";

                    // SQL Command
                    string sq1 = "SELECT * FROM Contract;";
                    MySqlCommand selectAllContract = new MySqlCommand(sq1, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllContract);

                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtMarketPlace);
                    DtOrder = DtMarketPlace.Copy();


                    // 6-> id, 7-> Status, 8 -> Carrier, 9 -> km , 10 -> time, 11 -> CarrierFee, 12 -> OSHTFee
                    DataColumn Order_ID = new DataColumn("OrderID", typeof(Int32));
                    DataColumn Order_Status = new DataColumn("OrderStatus", typeof(Int32));
                    DataColumn Order_Carrier = new DataColumn("OrderCarrier", typeof(string));
                    DataColumn Order_Km = new DataColumn("Km", typeof(Int32));
                    DataColumn EstTime = new DataColumn("EstTime", typeof(Double));
                    DataColumn CarrierFee = new DataColumn("CarrierFee", typeof(Int32));
                    DataColumn OSHTFee = new DataColumn("OSHTFee", typeof(Int32));


                    DtMarketPlace.Columns.Add(Order_ID);
                    DtMarketPlace.Columns.Add(Order_Status);
                    DtMarketPlace.Columns.Add(Order_Carrier);
                    DtMarketPlace.Columns.Add(Order_Km);
                    DtMarketPlace.Columns.Add(EstTime);
                    DtMarketPlace.Columns.Add(CarrierFee);
                    DtMarketPlace.Columns.Add(OSHTFee);


                    // Render the Columns and the rows 
                    Marketplace_datagrid.ItemsSource = DtMarketPlace.DefaultView;



                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Buyer] Market Database Loaded");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Buyer] Market Database Loading Failed");
            }

        }

        /**
        *  \brief   accept_contract_MouseLeftButtonDown -- Gets selected client contracts,
        *  \details  this method fetch customers Contract, Generates a DataGrid of selected Items.
        *  \param    NONE
        *  \returns  NONE
        */
        private void accept_contract_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {



            // Add Selected orders. SelectedItem are stored in a temporary List DataStructure,
            // this creates a Disconnect Between the two datagrids. however the datagrids do share
            // the same dimensions and schema.
            foreach (var selectedItem in Marketplace_datagrid.SelectedItems)
            {

                if (selectedItem != null)
                {

                    newList.Add(selectedItem);


                    ORDER_datagrid.ItemsSource = newList;
                    ORDER_datagrid.Items.Refresh();

                }

            }

            AdminController.addLog("[Buyer] Marketplace Contract Accepted");


        }

        /**
       *  \brief    Button_Add_Carrier -- Selected Orders are added a Carrier Value
       *  \details  this method fetch selected orders, Assigns a carrier only if the fullfil validation rule.
       *  \param    NONE
       *  \returns  NONE
       */
        private void Button_Add_Carrier(object sender, RoutedEventArgs e)
        {
            bool ValidCarrierAssigned = false;
            string Carrier_Error_Message = "";

            foreach (System.Data.DataRowView acceptedRows in ORDER_datagrid.SelectedItems)
            {


                string origin = acceptedRows[3].ToString();
                string carrier = Carrier_ComboBox.SelectedItem.ToString();

                if (carrier == "We Haul")
                {
                    if ((origin == "Ottawa" || origin == "Toronto"))
                    {
                        ValidCarrierAssigned = true;
                    }
                    else
                    {

                        Carrier_Error_Message = "We Haul - operates from Ottawa and Toronto.";
                    }


                }
                else if (carrier == "Planet Express")
                {
                    if ((origin == "Ottawa" || origin == "Hamilton" || origin == "Belleville" || origin == "Oshawa" || origin == "Windsor"))
                    {
                        ValidCarrierAssigned = true;
                    }
                    else
                    {

                        Carrier_Error_Message = "Planet Express - operates from Ottawa, Hamilton, Belleville, Oshawa and Windsor.";
                    }

                }

                else if (carrier == "Schooner's")
                {
                    if ((origin == "London" || origin == "Toronto" || origin == "Kingston"))
                    {
                        ValidCarrierAssigned = true;
                    }
                    else
                    {

                        Carrier_Error_Message = "Schooner's - operates from London,Toronto and Kingston.";
                    }


                }
                else if (carrier == "Tillman Transport")
                {
                    if ((origin == "London" || origin == "Windsor" || origin == "Hamilton"))
                    {
                        ValidCarrierAssigned = true;
                    }
                    else
                    {

                        Carrier_Error_Message = "Tillman Transport - operates from London, Windsor and Hamilton.";
                    }

                }

                if (ValidCarrierAssigned == true)
                {
                    // Randomly generate and assign a Order ID betweem 1 and 10k
                    Random randomNumber = new Random();
                    acceptedRows[6] = randomNumber.Next(1, 10000);

                    // Set Status to 2 and Carrier to the ComboValues
                    acceptedRows[7] = 2;
                    acceptedRows[8] = carrier;

                    // Default Values for km, time and cost
                    acceptedRows[9] = 0;
                    acceptedRows[10] = 0;
                    acceptedRows[11] = 0;
                    acceptedRows[12] = 0;

                    // Display OK Status
                    tms_status_bar.Content = "Order ID: " + acceptedRows[6].ToString() + " has been assigned a Carrier.";





                    // [D] - Hacky solution - fix  
                    //Marketplace_datagrid.ItemsSource = DtOrder.DefaultView;
                    AdminController.addLog("[Buyer] Carrier Assigned");

                }
                else
                {
                    // Display Error Status
                    tms_status_bar.Content = Carrier_Error_Message;
                    AdminController.addLog("[Buyer] Carrier Assignment Failed");
                }
            }

        }


        private void Button_Update_Order_DB(object sender, RoutedEventArgs e)
        {


            // MySql
            try
            {

                //var connstrSohaib = "Server=localhost;Uid=Group02;Pwd=group2password;database=tms_db;";

                // [R] - Remove test connection string with Admin string for final Submit.
                var connstr = AdminController.ConnectionStringForTMS;
                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    // Shows Connection Accepted on UI
                    tms_status_bar.Content = $"Connected to MySql {conn.ServerVersion}";

                    int rowCounter = 0;

                    foreach (System.Data.DataRowView selectedInsertRows in ORDER_datagrid.SelectedItems)
                    {

                        if (selectedInsertRows != null)
                        {

                            string sq1 = generateInsertString(selectedInsertRows);
                            MySqlCommand mySqlCommand = new MySqlCommand(sq1, conn);
                            mySqlCommand.ExecuteNonQuery();

                        }

                        rowCounter++;

                    }

                    tms_status_bar.Content = $"Updated {rowCounter} selected rows";
                    conn.Close(); // Close connection
                }
                AdminController.addLog("[Buyer] Order Updated");
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Buyer] Order Updating Failed");
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
            string Order7 = "'" + selectedInsertRows[7].ToString() + "'";
            string Order8 = "'" + selectedInsertRows[8].ToString() + "'";
            string Order9 = "'" + selectedInsertRows[9].ToString() + "'";
            string Order10 = "'" + selectedInsertRows[10].ToString() + "'";
            string Order11 = "'" + selectedInsertRows[11].ToString() + "'";
            string Order12 = "'" + selectedInsertRows[12].ToString() + "'";

            // SQL Command
            string sq1 = "INSERT INTO tms_db.order " +
                "(ClientName,JobType,Quantity,Origin,Destination,VanType,OrderID,OrderStatus,OrderCarrier,Km,EstTime,CarrierFee,OSHTFee)" +
                " VALUES" +
                "(" + Order0 + "," + Order1 + "," + Order2 + "," + Order3 + "," + Order4 + "," + Order5 + "," + Order6 + ","
                + Order7 + "," + Order8 + "," + Order9 + "," + Order10 + "," + Order11 + "," + Order12 + ");";

            tms_status_bar.Content = $"SQL COMMAND :[" + sq1 + "]";
            return sq1;
        }




        // ----------------------------------------------------------------------------------------------//
        // ----------------------------------   Colby functions   ---------------------------------------//
        // ----------------------------------------------------------------------------------------------//


        // Event for "Generate Invoice"
        private void buyer_menu3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            buyer_dashboard_generate_invoice.Visibility = Visibility.Visible;

            // hide others
            buyer_dashboard_main.Visibility = Visibility.Collapsed;
            buyer_dashboard_ini_orders.Visibility = Visibility.Collapsed;


            // MySql
            try
            {

                //var connstr = $"Server={buyer_dashboard_market_ip.Text};Uid={buyer_dashboard_market_id.Text};Pwd={buyer_dashboard_market_password.Password};database={buyer_dashboard_market_ip_dbName.Text}";
                //var connstr = $"Server=localhost;Uid=group02;Pwd=group2password;database=tms_db;";//colbys con string
                var connstr = AdminController.ConnectionStringForTMS;

                // Clears the dataTable to avoid Double entry situation upon clicking.
                DtCompletedOrder.Clear();


                // [R] - Remove test connection string with Admin string for final Submit.
                //var connstr = AdminController.ConnectionStringForTMS;
                using (var conn = new MySqlConnection(connstr))
                {
                    // Open connection
                    conn.Open();

                    // Shows Connection Accepted on UI
                    market_status_bar.Content = $"Connected to MySql {conn.ServerVersion}";

                    // SQL Command
                    string sq1 = "SELECT * FROM tms_db.order where OrderStatus = '9';";
                    MySqlCommand selectAllContract = new MySqlCommand(sq1, conn);

                    // Create A data Adapter
                    MySqlDataAdapter reader = new MySqlDataAdapter(selectAllContract);


                    // fills Data Table Object with All Contract Rows 
                    reader.Fill(DtCompletedOrder);


                    // Render the Columns and the rows 
                    completed_orders.ItemsSource = DtCompletedOrder.DefaultView;

                    conn.Close(); // Close connection
                    AdminController.addLog("[Buyer] Completed Order Data Loaded");
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Buyer] Completed Order Data Loading Failed");
            }

        }

        private void generate_invoice_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                foreach (System.Data.DataRowView acceptedRows in completed_orders.SelectedItems)
                {

                    if (acceptedRows != null)
                    {
                        Generate_Invoice(acceptedRows);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Buyer] Data for Generating Invoice Loading Failed");
            }
        }

        public static void OpenApp(string path)
        {
            Process.Start("WINWORD.EXE", path);
        }

        public static void Generate_Invoice(System.Data.DataRowView info)
        {
            string InvoiceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string folder = "Invoices";
            InvoiceDirectory = System.IO.Path.Combine(InvoiceDirectory, folder);
            System.IO.Directory.CreateDirectory(InvoiceDirectory);

            string clientName = info[0].ToString();
            int job_type = int.Parse(info[1].ToString());
            int quantity = int.Parse(info[2].ToString());
            string origin = info[3].ToString();
            string dest = info[4].ToString();
            int van_type = int.Parse(info[5].ToString());
            string orderID = info[6].ToString();//
            int orderStatus = int.Parse(info[7].ToString());
            string carrierName = info[8].ToString();
            int km = int.Parse(info[9].ToString());
            double estTime = double.Parse(info[10].ToString());
            int amountDue = int.Parse(info[11].ToString());
            int OSHTDue = int.Parse(info[12].ToString());

            string vanType = "";
            string jobType = "";
            if (van_type==1)
            {
                vanType = "Refrigerated";
            }
            if(van_type == 0)
            {
                vanType = "Dry Van";
            }

            if (job_type == 1)
            {
                jobType = "Less Truck Load";
            }
            if (job_type == 0)
            {
                jobType = "Full Truck Load";
            }

            try
            {

                string filePath = System.IO.Path.Combine(InvoiceDirectory, orderID + ".doc");
                FileStream file;
                StreamWriter sw;
                //string filepath = InvoiceDirectory + orderID + ".docx";
                file = File.Create(filePath);
                file.Close();
                string invoicemsg = "Hello there, " + clientName + ", Thank you for using our service, below is a breakdown of your order" + "\r\n\r\n" +
                                     "Order ID:" + orderID + "\r\n" +
                                     "-------------------------------------------------------------------------\r\n\r\n" +
                                     "Job Type: " + jobType + "\r\n" +
                                     "Order Shipped From: " + origin + "\r\n" +
                                     "Order Destination: " + dest + "\r\n" +
                                     "Order Shipped by: " + carrierName + "\r\n" +
                                     "Van Type: " + vanType + "\r\n" +
                                     "Total Distance: " + km + " km\r\n" +
                                     "Estimated Delivery Time: " + estTime + " hr\r\n" +
                                     "                                                  Total Amount Due: $" + OSHTDue + "\r\n\r\n" +
                                     "\tThank you so much for choosing our service! We hope that the delivery method was easy and convenient for you. " +
                                     "If you have any questions please reach out to us. " +
                                     "You may pay your balance by cheque through the mail - Please allow 2-3 business days for mail to arrive. " +
                                     "Or over the phone with your credit card.  Or in person at one of our offices. " +
                                     "We look forward to working with you again! Sincerely our team at TMS OSHT. ";

                file = File.Open(filePath, FileMode.Append);

                sw = new StreamWriter(file);
                sw.Write(invoicemsg);
                sw.Close();
                file.Close();
                //OpenApp(filePath);

                AdminController.addLog("[Buyer] Invoice Generated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                AdminController.addLog("[Buyer] Invoice Generating Failed");
            }
        }





        // ----------------------------------------------------------------------------------------------//
        // ---------------------------------- MISC /UI functions   --------------------------------------//
        // ----------------------------------------------------------------------------------------------//


        /**
        *  \brief   buyer_menu1_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color to accent colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */
        private void buyer_menu1_MouseEnter(object sender, MouseEventArgs e)
        {
            buyer_menu1.Background = new SolidColorBrush(Color.FromRgb(239, 70, 111));
            buyer_menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(233, 224, 226));
        }


        /**
        *  \brief   buyer_menu1_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */
        private void buyer_menu1_MouseLeave(object sender, MouseEventArgs e)
        {
            buyer_menu1.Background = new SolidColorBrush(Color.FromRgb(233, 224, 226));
            buyer_menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(239, 70, 111));
        }


        /**
        *  \brief   buyer_menu3_MouseEnter -- event handling of menu item mouse enter
        *  \details this method handlesmenu item mouse enter event and change the background and text color to accent colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */
        private void buyer_menu3_MouseEnter(object sender, MouseEventArgs e)
        {
            buyer_menu3.Background = new SolidColorBrush(Color.FromRgb(239, 70, 111));
            buyer_menu3_label.Foreground = new SolidColorBrush(Color.FromRgb(233, 224, 226));
        }


        /**
        *  \brief   buyer_menu3_MouseLeave -- event handling of menu item mouse leave
        *  \details this method handlesmenu item mouse leave event and change the background and text color to original colors
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */
        private void buyer_menu3_MouseLeave(object sender, MouseEventArgs e)
        {
            buyer_menu3.Background = new SolidColorBrush(Color.FromRgb(233, 224, 226));
            buyer_menu3_label.Foreground = new SolidColorBrush(Color.FromRgb(239, 70, 111));
        }


        /**
        *  \brief   BuyerBackToMain_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event and lead to ChooseRole.xaml
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */
        private void BuyerBackToMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ChooseRole.xaml", UriKind.Relative));
        }

        /**
        *  \brief   buyer_menu1_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu which is main dashboard page and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */
        private void buyer_menu1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            buyer_dashboard_ini_orders.Visibility = Visibility.Visible;

            // hide others
            buyer_dashboard_main.Visibility = Visibility.Collapsed;
            buyer_dashboard_generate_invoice.Visibility = Visibility.Collapsed;
        }

        /**
        *  \brief   Label_MouseLeftButtonDown -- event handling of menu item mouse click
        *  \details this method handles menu item mouse click event, display content of the menu which is main dashboard page and hide non-related content
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        // Home button
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.NavigationService.Navigate(new Uri("BuyerDashBoard.xaml", UriKind.Relative));

            buyer_dashboard_main.Visibility = Visibility.Visible;

            // Hide others
            buyer_dashboard_ini_orders.Visibility = Visibility.Collapsed;
            buyer_dashboard_generate_invoice.Visibility = Visibility.Collapsed;
        }

        // From initial order menu
        private void Label_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            buyer_market_db_signin.Visibility = Visibility.Visible;

        }

        private void buyer_market_signin_close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            buyer_market_db_signin.Visibility = Visibility.Collapsed;
        }

        private void Carrier_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ORDER_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}


