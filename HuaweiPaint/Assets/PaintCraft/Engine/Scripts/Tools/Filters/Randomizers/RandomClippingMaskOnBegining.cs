using UnityEngine;
using NodeInspector;

namespace PaintCraft.Tools.Filters.Randomizers{
	/// <summary>
	/// Change brush context clipping mask offset
	/// </summary>
    [NodeMenuItem("Randomizers/RandomClippingMaskOffsetOnBegining")]
    public class RandomClippingMaskOnBegining : FilterWithNextNode {
		#region implemented abstract members of FilterWithNextNode

		Vector2 baseVector;
		public override bool FilterBody (BrushContext brushLineContext)
		{
            if (brushLineContext.IsFirstPointInLine
			    && brushLineContext.BasePoints.First.Value.BasePointId == 0){
                baseVector = new Vector2(Random.value * 100.0f, Random.value * 100.0f);                         
            } else {
                baseVector.y += Time.deltaTime * 0.01f;
            }
            brushLineContext.ClippingMaskOffset = baseVector;			
			return true;
		}

		#endregion
			
	}
}