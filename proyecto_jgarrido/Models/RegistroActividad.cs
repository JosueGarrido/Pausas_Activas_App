using System;
namespace proyecto_jgarrido.Models
{
	public class RegistroActividad
	{
		public RegistroActividad()
		{
		}
        public int Id { get; set; }
        public int Usuario_Id { get; set; }
        public int Actividad_Id { get; set; }
        public DateTime FechaRealizacion { get; set; }
    }
}

