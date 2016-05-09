using System;
using System.Collections.Generic;

public interface HumanInterface
{
	int AskIntValue(String question);
	bool AskBoolValue(String question);
	void PrintFacts(List<IFact> facts);
	void PrintRules(List<Rule> rules);
}