using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public bool isWithFriction;

    public Text informationText;
    public Text titleText;

    public int experimentNumberWithFriction, experimentNumberWithoutFriction;

    public GameObject mainMenuCanvas;

    //public Vector3 mainMenuCanvasInitialPosition;
    //public Vector3 hideCanvas = new Vector3(1512f, 1384f, 0f);

    public GameObject frictionSelectionPanel;
    public GameObject withFrictionMenuPanel;
    public GameObject withoutFrictionMenuPanel;


    void Start()
    {
        //mainMenuCanvasInitialPosition = new Vector3(4.1f, 3.89f, 11.596f);
        isWithFriction = true;
    }

    public void Experiment1WithFriction()
    {
        experimentNumberWithFriction = 1;
        isWithFriction = true;
    }

    public void Experiment2WithFriction()
    {
        experimentNumberWithFriction = 2;
        isWithFriction = true;
        mainMenuCanvas.SetActive(false);
    }

    public void Experiment3WithFriction()
    {
        experimentNumberWithFriction = 3;
        isWithFriction = true;
        mainMenuCanvas.SetActive(false);

    }

    public void Experiment1WithoutFriction()
    {
        experimentNumberWithoutFriction = 1;
        isWithFriction = false;
        mainMenuCanvas.SetActive(false);

    }

    public void Experiment2WithoutFriction()
    {
        experimentNumberWithoutFriction = 2;
        isWithFriction = false;
        mainMenuCanvas.SetActive(false);

    }

    public void Experiment3WithoutFriction()
    {
        experimentNumberWithoutFriction = 3;
        isWithFriction = false;
        mainMenuCanvas.SetActive(false);

    }

    public void selectFrictionMode(int frictionMode)
    {
        frictionSelectionPanel.SetActive(false);
        if(frictionMode == 0)
        {
            withFrictionMenuPanel.SetActive(true);
            withoutFrictionMenuPanel.SetActive(false);
        }
        else
        {
            withFrictionMenuPanel.SetActive(false);
            withoutFrictionMenuPanel.SetActive(true);
        }
    }

    public void backToMainMenu()
    {
        frictionSelectionPanel.SetActive(true);
        withFrictionMenuPanel.SetActive(false);
        withoutFrictionMenuPanel.SetActive(false);
    } 

    public void CloseApp()
    {
        Application.Quit();
    }

}
