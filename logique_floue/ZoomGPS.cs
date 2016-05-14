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
		system.addInputVariable(distance);s
	}
}