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
    /// class for the logic of ChooseRole.xaml
    /// </summary>
    /// 
    /// \class  ChooseRole
    /// 
    /// \brief  The purpose of this class is for users to choose their roles
    /// 
    /// 
    /// Methods:
    ///     -ChooseRole()
    ///     -StartAdmin_Click(object sender, RoutedEventArgs e)
    ///     -StartBuyer_Click(object sender, RoutedEventArgs e)
    ///     -StartPlanner_Click(object sender, RoutedEventArgs e)
    ///     
    /// \author <i>Colby Taylor & Sohaib Sheikh & Seungjae Lee & Parichehr Moghanloo</i>
    ///         
    /// </summary>
    /// 
    public partial class ChooseRole : Page
    {

        /**
        *  \brief   ChooseRole -- initialize components
        *  \details this method initialize components of ChooseRole.xaml
        *  \param   NONE
        *  \returns NONE
        */

        public ChooseRole()
        {
            InitializeComponent();
        }


        /**
        *  \brief   StartAdmin_Click -- event handling of admin get started button
        *  \details this method handles get started button click event and leads to AdminSignin.xaml
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void StartAdmin_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AdminSignin.xaml", UriKind.Relative));

        }


        /**
        *  \brief   StartBuyer_Click -- event handling of buyer get started button
        *  \details this method handles get started button click event and leads to BuyerSignin.xaml
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void StartBuyer_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("BuyerSignin.xaml", UriKind.Relative));
        }


        /**
        *  \brief   StartPlanner_Click -- event handling of buyer get started button
        *  \details this method handles get started button click event and leads to PlannerSignin.xaml
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void StartPlanner_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PlannerSignin.xaml", UriKind.Relative));
        }
    }
}
