using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
               UnityEngine.Debug.Log("You win!");
               Debug.Log($"It took you {m_stopwatch.Elapsed} to find all {m_collectablesTotalCount} collectables. ");
#if UNITY_EDITOR
               UnityEditor.EditorApplication.ExitPlaymode();
#endif
           }
           else
           {
               Debug.Log($"'You've already found {m_collectablesTotalCount - m_collectablesCounter} of {m_collectablesTotalCount} collectables!");
           }
       }
       else if(other.gameObject.CompareTag("Enemy"))
       {
           Debug.Log("GAME OVER. YOU GOT CAUGHT!");
#if UNITY_EDITOR
           UnityEditor.EditorApplication.ExitPlaymode();
#endif
       }
       else if(other.gameObject.CompareTag("MovingObstacle"))
       {
           Debug.Log("GAME OVER. YOU HIT AN OBSTACLE!");
#if UNITY_EDITOR
           UnityEditor.EditorApplication.ExitPlaymode();
#endif
       }
       
    }
   
}
