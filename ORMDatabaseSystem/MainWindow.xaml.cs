using Microsoft.EntityFrameworkCore;
using ORMDatabaseSystem.Db;
using ORMDatabaseSystem.Models;
using System.Windows;

namespace ORMDatabaseSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProduktDb _db = new();
        public MainWindow()
        {
            InitializeComponent();
            _db.Database.EnsureCreated(); // vytvoří se nám fyzicky databáze (soubor Produkty.db)
            _db.Produkty.Load(); // uložíme si data do lokální paměti

            Seznam.ItemsSource = _db.Produkty.Local.ToObservableCollection();            
        }

        private void btnPridat_Click(object sender, RoutedEventArgs e)
        {
            var nazev = txtNazev.Text;
            var cena = double.Parse(txtCena.Text);

            _db.Produkty.Add(new Produkt { Nazev = nazev, Cena = cena });
            _db.SaveChanges();

            txtCena.Text = txtNazev.Text = string.Empty;
        }

        private void btnUlozit_Click(object sender, RoutedEventArgs e)
        {
            if (Seznam.SelectedItem is Produkt vybranyProdukt)
            {
                vybranyProdukt.Nazev = txtEditNazev.Text;
                vybranyProdukt.Cena = double.Parse(txtEditCena.Text);

                _db.SaveChanges();
                Seznam.Items.Refresh(); // doplněno ručně

                // Refreshni ListBox a vyprázdní textboxy
                Seznam.SelectedItem = null;
                txtEditNazev.Text = string.Empty;
                txtEditCena.Text = string.Empty;
            }
        }

        private void Seznam_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (Seznam.SelectedItem is Produkt vybranyProdukt)
            {
                txtEditNazev.Text = vybranyProdukt.Nazev;
                txtEditCena.Text = vybranyProdukt.Cena.ToString();
            }
        }

        private void MenuItem_Smazat_Click(object sender, RoutedEventArgs e)
        {
            if (Seznam.SelectedItem is Produkt vybranyProdukt)
            {
                _db.Produkty.Remove(vybranyProdukt);
                _db.SaveChanges();

                // Refreshni ListBox
                Seznam.Items.Refresh();

                // Vyprázdní editační pole
                txtEditNazev.Text = string.Empty;
                txtEditCena.Text = string.Empty;
            }
        }
    }
}