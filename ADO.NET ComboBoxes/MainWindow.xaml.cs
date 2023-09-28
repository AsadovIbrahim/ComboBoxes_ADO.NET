using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ADO.NET_ComboBoxes
{
    public partial class MainWindow : Window
    { 

        public MainWindow()
        {
            InitializeComponent();
            booksName.IsEnabled = false;
        }

        private void authorsName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SqlConnection authorsConnection = new SqlConnection();
            authorsConnection.ConnectionString = "Data Source=DESKTOP-IBRAHIM\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;";
            SqlCommand authorsCommand=new SqlCommand();
            authorsCommand.CommandText = "SELECT * FROM Authors";
            authorsCommand.Connection= authorsConnection;

            authorsConnection.Open();
            SqlDataReader reader= authorsCommand.ExecuteReader();  
            while(reader.Read())
            {
                string name = reader["FirstName"].ToString();
                string surname = reader["LastName"].ToString();

                authorsName.Items.Add(string.Format("{0}-{1}", name, surname));
               
                
            }
            authorsConnection.Close();
            booksName.Items.Clear();
        }

        private void booksName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SqlConnection booksConnection = new SqlConnection();
            booksConnection.ConnectionString = "Data Source=DESKTOP-IBRAHIM\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;";
            SqlCommand booksCommand = new SqlCommand();
            booksCommand.CommandText = "SELECT Books.[Name], Authors.FirstName, " +
                "Authors.LastName FROM Books " +
                "INNER JOIN Authors ON Books.Id_Author = Authors.Id"; 
            booksCommand.Connection = booksConnection;

            booksConnection.Open();
            SqlDataReader readerBook = booksCommand.ExecuteReader();

            while (readerBook.Read())
            {
                string bookName = readerBook["Name"].ToString();
                string authorName = string.Format("{0}-{1}", readerBook["FirstName"], readerBook["LastName"]);

                if (authorName == authorsName.SelectedItem.ToString())
                {
                    booksName.Items.Add(bookName);
                }
            }

            booksConnection.Close();
        }

        private void authorsName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (authorsName.SelectedItem != null)
            {
                booksName.IsEnabled = true;
            }
            else
            {
                booksName.IsEnabled = false;
            }
        }
    }
}
