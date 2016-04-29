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

			// On a un fait eb base, on vérfie sa valeur
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
}