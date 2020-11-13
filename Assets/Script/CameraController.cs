using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject m_playerObject;
    
    private Vector3 m_offset;
    // Start is called before the first frame update
    void Start()
    {
        m_offset = gameObject.transform.position - m_playerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       gameObject.transform.position = m_playerObject.transform.position + m_offset; 
    }
}
