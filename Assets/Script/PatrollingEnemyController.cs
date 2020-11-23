using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyController : EnemyController
{
    [SerializeField] private Transform[] m_wayPoints = null;

    private int m_wayPointIndex = 0;

    
    /* Protected - can only be called from inside its class, OR within a subclass which
     implements that class
     
     Virtual - a method which may be overridden by a subclass, but still contains an 
     implementation. An overriding member in a subclass may also call base.MethodName() 
     to invoke the underlying Virtual method before/after doing its own thing.
    */
    protected override Vector3 GetNextDestination()
    {
        Vector3 destination = m_wayPoints[m_wayPointIndex].position;
        m_wayPointIndex = (m_wayPointIndex + 1) % m_wayPoints.Length; // 2 % 2 = 0 -> kein overflow
        return destination;
    }
}
