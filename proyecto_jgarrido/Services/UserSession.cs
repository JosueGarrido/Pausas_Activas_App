using System;
using proyecto_jgarrido.Models;

namespace proyecto_jgarrido.Services
{
	public class UserSession
	{
        private static UserSession _instance;
        public Usuario AuthenticatedUser { get; private set; }
        public UserSession()
		{
		}
        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSession();
                }
                return _instance;
            }
        }
        public void SetAuthenticatedUser(Usuario user)
        {
            AuthenticatedUser = user;
        }

        public bool IsAuthenticated()
        {
            return AuthenticatedUser != null;
        }
    }
}

