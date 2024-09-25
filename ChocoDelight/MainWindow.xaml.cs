using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChocoDelight
{
    public partial class MainWindow : Window
    {
        List<Choco> lista = new List<Choco>();
        public MainWindow()
        {
            InitializeComponent();

            var sr = new StreamReader("../../../source/choco.txt", System.Text.Encoding.UTF8);
            string headerLine = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                lista.Add(new Choco(sr.ReadLine()));
            }

            labelKulonbozoTermek.Content = $"{lista.GroupBy(x => x.TermekTipusa).Count()} különböző termék típusunk van";

            labelLegolcsobb.Content = lista.MinBy(x => x.Ar).Nev;

            int atlagAr = (int)lista.Average(x => x.Ar);
            Choco atlagTermek = lista.Where(x => x.Ar >= atlagAr).OrderBy(x => x.Ar).First();
            labelAjanlat.Content = $"{atlagTermek.Nev}, {atlagTermek.Ar}";

            using (StreamWriter sw = new StreamWriter("../../../source/lista.txt", false))
            {
                var elerhetoTipusok = lista.GroupBy(x => x.TermekTipusa).ToList();

                foreach (var item in elerhetoTipusok)
                {
                    var kulonbozoTipusok = lista.Where(x => x.TermekTipusa == item.Key).First();
                    sw.WriteLine($"{item.Key} {kulonbozoTipusok.Nev}");
                }
            }

            using (StreamWriter sw = new StreamWriter("../../../source/stat.txt", false))
            {
                foreach (var item in lista.GroupBy(x => x.KakaoTartalom).Select(x => new { kakaoTartalom = x.First().KakaoTartalom, mennyiseg = x.Count() }))
                {
                    sw.WriteLine($"{item.kakaoTartalom} ; {item.mennyiseg}");
                }
            }
        }

        private void buttonArajanlat_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("../../../source/ajanlat.txt", false))
            {
                var ajanlat = lista.Where(d => d.CsokiTipus.ToLower() == tbxSearch.Text.ToLower());
                if (ajanlat.Count() == 0)
                {
                    MessageBox.Show("Nincs a feltételeknek megfelelő \r\ntermékünk. Kérjük, módosítsa a választását!");
                }
                else
                {
                    foreach (var csoki in ajanlat)
                    {
                        sw.WriteLine($"{csoki.Nev} - {csoki.Ar} Ft/{csoki.Tomeg}");
                    }
                    MessageBox.Show($"{ajanlat.Count()} db csoki, átlagos kakaó tartalom: {ajanlat.Average(d => (int)d.KakaoTartalom)}");
                }
            }
        }

        private void buttonUjtermek_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("../../../source/choco.txt", true))
            {
                if (tbxAr.Text != "" || tbxCsokiNev.Text != "" || tbxKakaoTartalom.Text != "" || tbxNettoTomeg.Text != "" || tbxNev.Text != "" || tbxSearch.Text != "" || tbxTipus.Text != "")
                {
                    sw.WriteLine($"{tbxNev.Text};{tbxCsokiNev.Text};{tbxAr.Text};{tbxTipus.Text};{tbxKakaoTartalom.Text};{tbxNettoTomeg.Text}");
                    MessageBox.Show("Sikeres termék felvétel!");
                }
                else
                {
                    MessageBox.Show("Hiba! Valamelyik mező üresen maradt!");
                }
                
            }
        }
    }
}