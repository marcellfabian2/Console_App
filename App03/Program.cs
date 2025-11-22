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
        static int CompareByName(FileInfo a, FileInfo b)
        {
            return a.Name.CompareTo(b.Name);
        }

        static int CompareBySize(FileInfo a, FileInfo b)
        {
            return a.Length.CompareTo(b.Length);
        }

        static int CompareByDate(FileInfo a, FileInfo b)
        {
            return a.LastWriteTime.CompareTo(b.LastWriteTime);
        }

        static int CompareByExtension(FileInfo a, FileInfo b)
        {
            return a.Extension.CompareTo(b.Extension);
        }

        static void Main(string[] args)
        {
            //Fehér betűszín alapvetően
            Console.ForegroundColor = ConsoleColor.White;

            //Kezdő útvonal megadás
            Console.Write("Add meg a kezdő útvonalat: ");
            string path = Console.ReadLine();
            path += @":\";
            Console.Write("Add meg a fájlok méretének formátumát (B, KB, MB, GB): ");
            string fajlmeret = Console.ReadLine();
            fajlmeret = fajlmeret.ToUpper();

            //Létezik-e vizsgálat
            while (!Directory.Exists(path))
            {
                Console.WriteLine("A megadott útvonal nem létezik vagy nem elérhető!");
                Console.Write("Add meg a kezdő útvonalat: ");
                path = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(path))
                    return;
            }

            //Listázás előtti clear és tájékoztatás
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Aktuális mappa: {path}");
                Console.WriteLine("\nMappák és fájlok:");
                Console.WriteLine("-----------------");

                //Try catches megoldással, hibák kiküszöbölése illetve fájlok és mappák kilistázása
                try
                {
                    //Mappák kilistázása
                    var directories = Directory.GetDirectories(path);
                    foreach (var i in directories)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[MAPPA] {Path.GetFileName(i)}");
                    }
                    Console.ForegroundColor = ConsoleColor.White;

                    //Fájlok listázása
                    var files = Directory.GetFiles(path);
                    foreach (var j in files)
                    {
                        FileInfo fileInfo = new FileInfo(j);
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (fajlmeret == "B")
                        {
                            Console.WriteLine($"[FÁJL]  {Path.GetFileName(j)} ({(double)fileInfo.Length} B)");
                        }
                        else if (fajlmeret == "KB")
                        {
                            Console.WriteLine($"[FÁJL]  {Path.GetFileName(j)} ({Math.Round((double)fileInfo.Length / 1000, 2)} KB)");
                        }
                        else if (fajlmeret == "MB")
                        {
                            Console.WriteLine($"[FÁJL]  {Path.GetFileName(j)} ({Math.Round((double)fileInfo.Length / 1000000, 3)} MB)");
                        }
                        else if (fajlmeret == "GB")
                        {
                            Console.WriteLine($"[FÁJL]  {Path.GetFileName(j)} ({Math.Round((double)fileInfo.Length / 1000000000, 4)} GB)");
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("-----------------");

                    //Tájékoztatás és parancsok
                    Console.WriteLine("\nVálasztási lehetőségek:");
                    Console.WriteLine("- Mappa neve: Belépés a mappába");
                    Console.WriteLine("- q: Vissza a szülőmappába");
                    Console.WriteLine("- rf: Rendezése a fájloknak (Név, Méret, Dátum, Kiterjesztés)");
                    Console.WriteLine("- rd: Rendezése a mappáknak név szerint");
                    Console.WriteLine("- m: Fájlok másolása az asztalra");
                    Console.WriteLine("- exit: Kilépés");
                    Console.Write("\nVálasztásod: ");
                    string input = Console.ReadLine();

                    //Kilépés parancs
                    if (input.ToLower() == "exit")
                        break;

                    //Visszalépés a szülőmappába
                    if (input == "q")
                    {
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

                    //Rendezett mappák mutatása
                    else if (input == "rd")
                    {
                        List<string> dirList = new List<string>();

                        foreach (var d in Directory.GetDirectories(path))
                            dirList.Add(Path.GetFileName(d));

                        dirList.Sort();

                        Console.WriteLine("\nRendezett mappák (másold ki a kívánt mappa nevét):");
                        foreach (var d in dirList)
                            Console.WriteLine(d);

                        Console.WriteLine("\nNyomj Entert a kilépéshez...");
                        Console.ReadLine();
                    }

                    //Fájlok rendezése
                    else if (input == "rf")
                    {
                        Console.Write("Rendezés (Név/Méret/Dátum/Kiterjesztés): ");
                        string mode = Console.ReadLine().ToLower();

                        //Ideiglenes lista fájlokhoz
                        List<FileInfo> fileList = new List<FileInfo>();

                        foreach (var f in Directory.GetFiles(path))
                            fileList.Add(new FileInfo(f));

                        //Rendezés
                        if (mode == "név")
                            fileList.Sort(CompareByName);
                        else if (mode == "méret")
                            fileList.Sort(CompareBySize);
                        else if (mode == "dátum")
                            fileList.Sort(CompareByDate);
                        else if (mode == "kiterjesztés")
                            fileList.Sort(CompareByExtension);
                        else
                        {
                            Console.WriteLine("Ismeretlen mód!");
                            Console.ReadLine();
                            continue;
                        }

                        Console.WriteLine("\nRendezett fájlok:");
                        foreach (var f in fileList)
                        {
                            string sizeText = "";
                            if (fajlmeret == "B")
                                sizeText = $"{(double)f.Length} B";
                            else if (fajlmeret == "KB")
                                sizeText = $"{Math.Round((double)f.Length / 1000, 2)} KB";
                            else if (fajlmeret == "MB")
                                sizeText = $"{Math.Round((double)f.Length / 1000000, 3)} MB";
                            else if (fajlmeret == "GB")
                                sizeText = $"{Math.Round((double)f.Length / 1000000000, 4)} GB";

                            Console.WriteLine($"{f.Name} | {sizeText} | {f.LastWriteTime} | {f.Extension}");
                        }

                        Console.WriteLine("\nNyomj Entert...");
                        Console.ReadLine();
                    }

                    //Fájlok másolása
                    else if (input.ToLower() == "m")
                    {
                        Console.Write("Add meg a célmappa nevét (az asztalon jön létre): ");
                        string folderName = Console.ReadLine();

                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string targetPath = Path.Combine(desktopPath, folderName);

                        //Célmappa létrehozása
                        if (!Directory.Exists(targetPath))
                        {
                            Directory.CreateDirectory(targetPath);
                        }

                        List<FileInfo> masolando = new List<FileInfo>();

                        //Fájlok listázása
                        foreach (var f in Directory.GetFiles(path))
                            masolando.Add(new FileInfo(f));

                        //Kombinált szűrés
                        Console.Write("Kiterjesztés filter (pl: .txt, üres ha nem számít): ");
                        string extensionFilter = Console.ReadLine();

                        Console.Write("Minimális méret (byte, üres ha nem számít): ");
                        string minSizeInput = Console.ReadLine();
                        long minSize = string.IsNullOrEmpty(minSizeInput) ? 0 : long.Parse(minSizeInput);

                        Console.Write("Maximális méret (byte, üres ha nem számít): ");
                        string maxSizeInput = Console.ReadLine();
                        long maxSize = string.IsNullOrEmpty(maxSizeInput) ? long.MaxValue : long.Parse(maxSizeInput);

                        //Filterezés
                        List<FileInfo> filteredList = new List<FileInfo>();
                        foreach (FileInfo f in masolando)
                        {
                            bool extensionMatches = string.IsNullOrEmpty(extensionFilter) ||
                                                   f.Extension.Equals(extensionFilter, StringComparison.OrdinalIgnoreCase);
                            bool sizeMatches = f.Length >= minSize && f.Length <= maxSize;

                            if (extensionMatches && sizeMatches)
                            {
                                filteredList.Add(f);
                            }
                        }
                        masolando = filteredList;

                        Console.Write("Rendezés (név/méret/dátum/kiterjesztés): ");
                        string sortMode = Console.ReadLine().ToLower();

                        if (sortMode == "név")
                            masolando.Sort(CompareByName);
                        else if (sortMode == "méret")
                            masolando.Sort(CompareBySize);
                        else if (sortMode == "dátum")
                            masolando.Sort(CompareByDate);
                        else if (sortMode == "kiterjesztés")
                            masolando.Sort(CompareByExtension);

                        //Fájlok másolása
                        int copiedCount = 0;
                        foreach (var file in masolando)
                        {
                            try
                            {
                                string destFile = Path.Combine(targetPath, file.Name);
                                File.Copy(file.FullName, destFile, true);
                                Console.WriteLine($"Másolva: {file.Name}");
                                copiedCount++;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Hiba a {file.Name} másolásakor: {ex.Message}");
                            }
                        }

                        Console.WriteLine($"\nÖsszesen {copiedCount} fájl másolva a {targetPath} mappába.");
                        Console.WriteLine("Nyomj Entert a folytatáshoz...");
                        Console.ReadLine();
                    }

                    //Nemlétező fájlok vizsgálata
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
                //Nincs jogosultság 
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Nincs jogosultságod ehhez a mappához!");
                    Console.WriteLine("Nyomj Entert a folytatáshoz...");
                    Console.ReadLine();
                }
                //Egyéb hiba
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
