using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Play
{
    public class Work_with_files
    {
        //Метод для чтения предыдущих результатов
        public List<int> ReadNumbersFromFile(string _filename)
        {
            List<int> numbers = new List<int>();

            try
            {
                using (StreamReader reader = new StreamReader(_filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (int.TryParse(line, out int number))
                        {
                            numbers.Add(number);
                        }
                        else
                        {
                            Console.WriteLine($"Не удалось преобразовать строку в число: {line}");
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }

            return numbers;
        }
        //Метод для записи списка в файл
        public void WriteNumbersToFile(string _filename, List<int> _numbers)
        {
            using (StreamWriter writer = new StreamWriter(_filename, false))
            {
                foreach (int c in _numbers)
                {
                    writer.WriteLine(c.ToString());
                }
            }
            Console.ReadKey();
        }
    }
}
