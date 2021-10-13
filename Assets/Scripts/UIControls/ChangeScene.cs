using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public MainMenuScript mainMenu;
        
    public void goExperiment()
    {
        //GameObject.Find("MainMenuCanvas").gameObject.transform.position = mainMenu.hideCanvas;
        //if (mainMenu.dropdownForFriction.value == 1 && mainMenu.isWithFriction == true)
        //{
        //    GameObject.Find("xVectorText").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //    GameObject.Find("yVectorText").gameObject.SetActive(false);
        //    GameObject.Find("bVectorText").gameObject.SetActive(false);
        //    GameObject.Find("angle").gameObject.SetActive(false);
        //    GameObject.Find("slowMotionToggle").gameObject.SetActive(false);
        //    GameObject.Find("slowMotionSlider").gameObject.SetActive(false);
        //    GameObject.Find("xHız").gameObject.SetActive(true);
        //    GameObject.Find("yHız").gameObject.SetActive(true);
        //    GameObject.Find("vztest").gameObject.SetActive(true);
        //    GameObject.Find("changeAngleToggle").gameObject.SetActive(false);
        //    GameObject.Find("angleSlider").gameObject.SetActive(false);
        //}
        //else Debug.Log("buradasın");
        //if (mainMenu.dropdownForFrictionless.value == 1 && mainMenu.isWithFriction == false)
        //{
        //    GameObject.Find("xVectorText").gameObject.SetActive(true);
        //    GameObject.Find("yVectorText").gameObject.SetActive(false);
        //    GameObject.Find("bVectorText").gameObject.SetActive(false);
        //    GameObject.Find("angle").gameObject.SetActive(false);
        //    GameObject.Find("slowMotionToggle").gameObject.SetActive(false);
        //    GameObject.Find("slowMotionSlider").gameObject.SetActive(false);
        //    GameObject.Find("xHız").gameObject.SetActive(false);
        //    GameObject.Find("yHız").gameObject.SetActive(false);
        //    GameObject.Find("vztest").gameObject.SetActive(false);
        //    GameObject.Find("changeAngleToggle").gameObject.SetActive(false);
        //    GameObject.Find("angleSlider").gameObject.SetActive(false);
        //}
        //else Debug.Log("şimdi de buradasın");
    }

    public void goMainMenu()
    {
        //GameObject.Find("MainMenuCanvas").gameObject.transform.position = mainMenu.mainMenuCanvasInitialPosition;
        //GameObject.Find("xVectorText").gameObject.SetActive(true);
        //GameObject.Find("yVectorText").gameObject.SetActive(true);
        //GameObject.Find("bVectorText").gameObject.SetActive(true);
        //GameObject.Find("angle").gameObject.SetActive(true);
        //GameObject.Find("slowMotionToggle").gameObject.SetActive(true);
        //GameObject.Find("slowMotionSlider").gameObject.SetActive(true);
        //GameObject.Find("xHız").gameObject.SetActive(true);
        //GameObject.Find("yHız").gameObject.SetActive(true);
        //GameObject.Find("vztest").gameObject.SetActive(true);
        //GameObject.Find("changeAngleToggle").gameObject.SetActive(true);
        //GameObject.Find("angleSlider").gameObject.SetActive(true);
    }
}
