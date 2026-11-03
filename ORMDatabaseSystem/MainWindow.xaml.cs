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


            var produktyDrazsiNez100 = _db.Produkty.Where(p => p.Cena > 100).ToList();
        }

        private void btnPridat_Click(object sender, RoutedEventArgs e)
        {
            var nazev = txtNazev.Text;
            var cena = double.Parse(txtCena.Text);

            _db.Produkty.Add(new Produkt { Nazev = nazev, Cena = cena });
            _db.SaveChanges();

            txtCena.Text = txtNazev.Text = string.Empty;
        }
    }
}