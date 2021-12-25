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
    /// class for the logic of Home.xaml
    /// </summary>
    /// 
    /// \class  Home
    /// 
    /// \brief  The purpose of this class is to create the very initial page of the app
    /// 
    /// 
    /// Methods:
    ///     -Home()
    ///     -HomePageButton_Click(object sender, RoutedEventArgs e)
    ///     
    /// \author <i>Colby Taylor & Sohaib Sheikh & Seungjae Lee & Parichehr Moghanloo</i>
    ///         
    /// </summary>
    ///
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            AdminController.addLog($"Group2 TMS App has Initialized!");
        }


        /**
        *  \brief   HomePageButton_Click -- event handling of home page get started button
        *  \details this method handles get started button click event and leads to ChooseRole.xaml
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ChooseRole.xaml", UriKind.Relative));
                      
            AdminController.addLog("Welcome Back to Group2 TMS App!");

        }
    }
}

