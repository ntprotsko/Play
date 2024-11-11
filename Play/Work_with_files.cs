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
        public List<int> ReadNumbersFromFile(string filename)
        {
            List<int> numbers = new List<int>();

            try
            {
                using (StreamReader reader = new StreamReader(filename))
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
        public void WriteNumbersToFile(string filename, List<int> numbers)
        {
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
