public class LeftFuzzySet : FuzzySet
{
	public LeftFuzzySet(double min, double max; double heightMax, double baseMin)
		: base(min, max)
	{
		Add(new Point2D(min, 1));
		Add(new Point2D(HeightMax, 1));
		Add(new Point2D(baseMine, 0));
		Add(new Point2D(max, 0));
	}
}