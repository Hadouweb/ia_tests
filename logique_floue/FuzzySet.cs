using System;
using System.Collections.Generic;
using System.Linq;

public class FuzzySet
{

	private static FuzzySet Merge(FuzzySet fs1, FuzzySet fs2,
		Func<double, double, double> MergeFt)
	{
		// On crée un nouvel ensemble flou
		FuzzySet result = new FuzzySet(Math.Min(fs1.Min, fs2.Min),
			Math.Max(fs1.Max, fs2.Max));

		// On va parcourir les listes via les enumérateurs
		List<Point2D>.Enumerator enum1 = fs1.Points.GetEnumerator();
		List<Point2D>.Enumerator enum2 = fs2.Points.GetEnumerator();
		enum1.MoveNext();
		enum2.MoveNext();
		Point2D oldPt1 = enume1.Current;

		// On calcule la position relative des deux courbes
		int relativePosition = 0;
		int newRelativePosition = Math.Sign(enum1.Current.Y - enum2.Current.Y);

		// On boucle tant qu'il y a des points dans les collections
		Boolean endOfList1 = false;
		Boolean endOfList2 = false;
		while (!endOfList1 && !endOfList2)
		{
			// On récupère les valeurs x des points en cours
			double x1 = enum1.Current.X;
			double x2 = enum2.Current.X;

			// Calcul des position relatives
			relativePosition = newRelativePosition;
			newRelativePosition = Math.Sign(enum1.Current.Y - enum2.Current.Y);

			if (relativePosition != newRelativePosition && relativePosition != 0 &&
				newRelativePosition != 0)
			{
				// Les positions ont changé :
				// on doit trouver l'interserction
				// On calcule les coordonnées des points extrêmes
				double x = (x1 == x2 ? oldPt1.X : Math.Min(x1, x2));
				double xPrime = Math.Max(x1, x2);

				// Calcul des pentes puis du delta
				double slope1 = (fs1.DegreeAtValue(xPrime) - fs1.DegreeAtValue(x)) / (xPrime - x);
				double slope2 = (fs2.DegreeAtValue(xPrime) - fs2.DegreeAtValue(x)) / (xPrime - x);
				double delta = (fs2.DegreeAtValue(x) - fs1.DegreeAtValue(x)) / (slope1 - slope2);

				// On ajoute le point d'intersection
				result.Add(x + delta, fs1.DegreeAtValue(x + delta));

				// Et on pass aux points suivants
				if (x1 < x2)
				{
					oldPt1 = enum1.Current;
					endofList1 = !(enum1.MoveNext());
				}
				else if (x1 > x2)
				{
					endOfList2 = !(enum2.MoveNext());
				}
			}
			else if (x1 == x2)
			{
				// Les deux points sont au même x, on garde le bon
				result.Add(x1, MergeFt(enum1.Current.Y, enum2.Current.Y));
				oldPt1 = enum1.Current;
				endOfList1 = !(enum1.MoveNext());
				endOfList2 = !(enum2.MoveNext());
			}
			else if (x1 < x2)
			{
				// La courbe 1 a un point avant, on calcule le degré pour
				// la deuxième courbe et on garde la bonne value
				result.Add(x1, MergeFt(enum1.Current.Y, fs2.DegreeAtValue(x1)));
				oldPt1 = enum1.Current;
				endOfList1 = !(enum1.MoveNext());
			}
			else
			{
				// Ce coup-ci, c'est la courbe2
				result.Add(x2, MergeFt(fs1.DegreeAtValue(x2), enum2.Current.Y));
				endOfList2 = !(enum2.MoveNext());
			}
		}
		// Une des deux listes est finie, on ajoute les points restants
		if (!endOfList1)
		{
			while (!endOfList1)
			{
				result.Add(enum1.Current.X, MergeFt(0, enum1.Current.Y));
				endOfList1 = !(enum1.MoveNext());
			}
		}
		else if (!endOfList2)
		{
			while (!endOfList2)
			{
				result.Add(enum2.Current.X, MergeFt(0, enum2.Current.Y));
				endoOfList2 = !(enum2.MoveNext());
			}
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