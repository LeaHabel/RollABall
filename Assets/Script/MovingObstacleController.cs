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
        
        

    }

    private void moveLeft()
    {
        

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        move();
        //transform.position = Vector3.MoveTowards(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(5.0f, 5.0f, 5.0f), 1.0f);
        //Debug.Log("duration "+ duration);
        
/*
        if (Mathf.Sign(duration) == 1)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * speed));
            
        }*/

        /*if (duration < durationLimit & duration != 0) {
            transform.Translate(Vector3.right * (Time.deltaTime * speed));
            duration++;
            
        } else {
            Debug.Log("here");
            transform.Translate(Vector3.left * (Time.deltaTime * speed));
            duration--;
        }*/
        
        
        //fly:
        //transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }
}
