using UnityEngine;
using System.Collections.Generic;
using NodeInspector;
using PaintCraft.Utils;


namespace PaintCraft.Tools.Filters{
    /// <summary>
    /// Rotate every point along the path.
    /// </summary>
	[NodeMenuItem("Align/AlignAlongPath")]
    public class AlignAlongPath : FilterWithNextNode {        
	    [EnumFlags]
	    public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
	    
	    
	    /// <summary>
		/// The angle add. in degree
		/// </summary>
		public float AngleAdd = 0.0f;

		#region implemented abstract members of FilterWithNextNode

        public override bool FilterBody (BrushContext brushLineContext)
		{			
			if ((PointType & PointType.InterpolatedPoint) == PointType.InterpolatedPoint)
			{				
				RotatePoints(brushLineContext.Points.Last);
			}
			if ((PointType & PointType.BasePoint) == PointType.BasePoint)
			{
				RotatePoints(brushLineContext.BasePoints.Last);
			}
			
			return true;
		}

	    private void RotatePoints(LinkedListNode<Point> node)
	    {
		    Vector2 delta;
		    float rotation;

		    LinkedListNode<Point> previousNode = null;
		    bool setLastPosition = true;
		    while (node != null && node.Value.Status != PointStatus.CopiedToCanvas)
		    {			    
				// we need to count only not based points. because they could located at the same position as normal points
				if (previousNode != null)
				{
					delta = previousNode.Value.Position - node.Value.Position;
					rotation = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg + AngleAdd;
					node.Value.Rotation = rotation;
					if (setLastPosition)
					{
						previousNode.Value.Rotation = rotation;
						setLastPosition = false;
					}
				}
				previousNode = node;			    
			    node = node.Previous;
		    }
	    }

	    #endregion
	    
	    
	    
	}
}