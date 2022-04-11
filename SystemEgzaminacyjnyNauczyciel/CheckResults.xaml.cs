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
    /// Interaction logic for CheckResults.xaml
    /// </summary>
    public partial class CheckResults : Window
    {
        public CheckResults()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Results results = new Results(CodeTextBox.Text, GroupTextBox.Text);
            results.Show();
            this.Close();
        }
    }
}
