using System;
using UnityEngine;
using System.Collections.Generic;
using NodeInspector;
using PaintCraft.Utils;
using Random = UnityEngine.Random;


namespace PaintCraft.Tools.Filters{
    /// <summary>
    /// Move to random offset within radius 
    /// you can also make it depends to BrushSize
    /// </summary>
	[NodeMenuItem("Randomizers/MoveToRandomOffset")]
    public class MoveToRandomOffset : FilterWithNextNode {
	    [EnumFlags]
	    public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
	    
	    [Tooltip("Radius in pixels")]
		public float Radius = 50;

	    public bool MultiplyToBrushSize;
	    
		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody(BrushContext brushLineContext)
		{						
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point =>
				{
					
					if (MultiplyToBrushSize)
					{
						point.Offset += Random.insideUnitCircle * Radius * brushLineContext.LineConfig.Scale;
					}
					else
					{
						point.Offset += Random.insideUnitCircle * Radius;
					}
					
				});
			
			return true;
		}

		#endregion


	}
}