using UnityEngine;
using PaintCraft.Tools;
using System.Collections.Generic;
using PaintCraft.Utils;
using NodeInspector;
using PaintCraft.Controllers;


namespace PaintCraft.Tools.Filters{
	/// <summary>
	/// Render swatch on screen or on canvas
	/// This renderer USE MATERIAL assigned to the POINT
	///  Be carefull this filter render only interpolated point NOT THE BASE POINT 
	/// </summary>

	[NodeMenuItem("Renderer/RenderSwatchWithPointMaterial")]
    public class RenderSwatchWithPointMaterial : FilterWithNextNode {        
		int lastUsedFrame = -1;

		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{
            Camera tempCamera = null;
            if (brushLineContext.SourceInputHandler is ScreenCameraController){
                tempCamera = (brushLineContext.SourceInputHandler as ScreenCameraController).Camera;
            }

			Vector2 firstPointUV = brushLineContext.FirstPointUVPosition;
			LinkedListNode<Point> point = null;
			
			MeshPool meshPool = MeshPool.Instance;			
			
			point = brushLineContext.Points.Last;
            Mesh mesh;
            while (point != null ){                
				if (!point.Value.IsBasePoint){
                    Vector3 pointPosition = point.Value.Position + point.Value.Offset;
                    pointPosition.z = brushLineContext.Canvas.transform.position.z + brushLineContext.Canvas.BrushOffset;
                    mesh = meshPool.GetMesh(brushLineContext, point.Value, firstPointUV);
                    if (point.Value.Status == PointStatus.ReadyToApply){                                                                        
                        Graphics.DrawMesh(mesh, pointPosition, Quaternion.Euler(0,0, point.Value.Rotation), point.Value.Material, 
                            brushLineContext.Canvas.BrushLayerId, brushLineContext.Canvas.CanvasCameraController.Camera);                       
						point.Value.Status = PointStatus.CopiedToCanvas;                                                         
                    } else if (point.Value.Status == PointStatus.Temporary && tempCamera != null){                           
                        Graphics.DrawMesh(mesh, pointPosition, Quaternion.Euler(0,0, point.Value.Rotation), point.Value.Material, 
                            brushLineContext.Canvas.TempRenderLayerId, tempCamera);						
					}
				}
				point = point.Previous;
			}
			return true;
		}
      
		#endregion
	}
}
