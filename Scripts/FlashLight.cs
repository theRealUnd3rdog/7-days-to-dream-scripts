using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashLight : MonoBehaviour
{
    [SerializeField] Light spotlight;
    [SerializeField] Material _lens;

    private bool isLight = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            isLight = !isLight;
        }

        if (isLight) 
        {
            spotlight.enabled = true;
            _lens.SetFloat("_EmissiveExposureWeight", 0f);
        }
        else if (!isLight) 
        {
            spotlight.enabled = false;
            _lens.SetFloat("_EmissiveExposureWeight", 1f);
        }
            
    }
}
