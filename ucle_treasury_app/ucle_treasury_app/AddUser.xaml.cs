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
using System.IO;
using Microsoft.Win32;
using System.Data.SQLite;
using System.Drawing;
using System.Data;

namespace ucle_treasury_app
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private long lngUserPhoto;
        private string userPhoto;
        public AddUser()
        {
            InitializeComponent();
            LoadUser();
        }

        private void LoadUser()
        {
            try
            {
                // [snip] - As C# is purely object-oriented the following lines must be put into a class:

                // We use these three SQLite objects:
                SQLiteConnection sqlite_conn;
                SQLiteCommand sqlite_cmd;
                SQLiteDataReader sqlite_datareader;

                // create a new database connection:
                sqlite_conn = new SQLiteConnection(@"Data Source=C:\Users\txawjteeb\Documents\Visual Studio 2015\Projects\ucle_treasury_app\ucle_treasury_app\bin\Debug\dbUCLE_Treasury.db;Version=3;");

                // open the connection:
                sqlite_conn.Open();

                //// create a new SQL command:
                sqlite_cmd = sqlite_conn.CreateCommand();

                // But how do we read something out of our table ?
                // First lets build a SQL-Query again:
                sqlite_cmd.CommandText = "SELECT * FROM tMembers";

                // Now the SQLiteCommand object can give us a DataReader-Object:
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                // The SQLiteDataReader allows us to run through the result lines:
                while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                {
                    // Print out the content of the text field:
                    tbFirstName.Text = sqlite_datareader["strFirstName"].ToString();
                    tbLastName.Text = sqlite_datareader["strLastName"].ToString();
                }

                // We are ready, now lets cleanup and close our connection:
                sqlite_conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("db error: " + ex.Message);
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
                // long filesize = ((FileUpload1.PostedFile.ContentLength) / 1024) / 1024;
                if (filesize < 10000000)
                {
                    lngUserPhoto = filesize;
                    userPhoto = op.FileName;
                    imgUserPhoto.Source = new BitmapImage(new Uri(op.FileName));
                    MessageBox.Show("Image File Size: " + lngUserPhoto);
                }
                else
                {
                    MessageBox.Show("You are not allowed to upload more than 1MB.");
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Image photo = new Bitmap(userPhoto);
            byte[] pic = ImageToByte(photo, System.Drawing.Imaging.ImageFormat.Jpeg);
            SaveImage(pic);
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
        //public Image Base64ToImage(string base64String)
        public BitmapImage ByteToImage(byte[] imageBytes)
        {
            // Convert byte[] to Image
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            ms.Position = 0;

            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            return ix;
        }
        /***************** SQLite **************************/
        void SaveImage(byte[] imagen)
        {
            string conString = @"Data Source=C:\Users\txawjteeb\Documents\Visual Studio 2015\Projects\ucle_treasury_app\ucle_treasury_app\bin\Debug\dbUCLE_Treasury.db";
            SQLiteConnection con = new SQLiteConnection(conString);
            SQLiteCommand cmd = con.CreateCommand();
            cmd.CommandText = String.Format("INSERT INTO tMembers (iUserPhoto) VALUES (@0);");
            SQLiteParameter param = new SQLiteParameter("@0", System.Data.DbType.Binary);
            param.Value = imagen;
            cmd.Parameters.Add(param);
            con.Open();

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saving Image into Database SUCCESSFUL!");
            }
            catch (Exception exc1)
            {
                MessageBox.Show(exc1.Message);
            }
            con.Close();
        }
        void LoadImage()
        {
            string query = "SELECT iUserPhoto FROM tMembers WHERE lngID=3;";
            string conString = @"Data Source=C:\Users\txawjteeb\Documents\Visual Studio 2015\Projects\ucle_treasury_app\ucle_treasury_app\bin\Debug\dbUCLE_Treasury.db";
            SQLiteConnection con = new SQLiteConnection(conString);
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            con.Open();
            try
            {
                IDataReader rdr = cmd.ExecuteReader();
                try
                {
                    while (rdr.Read())
                    {
                        byte[] a = (System.Byte[])rdr[0];
                        imgUserPhoto.Source = ByteToImage(a);
                    }
                    MessageBox.Show("Loading Image from Database SUCCESSFUL!");
                }
                catch (Exception exc) { MessageBox.Show(exc.Message); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            con.Close();
        }

        private void btnPreload_Click(object sender, RoutedEventArgs e)
        {
            LoadImage();
        }
    }
}
