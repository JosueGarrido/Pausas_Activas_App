using System;
namespace proyecto_jgarrido.Models
{
	public class NewActivityModel
	{
		public NewActivityModel()
		{
		}
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public int Categoria_Id { get; set; }
    }
}

