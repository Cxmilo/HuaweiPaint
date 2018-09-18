using System.Collections.Generic;
using UnityEngine;
using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.Randomizers{
	/// <summary>
	/// Set random Hue to the Point
	/// </summary>
	[NodeMenuItem("Randomizers/RandomHue")]
    public class RandomHue : FilterWithNextNode {
		[EnumFlags]
		public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
		
		#region implemented abstract members of FilterWithNextNode
		
		public override bool FilterBody (BrushContext brushLineContext)
		{			
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point => point.PointColor.H = Random.Range(0, 1.0f));
			return true;
		}
		
		#endregion
		
	}
}