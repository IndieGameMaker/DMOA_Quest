using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private LineRenderer laser;
    private RaycastHit hit;

    [Range(5.0f, 20.0f)]
    public float maxDistance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 다른 오브젝트와 충돌했을 경우
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            laser.SetPosition(1, new Vector3(0, 0, hit.distance));
        }
        else
        {
            laser.SetPosition(1, new Vector3(0, 0, maxDistance));
        }
    }
}
