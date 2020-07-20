﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Utilities;

namespace Terraria
{
	partial class Utils
	{
		//Conversions

		public static Vector2 ToWorldCoordinates(this Point p, Vector2 autoAddXY)
			=> ToWorldCoordinates(p, autoAddXY.X, autoAddXY.Y);

		public static Vector2 ToWorldCoordinates(this Point16 p, Vector2 autoAddXY)
			=> p.ToVector2().ToWorldCoordinates(autoAddXY);

		public static Vector2 ToWorldCoordinates(this Vector2 v, float autoAddX = 8f, float autoAddY = 8f)
			=> v.ToWorldCoordinates(new Vector2(autoAddX, autoAddY));

		public static Vector2 ToWorldCoordinates(this Vector2 v, Vector2 autoAddXY)
			=> v * 16f + autoAddXY;

		public static Point ToPoint(this Point16 p)
			=> new Point(p.X, p.Y);

		public static Point16 ToPoint16(this Vector2 v)
			=> new Point16((short)v.X, (short)v.Y);

		//Struct extensions

		public static T NextEnum<T>(this T src) where T : struct
		{
			if(!typeof(T).IsEnum)
				throw new ArgumentException($"Argumnent {typeof(T).FullName} is not an Enum");

			T[] Arr = (T[])Enum.GetValues(src.GetType());
			int j = Array.IndexOf(Arr, src) + 1;

			return Arr.Length == j ? Arr[0] : Arr[j];
		}

		public static T PreviousEnum<T>(this T src) where T : struct
		{
			if(!typeof(T).IsEnum)
				throw new ArgumentException($"Argumnent {typeof(T).FullName} is not an Enum");

			T[] Arr = (T[])Enum.GetValues(src.GetType());
			int j = Array.IndexOf(Arr, src) - 1;

			return j < 0 ? Arr[Arr.Length - 1] : Arr[j];
		}

		//Random extensions

		public static T Next<T>(this UnifiedRandom r, T[] array)
			=> array[r.Next(array.Length)];

		public static T Next<T>(this UnifiedRandom r, IList<T> list)
			=> list[r.Next(list.Count)];

		public static float NextFloat(this UnifiedRandom r, float maxValue)
			=> (float)r.NextDouble() * maxValue;

		public static float NextFloat(this UnifiedRandom r, float minValue, float maxValue)
			=> (float)r.NextDouble() * (maxValue - minValue) + minValue;

		public static bool NextBool(this UnifiedRandom r)
			=> r.NextDouble() < .5;

		/// <summary> Returns true 1 out of X times. </summary>
		public static bool NextBool(this UnifiedRandom r, int consequent)
		{
			if(consequent < 1)
				throw new ArgumentOutOfRangeException(nameof(consequent), "consequent must be greater than or equal to 1.");

			return r.Next(consequent) == 0;
		}

		/// <summary> Returns true X out of Y times. </summary>
		public static bool NextBool(this UnifiedRandom r, int antecedent, int consequent)
		{
			if(antecedent > consequent)
				throw new ArgumentOutOfRangeException(nameof(antecedent), "antecedent must be less than or equal to consequent.");

			return r.Next(consequent) < antecedent;
		}

		public static bool Contains(this Rectangle rectangle, Vector2 point) => point.X >= rectangle.Left && point.X <= rectangle.Right && point.Y >= rectangle.Top && point.Y <= rectangle.Bottom;
	}
}
