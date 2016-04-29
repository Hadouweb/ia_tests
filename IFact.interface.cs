using System;

public interface IFact
{
	String Name();
	Object Value();
	int Level();
	String Question();

	void SetLevel(int p);
}