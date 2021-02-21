using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    [SerializeField] AudioSource breath;
    [SerializeField] AudioSource natureSound;

    public void PlayBreath() 
    {
        breath.Play();
    }

    public void PlayNature() 
    {
        natureSound.Play();
    }
}
