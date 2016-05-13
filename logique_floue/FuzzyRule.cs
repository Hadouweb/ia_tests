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
				if (premise.Lv == pv.Lv)
				{
					val = premise.Lv.LinguisticValueByName(premise.LinguisticValueName);
					if (val != null)
					{
						localDegree = val.DegreeAtValue(pv.Value);
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
}