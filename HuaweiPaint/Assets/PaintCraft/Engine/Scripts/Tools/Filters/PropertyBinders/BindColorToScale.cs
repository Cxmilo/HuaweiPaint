using System.Collections.Generic;
using NodeInspector;
using PaintCraft.Utils;


namespace PaintCraft.Tools.Filters{
	public enum ChangeType{
		Red,
		Green,
		Blue,
		Alpha,
		Hue,
		Saturation,
		Value
	}

	/// <summary>
	/// Take value from scale (like 0->1) and set it as color parameter (r,g,b, etc..)
	/// </summary>
    [NodeMenuItem("PropertyBinders/BindColorToScale")]
    public class BindColorToScale : FilterWithNextNode {
	    [EnumFlags]
	    public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
	    
	    public ChangeType ChangeType;


		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point =>
				{
					switch (ChangeType)
					{
						case ChangeType.Alpha:
							point.PointColor.Alpha = point.Scale;
							break;
						case ChangeType.Red:
							point.PointColor.R = point.Scale;
							break;
						case ChangeType.Green:
							point.PointColor.G = point.Scale;
							break;
						case ChangeType.Blue:
							point.PointColor.B = point.Scale;
							break;
						case ChangeType.Hue:
							point.PointColor.H = point.Scale;
							break;
						case ChangeType.Saturation:
							point.PointColor.S = point.Scale;
							break;
						case ChangeType.Value:
							point.PointColor.V = point.Scale;
							break;
					}
				});   						
			return true;
		}
		#endregion
	}
}