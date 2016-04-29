using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class FactsBase
{
	protected List<IFact> facts;
	public List<IFact> Facts
	{
		get
		{
			return facts;
		}
	}

	public FactsBase()
	{
		facts = new List<IFact>();
	}
}