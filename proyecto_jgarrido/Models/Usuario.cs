﻿using System;
namespace proyecto_jgarrido.Models
{
	public class Usuario
	{
		public Usuario()
		{
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}

