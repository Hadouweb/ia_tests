using System;

internal static class FactFactory
{
	internal static IFact Fact(IFact f, Motor m)
	{
		IFact newFact;
		if (f.GetType().Equals(typeof(IntFact)))
		{
			// C'est un fait à valeur entière
			int value = m.AskIntValue(f.Question());
			newFact = new IntFact(f.Name(), value, null, 0);
		}
		else
		{
			// C'est un fait à valeur booléenne
			bool value = m.AskBoolValue(f.Question());
			newFact = new BoolFact(f.Name(), value, null, 0);
		}
		return newFact;
	}

	internal static IFact Fact(string factStr)
	{
		factStr = factStr.Trim();
		if (factStr.Contains("="))
		{
			// Il y a un symbole '=' donc c'est un IntFact
			// on sépare le nom de la valeur
			String[] nameValue = factStr.Split(new String[] {
				"=", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
			if (nameValue.Length >= 2)
			{
				String question = null;
				if (nameValue.Length == 3)
				{
					// On peut le demander, donc on récupère
					// la question liée
					question = nameValue[2].Trim();
				}
				return new IntFact(nameValue[0].Trim(), 
					int.Parse(nameValue[1].Trim()), question);
			}
			else
			{
				// Syntaxe incorrecte;
				return null;
			}
		}
		else
		{
			// Pas d'égalité, c'est un fait de class BoolFact
			bool value = true;
			if (factStr.StartsWith("!"))
			{
				value = false;
				factStr = factStr.Substring(1).Trim();
				// On enlève le ! du nom
			}
			String[] nameQuestion = factStr.Split(new String[] {
				"(", ")" }, StringSplitOptions.RemoveEmptyEntries);
			String question = null;
			if (nameQuestion.Length == 2)
			{
				// ON récupère la question si on peut
				question = nameQuestion[1].Trim();
			}
			return new BoolFact(nameQuestion[0].Trim(), value, question);
		}
	}

}