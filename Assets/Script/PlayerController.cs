using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 1f;

    private Rigidbody m_playerRigidbody;

    private float m_movementX;
    private float m_movementY;

    // Start is called before the first frame update
    private void Start()
    {
        m_playerRigidbody = GetComponent<Rigidbody>();
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
       }
    }
   
}
