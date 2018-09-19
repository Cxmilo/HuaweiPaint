using PaintCraft.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public EasyTween startScreen;
    public EasyTween instructionScreen;
    public EasyTween previewImageScreen;
    public EasyTween sendMailForm;
    public EasyTween GameOverScreen;

    public GameObject paintScreen;
    public CanvasController paintPrefab;

    public InputField name;
    public InputField email;

    public EmailManager eManager;

    public Camera previewCamera;

    

    public void OnStartScreenClick ()
    {
        startScreen.OpenCloseObjectAnimation();
        instructionScreen.OpenCloseObjectAnimation();
       // StartCoroutine(InactivityRutine());
    }

    public void OnInstructionScreen ()
    {
        instructionScreen.OpenCloseObjectAnimation();
        paintPrefab.transform.parent.gameObject.SetActive(true);
        paintScreen.SetActive(true);
    }

    public void OnYesPreview ()
    {
        previewImageScreen.OpenCloseObjectAnimation();
        sendMailForm.OpenCloseObjectAnimation();
    }

    public void OnNoPreview ()
    {
        previewImageScreen.OpenCloseObjectAnimation();
        GameOverScreen.OpenCloseObjectAnimation();
        Invoke("ResetGame", 120f);

    }

    public void OnMailFinish ()
    {
        sendMailForm.OpenCloseObjectAnimation();
        GameOverScreen.OpenCloseObjectAnimation();
        Invoke("ResetGame", 10f);
    }

    public void ResetGame ()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnSendEmail ()
    {
        GameInfoManager.playerEmail = email.text;
        eManager.SendEmail();
    }

    public void SaveImage ()
    {
        takeHiResShot = true;
    }

    bool isInactivity = true;

    private void Update()
    {
        if (Input.anyKey) { inactivityTime = 0; }
    }

    bool takeHiResShot = false;

    public int resWidth = 1080;
    public int resHeight = 1920;
    Texture2D screenShot;

    void LateUpdate()
    {
        Camera currentCamera = previewCamera ;

        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot)
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            currentCamera.targetTexture = rt;
            screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            currentCamera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            
            currentCamera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);

           


            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight);
            GameInfoManager.photoPath = filename;

            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
            takeHiResShot = false;

            previewImageScreen.OpenCloseObjectAnimation();
            StartCoroutine(TakePreview());
        }
    }

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screen_{1}x{2}_{3}.png",
                             Application.persistentDataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    IEnumerator TakePreview ()
    {
        yield return new WaitForSeconds(2f);
        paintPrefab.transform.parent.gameObject.SetActive(false);
        paintScreen.SetActive(false);
    }

    public float inactivityTime;
    IEnumerator InactivityRutine ()
    {
        while (inactivityTime < 10)
        {
            yield return new WaitForSeconds(1);
            inactivityTime++;
        }

        ResetGame();
    }
}
