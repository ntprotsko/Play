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
        private readonly string _filename;

        public User(string filename = "users.txt")
        {
            _filename = filename;
            // Если файл не существует, создаем пустой файл
            if (!File.Exists(_filename))
            {
                File.WriteAllText(_filename, string.Empty);
            }
        }
        public bool Login(string username, string password)
        {
            foreach (var line in File.ReadLines(_filename))
            {
                var parts = line.Split(';');
                if (parts.Length == 2 && parts[0] == username && parts[1] == password)
                {
                    return true; // Успешный вход
                }
            }
            return false; // Неверный логин или пароль
        }

        public bool Register(string username, string password)
        {
            // Проверяем, существует ли пользователь
            if (UserExists(username))
            {
                return false; // Пользователь уже существует
            }

            // Добавляем нового пользователя
            File.AppendAllText(_filename, $"{username};{password}"+"\n");
            return true;
        }

        private bool UserExists(string username)
        {
            foreach (var line in File.ReadLines(_filename))
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
