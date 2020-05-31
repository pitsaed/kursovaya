using System;

namespace _4sem_kurs
{
    using Enums;
    [Serializable]
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public User() { }
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public UserRole UserRole { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                User user = obj as User;
                return Login == user.Login && Password == user.Password;
            }
            return false;
        }

    }
}
