using UnityEngine;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace PaintCraft.Demo.ColoringBook{    
    
    public class ExitToGalleryByExitButtonController : MonoBehaviour {
        
        void Update () {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
                SceneManager.LoadScene("PageSelect");
#else
                Application.LoadLevel("PageSelect");
#endif
            }               
        }
    }    
}