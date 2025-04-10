namespace DeudasAPI.Models
{
    public class Cliente
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public int Cedula { get; set; }
        public decimal Deuda { get; set; }

        public string Producto { get; set; } = "";

    }
}
