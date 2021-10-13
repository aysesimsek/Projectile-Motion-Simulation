using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject platform;
    public GameObject mainCamera;
    public float positionSpeed = 10;
    public float zoom = 20;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        float xRotation = platform.transform.rotation.x;
        float yRotation = platform.transform.rotation.y;
        float zRotation = platform.transform.rotation.z;

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(xRotation, positionSpeed * Time.deltaTime, zRotation);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(xRotation, -positionSpeed * Time.deltaTime, zRotation);
        }

        if (Input.GetKey(KeyCode.T))
        {
            transform.Rotate(positionSpeed * Time.deltaTime, yRotation, zRotation);
        }

        if (Input.GetKey(KeyCode.G))
        {
            transform.Rotate(-positionSpeed * Time.deltaTime, yRotation, zRotation);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            var mainCamera = Camera.main;
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x , mainCamera.transform.position.y - .01f, mainCamera.transform.position.z + .01f);
        }

        if (Input.GetKey(KeyCode.X))
        {
            var mainCamera = Camera.main;
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + .01f, mainCamera.transform.position.z - .01f);
        }
        //.uzaklaştırma
    }
}