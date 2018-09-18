using UnityEngine;
using PaintCraft.Controllers;
using UnityEngine.Assertions;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class RectToCanvasPosition : MonoBehaviour {
    public ScreenCameraController ScreenCameraController;
    RectTransform _rt;
	private CanvasScaler _cs;
	
    void Awake(){
        _rt =GetComponent<RectTransform>();
        _rt.hasChanged = true;
        Assert.IsNotNull(_rt);
	    _cs = GetComponentInParent<CanvasScaler>();
    }
	
	void Update () {
	    if (_rt.hasChanged)
	    {   
			_rt.hasChanged = false;
			Vector3[] corners = GetScreenRect(_rt);
		    
			ScreenCameraController.CameraSize.ViewPortOffset.left = (int)corners[0].x;
			ScreenCameraController.CameraSize.ViewPortOffset.bottom = (int)corners[0].y;
			ScreenCameraController.CameraSize.ViewPortOffset.right = Screen.width - (int)corners[2].x;
			ScreenCameraController.CameraSize.ViewPortOffset.top = Screen.height - (int)corners[2].y;
			ScreenCameraController.CameraSize.Init(ScreenCameraController.Camera, ScreenCameraController.Canvas);
		    ScreenCameraController.CameraSize.ResetSize();
	    }
	}

    public Vector3[] GetScreenRect(RectTransform rectTransform)
    {   
        
        Vector3[] corners  = new Vector3[4];
	    rectTransform.GetWorldCorners(corners);
	    if (_cs != null)
	    {
		    for (int j = 0; j < 4; j++)
		    {
			    corners[j] = corners[j] * _cs.scaleFactor;
		    }
	    }
        return corners;
    }    
}
