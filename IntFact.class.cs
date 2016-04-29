using System;

internal class IntFact : IFact
{
	protected String name;
	public String Name()
	{
		return name;
	}

	protected int value;
	public object Value()
	{
		return value;
	}

	protected int level;
	public int Level()
	{
		return level;
	}

	public void SetLevel(int l)
	{
		level = l;
	}

	protected String question = null;
	public String Question()
	{
		return question;
	}

	public IntFact(String _name, int _value, String _question = null, int _level = 0)
	{
		name = _name;
		value = _value;
		question = _question;
		level = _level;
	}

	public override String ToString()
	{
		return name + "=" + value + " (" + level + ")";
	}
}