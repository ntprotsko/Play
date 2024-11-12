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
        //public string Login;
        public string Login { get; set; }
        public string Password { get; set; }
        public List<int> History { get; set; }
        public User(string username, string password, List<int> history)
        {
            Login = username;
            Password = password;
            History = history;
        }

        public static User Logon(string username, string password)
        {
            foreach (var line in File.ReadLines(Program.Filename))
            {
                var parts = line.Split(';');
                if (parts.Length == 3 && parts[0] == username && parts[1] == password)
                {
                    List<int> history = SplitStringToIntList(parts[2]);
                    return new User(username, password, history);// Успешный вход
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
            List<int> history = new List<int>();
            history.Add(-1);
            File.AppendAllText(Program.Filename, $"{username};{password};-1" + "\n");
            return new User(username, password, history);
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
        static List<int> SplitStringToIntList(string input)
        {
            return input.Split(',')
                        .Select(int.Parse)
                        .ToList();
        }
        static public void UpdateHistory(string username, string password, List<int> history)
        {
            // Читаем все строки из файла
            var lines = File.ReadAllLines(Program.Filename).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                var fields = lines[i].Split(';');

                // Проверяем, что у нас достаточно полей
                if (fields[0] == username && fields[1] == password)
                {
                    // Обновляем историю
                    fields[2] = string.Join(",", history);
                    lines[i] = string.Join(";", fields); // Объединяем обратно в строку
                    break; // Выходим из цикла после обновления
                }
            }
        }
    }
}
