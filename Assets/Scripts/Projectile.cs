using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public mouseScale scale;
    public TimeManager timeManager;
    public MainMenuScript mainMenu;

    public Text xSpeedText;
    public Text ySpeedText;
    public Text rangeText;
    public Text[] testTexts = new Text[] { };
    public Toggle toggle;

    [SerializeField] private Transform TargetObjectTF;
    [Range(1.0f, 15.0f)] public float TargetRadius;
    [Range(1.0f, 90f)] public float LaunchAngle;
    [Range(0.0f, 10.0f)] public float TargetHeightOffsetFromGround;
    public bool RandomizeHeightOffset;

    public float m = 0.1f, g = 9.81f, fd, gh, C, A, test, H, time;

    //public List<float> Time;
    public List<float> vx;
    public List<float> vy;
    public List<float> ax;
    public List<float> ay;
    public List<float> coorX;
    public List<float> coorY;

    private bool bTargetReady;
    private bool bTouchingGround;
    public bool isSlowMotion;
    public bool isAngleChange;


    public Rigidbody rigid;
    public Vector3 initialPosition;
    private Vector3 curVelocity;
    private Quaternion initialRotation;

    public float Vx, Vy, t, menzil;

    void Start()
    {
        //A = Mathf.PI * Mathf.Pow(rigid.transform.localScale.x, 2);
        rigid = GetComponent<Rigidbody>();
        bTargetReady = true;
        bTouchingGround = true;
        //LaunchAngle = 45f;
        isSlowMotion = false;
        isAngleChange = false;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void ResetToInitialState()
    {
        rigid.velocity = Vector3.zero;
        this.transform.SetPositionAndRotation(initialPosition, initialRotation);
        bTouchingGround = true;
        bTargetReady = true;
        scale.xVectorForVelocity.transform.localScale = new Vector3(1f, .01f, .01f);
        scale.yVectorForVelocity.transform.localScale = new Vector3(.01f, 01f, .01f);
        scale.bVectorForVelocity.transform.localScale = new Vector3(1.414216f, .01f, .01f);
        scale.xVectorForVelocity.transform.position = new Vector3(-1.2f, 2.698f, 4.709f);
        scale.yVectorForVelocity.transform.position = new Vector3(-1.7f, 3.198f, 4.709f);
        scale.bVectorForVelocity.transform.position = new Vector3(-1.2f, 3.198f, 4.709f);
        //scale.bVectorForVelocity.transform.rotation = new Quaternion(0f, 0f, LaunchAngle, 0f);
        Vx = 0;
        Vy = 0;
    }

    void Update()
    {
        testTexts[0].text = "m: " + m.ToString();
        testTexts[1].text = "g: " + g.ToString();
        testTexts[2].text = "fd: " + fd.ToString();
        testTexts[3].text = "gh: " + gh.ToString();
        testTexts[4].text = "test: " + test.ToString();
        testTexts[5].text = "fd: " + fd.ToString();

        GameObject.Find("mInputField").GetComponentInChildren<Text>().text = "Current mass of the object is " + m.ToString();
        GameObject.Find("gInputField").GetComponentInChildren<Text>().text = "Current gravity is " + g.ToString();
        GameObject.Find("fdInputField").GetComponentInChildren<Text>().text = "Current fd is " + fd.ToString();
        GameObject.Find("ghInputField").GetComponentInChildren<Text>().text = "Current gh is " + gh.ToString();

        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.Get(OVRInput.Button.One))
        {
            if (bTargetReady && mainMenu.experimentNumberWithoutFriction == 1) LaunchFromTargetPositionWithoutFrictionForce();
            else if (bTargetReady && (mainMenu.experimentNumberWithoutFriction == 2 || mainMenu.experimentNumberWithoutFriction == 3)) LaunchFromVectorsWithoutFrictionForce();
            //else if (bTargetReady && mainMenu.experimentNumberWithFriction == 1) LaunchFromTargetPositionWithFrictionForce();
            else if (bTargetReady && (mainMenu.experimentNumberWithFriction == 2 || mainMenu.experimentNumberWithFriction == 3)) LaunchFromVectorsWithFrictionForce();
            else ResetToInitialState();
        }

        //text.text = "x hız: " + Vx.ToString("0.##");
        //text1.text = "y hız: " + Vy.ToString("0.##");

        if (Input.GetKeyDown(KeyCode.R) || OVRInput.GetUp(OVRInput.Button.Two))
        {
            ResetToInitialState();
        }

        if (!bTouchingGround && !bTargetReady)
        {
            transform.rotation = Quaternion.LookRotation(rigid.velocity) * initialRotation;
        }

        if (Input.GetKeyDown(KeyCode.P) || OVRInput.GetUp(OVRInput.Button.Three))
        {
            //text1.text = text.text;
            curVelocity = rigid.velocity;
            rigid.Sleep();
            CalculateHeight();
            testTexts[6].text = H.ToString();
        }

        if (Input.GetKeyDown(KeyCode.C) || OVRInput.GetUp(OVRInput.Button.Four))
        {
            rigid.velocity = curVelocity;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Vx = (scale.bVectorForVelocity.transform.localScale.x * Mathf.Cos(LaunchAngle * Mathf.Deg2Rad)) * 5;
            Vy = (scale.bVectorForVelocity.transform.localScale.x * Mathf.Sin(LaunchAngle * Mathf.Deg2Rad)) * 5;
        }

    }

    void OnCollisionEnter()
    {
        bTouchingGround = true;
        rigid.velocity = Vector3.zero;
    }

    void OnCollisionExit()
    {
        bTouchingGround = false;
    }

    float GetPlatformOffset()
    {
        float platformOffset = 0.0f;

        foreach (Transform childTransform in TargetObjectTF.GetComponentsInChildren<Transform>())
        {
            if (childTransform.name == "Mark")
            {
                platformOffset = childTransform.localPosition.y;
                break;
            }
        }
        return platformOffset;
    }

    void LaunchFromTargetPositionWithoutFrictionForce()
    {
        Vector3 projectileXZPos = transform.position;
        Vector3 targetXZPos = TargetObjectTF.position;
        float dist = Vector3.Distance(projectileXZPos, targetXZPos);
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        transform.LookAt(targetXZPos);

        float Vz = Mathf.Sqrt(g * R / Mathf.Sin((2.0f * LaunchAngle) * Mathf.Deg2Rad));
        float Vy = Vz * Mathf.Sin(LaunchAngle * Mathf.Deg2Rad);
        float Vx = Vz * Mathf.Cos(LaunchAngle * Mathf.Deg2Rad);

        xSpeedText.text = "x hız: " + Vz.ToString();
        ySpeedText.text = "y hız: " + Vy.ToString();
        rangeText.text = "menzil: " + R.ToString();

        Vector3 localVelocity = new Vector3(0f, Vy, Vx);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);
        rigid.velocity = globalVelocity;
        bTargetReady = true;

        if (isSlowMotion) timeManager.slowMotion();
    }

    void LaunchFromVectorsWithoutFrictionForce()
    {
        Vx = (scale.bVectorForVelocity.transform.localScale.x * 10 * Mathf.Cos(LaunchAngle * Mathf.Deg2Rad));
        Vy = (scale.bVectorForVelocity.transform.localScale.x * 10 * Mathf.Sin(LaunchAngle * Mathf.Deg2Rad));

        menzil = Vx * ((2.0f * Vy) / g);
        TargetObjectTF.transform.position = new Vector3(rigid.position.x + menzil, TargetObjectTF.position.y, TargetObjectTF.position.z);
        transform.LookAt(TargetObjectTF);

        Vector3 localVelocity = new Vector3(0f, Vy, Vx);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        xSpeedText.text = "x hız: " + Vx.ToString();
        ySpeedText.text = "y hız: " + Vy.ToString();
        rangeText.text = "menzil: " + menzil.ToString();

        rigid.velocity = globalVelocity;
        bTargetReady = true;

        if (isSlowMotion) timeManager.slowMotion();
    }

    void LaunchFromVectorsWithFrictionForce()
    {
        //float V = scale.bVectorForVelocity.transform.localScale.x;
        Vx = (scale.bVectorForVelocity.transform.localScale.x * 10 *  Mathf.Cos(LaunchAngle * Mathf.Deg2Rad));
        Vy = (scale.bVectorForVelocity.transform.localScale.x * 10 *  Mathf.Sin(LaunchAngle * Mathf.Deg2Rad));
        float V = Mathf.Sqrt(Mathf.Pow(Vx, 2) + Mathf.Pow(Vy, 2));

  
        StartCoroutine(calculationsForFrictionEnv(Vx, Vy, V));

        xSpeedText.text = "x hız: " + Vx.ToString();
        ySpeedText.text = "y hız: " + Vy.ToString();
        rangeText.text = "menzil: " + menzil.ToString();

        transform.LookAt(TargetObjectTF);
        if (isSlowMotion) timeManager.slowMotion();
    }


    IEnumerator calculationsForFrictionEnv(float Vx, float Vy, float V)
    {
        int counter = 0;
        ArrayList yList = new ArrayList();
        ArrayList xList = new ArrayList();
        ArrayList ayList = new ArrayList();
        ArrayList axList = new ArrayList();
        ArrayList vyList = new ArrayList();
        ArrayList vxList = new ArrayList();
        yList.Add(2.685f);
        xList.Add(0);
        vxList.Add(Vx);
        vyList.Add(Vy);
        axList.Add(0);
        ayList.Add(0);

        while ((float) (yList[counter]) >= 2.685f)
        {
            float dt = 0.1f;
            float cd = 0.005f;

            float fd = cd * Mathf.Pow(V, 2);
            float ax = -(fd * Mathf.Cos(LaunchAngle * Mathf.Deg2Rad) / m);
            float ay = -(g) - (fd * Mathf.Sin(LaunchAngle * Mathf.Deg2Rad) / m);
            axList.Add(ax);
            ayList.Add(ay);

            transform.LookAt(TargetObjectTF);
            
            yList.Add(rigid.position.y + dt * Vy);
            xList.Add(rigid.position.x + dt * Vx);


            if (rigid.position.y < 2.685f)
            {

                rigid.transform.position = new Vector3((float)xList[counter], 2.685f, 4.488f);

            }
            else
            {
                rigid.transform.position = new Vector3(rigid.position.x + dt * Vx, rigid.position.y + dt * Vy, rigid.position.z);
                //rigid.transform.position = Vector3.Lerp(rigid.position, new Vector3(rigid.position.x + dt * Vx, rigid.position.y + dt * Vy, rigid.position.z), dt);
            }

            bTargetReady = true;

            Vx = Vx + dt * ax;
            Vy = Vy + dt * ay;

            vxList.Add(Vx);
            vyList.Add(Vy);

            V = Mathf.Sqrt(Mathf.Pow(Vx, 2) + Mathf.Pow(Vy, 2));
            counter++;
            yield return new WaitForSeconds(0.01f);
        }
        
        if(rigid.position.y <= 2.685f)
        {

            rigid.transform.position = new Vector3((float)xList[counter], 2.685f, 4.488f);

        }

        //while (bTouchingGround == false || flag == false)
        //{
        //    float dt = 0.01f;
        //    float cd = 0.005f;

        //    float fd = cd * Mathf.Pow(V, 2);

        //    float ax = -(fd * Mathf.Cos(LaunchAngle * Mathf.Deg2Rad) / m);
        //    float ay = -(g) - (fd * Mathf.Sin(LaunchAngle * Mathf.Deg2Rad) / m);

        //    transform.LookAt(TargetObjectTF);
        //    rigid.transform.position = new Vector3(rigid.position.x + dt * Vx, rigid.position.y + dt * Vy, rigid.position.z);

        //    //Vector3 localVelocity = new Vector3(0f, Vy, Vx);
        //    //Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        //    //rigid.velocity = globalVelocity;
        //    bTargetReady = true;

        //    Vx = Vx + dt * ax;
        //    Vy = Vy + dt * ay;
        //    V = Mathf.Sqrt(Mathf.Pow(Vx, 2) + Mathf.Pow(Vy, 2));
        //    flag = true;
        //    yield return new WaitForSeconds(dt);
        //}

    }


    #region UI THINGS

    public void checkSlowMotion(bool newValue)
    {
        isSlowMotion = newValue;
    }

    public void checkAngelChange(bool newValue)
    {
        isAngleChange = newValue;
    }

    public void mChange(string newText)
    {
        float tempM = float.Parse(newText);
        m = tempM;
    }

    public void gChangeWInputField(string newText)
    {
        float tempG = float.Parse(newText);
        g = tempG;

        if (g < GameObject.Find("gSlider").GetComponent<Slider>().minValue)
        {
            GameObject.Find("gSlider").GetComponent<Slider>().value = GameObject.Find("gSlider").GetComponent<Slider>().minValue;
            g = GameObject.Find("gSlider").GetComponent<Slider>().minValue;
        }

        if (g > GameObject.Find("gSlider").GetComponent<Slider>().maxValue)
        {
            GameObject.Find("gSlider").GetComponent<Slider>().value = GameObject.Find("gSlider").GetComponent<Slider>().maxValue;
            g = GameObject.Find("gSlider").GetComponent<Slider>().maxValue;
        }

        GameObject.Find("gSlider").GetComponent<Slider>().value = g;
    }

    public void gChangeWSlider(float newValue)
    {
        g = newValue;
        GameObject.Find("gInputField").GetComponentInChildren<Text>().text = "Current Gravity is " + newValue.ToString();
    }

    public void fdChangeWInputField(string newText)
    {
        float tempFD = float.Parse(newText);
        fd = tempFD;

        if (fd < GameObject.Find("fdSlider").GetComponent<Slider>().minValue)
        {
            GameObject.Find("fdSlider").GetComponent<Slider>().value = GameObject.Find("fdSlider").GetComponent<Slider>().minValue;
            fd = GameObject.Find("fdSlider").GetComponent<Slider>().minValue;
        }

        if (fd > GameObject.Find("fdSlider").GetComponent<Slider>().maxValue)
        {
            GameObject.Find("fdSlider").GetComponent<Slider>().value = GameObject.Find("fdSlider").GetComponent<Slider>().maxValue;
            fd = GameObject.Find("fdSlider").GetComponent<Slider>().maxValue;
        }

        GameObject.Find("fdSlider").GetComponent<Slider>().value = fd;
    }

    public void fdChangeWSlider(float newValue)
    {
        fd = newValue;
        GameObject.Find("fdInputField").GetComponentInChildren<Text>().text = newValue.ToString();
    }

    public void ghChangeWInputField(string newText)
    {
        float tempGH = float.Parse(newText);
        gh = tempGH;

        if (gh < GameObject.Find("ghSlider").GetComponent<Slider>().minValue)
        {
            GameObject.Find("ghSlider").GetComponent<Slider>().value = GameObject.Find("ghSlider").GetComponent<Slider>().minValue;
            gh = GameObject.Find("ghSlider").GetComponent<Slider>().minValue;
        }

        if (gh > GameObject.Find("ghSlider").GetComponent<Slider>().maxValue)
        {
            GameObject.Find("ghSlider").GetComponent<Slider>().value = GameObject.Find("ghSlider").GetComponent<Slider>().maxValue;
            gh = GameObject.Find("ghSlider").GetComponent<Slider>().maxValue;
        }

        GameObject.Find("ghSlider").GetComponent<Slider>().value = gh;
    }

    public void ghChangeWSlider(float newValue)
    {
        gh = newValue;
        GameObject.Find("ghInputField").GetComponentInChildren<Text>().text = newValue.ToString();
    }

    #endregion

    public void vxCalculation()
    {
        test = Vx;
        test = test - (((gh * C * A) / 2f) / m) * Vx * scale.bVectorForVelocity.transform.localScale.x * 1f;
        Vx = test;
    }

    IEnumerator calculateVx()
    {
        while (true)
        {
            vxCalculation();
            yield return new WaitForSeconds(1f);
        }
    }

    private void CalculateHeight()
    {
        H = Vy * time - .5f * g * Mathf.Pow(time, 2);
    }
}


/*
void Launch()
    {
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(TargetObjectTF.position.x, 0.0f, TargetObjectTF.position.z);
        
        transform.LookAt(targetXZPos);
        
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float H = (TargetObjectTF.position.y + GetPlatformOffset()) - transform.position.y;
        
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        text2.text = "vz: " + Vz.ToString() + " vy: " + Vy.ToString();

        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);
        
        rigid.velocity = globalVelocity;
        bTargetReady = true;

        if (isSlowMotion) timeManager.slowMotion();
    }
  */
