using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleController : MonoBehaviour
{

    [SerializeField] private float speed = 2f;

    private int durationCounter = 0;
    [SerializeField] private int durationLimit = 100;
    [SerializeField] private bool moveRight = true;

    private float edge = 10.0f;
    
    [SerializeField] private Material m_idleMaterial = null;

    private void move()
    { 
        if (moveRight)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * speed));
            durationCounter++;
            if (durationCounter >= durationLimit)
            {
                moveRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
            durationCounter--;
            if (durationCounter == 0 || durationCounter == (durationLimit*-1))
            {
                moveRight = true;
            }
        }

        if (gameObject.transform.position.x == edge || gameObject.transform.position.x == (edge * -1) )
        {
           
            moveRight = !moveRight;
        }
        
        

    }

   
    private void FixedUpdate()
    {
        move();
        
    }
}
