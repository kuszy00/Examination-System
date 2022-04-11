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
    public partial class ResultScreen : Window
    {
        static string conString = ConfigurationManager.AppSettings["ConString"];
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cm = new SqlCommand();

        public ResultScreen(int Points, string ExamID, string ID)
        {
            InitializeComponent();
            PointsLabel.Content = Points;
            Add(Points, ExamID, ID);
        }

        private void Add(int result, string examCode, string studentID)
        {
            try
            {
                cm = new SqlCommand("UPDATE Results SET Result = @result WHERE (ExamID='" + examCode + "' AND ID='" + studentID + "')", con);
                cm.Parameters.AddWithValue("@result", result);
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

