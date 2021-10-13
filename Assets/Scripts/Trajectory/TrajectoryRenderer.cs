using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    public Projectile projectile;
    public mouseScale scale;

    LineRenderer lr;

    public float velocity;
    public float angle;
    public int resolution;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Start()
    {
        angle = projectile.LaunchAngle;
        velocity = scale.bVectorForVelocity.transform.localScale.x * 5;
        RenderArc();
    }

    void Update()
    {
        if (lr != null && Application.isPlaying) RenderArc();
        angle = projectile.LaunchAngle;
        velocity = scale.bVectorForVelocity.transform.localScale.x * 5;
    }

    void RenderArc()
    {
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        float maxDistance = (velocity * velocity * Mathf.Sin(2 * (Mathf.Deg2Rad * angle))) / Mathf.Abs(Physics2D.gravity.y);

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(angle * Mathf.Deg2Rad) - (((Mathf.Abs(Physics2D.gravity.y) * x * x)) / (2 * velocity * velocity * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Cos(angle * Mathf.Deg2Rad)));
        return new Vector3(x, y);
    }
}


