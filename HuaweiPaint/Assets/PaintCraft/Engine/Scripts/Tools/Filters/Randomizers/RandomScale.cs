﻿using System.Collections.Generic;
using UnityEngine;
using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.Randomizers{
	/// <summary>
	/// Set random scale to the point
	/// </summary>
	[NodeMenuItem("Randomizers/RandomScale")]
    public class RandomScale : FilterWithNextNode {
		[EnumFlags]
		public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
		
		public float ScaleMax = 1.0f;
		public float ScaleMin = 0.1f;

		#region implemented abstract members of FilterWithNextNode
		
		public override bool FilterBody (BrushContext brushLineContext)
		{		
			float baseScale = brushLineContext.LineConfig.Scale;
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point => point.Scale = Random.Range(ScaleMin, ScaleMax) * baseScale);						
			return true;
		}
		
		#endregion

	}
}