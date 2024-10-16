namespace M01_Entite
{
    public class Municipalite
    {
        public int CodeGeographique { get; set; }
        public string Nom { get; set; }
        public string AdresseCourriel { get; set; }
        public string AdresseWeb { get; set; }
        public string DateConstruction { get; set; }
        public double Superficie { get; set; }
        public int Population { get; set; }
        public bool Actif { get; set; } = true;
    }
}
