using System;
namespace proyecto_jgarrido.Models
{
	public class Actividad
	{
		public Actividad()
		{
		}
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public int Categoria_Id { get; set; }
        public DateTime Fecha_Creacion { get; set; }
    }
}

