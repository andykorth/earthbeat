/*
Copyright (c) 2008, Rune Skovbo Johansen & Unity Technologies ApS

See the document "TERMS OF USE" included in the project folder for licencing details.

And Andy Korth
*/
using UnityEngine;
using System.Collections;
using System;

public class Util {

	public static string MD5(string strToEncrypt) {
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++) {
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}

	public static string[] Split(string s) {
		return s.Split(new char [] {'\n'});
	}	

	public static Vector3[] GetCornerPointsFromBounds(Bounds b) {
		Vector3[] boundsCorners = new Vector3[8];

		boundsCorners[0] = b.min;
		boundsCorners[1] = b.center + new Vector3(-b.extents.x, -b.extents.y, b.extents.z);
		boundsCorners[2] = b.center + new Vector3(-b.extents.x, b.extents.y, -b.extents.z);
		boundsCorners[3] = b.center + new Vector3(-b.extents.x, b.extents.y, b.extents.z);
		boundsCorners[4] = b.center + new Vector3(b.extents.x, -b.extents.y, -b.extents.z);
		boundsCorners[5] = b.center + new Vector3(b.extents.x, -b.extents.y, b.extents.z);
		boundsCorners[6] = b.center + new Vector3(b.extents.x, b.extents.y, -b.extents.z);
		boundsCorners[7] = b.max;

		return boundsCorners;
	}


	public static string toMinuteSeconds(float f){
		int min = (int) (f / 60);
		int sec = (int) (f % 60);
		return string.Format("{0:00}:", min) +string.Format("{0:00}", sec);
	}

	public static string toMinuteSubSeconds(float f){
		int min = (int) (f / 60);
		int sec = (int) (f % 60);
		int subsec = (int) (f*100 % 100);
		return string.Format("{0:00}:", min) +string.Format("{0:00}.", sec) + string.Format("{0:00}", subsec);
	}

	public static string GetTimeString(float t) {
		int minutes = (int)Mathf.Floor(t / 60.0f);
		int seconds = (int)Mathf.Floor(t - (minutes * 60.0f));
		string secondsString = seconds.ToString("f0");
		while (secondsString.Length < 2)
			secondsString = "0" + secondsString;
		string minutesString = minutes.ToString();
		while (minutesString.Length < 2)
			minutesString = "0" + minutesString;
		return minutesString + ":" + secondsString;
	}	

