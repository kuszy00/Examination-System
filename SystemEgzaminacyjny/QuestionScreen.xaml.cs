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
using System.Configuration;
namespace SystemEgzaminacyjny
{
    /// <summary>
    /// Interaction logic for QuestionScreen.xaml
    /// </summary>
    public partial class QuestionScreen : Window
    {
        //Utworzenie połączenienia z bazą danych
        static string conString = ConfigurationManager.AppSettings["ConString"];
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cm = new SqlCommand();
        SqlCommand noq = new SqlCommand();
        SqlDataReader dr;
        private int QuestionNumber = 1;
        private int MaxNumber = 0;
        private int Points = 0;
        private string examCode;
        private string studentID;
        private string RightAnswer;
        DateTime endTime;
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        
        public QuestionScreen(string code, string ID)
        {
            InitializeComponent();
            try
            {
                examCode = code;
                studentID = ID;
                //Wczytanie liczby pytań
                noq = new SqlCommand("Select count(*) from Questions WHERE ExamID like '" + examCode + "'", con);
                con.Open();
                dr = noq.ExecuteReader();
                dr.Read();
                MaxNumber = (int)dr[0];
                QuestionNrLabel.Content = $"Pytanie {QuestionNumber}/{MaxNumber}";
                if (QuestionNumber == MaxNumber)
                {
                    NextButton.IsEnabled = false;
                }
                dr.Close();
                con.Close();
                //Ustawienia timera
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                endTime = DateTime.Now.AddSeconds(MaxNumber * 30);
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                ShowQuestion(QuestionNumber);
                dispatcherTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoginScreen login = new LoginScreen();
                login.Show();
                this.Close();
            }
        }
        //Zliczenie odpowiedzi po osatnim pytaniu i przejście do wyniku
        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            PointsCounter();
            ResultScreen result = new ResultScreen(Points, examCode, studentID);
            result.Show();
            this.Close();
        }
        //Wyświetlenie pytania
        private void ShowQuestion(int questionNumber)
        {
            cm = new SqlCommand("Select * from Questions WHERE ExamID like '" + examCode + "' AND ExamQuestion = " + questionNumber, con);
            con.Open();
            dr = cm.ExecuteReader();
            dr.Read();
            var Random = new Random();
            var list = new List<string> { dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString() };
            RightAnswer = dr[3].ToString();
            int answer = Random.Next(list.Count);
            QuestionContentLabel.Content = dr[2].ToString();
            RadioButton1.Content = list[answer];
            list.RemoveAt(answer);
            answer = Random.Next(list.Count);
            RadioButton2.Content = list[answer];
            list.RemoveAt(answer);
            answer = Random.Next(list.Count);
            RadioButton3.Content = list[answer];
            list.RemoveAt(answer);
            answer = Random.Next(list.Count);
            RadioButton4.Content = list[answer];
            list.RemoveAt(answer);
            dr.Close();
            con.Close();
        }
        //Liczenie punktów
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            PointsCounter();

            if (QuestionNumber + 1 <= MaxNumber)
            {
                QuestionNumber++;
                RadioButton1.IsChecked = false;
                RadioButton2.IsChecked = false;
                RadioButton3.IsChecked = false;
                RadioButton4.IsChecked = false;
                QuestionNrLabel.Content = $"Pytanie {QuestionNumber}/{MaxNumber}";
                ShowQuestion(QuestionNumber);
            }

            if (QuestionNumber == MaxNumber)
            {
                NextButton.IsEnabled = false;
            }
        }
        //Losowanie odpowiedzi
        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            var Random = new Random();
            var list = new List<int> { 1, 2, 3, 4 };
            int answer = Random.Next(list.Count + 1);

            switch (answer)
            {
                case 1:
                    RadioButton1.IsChecked = true;
                    break;
                case 2:
                    RadioButton2.IsChecked = true;
                    break;
                case 3:
                    RadioButton3.IsChecked = true;
                    break;
                case 4:
                    RadioButton4.IsChecked = true;
                    break;
            }
        }
        //Timer
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan leftTime = endTime.Subtract(DateTime.Now);
            if (leftTime.TotalSeconds < 0)
            {
                PointsCounter();
                dispatcherTimer.Stop();
                ResultScreen result = new ResultScreen(Points, examCode, studentID);
                result.Show();
                this.Close();
            }
            else
            {
                TimeLabel.Content = leftTime.Minutes.ToString("00") + ":" + leftTime.Seconds.ToString("00");
            }
        }
        //Liczenie punktów
        private void PointsCounter()
        {
            if (RadioButton1.IsChecked == true)
            {
                if (RadioButton1.Content == RightAnswer) Points++;
            }
            else if (RadioButton2.IsChecked == true)
            {
                if (RadioButton2.Content == RightAnswer) Points++;
            }
            else if (RadioButton3.IsChecked == true)
            {
                if (RadioButton3.Content == RightAnswer) Points++;
            }
            else if (RadioButton4.IsChecked == true)
            {
                if (RadioButton4.Content == RightAnswer) Points++;
            }
        }
    }
}
