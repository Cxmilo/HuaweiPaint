using System.Collections.Generic;
using PaintCraft.Utils;
using UnityEngine;
using NodeInspector;

namespace PaintCraft.Tools.Filters.PropertyBinders{
	/// <summary>
	/// Take velocity (how fast you move point) and set point Hue
	/// </summary>
	[NodeMenuItem("PropertyBinders/BindHueOffsetToVelocity")]
    public class BindHueOffsetToVelocity : FilterWithNextNode {
		[EnumFlags]
		public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
		
		public VelocityHueProp Min = new VelocityHueProp(){ Hue = 0.0f, Velocity = 500.0f};
		public VelocityHueProp Max = new VelocityHueProp(){ Hue = 0.1f, Velocity = 1000.0f};


		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{

			float newHue;
			float pointVelocity;
			LinkedListNode<Point> node = brushLineContext.Points.Last;
			float hueDiff = Min.Hue - Max.Hue;
			float velocityDiff = Max.Velocity - Min.Velocity;			
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point =>
				{
					pointVelocity = point.Velocity;
					if (pointVelocity <= Min.Velocity){
						newHue = Min.Hue;
					} else if (pointVelocity >= Max.Velocity){
						newHue = Max.Hue;
					} else {
						newHue = Min.Hue -  hueDiff * (pointVelocity - Min.Velocity)/ velocityDiff;
					}

					point.PointColor.H  = MathUtil.LoopValue( newHue + point.PointColor.H, 0.0f, 1.0f);					
				});
			
			return true;
		}
		#endregion
			


		[System.Serializable]
		public class VelocityHueProp{
			public float Hue;
			[Tooltip("Velocity (Pixels per Seconds)")]
			public float Velocity;
		}
	}
}