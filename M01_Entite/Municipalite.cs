namespace M01_Entite
{
    public class Municipalite
    {
        public int MunicipaliteId { get; set; }
        public int CodeGeographique { get; set; }
        public string Nom { get; set; }
        public string? AdresseCourriel { get; set; }
        public string? AdresseWeb { get; set; }
        public DateTime? DateConstruction { get; set; }
        public decimal? Superficie { get; set; }
        public int? Population { get; set; }
        public bool Actif { get; set; } = true;

        public string ProprietesNonUtilisees { get; set; }
    }
}
