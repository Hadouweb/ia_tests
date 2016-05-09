using System;
using System.Collections.Generic;

public class Motor
{
	private FactsBase fDB;
	private RulesBase rDB;
	private HumanInterface ihm;

	public Motor(HumanInterface _ihm)
	{
		ihm = _ihm;
		fDB = new FactsBase();
		rDB = new RulesBase();
	}

	internal int AskIntValue(string p)
	{
		return ihm.AskIntValue(p);
	}

	internal bool AskBoolValue(string p)
	{
		return ihm.AskBoolValue(p);
	}

	private int CanApply(Rule r)
	{
		int maxlevel = -1;
		// On vérifie si chaque prémises est vraie
		foreach (IFact f in r.Premises)
		{
			IFact foundFact = fDB.Search(f.Name());
			if (foundFact == null)
			{
				// Ce fait n'existe pas dans la base actuellement
				if (f.Question() != null)
				{
					// On le demande à l'utilisateur
					// et on l'ajoute en base
					foundFact = FactFactory.Fact(f, this);
					maxlevel = Math.Max(maxlevel, 0);
				}
				else
				{
					// On sait que la régle ne s'applique pas
					return -1;
				}
			}

			// On a un fait en base, on vérfie sa valeur
			if (!foundFact.Value().Equals(f.Value()))
			{
				// Elle ne correspond pas
				return -1;
			}
			else
			{
				// Elle correspond
				maxlevel = Math.Max(maxlevel, foundFact.Level());
			}
		}

		return maxlevel;
	}

	private Tuple<Rule, int> FindUsableRule(RulesBase rBase)
	{
		foreach (Rule r in rBase.Rules)
		{
			int level = CanApply(r);
			if (level != -1)
			{
				return Tuple.Create(r, level);
			}
		}
		return null;
	}

	public void Solve()
	{
		// On fait une copie des règle existantes
		// + création d'une base de faits vierge
		bool moreRules = true;
		RulesBase usableRules = new RulesBase();
		usableRules.Rules = new List<Rule>(rDB.Rules);
		fDB.Clear();

		// Tant qu'il existe des règles à appliquer
		while (moreRules)
		{
			// Cherche une règle à appeler
			Tuple<Rule, int> t = FindUsableRule(usableRules);

			if (t != null)
			{
				// Applique la règle et ajoute le nouveau fait
				// à la base
				IFact newFact = t.Item1.Conclusion;
				newFact.SetLevel(t.Item2 + 1);
				fDB.AddFact(newFact);

				// Enlève la règle des règles applicables
				usableRules.Remove(t.Item1);
			}
			else
			{
				// PLus de règles possible : on s'arrête
				moreRules = false;
			}
		}

		// Ecriture du résultat
		ihm.PrintFacts(fDB.Facts);
	}

	public void AddRule(string ruleStr)
	{
		// Séparation nom : règle
		String[] splitName = ruleStr.Split(new String[] {" : "},
			StringSplitOptions.RemoveEmptyEntries);
		if (splitName.Length == 2)
		{
			String name = splitName[0];
			// Séparation premisses THEN conclusion
			String[] splitPremConcl = splitName[1].Split(new String[] 
				{ "IF ", ", ", " THEN " }, StringSplitOptions.RemoveEmptyEntries);
			if (splitPremConcl.Length == 2)
			{
				// Lecture des premisses
				List<IFact> premises = new List<IFact>();
				String[] premisesStr = splitPremConcl[0].Split(new String[] {" AND "}, 
					StringSplitOptions.RemoveEmptyEntries);
				foreach (String prem in premisesStr)
				{
					IFact premise = FactFactory.Fact(prem);
					premises.Add(premise);
				}

				// Lecture de la conclusion
				String conclusionStr = splitPremConcl[1].Trim();
				IFact conclusion = FactFactory.Fact(conclusionStr);

				// Création de la règle et ajout
				rDB.AddRule(new Rule(name, premises, conclusion));
			}
		}
	}
}