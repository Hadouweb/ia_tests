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

}