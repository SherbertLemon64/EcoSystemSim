using System;
using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;
using UnityTemplateProjects;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float Speed = 5f;
    private Transform transformCache;
    private Vector3 startPos;
    private Vector3 startRotation;
    
    public float Rotation;

    public NeuralNetwork nn;

    public float RotationSpeed;

    public float left;
    public float right;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transformCache = gameObject.transform;
        startPos = transform.position;
        startRotation = transform.eulerAngles;
        Rotation = transformCache.eulerAngles.y;

        nn = new NeuralNetwork(new[] {10, 10, 10, 2});
    }

    public void ResetPosition()
    {
        transformCache.position = startPos;
        transformCache.eulerAngles = startRotation;
    }

    public void Update()
    {
        // caching
        Vector3 forward = transformCache.forward;
        
        // code
        RaycastHit[] rays = SendRays(10, 230, Color.green);
        float[] rayLengths = new float[rays.Length];

        for (int i = 0; i < rays.Length; i++)
        {
            rayLengths[i] = rays[i].distance;
        }

        Vector<float> output = nn.Predict(rayLengths);

        right = output[0];
        left = output[1];
        
        Rotation += (right - left) * RotationSpeed * Time.deltaTime;
        
        rb.velocity = new Vector3(forward.x * Speed, rb.velocity.y, forward.z * Speed);
        transformCache.eulerAngles = new Vector3(0,Rotation,0);
        
    }
    

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.collider.gameObject.name == "Walls")
        {
            gameObject.SetActive(false);
            if (LearningController.AliveAnimalsObjects.Count == 1)
            {
                LearningController.ResetWorlds(this);
            }
            else
            {
                LearningController.AliveAnimalsObjects.Remove(this);
            }
            //Rotation = (Rotation + 180 + Random.Range(-30, 30));
        }
    }


    private RaycastHit[] SendRays(int _rayAmount, float _angles)
    {
        float eachAngle = _angles / _rayAmount;
        float offset = -(_angles / 2) + Rotation;
        RaycastHit[] rays = new RaycastHit[_rayAmount];
        
        
        for (int i = 0; i < _rayAmount; i++)
        {
            var dir = Quaternion.Euler(new Vector3(0, i * eachAngle + eachAngle / 2 + offset, 0)) * Vector3.forward;
            Physics.Raycast(transformCache.position, dir, out rays[i]);
        }

        return rays;
    }


    private RaycastHit[] SendRays(int _rayAmount, float _angles, Color _debugColor)
    {
        RaycastHit[] rays = SendRays(_rayAmount, _angles);

        foreach (RaycastHit ray in rays)
        {
            Debug.DrawLine(transformCache.position, ray.point, _debugColor);
        }

        return rays;
    }
}