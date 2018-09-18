using System.Collections.Generic;
using UnityEngine;
using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.Randomizers{
    /// <summary>
    /// Change point position to random value within specific radius
    /// </summary>
	[NodeMenuItem("Randomizers/JitterPosition")]
    public class JitterPosition : FilterWithNextNode {
	    [EnumFlags]
	    public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
	    
	    public float Offset = 5.0f;
		#region implemented abstract members of FilterWithNextNode
		
        public override bool FilterBody(BrushContext brushLineContext)
		{			
			float t, u, r;
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point =>
				{
					t = 2.0f * Mathf.PI * Random.value;
					u = Random.value + Random.value;
					r = Offset * (u > 1 ? 2 - u : u);
			
					point.Offset.x = r * Mathf.Cos(t);
					point.Offset.y = r * Mathf.Sin(t);					
				});
			
			return true;
		}
		
		#endregion
		
	}
}