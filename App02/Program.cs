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
           }
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
            }*/

            Console.Write("Add meg a kezdő útvonalat: ");
            string path = Console.ReadLine();
            path += @":\";

            while (!Directory.Exists(path))
            {
                Console.WriteLine("A megadott útvonal nem létezik vagy nem elérhető!");
                Console.Write("Add meg a kezdő útvonalat: ");
                path = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(path))
                    return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Aktuális mappa: {path}");
                Console.WriteLine("\nMappák és fájlok:");
                Console.WriteLine("-------------------");

                try
                {
                    var directories = Directory.GetDirectories(path);
                    foreach (var i in directories)
                    {
                        Console.WriteLine($"[MAPPA] {Path.GetFileName(i)}");
                    }

                    // Fájlok listázása
                    var files = Directory.GetFiles(path);
                    foreach (var j in files)
                    {
                        FileInfo fileInfo = new FileInfo(j);
                        Console.WriteLine($"[FÁJL]  {Path.GetFileName(j)} ({fileInfo.Length} bytes)");
                    }

                    Console.WriteLine("\nVálasztási lehetőségek:");
                    Console.WriteLine("- Mappa neve: Belépés a mappába");
                    Console.WriteLine("- q : Vissza a szülőmappába");
                    Console.WriteLine("- exit: Kilépés");
                    Console.Write("\nVálassz: ");

                    string input = Console.ReadLine();

                    if (input.ToLower() == "exit")
                        break;

                    if (input == "q")
                    {
                        // Visszalépés a szülőmappába
                        DirectoryInfo parent = Directory.GetParent(path);
                        if (parent != null)
                            path = parent.FullName;
                        else
                        {
                            Console.WriteLine("Már a gyökérkönyvtárban van!");
                            Console.WriteLine("Nyomj Entert a folytatáshoz...");
                            Console.ReadLine();
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(input))
                    {
                        string newPath = Path.Combine(path, input);
                        if (Directory.Exists(newPath))
                        {
                            path = newPath;
                        }
                        else
                        {
                            Console.WriteLine("A megadott mappa nem létezik!");
                            Console.WriteLine("Nyomj Entert a folytatáshoz...");
                            Console.ReadLine();
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Nincs jogosultságod ehhez a mappához!");
                    Console.WriteLine("Nyomj Entert a folytatáshoz...");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hiba történt: {ex.Message}");
                    Console.WriteLine("Nyomj Entert a folytatáshoz...");
                    Console.ReadLine();
                }
            }

            Console.ReadKey();
        }
    }
}
