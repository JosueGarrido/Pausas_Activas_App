using System;
namespace proyecto_jgarrido.Models
{
	public class ComentarioModel
	{
		public ComentarioModel()
		{
		}
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ActividadId { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaComentario { get; set; }
    }
}

