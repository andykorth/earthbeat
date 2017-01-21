/*
	Colorx.cs
	© Fri Jul  7 20:17:52 CDT 2006 Graveck Interactive
	by Jonathan Czeck
*/
using UnityEngine;
using System.Collections;
using System;
using System.Globalization;
using System.Collections.Generic;

public class Colorx
{
	
	
	public enum PalleteMode  { normal, hair, skin };
	
// BY ANDY KORTH
	public static Color GetColorHex (string hex)
	{
		float r = (Int32.Parse (hex.Substring (0, 2), NumberStyles.HexNumber) / 256f);
		float g = (Int32.Parse (hex.Substring (2, 2), NumberStyles.HexNumber) / 256f);
		float b = (Int32.Parse (hex.Substring (4, 2), NumberStyles.HexNumber) / 256f);
	
		return new Color (r, g, b);
	}
	
	public static String ToHex(Color c){
		int r = (int)(c.r * 255);
		int g = (int)(c.g * 255);
		int b = (int)(c.b * 255);
		
	   return r.ToString("X2")+g.ToString("X2")+b.ToString("X2");
	}
	
	
	
	
	public static List<Color> GetColors(PalleteMode mode){
		
		//float d = 0.33f;

		List<Color> colors = new List<Color> ();
		
		if(mode == PalleteMode.normal){
			
			colors.Add(Colorx.GetColorHex("f40b0b"));
			colors.Add(Colorx.GetColorHex("fedee9"));
			colors.Add(Colorx.GetColorHex("9e23b0"));
			colors.Add(Colorx.GetColorHex("1b2e9f"));
			
			colors.Add(Colorx.GetColorHex("b2e4fd"));
			colors.Add(Colorx.GetColorHex("436e11"));
			colors.Add(Colorx.GetColorHex("cbf972"));
			colors.Add(Colorx.GetColorHex("fefda3"));
						
			colors.Add(Colorx.GetColorHex("353535"));
			colors.Add(Colorx.GetColorHex("ffffff"));
			colors.Add(Colorx.GetColorHex("f57b0e"));
			colors.Add(Colorx.GetColorHex("b3b3b3"));
						
			colors.Add(Colorx.GetColorHex("825923"));
			colors.Add(Colorx.GetColorHex("cbbb8b"));
			colors.Add(Colorx.GetColorHex("920101"));
			colors.Add(Colorx.GetColorHex("0faa8b"));
			
//			for (float i = 0f; i <= 1f; i += d) {
//				for (float j = 0f; j <= 1f; j += d) {
//					for (float k = 0f; k <= 1f; k += d) {
//						colors.Add (new Color (i, j, k));
//					}
//				}
//			}

		}else if(mode == PalleteMode.skin){// || mode ==  PalleteMode.hair){
			// new skin colors:
			colors.Add(Colorx.ColorInt(240, 211, 185));
			colors.Add(Colorx.ColorInt(215, 190, 153));
			colors.Add(Colorx.ColorInt(190, 167, 135));
			colors.Add(Colorx.ColorInt(168, 139, 108));
			colors.Add(Colorx.ColorInt(132, 112, 89));
			colors.Add(Colorx.ColorInt(107, 91, 69));
			
			
			// Andy's old skin colors:
			//			colors.Add(Colorx.GetColorHex("fecab2"));
//			colors.Add(Colorx.GetColorHex("f9cfb7"));
//			colors.Add(Colorx.GetColorHex("c5ad95"));
//			colors.Add(Colorx.GetColorHex("fddcd5"));
//			colors.Add(Colorx.GetColorHex("f8edeb"));
//			colors.Add(Colorx.GetColorHex("dcc29f"));
//			
//			colors.Add(Colorx.GetColorHex("daa07a"));
//			colors.Add(Colorx.GetColorHex("a66859"));
//			colors.Add(Colorx.GetColorHex("836a56"));
//			colors.Add(Colorx.GetColorHex("fcdabf"));
//			colors.Add(Colorx.GetColorHex("f0bc97"));
//			colors.Add(Colorx.GetColorHex("c3886a"));
//
//			colors.Add(Colorx.GetColorHex("774f35"));
//			colors.Add(Colorx.GetColorHex("583b37"));
//			colors.Add(Colorx.GetColorHex("524944"));
//			colors.Add(Colorx.GetColorHex("cc9667"));
//			colors.Add(Colorx.GetColorHex("cc9667"));
//			colors.Add(Colorx.GetColorHex("965b39"));
//		
		}else if(mode == PalleteMode.hair){
						
			colors.Add(Colorx.ColorInt(224, 217, 160));
			colors.Add(Colorx.ColorInt(145, 115, 071));
			colors.Add(Colorx.ColorInt(90, 73, 44));
			colors.Add(Colorx.ColorInt(213, 126, 74));
			colors.Add(Colorx.ColorInt(187, 89, 22));
			colors.Add(Colorx.ColorInt(53, 43, 32));
		
		}
		return colors;
	}
	
	public static Color ColorInt(int r, int g, int b){
		return new Color(r/255f, g/255f, b/255f);	
	}

	public static Color Slerp (Color a, Color b, float t)
	{
		return (HSBColor.Lerp (HSBColor.FromColor (a), HSBColor.FromColor (b), t)).ToColor ();
	}
}
