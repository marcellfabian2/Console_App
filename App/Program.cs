using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace App
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //----------------------
            /*
            string sourceFile = @"D:\kezdo\proba.txt";
            string destinationFile = @"D:\veg\probacopy.txt";
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }


            //---------------------

            /*
           DirectoryInfo place = new DirectoryInfo(@"D:\kezdo");

           DirectoryInfo[] Files = place.GetDirectories();
           Console.WriteLine("Files are:");
           Console.WriteLine(Files.Count());

           foreach (DirectoryInfo i in Files)
           {
               Console.WriteLine("File Name - {0}", i.Name);
           }*/
            Console.Write("Adj megy egy új útvonalat, vagy kilépéshez nyomj Entert: ");
            string vizsga = Console.ReadLine();
            while(vizsga != "")
            {
                DirectoryInfo Hely = new DirectoryInfo($@"{vizsga}");

                DirectoryInfo[] directories = Hely.GetDirectories();
                FileInfo[] files = Hely.GetFiles();
                Console.WriteLine("A mappák és fájlok:");
                Console.WriteLine();

                foreach (DirectoryInfo i in directories)
                {
                    Console.WriteLine($"Mappa neve: {i.Name}");
                }
                foreach (FileInfo j in files)
                {
                    Console.WriteLine($"Fájl neve: {j.Name}");
                }
                vizsga+=@"\";
                Console.Write("Adj megy egy új útvonalat, vagy kilépéshez nyomj Entert: ");
                vizsga = Console.ReadLine();
                vizsga += @"\";
            }
       

            Console.ReadKey();
        }
    }
}
