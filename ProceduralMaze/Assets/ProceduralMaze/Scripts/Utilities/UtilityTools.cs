using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeUtiliy {

	public static class UtilityTools  {

		/// <summary>
		/// Fill a list with increasing and decreasing occurrences of two families of Sprites,
		/// </summary>
		/// <param name="from">Decreasing Sprite Family.</param>
		/// <param name="to">Increasing Sprite Family.</param>
		/// <param name="startRange">Start Range.</param>
		/// <param name="value">Current Value.</param>
		/// <param name="range">Length of Range.</param>
		/// <param name="listToFill">The List to Fill</param>
		public static void IncreaseVariance(Sprite[] from, Sprite[] to, float startRange, float value, float range, ref List<Sprite> listToFill)
		{
			listToFill = new List<Sprite>();
			float ratio = (value - startRange) / range;

			foreach (Sprite x in from)
			{
				float rnd = Random.Range(0.0f, 1.0f);

				if (ratio == 0)
				{
					listToFill.Add(x);
				}
				if (rnd > ratio)
				{
					listToFill.Add(x);
				}
			}

			foreach (Sprite x in to)
			{
				float rnd = Random.Range(0.0f, 1.0f);

				if (ratio == 1)
				{
					listToFill.Add(x);
				}
				if (rnd < ratio)
				{
					listToFill.Add(x);
				}
			}
		}
	}
}

