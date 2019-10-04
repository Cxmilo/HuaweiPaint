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
            int randomIndex = Random.Range(0, pageConfig.Length);
            controller.PageConfig = pageConfig[randomIndex];
            controller.SetActivePageConfig(controller.PageConfig);
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
