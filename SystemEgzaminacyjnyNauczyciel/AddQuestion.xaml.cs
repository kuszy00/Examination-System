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
using System.Data.SqlClient;
using SystemEgzaminacyjnyNauczyciel;
using System.Configuration;

namespace SystemEgzaminacyjny

{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion : Window
    {
        //Utworzenie połączenienia z bazą danych
        static string conString = ConfigurationManager.AppSettings["ConString"];
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cm = new SqlCommand();
        private string examID;
        int questionNumber = 1;

        public AddQuestion(string examCode)
        {
            InitializeComponent();
            examID = examCode;
            QuestionNrLabel.Content = "Pytanie " + questionNumber;
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionContentTextBox.Text != "")
            {
                Add();
            }

            ExamCodeScreen code = new ExamCodeScreen(examID);
            code.Show();
            this.Close();
        }
        //Przejście go kolejnego pytania
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {        
            Add();
            questionNumber++;
            QuestionNrLabel.Content = "Pytanie " + questionNumber;
        }
        //Dodanie nowego pytania do bazy
        private void Add()
        {
            try
            {
                cm = new SqlCommand("INSERT INTO Questions(ExamID, ExamQuestion, Question, CorrectAnswer, Answer1, Answer2, Answer3)VALUES(@examID, @questionNumber, @questionContent, @correctAnswer, @answer1, @answer2, @answer3)", con);
                cm.Parameters.AddWithValue("@examID", examID);
                cm.Parameters.AddWithValue("@questionNumber", questionNumber);
                cm.Parameters.AddWithValue("@questionContent", QuestionContentTextBox.Text);
                cm.Parameters.AddWithValue("@correctAnswer", CorrectAnswerTextBox.Text);
                cm.Parameters.AddWithValue("@answer1", Answer1TextBox.Text);
                cm.Parameters.AddWithValue("@answer2", Answer2TextBox.Text);
                cm.Parameters.AddWithValue("@answer3", Answer3TextBox.Text);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                Clean();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Wyczyszczenie textboxów
        private void Clean()
        {
            QuestionContentTextBox.Clear();
            CorrectAnswerTextBox.Clear();
            Answer1TextBox.Clear();
            Answer2TextBox.Clear();
            Answer3TextBox.Clear();
        }
    }
}
