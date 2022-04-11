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
using System.Data.SqlClient;
using System.Configuration;

namespace SystemEgzaminacyjny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        //Utworzenie połączenienia z bazą danych
        static string conString = ConfigurationManager.AppSettings["ConString"];
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cm = new SqlCommand();
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Add();
                QuestionScreen question = new QuestionScreen(CodeTextBox.Text, IndexTextBox.Text);
                question.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Dodanie danych zdającego do bazy danych
        private void Add()
        {
            try
            {
                cm = new SqlCommand("INSERT INTO Results(ID, ExamID, Name, Surname, Student_Group, Date)VALUES(@ID, @ExamID, @Name, @Surname, @Student_Group, @Date)", con);
                cm.Parameters.AddWithValue("@ID", IndexTextBox.Text);
                cm.Parameters.AddWithValue("@ExamID", CodeTextBox.Text);
                cm.Parameters.AddWithValue("@Name", NameTextBox.Text);
                cm.Parameters.AddWithValue("@Surname", SurnameTextBox.Text);
                cm.Parameters.AddWithValue("@Student_Group", GroupTextBox.Text);
                cm.Parameters.AddWithValue("@Date", DateTime.Now.ToString("M/d/yyyy"));
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
