using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCScript : MonoBehaviour
{
    public string Address = "/example/1";

    public OSCReceiver Receiver;

    public PlayerController playerController;

    //public GameObject movingGround;
    [SerializeField] private Transform playerTransform;

    Quaternion rotation;
    Vector3 tmp, init;
    // Start is called before the first frame update
    void Start()
    {
        Receiver.Bind(Address, ReceivedMessage);

        void setInit(OSCMessage message)
        {
            if (message.ToQuaternion(out rotation))
            {
                init = rotation.eulerAngles;
                //Debug.Log("init " + init);
                ReceivedMessage(message);

            }
        }
    }

    private void ReceivedMessage(OSCMessage message)
    {
        Vector2 touch;
        
        if (message.ToVector2Double(out touch) == true)
        {
            playerController.OnMoveVector2(touch);
            Debug.Log(touch);
        }

        //Debug.Log(message.ToVector2Double(out touch));
        /*
        if (message.ToQuaternion(out rotation))
        {
            tmp = rotation.eulerAngles;
            movingGround.transform.rotation =
                Quaternion.Euler(
                         (Mathf.Round(tmp.x)), 0, (Mathf.Round(tmp.y))
                    ); // y and z axis are switched
            playerTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }*/

        if (message.ToQuaternion(out rotation))
        {
            tmp = rotation.eulerAngles;
            Debug.Log(tmp);
            if (tmp.x > 10 && tmp.x < 170) //right
            {
               //playerTransform.position = playerTransform.position + new Vector3((tmp.x / 500), 0f, 0f);
                playerTransform.Translate(Vector3.right/10);
            }
            if (tmp.x < 370 && tmp.x > 290) //left
            {
               //playerTransform.position = playerTransform.position - new Vector3(tmp.x / 8000, 0f, 0f);
               playerTransform.Translate(Vector3.left/10);
            }
            
            if (tmp.y < 370 && tmp.x > 290) //forward
            {
                //playerTransform.Translate(Vector3.back/10);
                playerTransform.position = playerTransform.position + new Vector3(0f, 0f, 0.1f);
            }
            if (tmp.y > 10 && tmp.x < 170) //backwards
            {
                //playerTransform.Translate(Vector3.forward/10);
                playerTransform.position = playerTransform.position - new Vector3(0f, 0f, 0.1f);
                //playerTransform.Translate(Vector3.right * (Time.deltaTime * 2.0f));
            }


        }

        

        //Debug.LogFormat("Received: {0}", message);
    }

    // Update is called once per frame
    void Update()
    {

    }
}


