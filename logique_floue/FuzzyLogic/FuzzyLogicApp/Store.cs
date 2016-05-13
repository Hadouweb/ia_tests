using FuzzyLogicPCL;
using FuzzyLogicPCL.FuzzySets;
using System;

namespace FuzzyLogicApp
{
    class Store
    {
        static void Main(string[] args)
        {
            // Création du système
            WriteLine("Gestion du store", true);
            FuzzySystem system = new FuzzySystem("Gestion du store");

            WriteLine("1) Ajout des variables", true);

            // Ajout de la variable linguistique "Température"
            WriteLine("Ajout de la variable Température");
            LinguisticVariable temp = new LinguisticVariable("Temperature", 0, 35);
            temp.AddValue(new LinguisticValue("Froid", new LeftFuzzySet(0, 35, 10, 12)));
            temp.AddValue(new LinguisticValue("Frais", new TrapezoidalFuzzySet(0, 35, 10, 12, 15, 17)));
            temp.AddValue(new LinguisticValue("Bon", new TrapezoidalFuzzySet(0, 35, 15, 17, 20, 25)));
            temp.AddValue(new LinguisticValue("Chaud", new RightFuzzySet(0, 35, 20, 25)));
            system.addInputVariable(temp);

            // Ajout de la variable linguistique "Eclairage" 
            WriteLine("Ajout de la variable Eclairage");
            LinguisticVariable eclair = new LinguisticVariable("Eclairage", 0, 125000);
            eclair.AddValue(new LinguisticValue("Sombre", new LeftFuzzySet(0, 125000, 20000, 30000)));
            eclair.AddValue(new LinguisticValue("Moyen", new TrapezoidalFuzzySet(0, 125000, 20000, 30000, 65000, 85000)));
            eclair.AddValue(new LinguisticValue("Fort", new RightFuzzySet(0, 125000, 65000, 85000)));
            system.addInputVariable(eclair);

            // Ajout de la variable linguistique "Store"
            WriteLine("Ajout de la variable Hauteur de Store");
            LinguisticVariable store = new LinguisticVariable("Store", 0, 115);
            store.AddValue(new LinguisticValue("Ferme", new LeftFuzzySet(0, 115, 25, 40)));
            store.AddValue(new LinguisticValue("MiHauteur", new TrapezoidalFuzzySet(0, 115, 25, 40, 85, 100)));
            store.AddValue(new LinguisticValue("Remonte", new RightFuzzySet(0, 115, 85, 100)));
            system.addOutputVariable(store);

            WriteLine("2) Ajout des règles", true);

            // Création des règles 
            system.addFuzzyRule("IF Eclairage IS Sombre THEN Store IS Remonte");
            system.addFuzzyRule("IF Eclairage IS Moyen AND Temperature IS Froid THEN Store IS Remonte");
            system.addFuzzyRule("IF Eclairage IS Moyen AND Temperature IS Frais THEN Store IS Remonte");
            system.addFuzzyRule("IF Eclairage IS Moyen AND Temperature IS Bon THEN Store IS MiHauteur");
            system.addFuzzyRule("IF Eclairage IS Moyen AND Temperature IS Chaud THEN Store IS MiHauteur");
            system.addFuzzyRule("IF Eclairage IS Fort AND Temperature IS Froid THEN Store IS Remonte");
            system.addFuzzyRule("IF Eclairage IS Fort AND Temperature IS Frais THEN Store IS MiHauteur");
            system.addFuzzyRule("IF Eclairage IS Fort AND Temperature IS Bon THEN Store IS Ferme");
            system.addFuzzyRule("IF Eclairage IS Fort AND Temperature IS Chaud THEN Store IS Ferme");
            WriteLine("9 règles ajoutées \n");

            WriteLine("3) Résolution de cas pratiques", true);
            // Cas pratique 1 : température de 21°, éclairage de 80000 lux
            WriteLine("Cas 1 :", true);
            WriteLine("T = 21 (80% bon, 20% chaud)");
            WriteLine("E = 80 000 (25% moyen, 75% fort)");
            system.SetInputVariable(temp, 21);
            system.SetInputVariable(eclair, 80000);
            WriteLine("Attendu : store plutôt fermé");
            WriteLine("Résultat : " + system.Solve() + "\n");

            while (true) ;
        }

        /// <summary>
        /// Aide pour l'écriture de messages en console (et ajouts d'*)
        /// </summary>
        /// <param name="msg">Message à afficher</param>
        /// <param name="stars">Besoin d'astérisques ?</param>
        private static void WriteLine(string msg, bool stars = false)
        {
            if (stars)
            {
                msg = "*** " + msg + " ";
                while (msg.Length < 45)
                {
                    msg += "*";
                }
            }
            Console.WriteLine(msg);
        }
    }
}
