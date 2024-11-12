# Описание игры
Игра представляет собой консольное приложение на языке C#, в котором пользователю предлагается угадать случайное 4-значное число, сгенерированное программой. Игроку необходимо ввести четырехзначное число, содержащее цифры от 1 до 9. После каждой попытки программа сообщает, сколько цифр совпали по значению и позиции (равно) и сколько цифр совпали, но находятся на другой позиции (совпало). Также в игре предусмотрена система авторизации игроков и ведется учет всех попыток.

## Вход или регистрация игрока
В первую очередь программа узнает у игрока зарегистрирован ли он. Если ответ положительный, то пользователю дается возможность ввести свой логин и пароль, в ином случае пользователь может зарегистрироваться.

```c#
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
            Console.WriteLine("Пользователь зарегистрирован");
        }
        else
        {
            Console.WriteLine("Пользователь уже существует");
        }
    }
    else { Console.WriteLine("Введен некорректный ответ");}
} while (loggedIn == false);
```

Также программа проверяет существует ли уже такой пользователь и если да, то оповещает об этом.
```c#
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
``` 
## Генерация числа
Генерация числа осуществляется благодаря встроенному классу Random. Данный класс генерирует число, а позже отдельный метод проверяет число на повторяющиеся цифры.

```c#
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
```
## Проверка ввода пользователем чисел
Ввод числа проверяется с помощью цикла и метода IsAllDigits, чтобы убедиться, что это четырехзначное число.
```c#
string userGuess = Console.ReadLine();
if (userGuess.Length != 4 || !IsAllDigits(userGuess))
{
    Console.WriteLine("Пожалуйста, введите ровно 4 цифры.");
    continue;
}
```
```c#
static bool IsAllDigits(string str)
{
    foreach (char c in str)
    {
        if (!char.IsDigit(c))
            return false;
    }
    return true;
}
```