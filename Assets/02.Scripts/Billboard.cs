using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cameraTr;

    void LateUpdate()
    {
        transform.LookAt(cameraTr.position);
    }
}
