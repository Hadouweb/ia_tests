using System;
using System.Collections.Generic;
using System.Linq;

public class Program : HumanInterface
{

	static void Main(string[] args)
	{
		Console.WriteLine("ok");
		return;
	}

	public int AskIntValue(String p)
	{
		Console.Out.WriteLine(p);
		try 
		{
			return int.Parse(Console.In.ReadLine());
		}
		catch (Exception)
		{
			return 0;
		}
	}

	public bool AskBoolValue(String p)
	{
		Console.Out.WriteLine(p + " (yes, no)");
		String res = Console.In.ReadLine();
		if (res.Equals("yes"))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void PrintFacts(List<IFact> facts)
	{
		String res = "Solutions(s) trouvée(s) : \n";
		foreach (IFact f in facts.Where(x => x.Level() > 0).OrderByDescending(x => x.Level()))
		{
			res += f.ToString() + "\n";
		}
		Console.Out.Write(res);
	}

	public void PrintRules(List<Rule> rules)
	{
		String res = "";
		foreach (Rule r in rules)
		{
			res += r.ToString() + "\n";
		}
		Console.Out.Write(res);
	}
} 