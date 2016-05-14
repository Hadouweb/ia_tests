using System;
using System.Collections.Generic;

public class FuzzyRule
{
	List<FuzzyExpression> Premises;
	FuzzyExpression Conclusion;

	public FuzzyRule(List<FuzzyExpression> _prem, FuzzyExpression _concl)
	{
		Premises = _prem;
		Conclusion = _concl;
	}

	internal FuzzySet Apply(List<FuzzyValue> Problem)
	{
		double degree = 1;
		foreach (FuzzyExpression premise in Premises)
		{
			double localDegree = 0;
			LinguisticValue val = null;
			foreach (FuzzyValue pb in Problem)
			{
				if (premise.Lv == pb.Lv)
				{
					val = premise.Lv.LinguisticValueByName(premise.LinguisticValueName);
					if (val != null)
					{
						localDegree = val.DegreeAtValue(pb.Value);
						break;
					}
				}
			}
			if (val == null)
			{
				return null;
			}
			degree = Math.Min(degree, localDegree);
		}
		return Conclusion.Lv.LinguisticValueByName(Conclusion.LinguisticValueName).Fs * degree;
	}

	public FuzzyRule(string ruleStr, FuzzySystem fuzzySystem)
	{
		// On met la chaîne en majuscules
		ruleStr = ruleStr.ToUpper();

		// On sépare les prémisses de la conclusion
		// par la présence du mot-clé THEN
		String[] rule = ruleStr.Split(new String[] {" THEN "},
			StringSplitOptions.RemoveEmptyEntries);
		if (rule.Length == 2)
		{
			// On a 2 parties, donc la synthaxe actuelle est exacte
			rule[0] = rule[0].Remove(0, 2); // ON enlève "IF" du début
			// On va maintenant séparer et traiter les prémisses (AND)
			String[] prem = rule[0].Trim().Split(new String[] {" AND "},
				StringSplitOptions.RemoveEmptyEntries);
			Premises = new List<FuzzyExpression>();
			foreach ( String exp in prem)
			{
				// On coupe chaque expression avec le mot-clé IS
				// et on crée la FuzzyExpression correspondante
				// qu'on ajoute aux prémisses
				String[] res = exp.Split(new Strint[] {" IS "},
					StringSplitOptions.RemoveEmptyEntries);
				if (res.Length == 2)
				{
					FuzzyExpression fexp = new FuzzyExpression(fuzzySystem.LinguisticVariableByName(res[0]), res[1]);
					Premises.Add(fexp);
				}
			}
			// On traite de la même façon la conclusion
			String[] conclu = rule[1].Split(new String[], {" IS "},
				StringSplitOptions.RemoveEmptyEntries);
			if (conclu.Length == 2)
			{
				Conclusion = new FuzzyExpression(fuzzySystem.LinguisticVariableByName(conclu[0]), conclu[1]);
			}
		}
	}
}