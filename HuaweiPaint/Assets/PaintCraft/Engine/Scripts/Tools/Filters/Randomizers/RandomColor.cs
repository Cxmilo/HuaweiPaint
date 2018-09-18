using System.Collections.Generic;
using UnityEngine;
using NodeInspector;
using PaintCraft.Utils;

namespace PaintCraft.Tools.Filters.Randomizers
{
    
    /// <summary>
    /// Set random color (R,G,B) to the Point
    /// </summary>
    [NodeMenuItem("Randomizers/RandomColor")]
    public class RandomColor : FilterWithNextNode
    {
        [EnumFlags]
        public PointType PointType = PointType.BasePoint | PointType.InterpolatedPoint;
        
        #region implemented abstract members of FilterWithNextNode
        public override bool FilterBody(BrushContext brushLineContext)
        {            
            brushLineContext.ForEachUncopiedToCanvasPoint(PointType,
                point => point.PointColor.Color = new Color(Random.value,Random.value,Random.value, 1.0f));
            return true;
        }

        #endregion

    }
}