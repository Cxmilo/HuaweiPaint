using System.Collections.Generic;
using UnityEngine;
using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.Randomizers{
	/// <summary>
	/// Set random rotation to the point
	/// </summary>
	[NodeMenuItem("Randomizers/RandomRotation")]
    public class RandomRotation : FilterWithNextNode {
		[EnumFlags]
		public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
		
		public float MinAngle = 0.0f;
		public float MaxAngle = 360.0f;
		#region implemented abstract members of FilterWithNextNode
		
		public override bool FilterBody (BrushContext brushLineContext)
		{			
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point => point.Rotation = Random.Range(MinAngle, MaxAngle));
			return true;
		}
		
		#endregion
		
	}
}