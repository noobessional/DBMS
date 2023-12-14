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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library_System_Draft
{
    /// <summary>
    /// Interaction logic for ADMIN_HOME.xaml
    /// </summary>
    public partial class ADMIN_HOME : Window
    {
        private bool canProcDash = true;
        private bool canProcAuth = false;
        private bool canProcBook = false;
        private bool canProcStud = false;
        private bool canProcHist = false;
        private bool canProcLog = false;
        private string selectedID;
        private int selectedAuthorId;
        static SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Library_System;Integrated Security=True");
        public ADMIN_HOME()
        {
            InitializeComponent();
            loading();
            dashboardGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
        }
        private void loading() 
        {
            Display_Grid_Author();
            Display_CB_Authors();
            Display_Grid_Book();
            Display_Grid_Student();
            Display_Grid_RBook();
            Display_Details();
        }

        //----------------This part is for dashboard--------------------
        private void Display_Details()
        {
            string StudentCount = "SELECT COUNT(*) FROM Students;";
            regStudentsCount.Text = DB_Connect.getValue(StudentCount);

            string BookCounts = "SELECT COUNT(*) FROM Books;";
            bookCount.Text = DB_Connect.getValue(BookCounts);

            string BorrowCounts = "SELECT COUNT(*) FROM BorrowedBooks;";
            borrowCount.Text = DB_Connect.getValue(BorrowCounts);

            string ReturnedCounts = "SELECT COUNT(*) FROM ReturnedBooks;";
            returnedCount.Text = DB_Connect.getValue(ReturnedCounts);

            string rbbBooks = "SELECT TOP 1 b.Book_Title FROM BorrowedBooks bb JOIN Books b ON bb.Book_Id = b.Book_Id ORDER BY bb.DateTime DESC;";
            recBorrowCount.Text = DB_Connect.getValue(rbbBooks);

            string RBooks = "SELECT TOP 1 b.Book_Title FROM ReturnedBooks rb JOIN Books b ON rb.Book_Id = b.Book_Id ORDER BY rb.Returned_date DESC;";
            recReturnedCount.Text = DB_Connect.getValue(RBooks);


        }

        private void dashboardGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            dashboardGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));


        }

        private void dashboardGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcDash)
            {
                dashboardGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            }
        }

        private void dashboardGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dashboardGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
            authorGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            bookGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            studentGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            historyGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            logoutGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            canProcDash = true;
            canProcAuth = false;
            canProcBook = false;
            canProcStud = false;
            canProcHist = false;
            canProcLog = false;
            homeView.Visibility = Visibility.Visible;
            authorView.Visibility = Visibility.Hidden;
            bookView.Visibility = Visibility.Hidden;
            studentView.Visibility = Visibility.Hidden;
            historyView.Visibility = Visibility.Hidden;
            Console.WriteLine("Ok naman gud");
            loading();
        }


        //----------------This part is for Author--------------------
        public class Author
        {
            public int AuthId { get; set; }
            public string Author_FirstName { get; set; }
            public string Author_LastName { get; set; }
            public string Author_Country { get; set; }

            public string FullName { get; set; }
        }


        private void authorGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            authorGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));

        }

        private void authorGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcAuth)
            {
                authorGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            }
        }

        private void authorGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dashboardGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            authorGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
            bookGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            studentGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            historyGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            logoutGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            canProcDash = false;
            canProcAuth = true;
            canProcBook = false;
            canProcStud = false;
            canProcHist = false;
            canProcLog = false;
            homeView.Visibility = Visibility.Hidden;
            authorView.Visibility = Visibility.Visible;
            bookView.Visibility = Visibility.Hidden;
            studentView.Visibility = Visibility.Hidden;
            historyView.Visibility = Visibility.Hidden;
            Console.WriteLine("Ok naman gud");
            loading();
        }
        private void authModifyButton_Click(object sender, RoutedEventArgs e)
        {
            authorDefView.Visibility = Visibility.Hidden;
            authorModView.Visibility = Visibility.Visible;
        }

        private void authCancelButton_Click(object sender, RoutedEventArgs e)
        {
            authorDefView.Visibility = Visibility.Visible;
            authorModView.Visibility = Visibility.Hidden;
            selectedID = "";

            txtAuthorFName.Clear();
            txtAuthorLName.Clear();
            txtAuthorCountry.Clear();
            loading();

        }
        private void authAddButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "insert into Authors values ('" + txtAuthorFName.Text + "','" + txtAuthorLName.Text + "','" + txtAuthorCountry.Text + "')";
            DB_Connect.QueryCommands(query);
            Display_Grid_Author();

        }

        private void authUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE Authors " +
                "SET Author_FirstName = '" + txtAuthorFName.Text + "', " +
                "Author_LastName = '" + txtAuthorLName.Text + "', " +
                "Author_Country = '" + txtAuthorCountry.Text + "' " +
                "WHERE Author_Id = " + selectedID;

            DB_Connect.QueryCommands(query);
            Display_Grid_Author();

        }

        private void authDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "delete from Authors where  Author_Id = " + selectedID + "";
            DB_Connect.QueryCommands(query);
            Display_Grid_Author();
        }
        private void authDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;

            if (gd.SelectedItem != null)
            {
                Author selectedAuthor = (Author)gd.SelectedItem;

                selectedID = selectedAuthor.AuthId.ToString();
                txtAuthorFName.Text = selectedAuthor.Author_FirstName;
                txtAuthorLName.Text = selectedAuthor.Author_LastName;
                txtAuthorCountry.Text = selectedAuthor.Author_Country;

            }
        }
        private void txtAuthSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAuthSearch.Text))
            {
                Display_Grid_Author();
            }
            else
            {
                List<Author> filteredList = new List<Author>();
                conn.Close();
                conn.Open();

                string query = "SELECT * FROM Authors WHERE " +
                    "Author_FirstName + ' ' + " +
                    "Author_Country + '' +" +
                    "Author_LastName LIKE '%" + txtAuthSearch.Text + "%'";

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Author author = new Author
                        {
                            AuthId = Convert.ToInt32(reader["Author_Id"]),
                            Author_FirstName = reader["Author_FirstName"].ToString(),
                            Author_LastName = reader["Author_LastName"].ToString(),
                            Author_Country = reader["Author_Country"].ToString()
                        };

                        filteredList.Add(author);
                    }
                }

                authDataGrid.ItemsSource = filteredList;
            }
        }

        private void Display_Grid_Author()
        {
            List<Author> dataList = new List<Author>();
            conn.Close();
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Authors", conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Author author = new Author
                    {
                        AuthId = Convert.ToInt32(reader["Author_Id"]),
                        Author_FirstName = reader["Author_FirstName"].ToString(),
                        Author_LastName = reader["Author_LastName"].ToString(),
                        Author_Country = reader["Author_Country"].ToString()
                    };

                    dataList.Add(author);
                }
            }
            authDataGrid.ItemsSource = dataList;

        }



        //----------------This part is for Books--------------------'
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
        private void bookGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            bookGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
            
        }

        private void bookGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcBook)
            {
                bookGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            }
        }

        private void bookGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dashboardGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            authorGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            bookGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
            studentGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            historyGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            logoutGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            canProcDash = false;
            canProcAuth = false;
            canProcBook = true;
            canProcStud = false;
            canProcHist = false;
            canProcLog = false;
            homeView.Visibility = Visibility.Hidden;
            authorView.Visibility = Visibility.Hidden;
            bookView.Visibility = Visibility.Visible;
            studentView.Visibility = Visibility.Hidden;
            historyView.Visibility = Visibility.Hidden;
            Console.WriteLine("Ok naman gud");
            loading();
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

        private void bookModifyButton_Click(object sender, RoutedEventArgs e)
        {
            bookDefView.Visibility = Visibility.Hidden;
            bookModView.Visibility = Visibility.Visible;
        }

        private void bookDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;

            if (gd.SelectedItem != null)
            {
                Book selectedBook = (Book)gd.SelectedItem;

                selectedID = selectedBook.BookId.ToString();
                txtBookTitle.Text = selectedBook.Book_Title;
                txtBookISBN.Text = selectedBook.Book_ISBN;
                selectedAuthorId = selectedBook.AuthID;
                txtBookQuantity.Text = selectedBook.Book_Quantity.ToString();

            }
        }

        private void bookAddButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "insert into Books values ('" + txtBookTitle.Text + "','" + txtBookISBN.Text +
                "','" + txtBookQuantity.Text + "','" + selectedAuthorId + "')";
            DB_Connect.QueryCommands(query);
            Display_Grid_Book();

        }

        private void bookUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE Books " +
               "SET Book_Title = '" + txtBookTitle.Text + "', " +
               "Book_ISBN = '" + txtBookISBN.Text + "', " +
               "Author_Id = '" + selectedAuthorId + "', " +
               "Book_Quantity = '" + txtBookQuantity.Text + "' " +
               "WHERE Book_Id = " + selectedID;

            DB_Connect.QueryCommands(query);
            Display_Grid_Book();

        }

        private void bookDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "delete from Books where  Book_Id = " + selectedID + "";
            DB_Connect.QueryCommands(query);
            Display_Grid_Book();
        }

        private void bookCancelButton_Click(object sender, RoutedEventArgs e)
        {
            bookDefView.Visibility = Visibility.Visible;
            bookModView.Visibility = Visibility.Hidden;

            selectedID = "";

            txtBookTitle.Clear();
            txtBookISBN.Clear();
            cboxBookAuthor.Text = "";
            txtBookQuantity.Clear();
            loading();

        }
        private void Display_CB_Authors()
        {
            List<Author> authorsList = new List<Author>();
            conn.Close();
            conn.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Authors", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Author author = new Author
                            {
                                AuthId = Convert.ToInt32(reader["Author_Id"]),
                                Author_FirstName = reader["Author_FirstName"].ToString(),
                                Author_LastName = reader["Author_LastName"].ToString()
                            };

                            authorsList.Add(author);
                        }
                    }
                }

            authorsList.ForEach(a => a.FullName = $"{a.Author_FirstName} {a.Author_LastName}");

            cboxBookAuthor.ItemsSource = authorsList;
            cboxBookAuthor.DisplayMemberPath = "FullName";
            cboxBookAuthor.SelectedValuePath = "Author_Id";
            cboxBookAuthor.SelectedValue = selectedAuthorId;
        }

        private void cboxBookAuthor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboxBookAuthor.SelectedItem != null)
            {
                selectedAuthorId = ((Author)cboxBookAuthor.SelectedItem).AuthId;
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


        //----------------This part is for Students--------------------
        public class Student
        {
            public int StudID { get; set; }
            public int Student_Id { get; set; }
            public string Student_FirstName { get; set; }
            public string Student_LastName { get; set; }
            public string Student_Course { get; set; }
            public int Student_Year { get; set; }
            public string Student_Email { get; set; }
            public string Student_Password { get; set; }
        }
        private void studentGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            studentGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
            
        }

        private void studentGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcStud)
            {
                studentGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            }
        }

        private void studentGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dashboardGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            authorGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            bookGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            studentGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
            historyGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            logoutGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            canProcDash = false;
            canProcAuth = false;
            canProcBook = false;
            canProcStud = true;
            canProcHist = false;
            canProcLog = false;
            homeView.Visibility = Visibility.Hidden;
            authorView.Visibility = Visibility.Hidden;
            bookView.Visibility = Visibility.Hidden;
            studentView.Visibility = Visibility.Visible;
            historyView.Visibility = Visibility.Hidden;
            Console.WriteLine("Ok naman gud");
            loading();
        }

        private void studModifyButton_Click(object sender, RoutedEventArgs e)
        {
            studDefView.Visibility = Visibility.Hidden;
            studModView.Visibility = Visibility.Visible;
        }

        private void studAddButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "insert into Students values ('" + txtStudentID.Text + "','" + txtStudentEmail.Text + "','" + txtStudentPassword.Text +
                "','" + txtStudentFName.Text + "','" + txtStudentLName.Text + "','" + txtStudentCourse.Text + "','" + txtStudentYear.Text + "')";
            DB_Connect.QueryCommands(query);
            Display_Grid_Student();

        }

        private void studUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE Students " +
                "SET Student_Number = '" + txtStudentID.Text + "', " +
                "Student_Email = '" + txtStudentEmail.Text + "', " +
                "Student_Password = '" + txtStudentPassword.Text + "', " +
                "Student_FirstName = '" + txtStudentFName.Text + "', " +
                "Student_LastName = '" + txtStudentLName.Text + "', " +
                "Course = '" + txtStudentCourse.Text + "', " +
                "Year= '" + txtStudentYear.Text + "' " +
                "WHERE Student_Id = " + selectedID;

            DB_Connect.QueryCommands(query);
            Display_Grid_Student();

        }

        private void studDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "delete from Students where  Student_Id = " + selectedID + "";
            DB_Connect.QueryCommands(query);
            Display_Grid_Student();

        }

        private void studCancelButton_Click(object sender, RoutedEventArgs e)
        {
            studDefView.Visibility = Visibility.Visible;
            studModView.Visibility = Visibility.Hidden;

            selectedID = "";

            txtStudentID.Clear();
            txtStudentFName.Clear();
            txtStudentLName.Clear();
            txtStudentCourse.Clear();
            txtStudentYear.Clear();
            txtStudentEmail.Clear();
            txtStudentPassword.Clear();
            loading();
        }

        private void txtStudSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudSearch.Text))
            {
                // If the search box is empty, display all students
                Display_Grid_Student();
            }
            else
            {
                List<Student> filteredList = new List<Student>();
                conn.Close();
                conn.Open();

                string query = "SELECT * FROM Students WHERE " +
                               "CONVERT(NVARCHAR(MAX), Student_Number) + ' ' + " +
                                "Student_FirstName + ' ' + " +
                                "Student_LastName + ' ' + " +
                                "Course + ' ' + " +
                                "CONVERT(NVARCHAR(MAX), Year) + ' ' + " +
                                "Student_Password + ' ' + " +
                                "Student_Email LIKE '%" + txtStudSearch.Text + "%'";

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            StudID = Convert.ToInt32(reader["Student_Id"]),
                            Student_Id = Convert.ToInt32(reader["Student_Number"]),
                            Student_FirstName = reader["Student_FirstName"].ToString(),
                            Student_LastName = reader["Student_LastName"].ToString(),
                            Student_Course = reader["Course"].ToString(),
                            Student_Year = Convert.ToInt32(reader["Year"]),
                            Student_Email = reader["Student_Email"].ToString(),
                            Student_Password = reader["Student_Password"].ToString(),
                        };

                        filteredList.Add(student);
                    }
                }
                studDataGrid.ItemsSource = filteredList;
            }

        }

        private void studDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid gd = (DataGrid)sender;

            if (gd.SelectedItem != null)
            {
                Student selectedStudent = (Student)gd.SelectedItem;

                selectedID = selectedStudent.StudID.ToString();
                txtStudentID.Text = selectedStudent.Student_Id.ToString();
                txtStudentFName.Text = selectedStudent.Student_FirstName;
                txtStudentLName.Text = selectedStudent.Student_LastName;
                txtStudentCourse.Text = selectedStudent.Student_Course;
                txtStudentYear.Text = selectedStudent.Student_Year.ToString();
                txtStudentEmail.Text = selectedStudent.Student_Email;
                txtStudentPassword.Text = selectedStudent.Student_Password;

            }
        }
        private void Display_Grid_Student() 
        {
            List<Student> dataList = new List<Student>();
            conn.Close();
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Students", conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Student student = new Student
                    {
                        StudID = Convert.ToInt32(reader["Student_Id"]),
                        Student_Id = Convert.ToInt32(reader["Student_Number"]),
                        Student_FirstName = reader["Student_FirstName"].ToString(),
                        Student_LastName = reader["Student_LastName"].ToString(),
                        Student_Course = reader["Course"].ToString(),
                        Student_Year = Convert.ToInt32(reader["Year"]),
                        Student_Email = reader["Student_Email"].ToString(),
                        Student_Password = reader["Student_Password"].ToString(),
                    };

                    dataList.Add(student);
                }
            }
            studDataGrid.ItemsSource = dataList;

        }

        //-----------------This part is for History
        public class RBook
        {
            public int Return_Borrow_ID { get; set; }
            public int Return_Borrow_Book_ID { get; set; }
            public int Return_Student_ID { get; set; }
            public string Return_Name { get; set; }
            public string Return_Title { get; set; }
            public string Return_ISBN { get; set; }
            public string Return_Author { get; set; }
            public string Return_Borrow_Date { get; set; }
            public string Return_Date { get; set; }
        }
        private void historyGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dashboardGridButton.Background = new SolidColorBrush(Color.FromArgb(0,81, 81, 245));
            authorGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            bookGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            studentGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            historyGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
            logoutGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            canProcDash = false;
            canProcAuth = false;
            canProcBook = false;
            canProcStud = false;
            canProcHist = true;
            canProcLog = false;
            homeView.Visibility = Visibility.Hidden;
            authorView.Visibility = Visibility.Hidden;
            bookView.Visibility = Visibility.Hidden;
            studentView.Visibility = Visibility.Hidden;
            historyView.Visibility = Visibility.Visible;
            Console.WriteLine("Ok naman gud");
            loading();
        }

        private void historyGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            historyGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
        }

        private void historyGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcHist)
            {
                historyGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            }
        }
        private void Display_Grid_RBook()
        {
            List<RBook> dataList = new List<RBook>();
            conn.Close();
            conn.Open();

            SqlCommand command = new SqlCommand(@"
        SELECT 
            rb.Returned_Books_Id AS Return_Borrow_ID, 
            rb.Borrowed_Book_Id AS Return_Borrow_Book_ID, 
            rb.Student_Id AS Return_Student_ID,
            CONCAT(s.Student_FirstName, ' ', s.Student_LastName) AS Return_Name,
            b.Book_Title AS Return_Title,
            b.Book_ISBN AS Return_ISBN,
            CONCAT(a.Author_FirstName, ' ', a.Author_LastName) AS Return_Author,
            rb.Borrowed_date AS Return_Borrow_Date, 
            rb.Returned_date AS Return_Date
        FROM ReturnedBooks rb 
        JOIN Books b ON rb.Book_Id = b.Book_Id 
        JOIN Authors a ON b.Author_Id = a.Author_Id 
        JOIN Students s ON rb.Student_Id = s.Student_Id", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    RBook rbook = new RBook
                    {
                        Return_Borrow_ID = Convert.ToInt32(reader["Return_Borrow_ID"]),
                        Return_Borrow_Book_ID = Convert.ToInt32(reader["Return_Borrow_Book_ID"]),
                        Return_Student_ID = Convert.ToInt32(reader["Return_Student_ID"]),
                        Return_Name = reader["Return_Name"].ToString(),
                        Return_Title = reader["Return_Title"].ToString(),
                        Return_ISBN = reader["Return_ISBN"].ToString(),
                        Return_Author = reader["Return_Author"].ToString(),
                        Return_Borrow_Date = reader["Return_Borrow_Date"].ToString(),
                        Return_Date = reader["Return_Date"].ToString()
                    };

                    dataList.Add(rbook);
                }
            }

            returnedDataGrid.ItemsSource = dataList;
        }
        //--------------This part is for logout--------------------

        private void logoutGridButton_MouseEnter(object sender, MouseEventArgs e)
        {
            logoutGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
        }

        private void logoutGridButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!canProcLog)
            {
                logoutGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            }
        }

        private void logoutGridButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dashboardGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            authorGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            bookGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            studentGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            historyGridButton.Background = new SolidColorBrush(Color.FromArgb(0, 81, 81, 245));
            logoutGridButton.Background = new SolidColorBrush(Color.FromRgb(81, 81, 245));
            canProcDash = false;
            canProcAuth = false;
            canProcBook = false;
            canProcStud = false;
            canProcHist = false;
            canProcLog = true;

            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}
