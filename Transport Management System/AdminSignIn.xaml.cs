using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for AdminSignIn.xaml
    /// </summary>
    public partial class AdminSignIn : Page
    {

        protected int signInAttemptRemain = 2;


        /**
        *  \brief   AdminSignIn -- initialize components
        *  \details this method initialize components of AdminSignIn.xaml
        *  \param   NONE
        *  \returns NONE
        */

        public AdminSignIn()
        {
            InitializeComponent();
        }



        /**
        *  \brief   AdminSignInButton_Click -- event handling of sign in button
        *  \details this method handles sign in button event and compare credentials with DB.
        *  \param   sender object
        *  \param   RoutedEventArgs e
        *  \returns NONE
        */

        private void AdminSignInButton_Click(object sender, RoutedEventArgs e)
        {
            int errorCount = 0;
            AdminIdError.Text = "";
            AdminiPasswordError.Text = "";

            if (ForAdminID.Text != "admin")
            {
                errorCount++;
                AdminIdError.Text = "* Incorrect User ID";
            }

            if (ForAdminPassWord.Password != "admin")
            {
                errorCount++;
                AdminiPasswordError.Text = "* Incorrect Password";
            } 

            if (errorCount > 0)
            {
                if (signInAttemptRemain == 0)
                {
                    AdminSignInButton.Visibility = Visibility.Collapsed;
                    adminWarning.Visibility = Visibility.Visible;
                    adminLock.Visibility = Visibility.Visible;
                    adminSignInFail.Visibility = Visibility.Visible;
                }
                signInAttemptRemain--;
                AdminSingInAttemptMsg.Text = $"Sign In Attempt Remaining {signInAttemptRemain+1}";
            } 
            else
            {
                signInAttemptRemain = 0;
                AdminSingInAttemptMsg.Text = "";                

                AdminController.addLog("[Admin] Sign in Success!");
                this.NavigationService.Navigate(new Uri("AdminDashBoard.xaml", UriKind.Relative));
            }                   

        }



        /**
        *  \brief   admin_go_back_MouseLeftButtonDown -- event handling of go back button
        *  \details this method handles go back button event and lead to ChooseRole.xaml
        *  \param   sender object
        *  \param   MouseButtonEventArgs e
        *  \returns NONE
        */

        private void admin_go_back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ChooseRole.xaml", UriKind.Relative));
        }



        /**
        *  \brief   admin_go_back_MouseEnter -- event handling of go back button mouse enter
        *  \details this method handles go back button mouse enter event and displays shortcuts
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void admin_go_back_MouseEnter(object sender, MouseEventArgs e)
        {
            admin_go_back_avatar.Visibility = Visibility.Visible;
        }


        /**
        *  \brief   admin_avatar_left_MouseEnter -- event handling of mouse enter of left part of avatar 
        *  \details this method handles mouse enter event of left part of avatar by changing color of the label
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void admin_avatar_left_MouseEnter(object sender, MouseEventArgs e)
        {
            admin_avatar_label_left.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            admin_avatar_label_left.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }


        /**
        *  \brief   admin_avatar_left_MouseLeave -- event handling of mouse enter of left part of avatar 
        *  \details this method handles mouse leave event of left part of avatar by changing color of the label
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void admin_avatar_left_MouseLeave(object sender, MouseEventArgs e)
        {
            admin_avatar_label_left.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            admin_avatar_label_left.Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51));
        }



        /**
        *  \brief   admin_avatar_right_MouseEnter -- event handling of mouse enter of right part of avatar 
        *  \details this method handles mouse enter event of right part of avatar by changing color of the label
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void admin_avatar_right_MouseEnter(object sender, MouseEventArgs e)
        {
            admin_avatar_label_right.Background = new SolidColorBrush(Color.FromRgb(151, 86, 217));
            admin_avatar_label_right.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }


        /**
        *  \brief   admin_avatar_right_MouseLeave -- event handling of mouse enter of right part of avatar 
        *  \details this method handles mouse leave event of right part of avatar by changing color of the label
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void admin_avatar_right_MouseLeave(object sender, MouseEventArgs e)
        {
            admin_avatar_label_right.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            admin_avatar_label_right.Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51));
        }


        /**
        *  \brief   admin_avatar_left_MouseLeftButtonDown -- event handling of click of left part of avatar 
        *  \details this method handles click event of left part of avatar and lead to BuyerSignIn.xaml
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void admin_avatar_left_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("BuyerSignIn.xaml", UriKind.Relative));
        }



        /**
        *  \brief   admin_avatar_right_MouseLeftButtonDown -- event handling of click of right part of avatar 
        *  \details this method handles click event of right part of avatar and lead to PlannerSignIn.xaml
        *  \param   sender object
        *  \param   MouseEventArgs e
        *  \returns NONE
        */

        private void admin_avatar_right_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("PlannerSignIn.xaml", UriKind.Relative));
        }

        // Go back button --- end
    }
}
