using Lib_System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Library_System_Draft.ADMIN_HOME;

namespace Library_System_Draft
{
    /// <summary>
    /// Interaction logic for STUDENT_HOME.xaml
    /// </summary>
    public partial class STUDENT_HOME : Window
    {
        private bool canProcDash = true;
        private bool canProcBorrow = false;
        private bool canProcReturn = false;
        private bool canProcHist = false;
        private bool canProcLog = false;
        private string selectedID;
        private string StudentId;
        private int BookQuantity;
        private string BB_Id;
        private SolidColorBrush color1 = new SolidColorBrush(Color.FromRgb(66, 85, 255)); //Blue color for text and background of nav panel
        private SolidColorBrush color2 = new SolidColorBrush(Color.FromRgb(246, 250, 255)); //Almost white for background for button and main bak
        private SolidColorBrush transparent = new SolidColorBrush(Color.FromArgb(0, 1, 1, 1)); //transparent color
        private string fileloc = "\\IMAGES\\";
        static SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Library_System;Integrated Security=True");

        public STUDENT_HOME()
        {
            InitializeComponent();          
            STUDENT_HOME_Loaded();

        }

        private void STUDENT_HOME_Loaded()
        {
            Display_Grid_Book();
            Display_Details();
            Display_Grid_Return();
            Display_Grid_History();
        }
        private void menuPanelColors(int choice) 
        {
            dashboardGridButton.Background = transparent;
            borrowGridButton.Background = transparent;
            returnGridButton.Background = transparent;
            historyGridButton.Background = transparent;
            logoutGridButton.Background = transparent;
            canProcDash = false;
            canProcBorrow = false;
            canProcReturn = false;
            canProcHist = false;
            canProcLog = false;
            homeView.Visibility = Visibility.Hidden;
            borrowView.Visibility = Visibility.Hidden;
            returnView.Visibility = Visibility.Hidden;
            histView.Visibility = Visibility.Hidden;
            txtDashboard.Foreground = color2;
            txtBorrow.Foreground = color2;
            txtReturn.Foreground = color2;
            txtHistory.Foreground = color2;
            txtLogOut.Foreground = color2;
            dashboardRec.Visibility = Visibility.Hidden;
            borrowRec.Visibility = Visibility.Hidden;
            returnRec.Visibility = Visibility.Hidden;
            historyRec.Visibility = Visibility.Hidden;
            logoutRec.Visibility = Visibility.Hidden;
            dashboardLogo.Source = new BitmapImage(new Uri(fileloc + "dashboard.png", UriKind.RelativeOrAbsolute));
            borrowLogo.Source = new BitmapImage(new Uri(fileloc + "borrow.png", UriKind.RelativeOrAbsolute));
            returnLogo.Source = new BitmapImage(new Uri(fileloc + "return.png", UriKind.RelativeOrAbsolute));
            historyLogo.Source = new BitmapImage(new Uri(fileloc + "history.png", UriKind.RelativeOrAbsolute));
            logoutLogo.Source = new BitmapImage(new Uri(fileloc + "logout.png", UriKind.RelativeOrAbsolute));

            if (choice == 1)
            {
                dashboardGridButton.Background = color2;
                canProcDash = true;
                homeView.Visibility = Visibility.Visible;
                txtDashboard.Foreground = color1;
                dashboardLogo.Source = new BitmapImage(new Uri(fileloc + "dashboardBlue.png", UriKind.RelativeOrAbsolute));
                dashboardRec.Visibility = Visibility.Visible;
            }
            else if (choice == 2)
            {
                borrowGridButton.Background = color2;
                canProcBorrow = true;
                borrowView.Visibility = Visibility.Visible;
                txtBorrow.Foreground = color1;
                borrowLogo.Source = new BitmapImage(new Uri(fileloc + "borrowBlue.png", UriKind.RelativeOrAbsolute));
                borrowRec.Visibility = Visibility.Visible;
            }
            else if (choice == 3)
            {
                returnGridButton.Background = color2;
                canProcReturn = true;
                returnView.Visibility = Visibility.Visible;
                txtReturn.Foreground = color1;
                returnLogo.Source = new BitmapImage(new Uri(fileloc + "returnBlue.png", UriKind.RelativeOrAbsolute));
                returnRec.Visibility = Visibility.Visible;
            }
            else if (choice == 4)
            {
                historyGridButton.Background = color2;
                canProcHist = true;
                histView.Visibility = Visibility.Visible;
                txtHistory.Foreground = color1;
                historyLogo.Source = new BitmapImage(new Uri(fileloc + "historyBlue.png", UriKind.RelativeOrAbsolute));
                historyRec.Visibility = Visibility.Visible;
            }
            else if (choice == 5) 
            {
                logoutGridButton.Background = color2;
                canProcLog = true;
                txtLogOut.Foreground = color1;
                logoutLogo.Source = new BitmapImage(new Uri(fileloc + "logoutBlue.png", UriKind.RelativeOrAbsolute));
                logoutRec.Visibility = Visibility.Visible;
            }
        }
        //--------------This part is for profile-----------------
        private void profileGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty,new DoubleAnimation(8, TimeSpan.FromSeconds(.1)));
        }

        private void profileGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(.1)));
        }


        //--------------This part is for dashboard-----------------
        private void Display_Details()
        {
            StudentId = DB_Connect.getStudentId;

            string FName = "SELECT Student_FirstName FROM Students WHERE Student_Id = '" + StudentId + "'";
            string tempFName = DB_Connect.getValue(FName);
            txtFLetName.Text = tempFName[0].ToString();
            txtFName.Text = tempFName;
            string LName = "SELECT Student_LastName FROM Students WHERE Student_Id = '" + StudentId + "'";
            string tempLName = DB_Connect.getValue(LName);
            txtLName.Text = tempLName;

            string Name = tempFName + " " + tempLName;
            txtstudentName.Text = "Name: " + Name;
            string Id = "SELECT Student_Number FROM Students WHERE Student_Id = '" + StudentId + "'";
            txtstudentID.Text = "ID #: " + DB_Connect.getValue(Id);
            txtID.Text = DB_Connect.getValue(Id);
            string Course = "SELECT Course FROM Students WHERE Student_Id = '" + StudentId + "'";
            txtstudentCourse.Text = "Course: " + DB_Connect.getValue(Course);
            string Year = "SELECT Year FROM Students WHERE Student_Id = '" + StudentId + "'";
            txtstudentYear.Text = "Year: " + DB_Connect.getValue(Year);

            string bbBooks = "SELECT COUNT(*) FROM BorrowedBooks WHERE Student_Id = '" + StudentId + "'";
            borrowCount.Text = DB_Connect.getValue(bbBooks);

            string rbbBooks = "SELECT TOP 1 b.Book_Title FROM BorrowedBooks bb JOIN Books b ON bb.Book_Id = b.Book_Id WHERE bb.Student_Id = '" + StudentId + "' ORDER BY bb.DateTime DESC;";
            recBorrow.Text = DB_Connect.getValue(rbbBooks);

            string RBooks = "SELECT TOP 1 b.Book_Title FROM ReturnedBooks rb JOIN Books b ON rb.Book_Id = b.Book_Id WHERE rb.Student_Id = '" + StudentId + "' ORDER BY rb.Returned_date DESC;";
            recReturn.Text = DB_Connect.getValue(RBooks);

            if (recBorrow.Text == "") 
            {
                recBorrow.Text = "None";
            }
            if (recReturn.Text == "") 
            {
                recReturn.Text = "None";
            }
        }

        private void dashboardGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            dashboardGridButton.Background = color2;
            txtDashboard.Foreground = color1;
            dashboardLogo.Source = new BitmapImage(new Uri(fileloc + "dashboardBlue.png", UriKind.RelativeOrAbsolute));
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(8, TimeSpan.FromSeconds(.1)));
        }

        private void dashboardGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcDash)
            {
                dashboardGridButton.Background = color1;
                txtDashboard.Foreground = color2;
                dashboardLogo.Source = new BitmapImage(new Uri(fileloc + "dashboard.png", UriKind.RelativeOrAbsolute));
            }
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(.1)));
        }

        private void dashboardGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuPanelColors(1);
            STUDENT_HOME_Loaded();
        }
        //-------------------This part is for borrow--------------------
        public class Book
        {
            public int BookId { get; set; }
            public string Book_Title { get; set; }
            public string Book_ISBN { get; set; }
            public int Book_Quantity { get; set; }
            public int AuthID { get; set; }
            public string Book_Author { get; set; }
            public string Book_Status { get; set; }
        }

        private void borrowGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            borrowGridButton.Background = color2;
            txtBorrow.Foreground = color1;
            borrowLogo.Source = new BitmapImage(new Uri(fileloc + "borrowBlue.png", UriKind.RelativeOrAbsolute));
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(8, TimeSpan.FromSeconds(.1)));
        }

        private void borrowGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcBorrow)
            {
                borrowGridButton.Background = color1;
                txtBorrow.Foreground = color2;
                borrowLogo.Source = new BitmapImage(new Uri(fileloc + "borrow.png", UriKind.RelativeOrAbsolute));
            }
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(.1)));
        }

        private void borrowGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuPanelColors(2);
            STUDENT_HOME_Loaded();
        }
        private void txtBookSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookSearch.Text))
            {
                Display_Grid_Book();
            }
            else
            {
                List<Book> filteredList = new List<Book>();
                conn.Close();
                conn.Open();

                string query = "SELECT *, " +
                               "CASE WHEN Books.Book_Quantity > 0 THEN 'AVAILABLE' ELSE 'NOT AVAILABLE' END AS Book_Status " +
                               "FROM Books " +
                               "JOIN Authors ON Books.Author_Id = Authors.Author_Id " +
                               "WHERE CONCAT(Book_Title, ' ', Book_ISBN, ' ', Authors.Author_FirstName, ' ', Authors.Author_LastName) LIKE '%" + txtBookSearch.Text + "%'";

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            BookId = Convert.ToInt32(reader["Book_Id"]),
                            Book_Title = reader["Book_Title"].ToString(),
                            Book_ISBN = reader["Book_ISBN"].ToString(),
                            Book_Quantity = (int)reader["Book_Quantity"],
                            AuthID = Convert.ToInt32(reader["Author_Id"]),
                            Book_Author = reader["Author_FirstName"].ToString() + " " + reader["Author_LastName"].ToString(),
                            Book_Status = reader["Book_Status"].ToString()
                        };

                        filteredList.Add(book);
                    }
                }

                bookDataGrid.ItemsSource = filteredList;
            }
        }
        private void borrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedID))
            {
                if (BookQuantity > 0)
                {
                    string query = "INSERT INTO BorrowedBooks (Student_Id, Book_Id, DateTime) " +
                    "VALUES ('" + StudentId + "','" + selectedID + "', GETDATE()) " +
                    "UPDATE Books SET Book_Quantity = Book_Quantity - 1 WHERE Book_Id = '" + selectedID + "'";
                    DB_Connect.QueryCommands(query);
                    Display_Grid_Book();
                }
                else
                {
                    MessageBox.Show("The book is out of stock!");
                }
            }
            else
            {
                MessageBox.Show("Select a Book First");
            }
            STUDENT_HOME_Loaded();

        }

        private void bookDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            

            if (gd.SelectedItem != null)
            {
                Book selectedBook = (Book)gd.SelectedItem;
                selectedID = selectedBook.BookId.ToString();
                BookQuantity = selectedBook.Book_Quantity;
            }

        }
        private void Display_Grid_Book()
        {
            List<Book> dataList = new List<Book>();
            conn.Close();
            conn.Open();
            SqlCommand command = new SqlCommand(@"
        SELECT 
            Books.Book_Id,
            Books.Book_Title,
            Books.Book_ISBN,
            Books.Book_Quantity,
            Books.Author_Id AS AuthID,
            Authors.Author_FirstName + ' ' + Authors.Author_LastName AS Book_Author,
            CASE
                WHEN Books.Book_Quantity > 0 THEN 'AVAILABLE'
                ELSE 'NOT AVAILABLE'
            END AS Book_Status
        FROM 
            Books
        JOIN
            Authors ON Books.Author_Id = Authors.Author_Id", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Book book = new Book
                    {
                        BookId = Convert.ToInt32(reader["Book_Id"]),
                        Book_Title = reader["Book_Title"].ToString(),
                        Book_ISBN = reader["Book_ISBN"].ToString(),
                        Book_Quantity = Convert.ToInt32(reader["Book_Quantity"]),
                        AuthID = Convert.ToInt32(reader["AuthID"]),
                        Book_Author = reader["Book_Author"].ToString(),
                        Book_Status = reader["Book_Status"].ToString()
                    };

                    dataList.Add(book);
                }
            }
            bookDataGrid.ItemsSource = dataList;
        }

        //-------------------This part is for return----------------
        public class RBook
        {
            public int BorrowedId { get; set; }
            public int ReturnBookID { get; set; }
            public string ReturnDate { get; set; }
            public string ReturnBook_Title { get; set; }
            public string ReturnBook_ISBN { get; set; }
            public string ReturnBook_Author { get; set; }
        }
        private void returnGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            returnGridButton.Background = color2;
            txtReturn.Foreground = color1;
            returnLogo.Source = new BitmapImage(new Uri(fileloc + "returnBlue.png", UriKind.RelativeOrAbsolute));
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(8, TimeSpan.FromSeconds(.1)));
        }

        private void returnGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcReturn)
            {
                returnGridButton.Background = color1;
                txtReturn.Foreground = color2;
                returnLogo.Source = new BitmapImage(new Uri(fileloc + "return.png", UriKind.RelativeOrAbsolute));
            }
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(.1)));
        }

        private void returnGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuPanelColors(3);
            STUDENT_HOME_Loaded();
        }

        private void txtRBookSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRBookSearch.Text))
            {
                Display_Grid_Return();
            }
            else
            {
                List<RBook> filteredList = new List<RBook>();
                conn.Close();
                conn.Open();

                string query = "SELECT BorrowedBooks.Borrowed_Book_Id, BorrowedBooks.Book_Id, BorrowedBooks.DateTime, " +
               "Books.Book_Title, Books.Book_ISBN, " +
               "CONCAT(Authors.Author_FirstName, ' ', Authors.Author_LastName) AS Author " +
               "FROM BorrowedBooks " +
               "JOIN Books ON BorrowedBooks.Book_Id = Books.Book_Id " +
               "JOIN Authors ON Books.Author_Id = Authors.Author_Id " +
               "WHERE (Books.Book_Title LIKE '%" + txtRBookSearch.Text + "%' OR " +
               "Books.Book_ISBN LIKE '" + txtRBookSearch.Text + "%') AND " +
               "BorrowedBooks.Student_Id = @StudentId";


                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@StudentId", StudentId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RBook rbook = new RBook
                        {
                            BorrowedId = Convert.ToInt32(reader["Borrowed_Book_Id"]),
                            ReturnBookID = Convert.ToInt32(reader["Book_Id"]),
                            ReturnDate = reader["DateTime"].ToString(),
                            ReturnBook_Title = reader["Book_Title"].ToString(),
                            ReturnBook_ISBN = reader["Book_ISBN"].ToString(),
                            ReturnBook_Author = reader["Author"].ToString(),

                        };

                        filteredList.Add(rbook);
                    }
                }

                returnDataGrid.ItemsSource = filteredList;
            }
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedID))
            {
                string query = "BEGIN TRANSACTION; " +
                "INSERT INTO ReturnedBooks (Borrowed_Book_Id, Book_Id, Student_Id, Borrowed_date, Returned_date) " +
                "SELECT Borrowed_Book_Id, Book_Id, Student_Id, DateTime, GETDATE() FROM BorrowedBooks WHERE Borrowed_Book_Id = " + BB_Id + "; " +
                "UPDATE Books SET Book_Quantity = Book_Quantity + 1 " +
                "WHERE Books.Book_Id = (SELECT Book_Id FROM BorrowedBooks WHERE Borrowed_Book_Id = " + BB_Id + "); " +
                "DELETE FROM BorrowedBooks WHERE Borrowed_Book_Id = " + BB_Id + "; " +
                "COMMIT TRANSACTION;";
                DB_Connect.QueryCommands(query);
                Display_Grid_Return();
                Display_Grid_Book();
            }
            else
            {
                MessageBox.Show("Select a Book First");
            }
            STUDENT_HOME_Loaded();

        }

        private void returnDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;


            if (gd.SelectedItem != null)
            {
                RBook selectedRBook = (RBook)gd.SelectedItem;
                selectedID = selectedRBook.ReturnBookID.ToString();
                BB_Id = selectedRBook.BorrowedId.ToString();
            }

        }
        private void Display_Grid_Return() 
        {
            
            List<RBook> dataList = new List<RBook>();
            conn.Close();
            conn.Open();
            SqlCommand command = new SqlCommand(@"
    SELECT 
        BorrowedBooks.Borrowed_Book_Id, 
        BorrowedBooks.Book_Id, 
        BorrowedBooks.DateTime, 
        Books.Book_Title, 
        Books.Book_ISBN, 
        CONCAT(Authors.Author_FirstName, ' ', Authors.Author_LastName) AS Author 
    FROM 
        BorrowedBooks
    JOIN 
        Books ON BorrowedBooks.Book_Id = Books.Book_Id
    JOIN 
        Authors ON Books.Author_Id = Authors.Author_Id
    WHERE 
        BorrowedBooks.Student_Id = @StudentId", conn);
            command.Parameters.AddWithValue("@StudentId", StudentId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    RBook rbook = new RBook
                    {
                        BorrowedId = Convert.ToInt32(reader["Borrowed_Book_Id"]),
                        ReturnBookID = Convert.ToInt32(reader["Book_Id"]),
                        ReturnDate = reader["DateTime"].ToString(),
                        ReturnBook_Title = reader["Book_Title"].ToString(),
                        ReturnBook_ISBN = reader["Book_ISBN"].ToString(),
                        ReturnBook_Author = reader["Author"].ToString(),

                    };

                    dataList.Add(rbook);
                }
            }
            returnDataGrid.ItemsSource = dataList;       
        }

        //----------------This part is for history-------------
        public class History
        {
            public int HistoryBorrowedId { get; set; }
            public int HistoryBookID { get; set; }
            public string HistoryBook_Title { get; set; }
            public string HistoryBook_ISBN { get; set; }
            public string HistoryBook_Author { get; set; }
            public string HistoryBook_Borrow_Date { get; set; }
            public string HistoryBook_Return_Date { get; set; }
        }
        private void historyGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            historyGridButton.Background = color2;
            txtHistory.Foreground = color1;
            historyLogo.Source = new BitmapImage(new Uri(fileloc + "historyBlue.png", UriKind.RelativeOrAbsolute));
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(8, TimeSpan.FromSeconds(.1)));
        }

        private void historyGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcHist)
            {
                historyGridButton.Background = color1;
                txtHistory.Foreground = color2;
                historyLogo.Source = new BitmapImage(new Uri(fileloc + "history.png", UriKind.RelativeOrAbsolute));
            }
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(.1)));
        }

        private void historyGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuPanelColors(4);
            STUDENT_HOME_Loaded();
        }
        private void Display_Grid_History() 
        {
            List<History> dataList = new List<History>();
            conn.Close();
            conn.Open();
            SqlCommand command = new SqlCommand(@"
    SELECT 
        rb.Borrowed_Book_Id, 
        rb.Book_Id, 
        b.Book_Title, 
        b.Book_ISBN, 
        CONCAT(a.Author_FirstName, ' ', a.Author_LastName) AS Author, 
        rb.Borrowed_date, 
        rb.Returned_date 
    FROM 
        ReturnedBooks rb 
    JOIN 
        Books b ON rb.Book_Id = b.Book_Id 
    JOIN 
        Authors a ON b.Author_Id = a.Author_Id 
    WHERE 
        rb.Student_Id = @StudentId", conn);
            command.Parameters.AddWithValue("@StudentId", StudentId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    History hist = new History
                    {
                        HistoryBorrowedId = Convert.ToInt32(reader["Borrowed_Book_Id"]),
                        HistoryBookID = Convert.ToInt32(reader["Book_Id"]),
                        HistoryBook_Title = reader["Book_Title"].ToString(),
                        HistoryBook_ISBN = reader["Book_ISBN"].ToString(),
                        HistoryBook_Author = reader["Author"].ToString(),
                        HistoryBook_Borrow_Date = reader["Borrowed_date"].ToString(),
                        HistoryBook_Return_Date = reader["Returned_date"].ToString(),

                    };

                    dataList.Add(hist);
                }
            }
            historyDataGrid.ItemsSource = dataList;
        }

        private void txtHistSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHistSearch.Text))
            {
                Display_Grid_History();
            }
            else
            {
                List<History> filteredList = new List<History>();
                conn.Close();
                conn.Open();

                string query = "SELECT rb.Borrowed_Book_Id, rb.Book_Id, b.Book_Title, b.Book_ISBN, " +
               "CONCAT(a.Author_FirstName, ' ', a.Author_LastName) AS Author, " +
               "rb.Borrowed_date, rb.Returned_date " +
               "FROM ReturnedBooks rb " +
               "JOIN Books b ON rb.Book_Id = b.Book_Id " +
               "JOIN Authors a ON b.Author_Id = a.Author_Id " +
               "WHERE b.Book_Title LIKE '%" + txtHistSearch.Text + "%' OR " +
               "b.Book_ISBN LIKE '" + txtHistSearch.Text + "%'OR " +
               "CONCAT(a.Author_FirstName, ' ', a.Author_LastName) LIKE '" + txtHistSearch.Text + "%'";

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        History hist = new History
                        {
                            HistoryBorrowedId = Convert.ToInt32(reader["Borrowed_Book_Id"]),
                            HistoryBookID = Convert.ToInt32(reader["Book_Id"]),
                            HistoryBook_Title = reader["Book_Title"].ToString(),
                            HistoryBook_ISBN = reader["Book_ISBN"].ToString(),
                            HistoryBook_Author = reader["Author"].ToString(),
                            HistoryBook_Borrow_Date = reader["Borrowed_date"].ToString(),
                            HistoryBook_Return_Date = reader["Returned_date"].ToString(),
                        };

                        filteredList.Add(hist);
                    }
                }

                historyDataGrid.ItemsSource = filteredList;
            }
        }
        //--------------This part is for logout--------------------

        private void logoutGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            logoutGridButton.Background = color2;
            txtLogOut.Foreground = color1;
            logoutLogo.Source = new BitmapImage(new Uri(fileloc + "logoutBlue.png", UriKind.RelativeOrAbsolute));
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(8, TimeSpan.FromSeconds(.1)));

        }

        private void logoutGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcLog)
            {
                logoutGridButton.Background = color1;
                txtLogOut.Foreground = color2;
                logoutLogo.Source = new BitmapImage(new Uri(fileloc + "logout.png", UriKind.RelativeOrAbsolute));

            }
            blurBitmapEffect.BeginAnimation(BlurBitmapEffect.RadiusProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(.1)));
        }

        private void logoutGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            menuPanelColors(5);

            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void minimizeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void closeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


    }
}
