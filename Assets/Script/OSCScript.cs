﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCScript : MonoBehaviour
{
    public string Address = "/example/1";

    public OSCReceiver Receiver;

    public PlayerController playerController;

    public GameObject movingGround;
    public Rigidbody movingGroundRigidbody;
    [SerializeField] private Transform playerTransform;

    Quaternion rotation;
    Vector3 tmp, init;
    // Start is called before the first frame update
    void Start()
    {
        Receiver.Bind(Address, setInit);

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


        //Debug.Log(message.ToVector2Double(out touch));

        //if (message.ToQuaternion(out rotation))
        //{
        //tmp = rotation.eulerAngles;
        //var result = Mathf.Lerp(0, 360, Mathf.InverseLerp(0, 360, tmp.x));

        /*int anotherTmpX = (int)tmp.x / 10;
        tmp.x = anotherTmpX * 10;
        int anotherTmpY = (int)tmp.y / 10;
        tmp.y = anotherTmpY * 10;*/
        /*movingGroundRigidbody.MoveRotation(Quaternion.Euler(
            (Mathf.Round(-tmp.x)), 0, (Mathf.Round(tmp.y))));*/
        /*movingGround.transform.rotation =
            Quaternion.Euler(
                     (Mathf.Round(tmp.x)), 0, (Mathf.Round(tmp.y))
                ); // y and z axis are switched*/
        //playerTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        //movingGroundRigidbody.GetComponent<Rigidbody>().AddForce(1000.0f, 1000.0f, 1000.0f);

        //}
        if (message.ToVector2Double(out touch) == true)
        {
            playerController.OnMoveVector2(touch);
            Debug.Log(touch);
        }

        //Debug.LogFormat("Received: {0}", message);
    }

    // Update is called once per frame
    void Update()
    {

    }
}


