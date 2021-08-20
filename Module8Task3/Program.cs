using System;
using System.IO;

namespace Module8Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\YOGA\Desktop\WaitingForDeath");
            try
            {
                if (directory.Exists)
                {
                    long size = 0;
                    long fileCount = 0;
                    long totalsize = EvaluateSpace(size, directory);
                    Console.WriteLine($"Исходный размер папки {directory}: {totalsize} байт");
                    long deletedfiles = DeleteAndCount(fileCount, directory);
                    Console.WriteLine($"Удалено файлов: {deletedfiles}");
                    long newtotalsize = EvaluateSpace(size, directory);
                    Console.WriteLine($"Освобождено: {totalsize - newtotalsize}");
                    Console.WriteLine($"Текущий размер папки: {newtotalsize}");
                }
                else
                {
                    Console.WriteLine("Такой папки нет...");
                }
            }
            catch (Exception ex)
            {
                if(ex is UnauthorizedAccessException)
                {
                    Console.WriteLine("Нет прав доступа...");
                }
            }
        }

        static long EvaluateSpace(long size, DirectoryInfo dirSpace)
        {
            foreach (FileInfo file in dirSpace.GetFiles())
            {
                size += file.Length;
            }
            foreach (DirectoryInfo papka in dirSpace.GetDirectories())
            {
                size += EvaluateSpace(0, papka);
            }
            return size;
        }

        static long DeleteAndCount(long fileCount, DirectoryInfo dirSpace)
        {
            DateTime thirty = DateTime.Now - TimeSpan.FromMinutes(30);
            foreach (FileInfo file in dirSpace.GetFiles())
            {
                if(file.LastAccessTime < thirty)
                {
                    fileCount++;
                    file.Delete();
                }
            }
            foreach(DirectoryInfo papka in dirSpace.GetDirectories())
            {
                DeleteAndCount(0, papka);
            }
            return fileCount;
        }
    }
}
