using FuzzyLogicPCL;
using FuzzyLogicPCL.FuzzySets;
using System;

namespace FuzzyLogicApp
{
    class Test
    {

        static void Main(string[] args)
        {
            // Création des Fuzzy Sets
            FuzzySet fz = new FuzzySet(0, 100);
            Point2D pt1 = new Point2D(0, 1);
            Point2D pt2 = new Point2D(50, 0);
            Point2D pt3 = new Point2D(100, 0);
            fz.Add(pt3);
            fz.Add(pt2);
            fz.Add(pt1);

            TriangularFuzzySet tifz = new TriangularFuzzySet(0, 100, 30, 50, 70);
            FuzzySet itifz = !tifz;
            TrapezoidalFuzzySet trfz = new TrapezoidalFuzzySet(0, 100, 30, 50, 70, 80);
            TrapezoidalFuzzySet trfz2 = new TrapezoidalFuzzySet(0, 100, 30, 50, 70, 80);
            LeftFuzzySet lfz = new LeftFuzzySet(0, 100, 30, 50);
            RightFuzzySet rfz = new RightFuzzySet(0, 100, 70, 80);

            // Calcul des degrés
            /*Console.WriteLine("**** Degree Fuzzy Set quelconque ****");
            PrintDegrees(fz);
            Console.WriteLine("\n**** Test Triangular Fuzzy Set ****");
            PrintDegrees(tifz);
            Console.WriteLine("\n**** Test Inversed Triangular Fuzzy Set ****");
            PrintDegrees(itifz);
            Console.WriteLine("\n**** Test Trapezoidal Fuzzy Set ****");
            PrintDegrees(trfz);
            Console.WriteLine("\n**** Test Left Fuzzy Set ****");
            PrintDegrees(lfz);
            Console.WriteLine("\n**** Test Right Fuzzy Set ****");
            PrintDegrees(rfz);*/

            // Affichage
            /*Console.WriteLine("\n**** Print ****");
            Console.WriteLine("Trap Fuzzy Set :");
            Console.WriteLine(trfz.ToString());
            Console.WriteLine("Left Fuzzy Set :");
            Console.WriteLine(lfz.ToString());
            Console.WriteLine("Triang Fuzzy Set :");
            Console.WriteLine(tifz.ToString());
            Console.WriteLine("Inverse triang Fuzzy Set :");
            Console.WriteLine(itifz.ToString());*/

            // Test des opérateurs
            /*Console.WriteLine("\n**** Test operators ****");
            Console.WriteLine("1/2 Trap Fuzzy Set :");
            Console.WriteLine((trfz * 0.5).ToString());
            Console.WriteLine("1/4 Left Fuzzy Set :");
            Console.WriteLine((lfz * 0.25).ToString());*/

            // Test égalité
            /*Console.WriteLine("\n**** Test equality ****");
            Console.WriteLine("Trap Fuzzy Set 1 :");
            Console.WriteLine(trfz.ToString());
            Console.WriteLine("Trap Fuzzy Set 2 :");
            Console.WriteLine(trfz2.ToString());
            Console.WriteLine("TRFZ == TRFZ2 (true) : " + (trfz == trfz2));
            Console.WriteLine("TRFZ != TRFZ2 (false) : " + (trfz != trfz2));
            Console.WriteLine("TRFZ == LFZ (false) : " + (trfz == lfz));*/

            // Test intersection
            /*Console.WriteLine("\n**** Test intersection");
            Console.WriteLine("FS1 : " + trfz.ToString());
            Console.WriteLine("FS2 : " + lfz.ToString());
            Console.WriteLine("TIFS : " + tifz.ToString());
            Console.WriteLine("FS : " + fz.ToString());
            Console.WriteLine("****");
            Console.WriteLine("FS1 & FS2 : " + (trfz & lfz).ToString());
            Console.WriteLine("FS2 & FS1 : " + (lfz & trfz).ToString());
            Console.WriteLine("FS1 & FS1 : " + (trfz & trfz).ToString());
            Console.WriteLine("FS1 & 0.5 * FS2 : " + (trfz & (lfz * 0.5)).ToString());
            Console.WriteLine("0.5 * FS1 & FS2 : " + ((trfz * 0.5) & lfz).ToString());
            Console.WriteLine("FS1 & TIFS : " + (trfz & tifz).ToString());
            Console.WriteLine("FS1 & FS : " + (trfz & fz).ToString());
            Console.WriteLine("FS2 & TIFS : " + (lfz & tifz).ToString());
            Console.WriteLine("FS2 & FS : " + (lfz & fz).ToString());
            Console.WriteLine("tri1 & tri2 : " + (new TriangularFuzzySet(20, 100, 40, 50, 60) & new TriangularFuzzySet(0, 80, 30, 40, 50)).ToString());
            Console.WriteLine("tri2 & tri1 : " + (new TriangularFuzzySet(0, 80, 30, 40, 50) & new TriangularFuzzySet(20, 100, 40, 50, 60)).ToString());*/

            // Test union 
            /*Console.WriteLine("\n**** Test union");
            Console.WriteLine("FS1 : " + trfz.ToString());
            Console.WriteLine("FS2 : " + lfz.ToString());
            Console.WriteLine("TIFS : " + tifz.ToString());
            Console.WriteLine("FS : " + fz.ToString());
            Console.WriteLine("****");
            Console.WriteLine("FS1 | FS2 : " + (trfz | lfz).ToString());
            Console.WriteLine("FS2 | FS1 : " + (lfz | trfz).ToString());
            Console.WriteLine("FS1 | FS1 : " + (trfz | trfz).ToString());
            Console.WriteLine("FS1 | 0.5 * FS2 : " + (trfz | (lfz * 0.5)).ToString());
            Console.WriteLine("0.5 * FS1 | FS2 : " + ((trfz * 0.5) | lfz).ToString());
            Console.WriteLine("FS1 | TIFS : " + (trfz | tifz).ToString());
            Console.WriteLine("FS1 | FS : " + (trfz | fz).ToString());
            Console.WriteLine("FS2 | TIFS : " + (lfz | tifz).ToString());
            Console.WriteLine("FS2 | FS : " + (lfz | fz).ToString());
            Console.WriteLine("tri1 | tri2 : " + (new TriangularFuzzySet(20, 100, 40, 50, 60) | new TriangularFuzzySet(0, 80, 30, 40, 50)).ToString());
            Console.WriteLine("tri2 | tri1 : " + (new TriangularFuzzySet(0, 80, 30, 40, 50) | new TriangularFuzzySet(20, 100, 40, 50, 60)).ToString());*/

            // Test centre de gravité
            /*Console.WriteLine("\n**** Test centre de gravité");
            Console.WriteLine("TRFS : " + trfz.ToString());
            Console.WriteLine("TRFS CoG : " + trfz.Centroid());
            Console.WriteLine("1/2 TRFS : " + (trfz*0.5).ToString());
            Console.WriteLine("1/2 TRFS CoG : " + (trfz*0.5).Centroid());
            Console.WriteLine("TIFS : " + tifz.ToString());
            Console.WriteLine("TIFS CoG : " + tifz.Centroid());
            Console.WriteLine("LFS : " + lfz.ToString());
            Console.WriteLine("LFS CoG : " + lfz.Centroid());
            Console.WriteLine("FS : " + fz.ToString());
            Console.WriteLine("FS CoG : " + fz.Centroid());
            Console.WriteLine("TRFS & LFZ : " + (trfz & lfz).ToString());
            Console.WriteLine("TRFS & LFZ CoG : " + (trfz & lfz).Centroid());
            Console.WriteLine("TRFS | LFZ : " + (trfz | lfz).ToString());
            Console.WriteLine("TRFS | LFZ CoG : " + (trfz | lfz).Centroid());*/

            while (true) ;
        }

        static void PrintDegree(double x, FuzzySet fs)
        {
            Console.WriteLine("Degree at value " + x + " : " + fs.DegreeAtValue(x));
        }

        static void PrintDegrees(FuzzySet fs)
        {
            PrintDegree(-2, fs);
            PrintDegree(0, fs);
            PrintDegree(25, fs);
            PrintDegree(30, fs);
            PrintDegree(48, fs);
            PrintDegree(50, fs);
            PrintDegree(60, fs);
            PrintDegree(75, fs);
            PrintDegree(100, fs);
            PrintDegree(120, fs);
        }
    }
}
