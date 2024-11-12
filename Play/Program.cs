using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Play
{
    class Program
    {
        static void Main(string[] args)
        {
            //Цикл для входа и регистрации, если это необходимо
            bool loggedIn = false;
            do
            {
                User user = new User();
                Console.WriteLine("Вы уже зарегистрированиы? (Введите Yes/No)");
                string otvet = Console.ReadLine();
                if (otvet == "Yes")
                {
                    Console.WriteLine("Вход в систему:");
                    Console.Write("Введите логин: ");
                    string username = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    string password = Console.ReadLine();

                    if (user.Login(username, password))
                    {
                        Console.WriteLine("Вход выполнен успешно.");
                        loggedIn = true; // Выход из цикла
                    }
                    else
                    {
                        Console.WriteLine("Неверный логин или пароль. Попробуйте снова.");
                    }
                }
                else if (otvet == "No")
                {
                    Console.WriteLine("Регистрация нового пользователя:");
                    Console.Write("Введите логин: ");
                    string regUsername = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    string regPassword = Console.ReadLine();

                    if (user.Register(regUsername, regPassword))
                    {
                        Console.WriteLine("Пользователь зарегистрирован.");
                    }
                    else
                    {
                        Console.WriteLine("Пользователь уже существует.");
                    }
                }
                else
                {
                    Console.WriteLine("Введен некорректный ответ");
                }
            } while (loggedIn == false);
            StartGame();
        } 
        //Механика игры
        static void StartGame()
        {
            Work_with_files work = new Work_with_files();
            string filename = "result.txt";
            List<int> numbers = work.ReadNumbersFromFile(filename);

            Random random = new Random();
            string generatedNumber = GenerateUniqueNumber(random);
            int attempts = 0;
            bool isGuessed = false;
            Console.WriteLine("Компьютер сгенерировал четырехзначное число");
            #if DEBUG
            Console.WriteLine(generatedNumber);
            #else
            Console.WriteLine();
            #endif
            while (!isGuessed)
            {
                Console.Write("Введите ваш вариант: ");
                string userGuess = Console.ReadLine();

                if (userGuess.Length != 4 || !IsAllDigits(userGuess))
                {
                    Console.WriteLine("Пожалуйста, введите ровно 4 цифры.");
                    continue;
                }

                attempts++;
                (int correctDigits, int correctPositions) = CheckGuess(generatedNumber, userGuess);

                Console.WriteLine($"Угадано цифр: {correctDigits}, На правильных позициях: {correctPositions}");

                if (correctPositions == 4)
                {
                    isGuessed = true;
                    Console.WriteLine($"Вы угадали число {generatedNumber} за {attempts} попыток.");
                    numbers = Result(numbers, attempts);

                    using (StreamWriter writer = new StreamWriter(filename, false))
                    {
                        foreach (int c in numbers)
                        {
                            writer.WriteLine(c.ToString());
                        }
                    }
                    Console.ReadKey();
                }
            }

        }
        static string GenerateUniqueNumber(Random random)
        {
            int number;
            do
            {
                number = random.Next(1000, 10000);
            } while (HasDuplicateDigits(number.ToString()));
            return number.ToString();
        }

        static bool HasDuplicateDigits(string number)
        {
            return number[0] == number[1] || number[0] == number[2] || number[0] == number[3] ||
                   number[1] == number[2] || number[1] == number[3] ||
                   number[2] == number[3];
        }

        static (int correctDigits, int correctPositions) CheckGuess(string generatedNumber, string userGuess)
        {
            int correctPositions = 0;
            int correctDigits = 0;

            for (int i = 0; i < 4; i++)
            {
                if (userGuess[i] == generatedNumber[i])
                {
                    correctPositions++;
                }
                if (generatedNumber.Contains(userGuess[i]))
                {
                    correctDigits++;
                }
            }
            return (correctDigits, correctPositions);
        }
        static bool IsAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        static List<int> Result(List<int> data, int result)
        {
            data.Add(result);
            data.Sort();
            int res = data.IndexOf(result) + 1;
            Console.WriteLine("Вы заняли {0} место из {1}", res, data.Count);
            return data;
        }
    }
}