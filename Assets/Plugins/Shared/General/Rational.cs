using UnityEngine;
using System;

public struct Rational
{
	public int n, d;

	public static Rational zero = new Rational(0, 1);

	public float f {
		get {
			return n / (float)d;
		}
	}
	public int i {
		get {
			return n / d;
		}
	}
	public Rational(int numerator, int denominator){
		n = numerator;
		d = denominator;
		Reduce ();
	}


	public void Reduce(){
		int l = gcf (n, d);
		n /= l;
		d /= l;
	}

	// thanks Euclid!
	public static int gcf(int a, int b)
	{
		while (b != 0)
		{
			int temp = b;
			b = a % b;
			a = temp;
		}
		return a;
	}

	static int lcm(int a, int b)
	{
		return (a / gcf(a, b)) * b;
	}
	/*
	public static implicit operator float(Rational r)  // implicit digit to byte conversion operator
	{
		return (r.n / (float)r.d);
	}
	*/
	
	private void SetDenom(int nd){
		if (nd % d != 0) {
			Debug.LogError ("Screwed up rational number");
		}
		int mult = nd / d;
		d = nd;
		n *= mult;
	}

	public bool isWhole(){
		Reduce ();
		return this.d == 1;
	}

	public static Rational operator +(Rational c1, Rational c2) 
	{
		int newDenom = lcm (c1.d, c2.d);
		int n1 = c1.n * newDenom / c1.d;
		int n2 = c2.n * newDenom / c2.d;
		Rational r = new Rational (n1 + n2, newDenom);
		r.Reduce ();
		return r;
	}

	public override bool Equals (object obj)
	{
		if (obj is Rational) {
			Rational r = (Rational) obj;
			return this.n == r.n && this.d == r.d;
		}
		return false;
	}

	public override int GetHashCode ()
	{
		// Analysis disable NonReadonlyReferencedInGetHashCode
		return n * 991 + d * 997;
		// Analysis restore NonReadonlyReferencedInGetHashCode
	}

	public static bool operator ==(Rational c1, Rational c2) 
	{
		return c1.n == c2.n && c1.d == c2.d;
	}

	public static bool operator !=(Rational c1, Rational c2) 
	{
		return c1.n != c2.n || c1.d != c2.d;
	}

	/// <summary>
	///  Divide by integer, discard remainder.
	/// </summary>
	public int DivideWhole (int divisor)
	{
		return (int) ((this.f) / divisor);
	}

	public override string ToString(){
		if (isWhole ()) {
			return "" + n;
		} else {
			return n + "/" + d;
		}
	}

}

