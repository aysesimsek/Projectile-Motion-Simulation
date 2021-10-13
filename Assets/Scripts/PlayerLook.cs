using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float rotationSpeed = 10;
    public float positionSpeed = 10;

    void Update()
    {
        Vector3 rotation = transform.eulerAngles;
        Vector3 position = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            position.y += positionSpeed * Time.deltaTime;
            //rotation.x += -rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            position.y += -positionSpeed * Time.deltaTime;
            //rotation.x += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            position.x += positionSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x += -positionSpeed * Time.deltaTime;
        }
        transform.eulerAngles = rotation;
        transform.position = position;
    }
}
