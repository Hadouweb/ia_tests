using System;
using System.Collections.Generic;

public class FuzzySystem
{
	String Name { get; set; }
	List<LinguisticVariable> Inputs;
	LinguisticVariable Output;
	List<FuzzyRule> Rules;
	List<FuzzyValue> Problem;

	// Constructeur
	public FuzzySystem(String _name)
	{
		Name = _name;
		Inputs = new List<LinguisticVariable>();
		Rules = new List<FuzzyRule>();
		Problem = new List<FuzzyValue>();
	}

	// Ajout d'une variable linguistique en entrée
	public void addInputVariable(LinguisticVariable lv)
	{
		Input.Add(lv);
	}

	// Ajout d'une variable linguistique en sortie
	public void addOutputVariable(LinguisticVariable lv)
	{
		Output = lv;
	}

	// Ajout d'une règle
	public void AddFuzzyRule(FuzzyRule fuzzyRule)
	{
		Rules.Add(fuzzyRule);
	}

	// Ajout d'une valeur numérique en entrée
	public void SetInputVariable(LinguisticVariable inputVar, double value)
	{
		Problem.Add(new FuzzyValue(inputVar, value));
	}

	// Remise à zéro des valeurs en entrée pour changer de cas
	public void ResetCase()
	{
		Problem.Clear();
	}

	// retrouver une variable linguistique à partir de son nom
	internal LinguisticVariable LinguisticVariableByName(string name)
	{
		foreach (LinguisticVariable input in Inputs)
		{
			if (input.Name.ToUpper().Equals(name))
			{
				return input;
			}
		}
		if (Output.Name.ToUpper().Equals(name))
		{
			return Output;
		}
		return null;
	}

	public double Solve()
	{
		// Initialisation du résultat
		FuzzySet res = new FuzzySet(Output.MinValue, Output.MaxValue);
		res.Add(Output.MinValue, 0);
		res.Add(Output.MaxValue, 0);

		// Application des règles et calcul
		// du fuzzy set résultant (union)
		foreach ( FuzzyRule rule in Rules)
		{
			res = res | rule.Apply(Problem);
		}

		// Défuzzification
		return res.Centroid();
	}

	public void AddFuzzyRule(string ruleStr)
	{
		FuzzyRule rule = new FuzzyRule(ruleStr, this);
		Rules.Add(rule);
	}
}