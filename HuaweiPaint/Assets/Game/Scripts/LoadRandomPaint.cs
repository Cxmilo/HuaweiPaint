using PaintCraft.Canvas.Configs;
using PaintCraft.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRandomPaint : MonoBehaviour {



    public CanvasController controller;
    public PageConfig[] pageConfig;
    // Use this for initialization
    void Start () {
		
        if(controller)
        {
            
            int paintIndex = PlayerPrefs.GetInt("CurrentIndex",0);
            Debug.Log("index : " + paintIndex);
            controller.PageConfig = pageConfig[paintIndex];
            controller.SetActivePageConfig(controller.PageConfig);

            paintIndex = paintIndex < pageConfig.Length - 1 ? (paintIndex+1) : 0;
            PlayerPrefs.SetInt("CurrentIndex", paintIndex);
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
