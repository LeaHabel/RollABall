using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f;

    private Rigidbody m_playerRigidbody;

    private float m_movementX;
    private float m_movementY;

    private int m_collectablesTotalCount, m_collectablesCounter;

    private Stopwatch m_stopwatch;
    
    [SerializeField] private GameObject text = null;
    [SerializeField] private GameObject countdown = null;
    [SerializeField] private GameObject timeText = null;
    public GameObject gameOverText;
    public GameObject winText;
    public GameObject[] respawns;
    private float endTime; 
    


    // Start is called before the first frame update
    private void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();
        m_collectablesTotalCount = m_collectablesCounter = GameObject.FindGameObjectsWithTag("Collectable").Length;
        m_stopwatch = Stopwatch.StartNew(); //equal to: m_stopwatch = new Stopwatch; Stopwatch.start();
        respawns = GameObject.FindGameObjectsWithTag("Enemy");
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

    void Update()
    {
        int timeLeft = (int)(endTime - Time.time);
        if (timeLeft <= 0) timeText.SetActive(false);
        timeText.GetComponent<Text>().text = timeLeft.ToString();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(m_movementX, 0f, m_movementY);

        m_playerRigidbody.AddForce(movement * m_speed);
        //playerTransform.eulerAngles  = new Vector3(0.0f, 0.0f, 0.0f);

        //GameObject.Transform.rotation = Vector3(0.0f,0.0f,0.0f);
       
    }
    
    private void OnTriggerEnter(Collider other)
    { 
        Scene currentScene = SceneManager.GetActiveScene (); 
        string sceneName = currentScene.name;
        if(other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            m_collectablesCounter--;
            if (m_collectablesCounter == 0)
            {
                if (sceneName == "level1")
                {
                    SceneManager.LoadScene("level2");
                }
                if (sceneName == "level2")
                {
                    text.GetComponent<UnityEngine.UI.Text>().text = "YOU WIN!";
                    winText.SetActive(true);
                    //Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables. ");
                    Invoke("endGame", 2);
                }
            }
            else
            {
                text.GetComponent<UnityEngine.UI.Text>().text = 
                    $"SCORE: {m_collectablesTotalCount-m_collectablesCounter} / {m_collectablesTotalCount}";
                //Debug.Log($"'You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
            }
        }
        else if (other.gameObject.CompareTag("Invincible"))
        {
            other.gameObject.SetActive(false);
            foreach (GameObject respawn in respawns)
            {
                respawn.SetActive(false);
            }
            Invoke("setFalse", 10);
            countdown.SetActive(true);
            countdown.GetComponent<Text>().text = $"Invincibility Countdown";
            timeText.SetActive(true);
            endTime = Time.time + 11;
            
            
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "YOU GOT CAUGHT!";
            gameOverText.SetActive(true);
            Invoke("endGame", 2);
        }
        else if(other.gameObject.CompareTag("MovingObstacle"))
        {
            text.GetComponent<UnityEngine.UI.Text>().text = "YOU HIT AN OBSTACLE!";
            gameOverText.SetActive(true);
            Invoke("endGame", 2);
        }
       
    }

    void setFalse()
    {
        foreach (GameObject respawn in respawns)
        {
            respawn.SetActive(true);
        }
        countdown.SetActive(false);
        timeText.SetActive(false);
    }
    private void endGame()
    {
        SceneManager.LoadScene("Menu");
    }
   
}


