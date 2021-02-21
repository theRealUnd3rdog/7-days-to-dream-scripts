using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioSource footSteps;

    public void PlayFootSteps() 
    {
        footSteps.Play();
    }
}
