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
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Drawing;

namespace ucle_treasury_app
{
    /// <summary>
    /// Interaction logic for ViewUsers.xaml
    /// </summary>
    public partial class ViewUsers : Window
    {
        private string defaultPhoto = @"" + System.Environment.CurrentDirectory + @"\resources\images\no_image.png";
        public string database_conn_string = @"Data Source=" + System.Environment.CurrentDirectory + @"\resources\database\dbUCLE_Treasury.db;Version=3;ReadOnly=false;Journal Mode=Off";
        public ViewUsers()
        {
            InitializeComponent();
            LoadDataGrid();
            ResetLabels();
        }

        private void LoadDataGrid()
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataAdapter sqlite_data_adapter;
            DataTable dtUsers = new DataTable();

            sqlite_conn = new SQLiteConnection(database_conn_string, true);
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_data_adapter = new SQLiteDataAdapter();

            try
            {
                //sqlite_cmd.CommandText = "SELECT strFirstName, strLastName, substr(dtAdded,0,11) AS dtAdded, lngID FROM tMembers";
                sqlite_cmd.CommandText = "SELECT strFirstName || ' ' || strLastName AS strName, dtAdded, lngID FROM tMembers";
                sqlite_data_adapter.SelectCommand = sqlite_cmd;
                sqlite_data_adapter.Fill(dtUsers);

                dgUsers.ItemsSource = dtUsers.DefaultView;
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

        private void ResetLabels()
        {
            lblFirstName.Content = "UNITED CHRISTIANS";
            lblLastName.Content = "LIBERTY EVANGELICAL";
            lblPhoneNumber.Content = "(559) 255-8804";
            lblEmail.Content = "TESTING@EMAIL.COM";
            lblAddress.Content = "4557 E TULARE ST";
            lblCityStateZip.Content = "FRESNO, CA 93702";
            lblTithing.Content = "TITHING INFORMATION";
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUsers.SelectedItem == null) { return; }
            DataRowView row = (DataRowView)dgUsers.SelectedItems[0];
            int userId = Convert.ToInt32(row["lngID"]);

            LoadUserInfo(userId);
        }

        protected void btnEdit_Clicked(object sender, EventArgs e)
        {
            Button userTag = ((Button)sender);
            int userId = Convert.ToInt32(userTag.CommandParameter.ToString());
            var EditUser = new EditUser(userId);
            EditUser.ShowDialog();
            EditUser.Close();
            LoadDataGrid();
            LoadUserInfo(userId);
        }

        private void LoadUserInfo(int userValue)
        {
            try
            {
                SQLiteConnection sqlite_conn;
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                sqlite_conn = new SQLiteConnection(database_conn_string, true);
                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT t0.*, SUM(t1.rTithingAmount) as rTithingAmount FROM tMembers t0 LEFT JOIN tTithing t1 ON t0.lngID = t1.lngMemberID WHERE t0.lngID = @lngID";
                SQLiteParameter paramUserID = new SQLiteParameter("@lngID", System.Data.DbType.Int32);
                paramUserID.Value = userValue;
                sqlite_cmd.Parameters.Add(paramUserID);
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                // The SQLiteDataReader allows us to run through the result lines:
                while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                {
                    // Print out the content of the text field:
                    lblFirstName.Content = (sqlite_datareader["strFirstName"] != DBNull.Value) ? sqlite_datareader["strFirstName"].ToString() : "FIRST NAME";
                    lblLastName.Content = (sqlite_datareader["strLastName"] != DBNull.Value) ? sqlite_datareader["strLastName"].ToString() : "LAST NAME";
                    lblPhoneNumber.Content = (sqlite_datareader["strPhoneNo"] != DBNull.Value) ? "(" + sqlite_datareader["strPhoneNo"].ToString().Substring(0, 3) + ") " + sqlite_datareader["strPhoneNo"].ToString().Substring(3, 3) + "-" + sqlite_datareader["strPhoneNo"].ToString().Substring(6, 4) : "(XXX) XXX-XXXX";

                    lblEmail.Content = (sqlite_datareader["strEmail"] != DBNull.Value) ? sqlite_datareader["strEmail"].ToString() : "EMAIL@EMAIL.COM";
                    lblAddress.Content = (sqlite_datareader["strAddress"] != DBNull.Value) ? sqlite_datareader["strAddress"].ToString() : "4557 E. TULARE ST";
                    lblCityStateZip.Content = (sqlite_datareader["strCity"] != DBNull.Value) ? sqlite_datareader["strCity"].ToString() + ", " + sqlite_datareader["strState"].ToString() + " " + sqlite_datareader["strZipCode"].ToString() : "FRESNO, CA 93702";
                    lblTithing.Content = (sqlite_datareader["rTithingAmount"] != DBNull.Value) ? "Total Tithing: $" + Convert.ToDouble(sqlite_datareader["rTithingAmount"]).ToString("N2") : "No Tithing Record";

                    if (sqlite_datareader["blobUserPhoto"] != DBNull.Value)
                    {
                        try
                        {
                            byte[] a = (System.Byte[])sqlite_datareader["blobUserPhoto"];
                            imgUser.Source = ByteToImage(a);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("There was an error loading user's photo: " + ex.Message, "Loading User Photo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        imgUser.Source = new BitmapImage(new Uri(defaultPhoto));
                    }
                }

                sqlite_conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("db error: " + ex.Message, "Loading Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
