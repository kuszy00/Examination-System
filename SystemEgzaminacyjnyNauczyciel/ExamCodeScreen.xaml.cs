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

namespace SystemEgzaminacyjnyNauczyciel
{
    /// <summary>
    /// Interaction logic for ExamCodeScreen.xaml
    /// </summary>
    public partial class ExamCodeScreen : Window
    {
        public ExamCodeScreen(string code)
        {
            InitializeComponent();
            CodeLabel.Content = code;//Wyświetlenie kodu egzaminu
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
