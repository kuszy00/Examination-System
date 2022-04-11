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
using SystemEgzaminacyjnyNauczyciel;

namespace SystemEgzaminacyjny
{
    /// <summary>
    /// Interaction logic for TeacherMenu.xaml
    /// </summary>
    public partial class TeacherMenu : Window
    {
        public TeacherMenu()
        {
            InitializeComponent();
        }

        private static Random random = new Random();
        private static string examCode;
        //Generowanie kodu egzaminu
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //Przejście do okienka dodawania pytań
        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            examCode = RandomString(5);
            AddQuestion add = new AddQuestion(examCode);
            add.Show();
            this.Close();
        }
        //Przejście do wyboru wyświetlenia wyników
        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            CheckResults results = new CheckResults();
            results.Show();
            this.Close();
        }
    }
}
