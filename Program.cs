using System;
using System.IO;

namespace FileTask
{
    class Program
    {
        static void Main()
        {
            string projectPath = Directory.GetCurrentDirectory();
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №10, Гаврилов Артём, Ис24\n");
            Console.WriteLine($"Рабочая папка: {projectPath}\n");

            while (true)
            {
                Console.WriteLine("1. ВАРИАНТ 1 - Операции с текстом");
                Console.WriteLine("2. ВАРИАНТ 2 - Дополнительные операции");
                Console.WriteLine("0. ВЫХОД");
                Console.Write("\nВыберите вариант: ");

                string choice = Console.ReadLine();

                if (choice == "0") break;

                if (choice == "1")
                    RunVariant1(projectPath);
                else if (choice == "2")
                    RunVariant2(projectPath);
                else
                    Console.WriteLine("Неверный выбор!");

                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("\nПрограмма завершена");
        }

        static void RunVariant1(string path)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ВАРИАНТ 1");
                Console.WriteLine("1. Удалить чётные числа из файла");
                Console.WriteLine("2. Реверс слов в файле");
                Console.WriteLine("3. Удалить слова длиной 3-5 символов");
                Console.WriteLine("4. Выравнивание строк вправо");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("\nВыберите: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                Console.Write("Введите имя входного файла: ");
                string inputFile = Path.Combine(path, Console.ReadLine());
                if (!File.Exists(inputFile))
                {
                    Console.WriteLine("Файл не найден. Создан тестовый файл.");
                    File.WriteAllLines(inputFile, new[] { "1 2 3 4 5 6", "hello world test", "The quick brown fox" });
                }

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("\nДо:");
                            Console.WriteLine(File.ReadAllText(inputFile));
                            FileOperations.RemoveEvenNumbers(inputFile);
                            Console.WriteLine("\nПосле удаления чётных:");
                            Console.WriteLine(File.ReadAllText(inputFile));
                            break;

                        case "2":
                            Console.Write("Введите имя выходного файла: ");
                            string outputFile2 = Path.Combine(path, Console.ReadLine());
                            FileOperations.ReverseWordsInFile(inputFile, outputFile2);
                            Console.WriteLine("\nРезультат:");
                            Console.WriteLine(File.ReadAllText(outputFile2));
                            break;

                        case "3":
                            Console.WriteLine("\nДо:");
                            Console.WriteLine(File.ReadAllText(inputFile));
                            FileOperations.RemoveWords3to5(inputFile);
                            Console.WriteLine("\nПосле удаления:");
                            Console.WriteLine(File.ReadAllText(inputFile));
                            break;

                        case "4":
                            Console.Write("Введите имя выходного файла: ");
                            string outputFile4 = Path.Combine(path, Console.ReadLine());
                            FileOperations.AlignRight(inputFile, outputFile4);
                            Console.WriteLine("\nРезультат:");
                            Console.WriteLine(File.ReadAllText(outputFile4));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка: {ex.Message}");
                }

                Console.ReadKey();
            }
        }

        static void RunVariant2(string path)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ВАРИАНТ 2");
                Console.WriteLine("1. Удалить слова с цифрами");
                Console.WriteLine("2. Генерация и сортировка чисел");
                Console.WriteLine("3. Транспонирование матрицы");
                Console.WriteLine("4. Шифр Цезаря");
                Console.WriteLine("0. Назад в главное меню");
                Console.Write("\nВыберите: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите имя файла: ");
                            string file1 = Path.Combine(path, Console.ReadLine());
                            if (!File.Exists(file1))
                            {
                                File.WriteAllLines(file1, new[] { "hello123 world test456", "abc def" });
                                Console.WriteLine("Создан тестовый файл.");
                            }
                            Console.WriteLine("\nДо:");
                            Console.WriteLine(File.ReadAllText(file1));
                            FileOperations.RemoveWordsWithDigits(file1);
                            Console.WriteLine("\nПосле:");
                            Console.WriteLine(File.ReadAllText(file1));
                            break;

                        case "2":
                            Console.Write("Имя файла: ");
                            string file2 = Path.Combine(path, Console.ReadLine());
                            Console.Write("Количество чисел: ");
                            int count = int.Parse(Console.ReadLine());
                            Console.Write("Минимум: ");
                            int min = int.Parse(Console.ReadLine());
                            Console.Write("Максимум: ");
                            int max = int.Parse(Console.ReadLine());

                            FileOperations.CreateAndSortRandomNumbers(file2, count, min, max);
                            Console.WriteLine("\nОтсортированные числа:");
                            Console.WriteLine(File.ReadAllText(file2));
                            break;

                        case "3":
                            Console.Write("Имя файла с матрицей: ");
                            string file3 = Path.Combine(path, Console.ReadLine());
                            if (!File.Exists(file3))
                            {
                                File.WriteAllLines(file3, new[] { "1 2 3", "4 5 6", "7 8 9" });
                                Console.WriteLine("Создан тестовый файл с матрицей 3x3.");
                            }
                            Console.WriteLine("\nИсходная матрица:");
                            Console.WriteLine(File.ReadAllText(file3));
                            var matrix = FileOperations.ReadAndTransposeMatrix(file3);
                            Console.WriteLine("\nТранспонированная матрица:");
                            FileOperations.PrintMatrix(matrix);
                            break;

                        case "4":
                            Console.Write("Имя входного файла: ");
                            string input = Path.Combine(path, Console.ReadLine());
                            if (!File.Exists(input))
                            {
                                File.WriteAllText(input, "Hello World");
                                Console.WriteLine("Создан тестовый файл.");
                            }
                            Console.Write("Сдвиг (1-25): ");
                            int shift = int.Parse(Console.ReadLine());

                            string encoded = Path.Combine(path, "encoded.txt");
                            string decoded = Path.Combine(path, "decoded.txt");

                            Console.WriteLine("\nИсходный текст:");
                            Console.WriteLine(File.ReadAllText(input));

                            FileOperations.CaesarEncrypt(input, encoded, shift);
                            Console.WriteLine($"\nЗашифровано (сдвиг {shift}):");
                            Console.WriteLine(File.ReadAllText(encoded));

                            FileOperations.CaesarDecrypt(encoded, decoded, shift);
                            Console.WriteLine("\nРасшифровано:");
                            Console.WriteLine(File.ReadAllText(decoded));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка: {ex.Message}");
                }

                Console.ReadKey();
            }
        }
    }
}