	public static int ParseInt(string value) {
		return Int32.Parse(value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
	}

	public static float ParseFloat(string value) {
		return Single.Parse(value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
	}

	public static double ParseDouble(string value) {
		return Double.Parse(value, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
	}


	public static GameObject Find(Transform root, string name){
		foreach(Transform child in root){
			GameObject result = Find(child, name);
			if(result){
				return result;
			}
			if(child.gameObject.name == name){
				return child.gameObject;
			}
		}
		return null;
	}

	public static void FatalError(string message){
		Debug.LogError(message);
		Debug.Break();
	}

	public static bool IsSaneNumber(float f) {
		if (System.Single.IsNaN(f)) return false;
		if (f==Mathf.Infinity) return false;
		if (f==Mathf.NegativeInfinity) return false;
		if (f>1000000000000) return false;
		if (f<-1000000000000) return false;
		return true;
	}
	
	public static Vector3 Clamp(Vector3 v, float length) {
		float l = v.magnitude;
		if (l > length) return v / l * length;
		return v;
	}
	
	public static float Mod(float x, float period) {
		float r = x % period;
		return (r>=0?r:r+period);
	}
	public static int Mod(int x, int period) {
		int r = x % period;
		return (r>=0?r:r+period);
	}
	public static float Mod(float x) { return Mod(x, 1); }
	public static int Mod(int x) { return Mod(x, 1); }
	
	public static float CyclicDiff(float high, float low, float period, bool skipWrap) {
		if (!skipWrap) {
			high = Mod(high,period);
			low = Mod(low,period);
		}
		return ( high>=low ? high-low : high+period-low );
	}
	public static int CyclicDiff(int high, int low, int period, bool skipWrap) {
		if (!skipWrap) {
			high = Mod(high,period);
			low = Mod(low,period);
		}
		return ( high>=low ? high-low : high+period-low );
	}
	public static float CyclicDiff(float high, float low, float period) { return CyclicDiff(high, low, period, false); }
	public static int CyclicDiff(int high, int low, int period) { return CyclicDiff(high, low, period, false); }
	public static float CyclicDiff(float high, float low) { return CyclicDiff(high, low, 1, false); }
	public static int CyclicDiff(int high, int low) { return CyclicDiff(high, low, 1, false); }
	
	// Returns true is compared is lower than comparedTo relative to reference,
	// which is assumed not to lie between compared and comparedTo.
	public static bool CyclicIsLower(float compared, float comparedTo, float reference, float period) {
		compared = Mod(compared,period);
		comparedTo = Mod(comparedTo,period);
		if (
			CyclicDiff(compared,reference,period,true)
			<
			CyclicDiff(comparedTo,reference,period,true)
		) return true;
		return false;
	}
	public static bool CyclicIsLower(int compared, int comparedTo, int reference, int period) {
		compared = Mod(compared,period);
		comparedTo = Mod(comparedTo,period);
		if (
			CyclicDiff(compared,reference,period,true)
			<
			CyclicDiff(comparedTo,reference,period,true)
		) return true;
		return false;
	}
	public static bool CyclicIsLower(float compared, float comparedTo, float reference) {
		return CyclicIsLower(compared, comparedTo, reference, 1); }
	public static bool CyclicIsLower(int compared, int comparedTo, int reference) {
		return CyclicIsLower(compared, comparedTo, reference, 1); }
	
	public static float CyclicLerp(float a, float b, float t, float period) {
		if (Mathf.Abs(b-a)<=period/2) { return a*(1-t)+b*t; }
		if (b<a) a -= period; else b -= period;
		return Util.Mod(a*(1-t)+b*t);
	}
	
	public static Vector3 ProjectOntoPlane(Vector3 v, Vector3 normal) {
		return v-Vector3.Project(v,normal);
	}
	
	public static Vector3 SetHeight(Vector3 originalVector, Vector3 referenceHeightVector, Vector3 upVector) {
		Vector3 originalOnPlane = ProjectOntoPlane(originalVector, upVector);
		Vector3 referenceOnAxis = Vector3.Project(referenceHeightVector, upVector);
		return originalOnPlane + referenceOnAxis;
	}
	
	public static Vector3 GetHighest(Vector3 a, Vector3 b, Vector3 upVector) {
		if (Vector3.Dot(a,upVector) >= Vector3.Dot(b,upVector)) return a;
		return b;
	}
	public static Vector3 GetLowest(Vector3 a, Vector3 b, Vector3 upVector) {
		if (Vector3.Dot(a,upVector) <= Vector3.Dot(b,upVector)) return a;
		return b;
	}
	
	public static Matrix4x4 RelativeMatrix(Transform t, Transform relativeTo) {
		return relativeTo.worldToLocalMatrix * t.localToWorldMatrix;
	}
	
	public static Vector3 TransformVector(Matrix4x4 m, Vector3 v) {
		return m.MultiplyPoint(v) - m.MultiplyPoint(Vector3.zero);
	}
	public static Vector3 TransformVector(Transform t, Vector3 v) {
		return TransformVector(t.localToWorldMatrix,v);
	}
	
	public static void TransformFromMatrix(Matrix4x4 matrix, Transform trans) {
		trans.rotation = Util.QuaternionFromMatrix(matrix);
		trans.position = matrix.GetColumn(3); // uses implicit conversion from Vector4 to Vector3
	}
	
	public static Quaternion QuaternionFromMatrix(Matrix4x4 m) {
		// Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
		Quaternion q = new Quaternion();
		q.w = Mathf.Sqrt( Mathf.Max( 0, 1 + m[0,0] + m[1,1] + m[2,2] ) ) / 2; 
		q.x = Mathf.Sqrt( Mathf.Max( 0, 1 + m[0,0] - m[1,1] - m[2,2] ) ) / 2; 
		q.y = Mathf.Sqrt( Mathf.Max( 0, 1 - m[0,0] + m[1,1] - m[2,2] ) ) / 2; 
		q.z = Mathf.Sqrt( Mathf.Max( 0, 1 - m[0,0] - m[1,1] + m[2,2] ) ) / 2; 
		q.x *= Mathf.Sign( q.x * ( m[2,1] - m[1,2] ) );
		q.y *= Mathf.Sign( q.y * ( m[0,2] - m[2,0] ) );
		q.z *= Mathf.Sign( q.z * ( m[1,0] - m[0,1] ) );
		return q;
	}
	
	public static Matrix4x4 MatrixFromQuaternion(Quaternion q) {
		return CreateMatrix(q*Vector3.right, q*Vector3.up, q*Vector3.forward, Vector3.zero);
	}
	
	public static Matrix4x4 MatrixFromQuaternionPosition(Quaternion q, Vector3 p) {
		Matrix4x4 m = MatrixFromQuaternion(q);
		m.SetColumn(3,p);
		m[3,3] = 1;
		return m;
	}
	
	public static Matrix4x4 MatrixSlerp(Matrix4x4 a, Matrix4x4 b, float t) {
		t = Mathf.Clamp01(t);
		Matrix4x4 m = MatrixFromQuaternion(Quaternion.Slerp(QuaternionFromMatrix(a),QuaternionFromMatrix(b),t));
		m.SetColumn(3,a.GetColumn(3)*(1-t)+b.GetColumn(3)*t);
		m[3,3] = 1;
		return m;
	}
	
	public static Matrix4x4 CreateMatrix(Vector3 right, Vector3 up, Vector3 forward, Vector3 position) {
		Matrix4x4 m = Matrix4x4.identity;
		m.SetColumn(0,right);
		m.SetColumn(1,up);
		m.SetColumn(2,forward);
		m.SetColumn(3,position);
		m[3,3] = 1;
		return m;
	}
	public static Matrix4x4 CreateMatrixPosition(Vector3 position) {
		Matrix4x4 m = Matrix4x4.identity;
		m.SetColumn(3,position);
		m[3,3] = 1;
		return m;
	}
	public static void TranslateMatrix(ref Matrix4x4 m, Vector3 position) {
		m.SetColumn(3,(Vector3)(m.GetColumn(3))+position);
		m[3,3] = 1;
	}
	
	public static Vector3 ConstantSlerp(Vector3 from, Vector3 to, float angle) {
		float a1 = Vector3.Angle(from, to);
		float value = 1;
		if(a1 != 0) value = Mathf.Min(1, angle / a1);
		return Vector3.Slerp(from, to, value);
	}
	public static Quaternion ConstantSlerp(Quaternion from, Quaternion to, float angle) {
		float a1 = Quaternion.Angle(from, to);
		float value = 1;
		if(a1 != 0) value = Mathf.Min(1, angle / a1);
		return Quaternion.Slerp(from, to, value);
	}
	public static Vector3 ConstantLerp(Vector3 from, Vector3 to, float length) {
		return from + Clamp(to-from, length);
	}
	public static float ConstantLerp(float from, float to, float length){
		return from + Mathf.Clamp(to - from, -length, length);
	}
	
	public static Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t) {
		Vector3 ab = Vector3.Lerp(a,b,t);
		Vector3 bc = Vector3.Lerp(b,c,t);
		Vector3 cd = Vector3.Lerp(c,d,t);
		Vector3 abc = Vector3.Lerp(ab,bc,t);
		Vector3 bcd = Vector3.Lerp(bc,cd,t);
		return Vector3.Lerp(abc,bcd,t);
	}
	
}
