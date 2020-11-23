using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject m_playerObject = null;
    [SerializeField] private float m_detectionRadius = 4f;

    [SerializeField] private Material m_idleMaterial = null;
    [SerializeField] private Material m_chasingMaterial = null;
    
    private NavMeshAgent m_agent;
    private Vector3 m_initialPosition;

    /* Protected - can only be called from inside its class, OR within a subclass which
     implements that class
     
     Virtual - a method which may be overridden by a subclass, but still contains an 
     implementation. An overriding member in a subclass may also call base.MethodName() 
     to invoke the underlying Virtual method before/after doing its own thing.
    */
    protected virtual Vector3 GetNextDestination()
    {
        return m_initialPosition;
    }
    void Start()
    {
        m_agent = gameObject.GetComponent<NavMeshAgent>();
        m_initialPosition = gameObject.transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(m_playerObject.transform.position, gameObject.transform.position) < m_detectionRadius) //if distance close enough. enemy is chasing the player
        {
            m_agent.GetComponent<Renderer>().material = m_chasingMaterial;
            m_agent.SetDestination(m_playerObject.transform.position); //chase the player
            return;
        }
        m_agent.GetComponent<Renderer>().material = m_idleMaterial;
        if (m_agent.remainingDistance < 0.5f) 
        {
            m_agent.SetDestination(GetNextDestination()); //go back to initial pos (idle state)
        }
    }
}
