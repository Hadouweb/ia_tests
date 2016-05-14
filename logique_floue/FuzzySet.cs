using System;
using System.Collections.Generic;
using System.Linq;

public class FuzzySet
{
	protected List<Point2D> Points;
	protected double Min { get; set; }
	protected double Max { get; set; }

	public FuzzySet(double min, double max)
	{
		this.Points = new List<Point2D>();
		this.Min = min;
		this.Max = max;
	}

    public void Add(Point2D pt)
    {
        Points.Add(pt);
        Points.Sort();
    }

    public void Add(double x, double y)
    {
        Point2D pt = new Point2D(x, y);
        Add(pt);
    }

    public override String ToString()
    {
        String result = "[" + Min + "-" + Max + "]:";
        foreach (Point2D pt in Points)
        {
            result += pt.ToString(); //"(" + pt.X + ";" + pt.Y + ")";
        }
        return result;
    }

    public double DegreeAtValue(double XValue)
    {
        // Check value in range (else, degree is null)
        if (XValue < Min || XValue > Max)
        {
            return 0;
        }

        // Compute value from interpolation : we locate the before and after point
        Point2D before = Points.LastOrDefault(pt => pt.X <= XValue);
        Point2D after = Points.FirstOrDefault(pt => pt.X >= XValue);
        if (before.Equals(after))
        {
            // x is the coordinate of a defined point
            return before.Y;
        }
        else
        {
            // x is between two points, we compute the interpolated value
            return (((before.Y - after.Y) * (after.X - XValue) / (after.X - before.X)) + after.Y);
        }
    }

	private static FuzzySet Merge(FuzzySet fs1, FuzzySet fs2, Func<double, double, double> MergeFt)
    {
        // New Fuzzy set
        FuzzySet result = new FuzzySet(Math.Min(fs1.Min, fs2.Min), Math.Max(fs1.Max, fs2.Max));

        // Creation of iterators on lists + initialization
        List<Point2D>.Enumerator enum1 = fs1.Points.GetEnumerator();
        List<Point2D>.Enumerator enum2 = fs2.Points.GetEnumerator();
        enum1.MoveNext();
        enum2.MoveNext();
        Point2D oldPt1 = enum1.Current;

        // Relative positions of fuzzy sets (to know when they intersect)
        int relativePosition = 0;
        int newRelativePosition = Math.Sign(enum1.Current.Y - enum2.Current.Y);

        // Loop while there are points in the two collections
        Boolean endOfList1 = false;
        Boolean endOfList2 = false;
        while (!endOfList1 && !endOfList2)
        {
            // New x values
            double x1 = enum1.Current.X;
            double x2 = enum2.Current.X;
            // New current position
            relativePosition = newRelativePosition;
            newRelativePosition = Math.Sign(enum1.Current.Y - enum2.Current.Y);

            if (relativePosition != newRelativePosition && relativePosition != 0 && newRelativePosition != 0)
            {
                // Positions have changed, so we have to compute the intersection and add it
                // Compute the points coordinates
                double x = (x1 == x2 ? oldPt1.X : Math.Min(x1, x2));
                double xPrime = Math.Max(x1, x2);
                // Intersection
                double slope1 = (fs1.DegreeAtValue(xPrime) - fs1.DegreeAtValue(x)) / (xPrime - x);
                double slope2 = (fs2.DegreeAtValue(xPrime) - fs2.DegreeAtValue(x)) / (xPrime - x);
                double delta = (fs2.DegreeAtValue(x) - fs1.DegreeAtValue(x)) / (slope1 - slope2);
                // Add point
                result.Add(x + delta, fs1.DegreeAtValue(x + delta));
                // Go on
                if (x1 < x2)
                {
                    oldPt1 = enum1.Current;
                    endOfList1 = !(enum1.MoveNext());
                }
                else if (x1 > x2)
                {
                    endOfList2 = !(enum2.MoveNext());
                }
            }
            else if (x1 == x2)
            {
                // The two points are on the same X, so we take the good value (eg min or max)
                result.Add(x1, MergeFt(enum1.Current.Y, enum2.Current.Y));
                oldPt1 = enum1.Current;
                endOfList1 = !(enum1.MoveNext());
                endOfList2 = !(enum2.MoveNext());
            }
            else if (x1 < x2)
            {
                // Fs1 point is first, we add the value (eg min or max) between the enum1 point and the degree for fs2
                result.Add(x1, MergeFt(enum1.Current.Y, fs2.DegreeAtValue(x1)));
                oldPt1 = enum1.Current;
                endOfList1 = !(enum1.MoveNext());
            }
            else
            {
                // This time, it's fs2 first
                result.Add(x2, MergeFt(fs1.DegreeAtValue(x2), enum2.Current.Y));
                endOfList2 = !(enum2.MoveNext());
            }
        }

        // Add end points
        if (!endOfList1)
        {
            while (!endOfList1)
            {
                result.Add(enum1.Current.X, MergeFt(0, enum1.Current.Y));
                endOfList1 = !enum1.MoveNext();
            }
        }
        else if (!endOfList2)
        {
            while (!endOfList2)
            {
                result.Add(enum2.Current.X, MergeFt(0, enum2.Current.Y));
                endOfList2 = !enum2.MoveNext();
            }
        }

        return result;
    }

