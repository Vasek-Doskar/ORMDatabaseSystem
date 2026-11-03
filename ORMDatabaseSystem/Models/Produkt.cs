namespace ORMDatabaseSystem.Models
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Nazev {  get; set; }
        public double Cena { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Název: {Nazev} Cena: {Cena:c}";
        }
    }
}
