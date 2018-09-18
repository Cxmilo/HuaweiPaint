using NodeInspector;

namespace PaintCraft.Tools{
	[OneWay]
    public abstract class IFilter : ScriptableObjectNode {		
		public abstract void Apply(BrushContext brushLineContext);	
	}
}
