using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDetector : MonoBehaviour
{
    [SerializeField] PlayerCamera cam;
    [SerializeField] Image crossHair;
    [SerializeField] Image[] Icons;
    [SerializeField] float range;

    private Transform _doorHandle;
    private Vector3 _direction;
    private float _angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
    }

    void CheckObject() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(crossHair.transform.position);

        if (Physics.Raycast(ray, out hit, range))
        {
            Door door = hit.transform.GetComponent<Door>();

            if (door != null)
            {
                Icons[0].enabled = true;
                crossHair.enabled = false;

                _doorHandle = door.transform.GetChild(0);

                if (Input.GetMouseButton(0))
                {
                    door.GetComponent<Rigidbody>().AddForce(_direction * 25f, ForceMode.Acceleration);
                    CheckAngle();

                    if (_angle > 100f) 
                    {
                        _direction = _doorHandle.position - cam.transform.position;
                    }
                    else
                        _direction = cam.transform.position - _doorHandle.position;

                    _doorHandle.localRotation = Quaternion.Euler(0, 0, 18f);
                }
                else 
                {
                    _doorHandle.localRotation = Quaternion.Euler(0, 0, 0f);
                }
            }
            else 
            {
                Icons[0].enabled = false;
                crossHair.enabled = true;

                if (_doorHandle == null)
                    return;

                _doorHandle.localRotation = Quaternion.Euler(0, 0, 0f);
            }
        }
        else 
        {
            crossHair.enabled = true;

            foreach (Image i in Icons) 
            {
                i.enabled = false;
            }
        }
    }

    void CheckAngle() 
    {
        //determine the direction
        Vector3 towardTarget = cam.transform.position - _doorHandle.position;
        Vector3 towardTargetProjected = Vector3.ProjectOnPlane(towardTarget, transform.up);

        _angle = Vector3.SignedAngle(transform.forward, towardTargetProjected, transform.up);
        _angle = Mathf.Abs(_angle);
    }
}
