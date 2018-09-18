using System.Collections.Generic;
using UnityEngine;
using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.PropertyBinders{
	
	/// <summary>
	/// Get Velocity and set it to Point scale
	/// </summary>
	[NodeMenuItem("PropertyBinders/BindScaleToVelocity")]
    public class BindScaleToVelocity : FilterWithNextNode {
		[EnumFlags]
		public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
		
		public VelocityScaleProp Min = new VelocityScaleProp(){Scale = 1.0f, Velocity = 500.0f};
		public VelocityScaleProp Max = new VelocityScaleProp(){Scale = 0.1f, Velocity = 10000.0f };

		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{
			float pointVelocity;
			LinkedListNode<Point> node = brushLineContext.Points.Last;
			float scaleDiff = Min.Scale - Max.Scale;
			float velocityDiff = Max.Velocity - Min.Velocity;
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point =>
				{
					pointVelocity = point.Velocity;
					if (pointVelocity <= Min.Velocity){
						point.Scale = Min.Scale;
					} else if (pointVelocity >= Max.Velocity){
						point.Scale = Max.Scale;
					} else {
						point.Scale = Min.Scale -  scaleDiff * (pointVelocity - Min.Velocity)/ velocityDiff;
					}
					point.Scale *= brushLineContext.LineConfig.Scale;
				});
			
			return true;
		}
		#endregion

	}

	[System.Serializable]
	public class VelocityScaleProp{
		[Tooltip("scale of the swatch (like 0.5 = 50% of original size")]
		public float Scale;
		[Tooltip("Velocity (Pixels per Seconds)")]
		public float Velocity;
	}
}