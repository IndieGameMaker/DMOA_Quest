using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerManager : MonoBehaviour
{
    public OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    public OVRInput.Controller rightController = OVRInput.Controller.RTouch;

    private OVRInput.Button handButton = OVRInput.Button.PrimaryHandTrigger;

    private CharacterController cc;
    public float moveSpeed = 1.5f;
    public Image bar;
    public TMP_Text text;

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
            bar.fillAmount = value;
            Debug.Log($"오른쪽 Index Value {value}");
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, rightController))
        {
            bar.fillAmount = 0.0f;
        }

        if (OVRInput.GetDown(handButton, leftController))
        {
            Debug.Log("왼쪽 핸드 그립");
        }

        if (OVRInput.GetDown(handButton, rightController))
        {
            Debug.Log("오른쪽 핸드 그립");
        }

        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, rightController))
        {
            Vector2 pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, rightController);
            Debug.Log($"Thumstick Pos = ({pos.x}/{pos.y})");

            Vector3 moveDir = new Vector3(pos.x, transform.position.y, pos.y);
            cc.SimpleMove(moveDir.normalized * moveSpeed);
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, rightController))
        {
            StartCoroutine(Haptic(0.5f));
        }

        Vector3 pos1 = OVRInput.GetLocalControllerPosition(rightController);
        Quaternion rot = OVRInput.GetLocalControllerRotation(rightController);

        text.text = $" pos=({pos1.x:0.00},{pos1.y:0.00},{pos1.z:0.00}) \n rot=({rot.eulerAngles.x:0.00}, {rot.eulerAngles.y:0.00}, {rot.eulerAngles.z:0.00})";
    }

    //헵틱 (진동효과)
    IEnumerator Haptic(float duration)
    {
        // (주파수, 진폭, 컨트롤러)
        OVRInput.SetControllerVibration(0.8f, 0.5f, rightController);

        yield return new WaitForSeconds(duration);

        OVRInput.SetControllerVibration(0, 0, rightController);
    }
}
