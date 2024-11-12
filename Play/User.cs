using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Play
{
    public class User
    {
        
        public string Login { get; set; }


        public static User Logon(string username, string password)
        {
            foreach (var line in File.ReadLines(Program.Filename))
            {
                var parts = line.Split(';');
                if (parts.Length == 2 && parts[0] == username && parts[1] == password)
                {
                    return new User() { Login = username }; // Успешный вход
                }
            }
            return null; // Неверный логин или пароль
        }

        public static User Register(string username, string password)
        {
            // Проверяем, существует ли пользователь
            if (UserExists(username))
            {
                return null; // Пользователь уже существует
            }

            // Добавляем нового пользователя
            File.AppendAllText(Program.Filename, $"{username};{password}"+"\n");
            return new User(){ Login = username}; 
        }

        private static bool UserExists(string username)
        {
            foreach (var line in File.ReadLines(Program.Filename))
            {
                var parts = line.Split(';');
                if (parts.Length > 0 && parts[0] == username)
                {
                    return true; // Пользователь найден
                }
            }
            return false; // Пользователь не найден
        }
    }
}
