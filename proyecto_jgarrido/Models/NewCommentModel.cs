using System;
namespace proyecto_jgarrido.Models
{
	public class NewCommentModel
	{
		public NewCommentModel()
		{
		}
        public int Usuario_Id { get; set; }
        public int Actividad_Id { get; set; }
        public string Comentario { get; set; }
    }
}

