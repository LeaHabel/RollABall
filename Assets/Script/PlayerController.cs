using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f;

    private Rigidbody m_playerRigidbody;

    private float m_movementX;
    private float m_movementY;

    private int m_collectablesTotalCount, m_collectablesCounter;

    private Stopwatch m_stopwatch;
    
    [SerializeField] private GameObject text = null;
    public GameObject gameOverText;
    public GameObject winText;

    // Start is called before the first frame update
    private void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();
        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length;
        m_stopwatch = Stopwatch.StartNew(); //equal to: m_stopwatch = new Stopwatch; Stopwatch.start();
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>();

        m_movementX = movementVector.x;
        m_movementY = movementVector.y;
    }

    public void OnMoveVector2(Vector2 input)
    {
        m_movementX = input.x;
        m_movementY = input.y*-1;
    }
    

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(m_movementX, 0f, m_movementY);

        m_playerRigidbody.AddForce(movement * m_speed);
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.CompareTag("Collectable"))
       {
           other.gameObject.SetActive(false);
           m_collectablesCounter--;
           if (m_collectablesCounter == 0)
           {
               text.GetComponent<UnityEngine.UI.Text>().text = "YOU WIN!";
               winText.SetActive(true);
               Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables. ");
               Invoke("endGame", 1);
           }
           else
           {
               text.GetComponent<UnityEngine.UI.Text>().text = 
                   $"SCORE: {m_collectablesTotalCount-m_collectablesCounter} / {m_collectablesTotalCount}";
               //Debug.Log($"'You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
           }
       }
       else if(other.gameObject.CompareTag("Enemy"))
       {
           text.GetComponent<UnityEngine.UI.Text>().text = "YOU GOT CAUGHT!";
           gameOverText.SetActive(true);
           Invoke("endGame", 1);
       }
       else if(other.gameObject.CompareTag("MovingObstacle"))
       {
           text.GetComponent<UnityEngine.UI.Text>().text = "YOU HIT AN OBSTACLE!";
           //Time.timeScale = 0;
           gameOverText.SetActive(true);
           Invoke("endGame", 1);

       }
       
    }

    private void endGame()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
   
}
