using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace WordCount
{
    public class Program
    {
        public static int count = 10;//Количество раз
        public static string doc = "http://www.gutenberg.org/cache/epub/5200/pg5200.txt";
        private static WebClient wClient = new WebClient();
        private static Stream sStream = wClient.OpenRead(doc);
        private static StreamReader sRead = new StreamReader(sStream);
        private static List<string> text = new List<string>();
        private static string emptyLine = "";
        private static string[] strSeparation = new string[] { " " };//Условие для разделения
        private static string[] result;
        private static List<string> words = new List<string>();

        public static void Main()
        {
             
            while (emptyLine != null)
            {
                emptyLine = sRead.ReadLine();
                text.Add(emptyLine);
            }
            if (emptyLine == null)
            {
                sStream.Close();
            }
            foreach (string s in text)
            {
                if (s != null)
                {
                    result = s.Split(strSeparation, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string w in result)
                    {
                        words.Add(w);
                    }
                }
            }

            var grouped = words
                .GroupBy(i => i)//Группируем
                .Where(x => x.Count() < words.Count())//Условие для выборки
                .Select(i => new { Word = i.Key, Count = i.Count() })//Создаём выходную последовательность типа элемент массива-количество
                .OrderByDescending(i => i.Count)//Сортировка по убыванию
                .Take(count)//Взять такое-то кол-во
                .ToList();//Добавляем в динамический массив

            foreach (var h in grouped)
            {
                Console.WriteLine(h);
            }

            Console.ReadKey();
        }
    }
}