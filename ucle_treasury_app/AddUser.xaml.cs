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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Data.SQLite;
using System.Drawing;
using System.Data;
using System.Threading;

namespace ucle_treasury_app
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private long lngUserPhoto;
        private string userPhoto;
        private string defaultPhoto = @"" + System.Environment.CurrentDirectory + @"\resources\images\no_image.png";
        private string defaultUriPhoto;
        public string database_conn_string = @"Data Source=" + System.Environment.CurrentDirectory + @"\resources\database\dbUCLE_Treasury.db;Version=3;ReadOnly=false;Journal Mode=Off";

        public AddUser()
        {
            InitializeComponent();
            imgUserPhoto.Source = new BitmapImage(new Uri(defaultPhoto));
            defaultUriPhoto = imgUserPhoto.Source.ToString();
            LoadComboBoxes();
        }

        private void LoadUser()
        {
            try
            {
                SQLiteConnection sqlite_conn;
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_conn = new SQLiteConnection(database_conn_string, true);
                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT * FROM tMembers";
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                // The SQLiteDataReader allows us to run through the result lines:
                while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                {
                    // Print out the content of the text field:
                    tbFirstName.Text = sqlite_datareader["strFirstName"].ToString();
                    tbLastName.Text = sqlite_datareader["strLastName"].ToString();
                }
                sqlite_conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("db error: " + ex.Message, "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadComboBoxes()
        {
            DataSet state_carriers = new DataSet();

            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataAdapter sqlite_data_adapter;

            sqlite_conn = new SQLiteConnection(database_conn_string, true);
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT strStateID, strStateFull FROM tState; SELECT strCarrierName, lngCarrierID FROM tPhoneCarriers";
            sqlite_data_adapter = new SQLiteDataAdapter();
            sqlite_data_adapter.SelectCommand = sqlite_cmd;
            try
            {
                sqlite_data_adapter.Fill(state_carriers);

                cboState.ItemsSource = state_carriers.Tables[0].DefaultView;
                cboState.DisplayMemberPath = state_carriers.Tables[0].Columns["strStateFull"].ToString();
                cboState.SelectedValuePath = state_carriers.Tables[0].Columns["strStateID"].ToString();

                cboCarrier.ItemsSource = state_carriers.Tables[1].DefaultView;
                cboCarrier.DisplayMemberPath = state_carriers.Tables[1].Columns["strCarrierName"].ToString();
                cboCarrier.SelectedValuePath = state_carriers.Tables[1].Columns["lngCarrierID"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);        
            }
            finally
            {
                sqlite_conn.Close();
                sqlite_cmd.Dispose();
                sqlite_data_adapter.Dispose();
            }
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                long filesize = new FileInfo(op.FileName).Length;
                if (filesize < 100000)
                {
                    lngUserPhoto = filesize;
                    userPhoto = op.FileName;
                    Storyboard stbFadeOut = (Storyboard)FindResource("FadeOut");
                    stbFadeOut.Begin();
                }
                else
                {
                    MessageBox.Show("You are not allowed to upload a photo more than 400KB in size. Resize your photo to an acceptable range.", "Large Image Filesize", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string strFirstName;
            string strLastName;
            string strPhone;
            int intCarrier;
            string strEmail;
            string strAddress;
            string strCity;
            string strState;
            string strZipCode;
            byte[] userPic = null;

            if (validateTextbox() != true)
                return;
            if (MessageBox.Show("Are you sure you want to save this user information?", "Creating New User", MessageBoxButton.YesNo, MessageBoxImage.None) != MessageBoxResult.Yes)
                return;

            strFirstName = tbFirstName.Text.ToString().Trim();
            strLastName = tbLastName.Text.ToString().Trim();
            strPhone = tbPhone.Text.ToString().Trim();
            intCarrier = Convert.ToInt32(cboCarrier.SelectedValue.ToString().Trim());
            strEmail = tbEmail.Text.ToString().Trim();
            strAddress = tbAddress.Text.ToString().Trim();
            strCity = tbCity.Text.ToString().Trim();
            strState = cboState.SelectedValue.ToString().Trim();
            strZipCode = tbZipCode.Text.ToString().Trim();

            if (imgUserPhoto.Source == null)
            {
                if (MessageBox.Show("You have not uploaded a profile photo yet." + Environment.NewLine + Environment.NewLine + "Are you sure you want to create new user without a photo?", "Upload User Photo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;
            }
            else
            {
                System.Drawing.Image photo = new Bitmap(userPhoto);
                userPic = ImageToByte(photo, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            SQLiteConnection con = new SQLiteConnection(database_conn_string, true);
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("INSERT INTO tMembers (strFirstName, strLastName, strPhoneNo, lngCarrierID, strEmail, strAddress, strCity, strState, strZipCode, blobUserPhoto, lngAdminLevel, dtAdded, strAddedBy) VALUES(@firstName, @lastName, @phone, @carrier, @email, @address, @city, @state, @zipCode, @imgUser, @adminLevel, @dateAdded, @addedByUser); ");

            SQLiteParameter paramFirstName = new SQLiteParameter("@firstName", System.Data.DbType.String);
            SQLiteParameter paramLastName = new SQLiteParameter("@lastName", System.Data.DbType.String);
            SQLiteParameter paramPhone = new SQLiteParameter("@phone", System.Data.DbType.String);
            SQLiteParameter paramCarrier = new SQLiteParameter("@carrier", System.Data.DbType.Int32);
            SQLiteParameter paramEmail = new SQLiteParameter("@email", System.Data.DbType.String);
            SQLiteParameter paramAddress = new SQLiteParameter("@address", System.Data.DbType.String);
            SQLiteParameter paramCity = new SQLiteParameter("@city", System.Data.DbType.String);
            SQLiteParameter paramState = new SQLiteParameter("@state", System.Data.DbType.String);
            SQLiteParameter paramZipCode = new SQLiteParameter("@zipCode", System.Data.DbType.String);
            SQLiteParameter paramImgUser = new SQLiteParameter("@imgUser", System.Data.DbType.Binary);
            SQLiteParameter paramAdminLevel = new SQLiteParameter("@adminLevel", System.Data.DbType.Int32);
            SQLiteParameter paramDateAdded = new SQLiteParameter("@dateAdded", System.Data.DbType.String);
            SQLiteParameter paramAddedByUser = new SQLiteParameter("@addedByUser", System.Data.DbType.String);

            paramFirstName.Value = strFirstName;
            paramLastName.Value = strLastName;
            paramPhone.Value = strPhone;
            paramCarrier.Value = intCarrier;
            paramEmail.Value = strEmail;
            paramAddress.Value = strAddress;
            paramCity.Value = strCity;
            paramState.Value = strState;
            paramZipCode.Value = strZipCode;
            paramImgUser.Value = userPic;
            paramAdminLevel.Value = 0;
            paramDateAdded.Value = DateTime.Now.ToString();
            paramAddedByUser.Value = "David Lee's Coding Skills";

            cmd.Parameters.Add(paramFirstName);
            cmd.Parameters.Add(paramLastName);
            cmd.Parameters.Add(paramPhone);
            cmd.Parameters.Add(paramCarrier);
            cmd.Parameters.Add(paramEmail);
            cmd.Parameters.Add(paramAddress);
            cmd.Parameters.Add(paramCity);
            cmd.Parameters.Add(paramState);
            cmd.Parameters.Add(paramZipCode);
            cmd.Parameters.Add(paramImgUser);
            cmd.Parameters.Add(paramAdminLevel);
            cmd.Parameters.Add(paramDateAdded);
            cmd.Parameters.Add(paramAddedByUser);

            con.Open();

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added User Profile Successfully!", "User Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Saving Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                con.Close();
            }
        }

        public byte[] ImageToByte(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }
        public BitmapImage ByteToImage(byte[] imageBytes)
        {
            // Convert byte[] to Image
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            ms.Position = 0;

            BitmapImage imgBytes = new BitmapImage();
            imgBytes.BeginInit();
            imgBytes.CacheOption = BitmapCacheOption.OnLoad;
            imgBytes.StreamSource = ms;
            imgBytes.EndInit();
            return imgBytes;
        }
        /***************** SQLite **************************/
        void SaveImage(byte[] imagen)
        {
            SQLiteConnection con = new SQLiteConnection(database_conn_string, true);
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("INSERT INTO tMembers (iUserPhoto) VALUES (@0);");
            SQLiteParameter param = new SQLiteParameter("@0", System.Data.DbType.Binary);
            param.Value = imagen;
            cmd.Parameters.Add(param);
            con.Open();

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saving Image into Database SUCCESSFUL!", "Saving Image to Database", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exc1)
            {
                MessageBox.Show(exc1.Message, "Database Image Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            con.Close();
        }

        private bool validateTextbox()
        {
            string errorText = string.Empty;
            int errors = 0;

            if(tbFirstName.Text.Length == 0 || string.IsNullOrEmpty(tbFirstName.Text))
            {
                errorText = Environment.NewLine + "- First Name is required.";
                lblFirstName.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblFirstName.Foreground = System.Windows.Media.Brushes.Black;
            }
            if(tbLastName.Text.Length == 0 || string.IsNullOrEmpty(tbLastName.Text))
            {
                errorText += Environment.NewLine + "- Last Name is required.";
                lblLastName.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblLastName.Foreground = System.Windows.Media.Brushes.Black;
            }
            if (tbPhone.Text.Length == 0 || string.IsNullOrEmpty(tbPhone.Text))
            {
                errorText += Environment.NewLine + "- Full Phone Number is required.";
                lblPhone.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else if (tbPhone.Text.Length != 10)
            {
                errorText += Environment.NewLine + "- 10-digit Phone Number is required";
                lblPhone.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblPhone.Foreground = System.Windows.Media.Brushes.Black;
            }
            if (cboCarrier.SelectedValue == null)
            {
                errorText += Environment.NewLine + "- Phone Carrier is required.";
                lblCarrier.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblCarrier.Foreground = System.Windows.Media.Brushes.Black;
            }
            if (tbEmail.Text.Length == 0 || string.IsNullOrEmpty(tbEmail.Text))
            {
                errorText += Environment.NewLine + "- Email is required.";
                lblEmail.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else if (!tbEmail.Text.Contains("@"))
            {
                errorText += Environment.NewLine + "- @ is required for an email";
                lblEmail.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblEmail.Foreground = System.Windows.Media.Brushes.Black;
            }
            if (tbAddress.Text.Length == 0 || string.IsNullOrEmpty(tbAddress.Text))
            {
                errorText += Environment.NewLine + "- Address is required.";
                lblAddress.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblAddress.Foreground = System.Windows.Media.Brushes.Black;
            }
            if (tbCity.Text.Length == 0 || string.IsNullOrEmpty(tbCity.Text))
            {
                errorText += Environment.NewLine + "- City is required.";
                lblCity.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblCity.Foreground = System.Windows.Media.Brushes.Black;
            }
            if (cboState.SelectedValue == null)
            {
                errorText += Environment.NewLine + "- State is required.";
                lblState.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblState.Foreground = System.Windows.Media.Brushes.Black;
            }
            if (tbZipCode.Text.Length == 0 || string.IsNullOrEmpty(tbZipCode.Text))
            {
                errorText += Environment.NewLine + "- Zip Code is required.";
                lblZipCode.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else if (tbZipCode.Text.Length != 5)
            {
                errorText += Environment.NewLine + "- Zip Code needs to be 5 digits long.";
                lblZipCode.Foreground = System.Windows.Media.Brushes.OrangeRed;
                errors++;
            }
            else
            {
                lblZipCode.Foreground = System.Windows.Media.Brushes.Black;
            }

            if (errors > 0)
            {
                MessageBox.Show(errors + " Required Fields need to be filled." + Environment.NewLine + errorText, "Required Fields", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }      
            return true;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (imgUserPhoto.Source == null)
            {
                MessageBox.Show("There is no image uploaded.", "No Image Set", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                userPhoto = null;
                Storyboard stbFadeOut = (Storyboard)FindResource("DefaultFadeOut");
                stbFadeOut.Begin();
            }
        }

        private void DefaultImg_Completed(object sender, EventArgs e)
        {
            imgUserPhoto.Source = new BitmapImage(new Uri(defaultPhoto));
            Storyboard stbFadeOut = (Storyboard)FindResource("DefaultFadeIn");
            stbFadeOut.Begin();
        }

        private void Image_Completed(object sender, EventArgs e)
        {
            imgUserPhoto.Source = new BitmapImage(new Uri(userPhoto));
            Storyboard stbFadeOut = (Storyboard)FindResource("DefaultFadeIn");
            stbFadeOut.Begin();
        }

        private void numbersOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //void LoadImage()
        //{
        //    string query = "SELECT TOP 1 lngUserPhoto FROM tMembers;";
        //    SQLiteConnection con = new SQLiteConnection(database_conn_string);
        //    SQLiteCommand cmd = new SQLiteCommand(query, con);
        //    con.Open();
        //    try
        //    {
        //        IDataReader rdr = cmd.ExecuteReader();
        //        try
        //        {
        //            while (rdr.Read())
        //            {
        //                byte[] a = (System.Byte[])rdr[0];
        //                imgUserPhoto.Source = ByteToImage(a);
        //            }
        //            MessageBox.Show("Loading Image from Database SUCCESSFUL!");
        //        }
        //        catch (Exception exc) { MessageBox.Show(exc.Message, "Database Image Reading Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.Message, "DataReader Execute Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        //    con.Close();
        //}

        //private void btnPreload_Click(object sender, RoutedEventArgs e)
        //{
        //    LoadImage();
        //}
    }
}
