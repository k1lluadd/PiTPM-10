using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace FileTask
{
    public static class FileOperations
    {

        public static void RemoveEvenNumbers(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var result = new List<string>();
            foreach (var line in lines)
            {
                var numbers = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => int.TryParse(n, out var num) ? (int?)num : null)
                    .Where(n => n.HasValue && n.Value % 2 != 0)
                    .Select(n => n.Value.ToString());
                result.Add(string.Join(" ", numbers));
            }
            File.WriteAllLines(filePath, result);
        }

        public static void ReverseWordsInFile(string inputPath, string outputPath)
        {
            var lines = File.ReadAllLines(inputPath);
            var result = lines.Select(line =>
            {
                var words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Array.Reverse(words);
                return string.Join(" ", words);
            });
            File.WriteAllLines(outputPath, result);
        }

        public static void RemoveWords3to5(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var result = new List<string>();
            foreach (var line in lines)
            {
                var words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var shortWords = words.Where(w => w.Length >= 3 && w.Length <= 5).ToList();
                if (shortWords.Count % 2 != 0)
                    shortWords.RemoveAt(shortWords.Count - 1);
                foreach (var sw in shortWords)
                    words.Remove(sw);
                result.Add(string.Join(" ", words));
            }
            File.WriteAllLines(filePath, result);
        }

        public static void AlignRight(string inputPath, string outputPath)
        {
            var lines = File.ReadAllLines(inputPath);
            var maxLength = lines.Max(l => l.Length);
            var result = lines.Select(l => l.PadLeft(maxLength));
            File.WriteAllLines(outputPath, result);
        }


        public static void RemoveWordsWithDigits(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var result = new List<string>();
            foreach (var line in lines)
            {
                var words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                var filtered = words.Where(w => !w.Any(char.IsDigit));
                result.Add(string.Join(" ", filtered));
            }
            File.WriteAllLines(filePath, result);
        }

        public static void CreateAndSortRandomNumbers(string filePath, int count, int min, int max)
        {
            var random = new Random();
            var numbers = new List<int>();
            for (int i = 0; i < count; i++)
            {
                numbers.Add(random.Next(min, max + 1));
            }
            File.WriteAllLines(filePath, numbers.Select(n => n.ToString()));
            numbers.Sort();
            File.WriteAllLines(filePath, numbers.Select(n => n.ToString()));
        }

        public static int[,] ReadAndTransposeMatrix(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var n = lines.Length;
            var matrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var row = lines[i].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < row.Length; j++)
                {
                    matrix[i, j] = int.Parse(row[j]);
                }
            }
            var transposed = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    transposed[i, j] = matrix[j, i];
                }
            }
            return transposed;
        }

        public static void CaesarEncrypt(string inputPath, string outputPath, int shift)
        {
            var text = File.ReadAllText(inputPath);
            var result = "";
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    result += (char)((c - offset + shift + 26) % 26 + offset);
                }
                else
                {
                    result += c;
                }
            }
            File.WriteAllText(outputPath, result);
        }

        public static void CaesarDecrypt(string inputPath, string outputPath, int shift)
        {
            CaesarEncrypt(inputPath, outputPath, -shift);
        }

        public static void PrintMatrix(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i, j],4} ");
                }
                Console.WriteLine();
            }
        }

        public static void SaveMatrixToFile(int[,] matrix, string filePath)
        {
            int n = matrix.GetLength(0);
            var lines = new List<string>();
            for (int i = 0; i < n; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < n; j++)
                {
                    row.Add(matrix[i, j].ToString());
                }
                lines.Add(string.Join(" ", row));
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}