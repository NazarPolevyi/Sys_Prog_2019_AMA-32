using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Information_Protection
{
    class Program
    {
        static void Main(string[] args)
        {
            string largeFilePath = "C:\\Users\\polzar\\Desktop\\full.txt";
            string encrypted24 = "C:\\Users\\polzar\\Desktop\\encrypted24.txt";
            string EncryptedFilePath = "C:\\Users\\polzar\\Desktop\\encrypted24.txt";


            string EncryptedFile = GiveMeStringFile(EncryptedFilePath);
            string file = GiveMeStringFile(largeFilePath);



            int encryptedFileSize = EncryptedFile.ToCharArray().Length;
            int myFileSize = file.ToCharArray().Length;

            Console.WriteLine("Encrypted File analysis:\n");
            Dictionary<char, int> entriesEncrypted = AmountOfEntries(EncryptedFile);
            Dictionary<char, int> MyFileEntries = AmountOfEntries(file);
            Dictionary<char, char> ReplacementLetters = new Dictionary<char, char>();
            int i = 0;
            List<KeyValuePair<char, int>> encryptedListStat = GetShowStatistics(entriesEncrypted, encryptedFileSize);

            List<char> officialStatistic = new List<char> { 'e', 't', 'a', 'o', 'i', 'n', 's', 'h', 'r', 'd', 'l', 'c', 'u', 'm', 'w', 'f', 'g', 'y', 'p', 'b', 'v', 'k', 'j', 'x', 'q', 'z' };

            Console.WriteLine("\nMy File analysis:\n");
            List<KeyValuePair<char, int>> myFile = GetShowStatistics(MyFileEntries, myFileSize);


            foreach (KeyValuePair<char, int> item in entriesEncrypted)
            {
                  ReplacementLetters.Add(encryptedListStat.ElementAt(i).Key, officialStatistic[i]); //Official Analysis
               //ReplacementLetters.Add(encryptedListStat.ElementAt(i).Key, MyFileEntries.ElementAt(i).Key); // My file frequency  
                //Console.WriteLine(ReplacementLetters.ElementAt(i).Key + ": " + ReplacementLetters.ElementAt(i).Value);
                i++;

            }


            Console.WriteLine("Decrypted file:\n");

            foreach (char item in EncryptedFile)
            {
                if (Char.IsLetter(item))
                {
                    char curLetter = Char.ToLower(item);
                    if ((int)curLetter >= 97 && (int)curLetter <= 122)
                    {
                        Console.Write(ReplacementLetters[curLetter]);
                    }
                }
                else Console.Write(item);
            }
        }

        public static List<KeyValuePair<char, int>> GetShowStatistics(Dictionary<char, int> entries, int fileSize)
        {
            //var keyList = entries.Keys.ToList();

            //keyList.Sort();

            //foreach (var key in keyList)
            //    Console.WriteLine($"Letter: {key} - Entry number: {entries[key]} - Frequency: {entries[key]}/{FileLength}");


            var myList = entries.ToList();

            myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            foreach (KeyValuePair<char, int> item in myList)
                Console.WriteLine($"Letter: {item.Key} - Entry number: {item.Value} - Frequency: {item.Value}/{fileSize}");
            return myList;
        }

        public static string GiveMeStringFile(string path)
        {
            StringBuilder line = new StringBuilder("");
            using (StreamReader readFile = new StreamReader(path))
            {
                while (!readFile.EndOfStream)
                {

                    line.Append(readFile.ReadLine());
                }
            }
            //Console.WriteLine(line);
            return line.ToString();
        }

        public static Dictionary<char, int> AmountOfEntries(string file)
        {
            Dictionary<char, int> entries = new Dictionary<char, int>();

            foreach (char symbol in file)
            {
                if (Char.IsLetter(symbol))
                {
                    char curLetter = Char.ToLower(symbol);
                    if ((int)symbol >= 97 && (int)symbol <= 122)
                    {

                        if (entries.ContainsKey(curLetter))
                        {
                            entries[curLetter] += 1;
                        }
                        else
                        {
                            entries.Add(curLetter, 1);
                        }
                    }
                }
            }
            return entries;
        }
    }
}
