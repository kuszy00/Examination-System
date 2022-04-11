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
using System.Data;
using System.Configuration;

namespace SystemEgzaminacyjnyNauczyciel
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        //Utworzenie połączenienia z bazą danych
        static string conString = ConfigurationManager.AppSettings["ConString"];
        SqlConnection con = new SqlConnection(conString);
        SqlCommand cm = new SqlCommand();

        public Results(string examCode, string groupCode)
        {
            InitializeComponent();
            ShowResults(examCode, groupCode);
        }
        //Wyświetlenie wyników danej grupy dla danego egzaminu
        private void ShowResults(string examCode, string groupCode)
        {
            try
            {
                cm = new SqlCommand("Select ID AS 'Numer indexu', ExamID as 'Kod egzaminu', Name as 'Imie', Surname as 'Nazwisko', Student_Group as 'Grupa', FORMAT(Date, 'dd/MM/yyyy') AS Data, Result as 'Wynik' from Results WHERE ExamID like '" + examCode + "' AND Student_Group = '" + groupCode + "'", con);
                con.Open();
                cm.ExecuteNonQuery();
                SqlDataAdapter dataAdp = new SqlDataAdapter(cm);
                DataTable dt = new DataTable("Results");//Wczytanie danych do DataTable WPF
                dataAdp.Fill(dt);
                ResultsGrid.ItemsSource = dt.DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
