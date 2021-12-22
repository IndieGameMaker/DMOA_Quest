using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private LineRenderer laser;
    private RaycastHit hit;

    [Range(5.0f, 20.0f)]
    public float maxDistance = 10.0f;

    public GameObject laserMakerPrefab;
    private GameObject laserMaker;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laserMaker = Instantiate<GameObject>(laserMakerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        // 다른 오브젝트와 충돌했을 경우
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            laser.SetPosition(1, new Vector3(0, 0, hit.distance));
            laserMaker.transform.position = hit.point + (Vector3.up * 0.02f);
            laserMaker.transform.rotation = Quaternion.LookRotation(hit.normal);

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                StartCoroutine(Teleport(hit.point));
            }
        }
        else
        {
            laser.SetPosition(1, new Vector3(0, 0, maxDistance));
            laserMaker.transform.position = transform.position + (transform.forward * maxDistance);
            laserMaker.transform.rotation = Quaternion.LookRotation(transform.forward);
        }
    }

    IEnumerator Teleport(Vector3 pos)
    {
        OVRScreenFade.instance.fadeTime = 0.0f;
        OVRScreenFade.instance.FadeOut();

        transform.root.position = pos;

        yield return new WaitForSeconds(0.15f);

        OVRScreenFade.instance.fadeTime = 0.2f;
        OVRScreenFade.instance.FadeIn();
    }
}
