using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class mouseScale : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    public Projectile projectile;
    public MainMenuScript mainMenu;

    private Vector2 startPosition;
    private bool click;

    public Text xVectorText;
    public Text yVectorText;
    public Text bVectorText;
    public Text angleText;
    float x, launchAngle, angle;
    private float proportion = 10f, scalingRate = 1f;

    public GameObject xVector;
    public GameObject yVector;
    public GameObject bVector;
    public GameObject xVectorForVelocity;
    public GameObject yVectorForVelocity;
    public GameObject bVectorForVelocity;

    private Vector3 newPosition;
 
    void Update()
    {
        xVectorText.text = "xVector: " + (xVectorForVelocity.transform.localScale.x * 10).ToString("0.##");
        yVectorText.text = "yVector: " + (yVectorForVelocity.transform.localScale.y * 10).ToString("0.##");
        bVectorText.text = "bVector: " + (bVectorForVelocity.transform.localScale.x * 10).ToString("0.##");
        if ((projectile.rigid.velocity.x > 0 || projectile.rigid.velocity.y > 0 || projectile.rigid.velocity.z > 0) || (projectile.rigid.velocity.x < 0 || projectile.rigid.velocity.y < 0 || projectile.rigid.velocity.z < 0)) moveVectors();
        if (projectile.isAngleChange && (mainMenu.experimentNumberWithFriction == 3 || mainMenu.experimentNumberWithoutFriction == 3 || mainMenu.experimentNumberWithoutFriction == 1 || mainMenu.experimentNumberWithFriction == 1))
        {
            changeAngle(newValue: projectile.LaunchAngle);
            bVectorForVelocityCalc();
        }
        else if (!projectile.isAngleChange && (mainMenu.experimentNumberWithoutFriction == 2 || mainMenu.experimentNumberWithoutFriction == 3 || mainMenu.experimentNumberWithFriction == 2)) 
        {
            bVectorForVelocityCalc();
            projectile.LaunchAngle = launchAngle;
        }

        angleText.text = "Angle: " + projectile.LaunchAngle + " " + (bVectorForVelocity.transform.localScale.x * Mathf.Cos(launchAngle * Mathf.Deg2Rad)).ToString();        // bVectorForVelocity.transform.rotation.z.ToString();
        
        bVectorCalc();
        if (projectile.rigid.velocity.x == 0 && projectile.rigid.position == projectile.initialPosition)
        {
            xVectorForVelocity.gameObject.SetActive(true);
            yVectorForVelocity.gameObject.SetActive(true);
            bVectorForVelocity.gameObject.SetActive(true);
            xVector.gameObject.SetActive(false);
            yVector.gameObject.SetActive(false);
            bVector.gameObject.SetActive(false);
        }
        else
        {
            xVectorForVelocity.gameObject.SetActive(false);
            yVectorForVelocity.gameObject.SetActive(false);
            bVectorForVelocity.gameObject.SetActive(false);
            xVector.gameObject.SetActive(true);
            yVector.gameObject.SetActive(true);
            bVector.gameObject.SetActive(true);
        }       
    }

    void OnMouseOver()
    {
        if(mainMenu.experimentNumberWithoutFriction == 2 || mainMenu.experimentNumberWithFriction == 2)
        {
            if (Input.GetMouseButtonDown(0) && this.gameObject.name == "xVectorForVelocity")
            {
            if (click) return;
            click = true;
            Debug.Log(click);
            this.transform.localScale += new Vector3(scalingRate, 0f, 0f);
            this.transform.position = new Vector3(transform.position.x + scalingRate / 2, transform.position.y, transform.position.z);
            click = false;
            bVectorForVelocityCalc();
            }

            if (Input.GetMouseButtonDown(1) && this.gameObject.name == "xVectorForVelocity")
            {
                if (click) return;
                click = true;
                Debug.Log(click);
                this.transform.localScale -= new Vector3(scalingRate, 0f, 0f);
                this.transform.position = new Vector3(transform.position.x - scalingRate / 2, transform.position.y, transform.position.z);
                click = false;
                bVectorForVelocityCalc();
            }

            if (Input.GetMouseButtonDown(0) && this.gameObject.name == "yVectorForVelocity")
            {
                if (click) return;
                click = true;
                Debug.Log(click);
                this.transform.localScale += new Vector3(0f, scalingRate, 0f);
                this.transform.position = new Vector3(transform.position.x, transform.position.y + scalingRate / 2, transform.position.z);
                click = false;
                bVectorForVelocityCalc();
            }

            if (Input.GetMouseButtonDown(1) && this.gameObject.name == "yVectorForVelocity")
            {
                if (click) return;
                click = true;
                Debug.Log(click);
                this.transform.localScale -= new Vector3(0f, scalingRate, 0f);
                this.transform.position = new Vector3(transform.position.x, transform.position.y - scalingRate / 2, transform.position.z);
                click = false;
                bVectorForVelocityCalc();
            }
        }

        if (mainMenu.experimentNumberWithoutFriction == 3 || mainMenu.experimentNumberWithFriction == 3)
        {
            if (Input.GetMouseButtonDown(0) && this.gameObject.name == "bVectorForVelocity")
            {
                if (click) return;
                click = true;
                Debug.Log(click);

                this.transform.localScale += new Vector3(scalingRate, 0, 0);
                xyVectorCalc();
                Debug.Log(projectile.LaunchAngle);

                Quaternion rot = new Quaternion();
                rot.eulerAngles = new Vector3(0, 0, projectile.LaunchAngle);

                Vector3 position = new Vector3(transform.position.x + scalingRate / 2, transform.position.y + scalingRate / 2, transform.position.z);

                this.transform.SetPositionAndRotation(position, rot);
                xyVectorPosCalc(); 
            
                click = false;
            }

            if (Input.GetMouseButtonDown(1) && this.gameObject.name == "bVectorForVelocity")
            {
                if (click) return;
                click = true;
                Debug.Log(click);

                this.transform.localScale -= new Vector3(scalingRate, 0, 0);
                xyVectorCalc();
                Debug.Log(projectile.LaunchAngle);

                Quaternion rot = new Quaternion();
                rot.eulerAngles = new Vector3(0, 0, projectile.LaunchAngle);

                Vector3 position = new Vector3(transform.position.x - scalingRate / 2, transform.position.y - scalingRate / 2, transform.position.z);

                this.transform.SetPositionAndRotation(position, rot);
                xyVectorPosCalc();

                click = false;
            }
        }      
    }

    void xyVectorCalc()
    {
        xVectorForVelocity.transform.localScale = new Vector3((bVectorForVelocity.transform.localScale.x * Mathf.Cos(projectile.LaunchAngle * Mathf.Deg2Rad)), transform.localScale.y, transform.localScale.z);
        yVectorForVelocity.transform.localScale = new Vector3(yVectorForVelocity.transform.localScale.x, (bVectorForVelocity.transform.localScale.x * Mathf.Sin(projectile.LaunchAngle * Mathf.Deg2Rad)), transform.localScale.z);
    }

    void xyVectorPosCalc()
    {
        xVectorForVelocity.transform.position = new Vector3(bVectorForVelocity.transform.position.x, xVectorForVelocity.transform.position.y, xVectorForVelocity.transform.position.z);
        yVectorForVelocity.transform.position = new Vector3(yVectorForVelocity.transform.position.x, bVectorForVelocity.transform.position.y, yVectorForVelocity.transform.position.z);
    }

    void bVectorForVelocityCalc()
    {
        if (projectile.isAngleChange)
        {
            xyVectorCalc();

            Quaternion rot = new Quaternion();
            rot.eulerAngles = new Vector3(0, 0, projectile.LaunchAngle);

            Vector3 position = new Vector3(bVectorForVelocity.transform.position.x, bVectorForVelocity.transform.position.y, bVectorForVelocity.transform.position.z);

            bVectorForVelocity.transform.SetPositionAndRotation(position, rot);
            
            moveVelocityVectors();
        }
        else
        {
            float a;

            x = Mathf.Sqrt(Mathf.Pow(xVectorForVelocity.transform.localScale.x, 2) + Mathf.Pow(yVectorForVelocity.transform.localScale.y, 2));
            bVectorForVelocity.transform.localScale = new Vector3(x, bVectorForVelocity.transform.localScale.y, bVectorForVelocity.transform.localScale.z);
            a = yVectorForVelocity.transform.localScale.y / xVectorForVelocity.transform.localScale.x;
            launchAngle = Mathf.Atan(a) / Mathf.Deg2Rad;

            Quaternion rot = new Quaternion();
            rot.eulerAngles = new Vector3(0, 0, launchAngle);

            Vector3 position = new Vector3(xVectorForVelocity.transform.position.x, yVectorForVelocity.transform.position.y, transform.position.z);
            bVectorForVelocity.transform.SetPositionAndRotation(position, rot);
        }
        
    }

    void bVectorCalc()
    {
        float a, launchAngle2;

        x = Mathf.Sqrt(Mathf.Pow(xVector.transform.localScale.x, 2) + Mathf.Pow(yVector.transform.localScale.y, 2));
        bVector.transform.localScale = new Vector3(x, bVector.transform.localScale.y, bVector.transform.localScale.z);
        a = yVector.transform.localScale.y / xVector.transform.localScale.x;
        launchAngle2 = Mathf.Atan(a) / Mathf.Deg2Rad;

        Quaternion rot = new Quaternion();
        rot.eulerAngles = new Vector3(0, 0, launchAngle2);

        Vector3 position = new Vector3(xVector.transform.position.x, yVector.transform.position.y, transform.position.z);
        bVector.transform.SetPositionAndRotation(position, rot);
    }

    void moveVectors()
    {
        if (this.gameObject.name == "xVector")
        {
            this.transform.position = new Vector3(projectile.rigid.transform.position.x + xVector.transform.localScale.x / 2, projectile.rigid.transform.position.y, projectile.rigid.transform.position.z + .2f);
            this.transform.localScale = new Vector3(projectile.rigid.velocity.x / proportion, xVector.transform.localScale.y, xVector.transform.localScale.z);
        }

        if (this.gameObject.name == "yVector")
        {
            this.transform.position = new Vector3(projectile.rigid.transform.position.x, projectile.rigid.transform.position.y + yVector.transform.localScale.y / 2, projectile.rigid.transform.position.z + .2f);
            this.transform.localScale = new Vector3(yVector.transform.localScale.x, projectile.rigid.velocity.y / proportion, yVector.transform.localScale.z);
        }

        if (this.gameObject.name == "bVector")
        {
            this.transform.position = new Vector3(projectile.rigid.transform.position.x + yVector.transform.localScale.y / 2, projectile.rigid.transform.position.y + xVector.transform.localScale.x / 2, projectile.rigid.transform.position.z + .2f);
            this.transform.localScale = new Vector3(projectile.rigid.velocity.x / proportion, bVector.transform.localScale.y, bVector.transform.localScale.z);
        }
    }

    void moveVelocityVectors()
    {
        if (this.gameObject.name == "xVectorForVelocity")
        {
            this.transform.position = new Vector3(projectile.rigid.transform.position.x + xVectorForVelocity.transform.localScale.x / 2, projectile.rigid.transform.position.y, projectile.rigid.transform.position.z + .2f);
        }

        if (this.gameObject.name == "yVectorForVelocity")
        {
            this.transform.position = new Vector3(projectile.rigid.transform.position.x, projectile.rigid.transform.position.y + yVectorForVelocity.transform.localScale.y / 2, projectile.rigid.transform.position.z + .2f);
        }

        if (this.gameObject.name == "bVectorForVelocity")
        {
            this.transform.position = new Vector3(projectile.rigid.transform.position.x + xVectorForVelocity.transform.localScale.x / 2, projectile.rigid.transform.position.y + yVectorForVelocity.transform.localScale.y  / 2, projectile.rigid.transform.position.z + .2f);
        }
    }

    public void changeAngle(float newValue)
    {
        if (mainMenu.experimentNumberWithoutFriction == 3 || mainMenu.experimentNumberWithFriction == 3 || mainMenu.experimentNumberWithoutFriction == 1 || mainMenu.experimentNumberWithFriction == 1) projectile.LaunchAngle = newValue;
    }
}

//boolean changeangle ekle 
//    true ise aşağıdaki fonksiyon ve diğerlerini çalıştır 
//    değilse eskilerini çalıştır