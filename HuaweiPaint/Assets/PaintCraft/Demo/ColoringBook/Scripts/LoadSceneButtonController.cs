using UnityEngine;
using UnityEngine.UI;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
    

namespace PaintCraft.Demo.ColoringBook{ 
    public class LoadSceneButtonController : MonoBehaviour
    {    	
    	void Start () {
            GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    #if UNITY_5_3 || UNITY_5_3_OR_NEWER
                    SceneManager.LoadScene("PageSelect");
                    #else
                    Application.LoadLevel("PageSelect");
                    #endif
                });
    	}	
    }
}