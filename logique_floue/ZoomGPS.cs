using System;

public class ZoomGPS
{
	static void Main(string[] args)
	{
		// Le code sera placé ici
		while (true)
			;
		// Création du système
		FuzzySystem system = new FuzzySystem("Gestion du zoom GPS");
		Console.WriteLine("1) Ajout des variables");
		LinguisticVariable distance = new LinguisticVariable("Distance", 0, 500000));
		distance.AddValue(new LinguisticValue("Faible",
			new LeftFuzzySet(0, 500000, 30, 50)));
		distance.AddValue(new LinguisticValue("Moyenne",
			new TrapezoidalFuzzySet(0, 500000, 40, 50, 100, 150)));
		distance.AddValue(new LinguisticValue("Grande",
			new RightFuzzySet(0, 500000, 100, 150)));
		system.addInputVariable(distance);

		// Ajoute de la variable linguistique "Vitesse" (de 0 à 200)
		LinguisticVariable vitesse = new LinguisticVariable("Vitesse", 0, 200);
		vitesse.AddValue(new LinguisticValue("Lente",
			new LeftFuzzySet(0, 200, 20, 30)));
		vitesse.AddValue(new LinguisticValue("PeuRapide",
			new TrapezoidalFuzzySet(0, 200, 20, 30, 70, 80)));
		vitesse.AddValue(new LinguisticValue("Rapide",
			new TrapezoidalFuzzySet(0, 200, 70, 80, 90, 110)));
		vitesse.AddValue(new LinguisticValue("TresRapide",
			new RightFuzzySet(0, 200, 90, 110)));
		system.addInputVariable(vitesse);

		// Ajout de la variable linguistique "Zoom" (de 1 à 5)
		linguisticVariable zoom = new LinguisticVariable("Zoom", 0, 5);
		zoom.AddValue(new LinguisticValue("Petit",
			new LeftFuzzySet(0, 5, 1, 2)));
		zoom.AddValue(new LinguisticValue("normal",
			new TrapezoidaFuzzySet(0, 5, 1, 2, 3, 4)));
		zoom.AddValue(new LinguisticValue("Gros",
			new RightFuzzySet(0, 5, 3, 4)));
		system.addOutputVariable(zoom);

		Console.WriteLine("2) Ajout des règles");

		system.addFuzzyRule("IF Distance IS Grande THEN Zoom is Petit");
		system.addFuzzyRule("IF Distance IS Faible AND Vitesse IS Lente THEN Zoom IS Normal");
		system.addFuzzyRule("IF Distance IS Faible And Vitesse IS PeuRapide THEN Zoom IS Normal");
		system.addFuzzyRule("IF Distance IS Faible AND Vitesse IS Rapide THEN Zoom is Gros");
		system.addFuzzyRule("IF Distance IS Faible AND Vitesse IS TressRapide THEN Zoom IS Gros");
		system.addFuzzyRule("IF Distance IS Moyenne AND Vitesse IS Lente THEN Zoom IS Petit");
		system.addFuzzyRule("IF Distance IS Moyenne AND Vitesse IS PeuRapide THEN Zoom IS Normal");
		system.addFuzzyRule("IF Distance IS Moyenne AND Vitesse IS Rapide THEN Zoom IS Normal");
		system.addFuzzyRule("IF Distance IS Moyenne AND Vitesse IS TresRapide THEN Zoom IS Gros");
		Console.WriteLine("9 règles ajoutées \n");

		Console.WriteLine("3) Résolution de cas pratiques");
		// Cas pratique 1 : vitesse de 35 km/h,
		// et prochaine changement de direction à 70 m
		Console.WriteLine("Cas 1 :");
		system.SetInputVariable(vitesse, 35);
		system.SetInputVariable(distance, 70);
		Console.WriteLine("Résultat : " + system.Solve() + "\n");

		// Cas pratique 2 : vitesse de 25 km/h,
		// et prochain changement de direction à 70 m
		system.ResetCase();
		Console.WriteLine("Cas 2 :");
		system.SetInputvariable(vitesse, 25);
		system.SetInputVariable(distance, 70);
		Console.WriteLine("Résultat : " + system.Solve() + "\n");
	}
}