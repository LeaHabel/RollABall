using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCScript : MonoBehaviour
{
    public string Address = "/example/1";


    public OSCReceiver Receiver;

    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        Receiver.Bind(Address, ReceivedMessage);
    }
    
    private void ReceivedMessage(OSCMessage message)
    {
        Vector2 touch;
        
        //Debug.Log(message.ToVector2Double(out touch));
        if (message.ToVector2Double(out touch) == true)
        {
            playerController.OnMoveVector2(touch);
            //Debug.Log(touch);
        }
        
        //Debug.LogFormat("Received: {0}", message);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
