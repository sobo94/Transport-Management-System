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

namespace Group2
{
    /// <summary>
    /// Interaction logic for Marketplace.xaml
    /// </summary>
    public partial class Marketplace : Page
    {
        public Marketplace()
        {
            InitializeComponent();

            // Constructor
            Customers customer1 = new Customers("John Doe", "Toronto", "Order Requested", "Images/customer1.jpg");

            // Display on the page
            Customer1_Name.Content = customer1.CustomerName;
            Customer1_City.Content = customer1.CustomerCity;
            Customer1_Order_Status.Content = customer1.OrderRequestStatus;
            Customer1_Image.Source = new BitmapImage(new Uri(customer1.ImagePath, UriKind.Relative));

        }

        private void BuyerBackToMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ChooseRole.xaml", UriKind.Relative));
        }

        private void buyer_menu1_MouseEnter(object sender, MouseEventArgs e)
        {
            buyer_menu1.Background = new SolidColorBrush(Color.FromRgb(239, 70, 111));
            buyer_menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(233, 224, 226));
        }

        private void buyer_menu1_MouseLeave(object sender, MouseEventArgs e)
        {
            buyer_menu1.Background = new SolidColorBrush(Color.FromRgb(233, 224, 226));
            buyer_menu1_label.Foreground = new SolidColorBrush(Color.FromRgb(239, 70, 111));
        }

        private void buyer_menu2_MouseEnter(object sender, MouseEventArgs e)
        {
            buyer_menu2.Background = new SolidColorBrush(Color.FromRgb(239, 70, 111));
            buyer_menu2_label.Foreground = new SolidColorBrush(Color.FromRgb(233, 224, 226));
        }

        private void buyer_menu2_MouseLeave(object sender, MouseEventArgs e)
        {
            buyer_menu2.Background = new SolidColorBrush(Color.FromRgb(233, 224, 226));
            buyer_menu2_label.Foreground = new SolidColorBrush(Color.FromRgb(239, 70, 111));
        }

        private void buyer_menu3_MouseEnter(object sender, MouseEventArgs e)
        {
            buyer_menu3.Background = new SolidColorBrush(Color.FromRgb(239, 70, 111));
            buyer_menu3_label.Foreground = new SolidColorBrush(Color.FromRgb(233, 224, 226));
        }

        private void buyer_menu3_MouseLeave(object sender, MouseEventArgs e)
        {
            buyer_menu3.Background = new SolidColorBrush(Color.FromRgb(233, 224, 226));
            buyer_menu3_label.Foreground = new SolidColorBrush(Color.FromRgb(239, 70, 111));
        }

        private void buyer_menu1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Marketplace.xaml", UriKind.Relative));
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("BuyerDashBoard.xaml", UriKind.Relative));
        }

    }
}
