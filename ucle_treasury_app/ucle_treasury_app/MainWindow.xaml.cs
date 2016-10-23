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
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace ucle_treasury_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string database_conn_string = @"Data Source=" + System.Environment.CurrentDirectory + @"\resources\database\dbUCLE_Treasury.db;Version=3;";
        public bool hmongEnglishChange = false;
        public MainWindow()
        {
            InitializeComponent();
            btnLanguage.FontFamily = new FontFamily("Mong New Plain");
            btnLanguage.FontSize = 16;
            btnLanguage.Content = "lufmOe";
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            string txtTo;
            string txtSubject;
            string txtContent;
            var email = new Email_SMS();
            email.ShowDialog();

            if (email.boolClosed == false)
            {
                return;
            }

            try
            {

                txtTo = email.tbEmail.Text;
                txtSubject = email.tbSubject.Text;
                txtContent = email.tbMessage.Text;
                email.Close();

                var smtpServerName = ConfigurationManager.AppSettings["SmtpServer"];
                var port = ConfigurationManager.AppSettings["Port"];
                var senderEmailId = ConfigurationManager.AppSettings["SenderEmailId"];
                var senderPassword = ConfigurationManager.AppSettings["SenderPassword"];

                var smptClient = new SmtpClient(smtpServerName, Convert.ToInt32(port))
                {
                    Credentials = new NetworkCredential(senderEmailId, senderPassword),
                    EnableSsl = true
                };
                smptClient.Send(senderEmailId, txtTo, txtSubject, txtContent);
                MessageBox.Show("Message Sent Successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show("There was an error that occurred: " + ex, "Error Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnLanguageChange_Click(object sender, RoutedEventArgs e)
        {
            hmongEnglishChange = !hmongEnglishChange;

            if (hmongEnglishChange == true)
            {
                btnEmail.FontFamily = new FontFamily("Mong New Plain");
                btnEmail.FontSize = 16;
                btnEmail.Content = "xa simWfl";
                tlbFinance.FontFamily = new FontFamily("Mong New Plain");
                tlbFinance.FontSize = 16;
                tlbFinance.Header = "YsEajsFa";
                tlbTithing.FontSize = 16;
                tlbTithing.Header = "seVUIkaIo";
                tlbOffering.FontSize = 16;
                tlbOffering.Header = "YsEaSseadaew";
                tlbAdmin.FontFamily = new FontFamily("Mong New Plain");
                tlbAdmin.FontSize = 16;
                tlbAdmin.Header = "gwESaes";
                tlbNewUser.FontSize = 16;
                tlbNewUser.Header = "cuibWghsea";
                tlbEditUser.FontSize = 16;
                tlbEditUser.Header = "khoTaiwgwicuea";
                tlbFile.FontFamily = new FontFamily("Mong New Plain");
                tlbFile.FontSize = 16;
                tlbFile.Header = "zwIcsa";
                tlbAbout.FontSize = 16;
                tlbAbout.Header = "paisys";
                tlbExiting.FontSize = 16;
                tlbExiting.Header = "kaw";

                btnLanguage.FontFamily = new FontFamily("Segoe UI");
                btnLanguage.FontSize = 14;
                btnLanguage.Content = "English";
            }
            else
            {
                btnEmail.FontFamily = new FontFamily("Segoe UI");
                btnEmail.FontSize = 14;
                btnEmail.Content = "Email Test";
                tlbFinance.FontFamily = new FontFamily("Segoe UI");
                tlbFinance.FontSize = 14;
                tlbFinance.Header = "Finance";
                tlbTithing.FontSize = 14;
                tlbTithing.Header = "Tithing";
                tlbOffering.FontSize = 14;
                tlbOffering.Header = "Offering Donation";
                tlbAdmin.FontFamily = new FontFamily("Segoe UI");
                tlbAdmin.FontSize = 14;
                tlbAdmin.Header = "Admin";
                tlbNewUser.FontSize = 14;
                tlbNewUser.Header = "Add New User";
                tlbEditUser.FontSize = 14;
                tlbEditUser.Header = "Edit User Information";
                tlbFile.FontFamily = new FontFamily("Segoe UI");
                tlbFile.FontSize = 14;
                tlbFile.Header = "File";
                tlbAbout.FontSize = 14;
                tlbAbout.Header = "About";
                tlbExiting.FontSize = 14;
                tlbExiting.Header = "Exit";

                btnLanguage.FontFamily = new FontFamily("Mong New Plain");
                btnLanguage.FontSize = 16;
                btnLanguage.Content = "lufmOe";
            }
        }

        private void tlbExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void tlbNewUser_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new AddUser();
            newUser.ShowDialog();

            try
            {
                newUser.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
