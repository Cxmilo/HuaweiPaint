using UnityEngine;
using System.Collections.Generic;
using PaintCraft.Utils;
using NodeInspector;
using System;
using System.Collections;
using PaintCraft.Controllers;


namespace PaintCraft.Tools.Filters.MaterialFilter{
	/// <summary>
	/// Render swatch on screen or on canvas
	///  Be carefull this filter render only interpolated point NOT THE BASE POINT 
	/// </summary>
    [Obsolete("please use RenderSwatchWithPointMaterial")]
    [NodeMenuItem("Renderer/RenderSwatch (Obsolete, use RenderSwatchWithPointMaterial)")]
    public class RenderSwatch : FilterWithNextNode {
        public Material NormalMaterial;
        public Material RegionMaterial;        

		#region implemented abstract members of FilterWithNextNode
		public override bool FilterBody (BrushContext brushLineContext)
		{
			LinkedListNode<Point> point = null;
			Queue<Mesh> usedMeshes = new Queue<Mesh>();
			point = brushLineContext.Points.Last;
			Vector2 firstPointUV = brushLineContext.FirstPointUVPosition;
			Mesh mesh;
            Material mat;
			MeshPool meshPool = MeshPool.Instance;
			while (point != null ){
				
				Vector3 pointPosition = point.Value.Position + point.Value.Offset;
				pointPosition.z = brushLineContext.Canvas.transform.position.z + brushLineContext.Canvas.BrushOffset;
				
				mesh = meshPool.GetMesh(brushLineContext, point.Value, firstPointUV);
				usedMeshes.Enqueue(mesh);
				mat = MaterialCache.GetMaterial(brushLineContext, NormalMaterial, RegionMaterial);
				if (point.Value.Status == PointStatus.ReadyToApply){                                                
					Graphics.DrawMesh(mesh, pointPosition, Quaternion.Euler(0,0, point.Value.Rotation), mat, 
						brushLineContext.Canvas.BrushLayerId, brushLineContext.Canvas.CanvasCameraController.Camera);                       
					point.Value.Status = PointStatus.CopiedToCanvas;                                 					
				} else if (point.Value.Status == PointStatus.Temporary){                                                						
					Graphics.DrawMesh(mesh, pointPosition, Quaternion.Euler(0,0, point.Value.Rotation), mat, 
						brushLineContext.Canvas.TempRenderLayerId);						
				}
				
				point = point.Previous;
			}
			
			return true;
		}
	  
		#endregion
	}
}
