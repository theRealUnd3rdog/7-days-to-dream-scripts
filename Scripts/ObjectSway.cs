using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSway : MonoBehaviour
{

    [SerializeField] float smoothSpeed = 15f;

    private Quaternion curRot;

    // Start is called before the first frame update
    void Start()
    {
        curRot = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion targetRot = transform.parent.rotation;

        curRot = Quaternion.Lerp(curRot, targetRot, smoothSpeed * Time.deltaTime);
        transform.rotation = curRot;
    }
}
