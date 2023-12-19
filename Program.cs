using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> godrok = new List<int>();

        // 1. feladat
        using (StreamReader bemenet = new StreamReader("melyseg.txt"))
        {
            string sor;
            while ((sor = bemenet.ReadLine()) != null)
            {
                godrok.Add(int.Parse(sor.Trim()));
            }
        }

        Console.WriteLine("1. feladat");
        Console.WriteLine($"A fájl adatainak száma: {godrok.Count}");

        // 2. feladat
        Console.WriteLine("2. feladat");
        Console.Write("Adjon meg egy távolságerteket! ");
        int hely = int.Parse(Console.ReadLine());
        Console.WriteLine($"Ezen a helyen a felszín {godrok[hely - 1]} méter mélyen van.");

        // 3. feladat
        Console.WriteLine("3. feladat");
        int erintetlen = godrok.Count(mert => mert == 0);
        Console.WriteLine($"Az erintetlen terület aránya {100.0 * erintetlen / godrok.Count:0.00}%.");

        // 4. feladat
        using (StreamWriter kimenet = new StreamWriter("godrok.txt"))
        {
            int elozo = 0;
            List<string> egysor = new List<string>();
            List<List<string>> sorok = new List<List<string>>();
            foreach (int ertek in godrok)
            {
                if (ertek > 0)
                {
                    egysor.Add(ertek.ToString());
                }
                if (ertek == 0 && elozo > 0)
                {
                    sorok.Add(new List<string>(egysor));
                    egysor.Clear();
                }
                elozo = ertek;
            }

            foreach (var sor in sorok)
            {
                kimenet.WriteLine(string.Join(" ", sor));
            }
        }

        Console.WriteLine("5. feladat");
        Console.WriteLine($"A gödrök száma: {godrok.Count}");

        // 6. feladat
        if (godrok[hely - 1] > 0)
        {
            Console.WriteLine("a)");
            int pozicio = hely - 1;
            while (godrok[pozicio] > 0)
            {
                pozicio--;
            }
            int kezdo = pozicio + 2;
            pozicio = hely;
            while (godrok[pozicio] > 0)
            {
                pozicio++;
            }
            int zaro = pozicio;
            Console.WriteLine($"A gödör kezdete: {kezdo} méter, a gödör vége: {zaro} méter.");

            Console.WriteLine("b)");
            int melypont = 0;
            pozicio = kezdo;
            while (godrok[pozicio] >= godrok[pozicio - 1] && pozicio <= zaro)
            {
                pozicio++;
            }
            while (godrok[pozicio] <= godrok[pozicio - 1] && pozicio <= zaro)
            {
                pozicio++;
            }
            if (pozicio > zaro)
            {
                Console.WriteLine("Folyamatosan mélyül.");
            }
            else
            {
                Console.WriteLine("Nem mélyül folyamatosan.");
            }

            Console.WriteLine("c)");
            Console.WriteLine($"A legnagyobb mélysége {godrok.GetRange(kezdo - 1, zaro - kezdo + 1).Max()} méter.");

            Console.WriteLine("d)");
            double terfogat = 10 * godrok.GetRange(kezdo - 1, zaro - kezdo + 1).Sum();
            Console.WriteLine($"A térfogata {terfogat} köbméter.");

            Console.WriteLine("e)");
            double biztonsagos = terfogat - 10 * (zaro - kezdo + 1);
            Console.WriteLine($"A vízmennyiség {biztonsagos} köbméter.");
        }
        else
        {
            Console.WriteLine("Az adott helyen nincs gödör.");
        }
    }
}