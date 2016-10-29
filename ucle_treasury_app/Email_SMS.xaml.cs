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
using System.Windows.Shapes;

namespace ucle_treasury_app
{
    /// <summary>
    /// Interaction logic for Email_SMS.xaml
    /// </summary>
    public partial class Email_SMS : Window
    {
        public bool boolClosed = false;

        public Email_SMS()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text == "" || tbEmail.Text.Length == 0)
            {
                MessageBox.Show("Email address must not be empty.");
                return;
            }
            if (!tbEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email address must contain "+"@"+" symbol");
                return;
            }
            //if (tbSubject.Text == "" || tbSubject.Text.Length == 0)
            //{
            //    MessageBox.Show("Subject must not be empty.");
            //    return;
            //}
            if (tbMessage.Text == "" || tbMessage.Text.Length == 0)
            {
                MessageBox.Show("Message must not be empty.");
                return;
            }
            boolClosed = true;
            this.Close();
        }
    }
}
