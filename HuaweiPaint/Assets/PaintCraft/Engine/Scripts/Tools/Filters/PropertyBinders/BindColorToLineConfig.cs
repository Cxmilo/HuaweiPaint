using System.Collections.Generic;
using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.PropertyBinders{
	/// <summary>
	/// Set color the same as line config color
	/// </summary>
	[NodeMenuItem("PropertyBinders/BindColorToLineConfig")]
    public class BindColorToLineConfig : FilterWithNextNode { 
		[EnumFlags]
		public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
		
		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{
			brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
				point => point.PointColor.CopyFrom(brushLineContext.LineConfig.Color));
						
			return true;
		}
		#endregion
	}
}