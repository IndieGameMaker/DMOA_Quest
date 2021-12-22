using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    public OVRInput.Controller rightController = OVRInput.Controller.RTouch;

    private OVRInput.Button handButton = OVRInput.Button.PrimaryHandTrigger;

    private CharacterController cc;
    public float moveSpeed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
            1. Combine 방식
                PrimaryIndexTrigger
                SecondaryIndexTrigger

            2. Individual 방식
                PrimaryIndexTrigger, LTouch  - 왼쪽 컨트롤러
                PrimaryIndexTrigger, RTouch  - 오른쪽 컨트롤러

            3. Raw 방식
            https://developer.oculus.com/documentation/unity/unity-ovrinput/

        */

        //Invidual 방식
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, leftController))
        {
            Debug.Log("왼쪽 컨트롤러 Index Trigger Down");
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, rightController))
        {
            Debug.Log("오른쪽 컨트롤러 Index Trigger Down");

            float value = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, rightController);
            Debug.Log($"오른쪽 Index Value {value}");
        }

        if (OVRInput.GetDown(handButton, leftController))
        {
            Debug.Log("왼쪽 핸드 그립");
        }

        if (OVRInput.GetDown(handButton, rightController))
        {
            Debug.Log("오른쪽 핸드 그립");
        }
    }
}