	public double Centroid()
    {
        // Less than two points : no area, so no centroid
        if (Points.Count < 2)
        {
            return 0;
        }
        else
        {
            // We compute the total area, and the ponderated one
            double ponderatedArea = 0;
            double totalArea = 0;
            double localArea;
            Point2D oldPt = null;
            foreach (Point2D newPt in Points)
            {
                if (oldPt != null)
                {
                    // Centroids computation
                    if (oldPt.Y == newPt.Y)
                    {
                        // For a rectangle : at the center value
                        localArea = oldPt.Y * (newPt.X - oldPt.X);
                        totalArea += localArea;
                        ponderatedArea += ((newPt.X - oldPt.X) / 2 + oldPt.X) * localArea;
                    }
                    else
                    {
                        // We have two geometric shapes : a rectangle (at half) and a triangle (at 1/3 or 2/3 depending on the slope)
                        // For the rectangle
                        localArea = Math.Min(oldPt.Y, newPt.Y) * (newPt.X - oldPt.X);
                        totalArea += localArea;
                        ponderatedArea += ((newPt.X - oldPt.X) / 2 + oldPt.X) * localArea;
                        // For the triangle
                        localArea = (newPt.X - oldPt.X) * (Math.Abs(newPt.Y - oldPt.Y)) / 2;
                        totalArea += localArea;
                        if (newPt.Y > oldPt.Y)
                        {
                            ponderatedArea += (2.0 / 3.0 * (newPt.X - oldPt.X) + oldPt.X) * localArea;
                        }
                        else
                        {
                            ponderatedArea += (1.0 / 3.0 * (newPt.X - oldPt.X) + oldPt.X) * localArea;
                        }
                    }
                }
                oldPt = newPt;
            }
            // Return the centroid, that is the division of the two areas
            return ponderatedArea / totalArea;
        }
    }

    public static Boolean operator == (FuzzySet fs1, FuzzySet fs2)
    {
        return fs1.ToString().Equals(fs2.ToString());
    }

    public static Boolean operator != (FuzzySet fs1, FuzzySet fs2)
    {
        return !(fs1 == fs2);
    }

    public static FuzzySet operator *(FuzzySet fs, double value)
    {
        FuzzySet result = new FuzzySet(fs.Min, fs.Max);
        foreach (Point2D pt in fs.Points)
        {
            result.Add(new Point2D(pt.X, pt.Y * value));
        }
        return result;
    }

    public static FuzzySet operator !(FuzzySet fs) {
        FuzzySet result = new FuzzySet(fs.Min, fs.Max);
        foreach (Point2D pt in fs.Points)
        {
            result.Add(new Point2D(pt.X, 1 - pt.Y));
        }
        return result;
    }

    public static FuzzySet operator &(FuzzySet fs1, FuzzySet fs2)
    {
        return Merge(fs1, fs2, Math.Min);
    }

    public static FuzzySet operator |(FuzzySet fs1, FuzzySet fs2)
    {
        return Merge(fs1, fs2, Math.Max);
    }
}