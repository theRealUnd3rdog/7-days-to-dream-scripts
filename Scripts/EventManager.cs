using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityCore.Audio;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Data;
using UnityEngine.UIElements.Experimental;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objToActivate;
    [SerializeField] private GameObject[] objToDeactivate;
    [SerializeField] private AudioSource[] audio;

    [Header("Super Natural forces")]
    [SerializeField] float superNaturalForce;

    [Header("Light manipulation")]
    [SerializeField] private HDAdditionalLightData[] lightData;
    [SerializeField] float lightDelay;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;

    bool lightFlicker = false;
    bool randomLight = true;
    bool randomForce = true;

    bool pushback = false;
    private float timeVal = 3;

    void Start() 
    {
        if (objToActivate == null)
            return;

        if (objToDeactivate == null)
            return;

        if (lightData == null)
            return;

        if (audio == null)
            return;
    }

    void Update() 
    {
        if (lightFlicker) 
        {
            if (lightData != null)
            {
                if (randomLight)
                    StartCoroutine(lightFlickerDelay());
            }
        }
    }

    public void playtheAudio() 
    {
        foreach (AudioSource sound in audio) 
        {
            sound.Play();
        }
    }

    public void ActivateRandomForce() 
    {
        StartCoroutine(RandomForce());
    }

    IEnumerator RandomForce() 
    {
        Debug.Log("RandomForce");
        randomForce = true;
        var playerpos = this.GetComponent<EventTrigger>().playerCam.transform.position;

        foreach (GameObject ob in objToActivate) 
        {
            ob.GetComponent<Rigidbody>().useGravity = true;
            ob.GetComponent<Rigidbody>().isKinematic = false;

            Vector3 dir = playerpos - ob.transform.position;

            if (randomForce)
                ob.GetComponent<Rigidbody>().AddForce(superNaturalForce * dir);
        }

        yield return new WaitForSeconds(1.5f);
        randomForce = false;
    }

    public void PlayerReduceDrag() 
    {
        Rigidbody player = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody>();
        player.drag = 2.5f;

        PlayerMovement movement = FindObjectOfType<PlayerMovement>();
        movement.force = 3000;
    }

    public void pushPlayer() 
    {
        StartCoroutine(pushBackPlayer());
    }

    IEnumerator pushBackPlayer() 
    {
        Debug.Log("PushingPlayerBack");
        pushback = true;

        Rigidbody player = FindObjectOfType<PlayerMovement>().GetComponent<Rigidbody>();
        player.drag = 0;
        player.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        var playerpos = this.GetComponent<EventTrigger>().playerCam.transform.position;
        Vector3 dir = playerpos - objToActivate[0].transform.position;

        float timeElapsed = 0;

        do
        {
            timeElapsed += Time.deltaTime;

            player.AddForce(dir * 70f, ForceMode.Impulse);
            Debug.Log(timeElapsed);

            yield return null;
        }
        while (timeElapsed < timeVal);

        player.drag = 5;
        player.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

        pushback = false;
    }

    public void DisableKinematic() 
    {
        foreach (GameObject ob in objToActivate) 
        {
            ob.GetComponent<Rigidbody>().isKinematic = false;
            ob.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void ActivateRoom() 
    {
        foreach (GameObject ob in objToActivate) 
        {
            ob.SetActive(true);
        }

        foreach (GameObject od in objToDeactivate) 
        {
            od.SetActive(false);
        }

    }

    public void ActivateSphere()
    {
        foreach (GameObject ob in objToActivate)
        {
            ob.SetActive(true);
        }
    }

    public void ActivateLightFlicker() 
    {
        lightFlicker = true; 
    }

    public void SwitchLightOff() 
    {
        foreach (HDAdditionalLightData ld in lightData)
        {
            ld.GetComponent<Light>().enabled = false;
        }
    }

    public void SwitchLightOffDelay() 
    {
        StartCoroutine(LightOffDelay());
    }

    public void audioDelay() 
    {
        StartCoroutine(lightAudioDelay());
    }

    IEnumerator lightAudioDelay() 
    {
        for (int x = 0; x < audio.Length; x++)
        {
            audio[x].GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator LightOffDelay() 
    {
        for (int i = 0; i < lightData.Length; i++) 
        {
            lightData[i].GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator lightFlickerDelay() 
    {
        randomLight = false;
        var intensities = new float[2];

        intensities[0] = minIntensity;
        intensities[1] = maxIntensity;

        Debug.Log("LightDelay");
        yield return new WaitForSeconds(lightDelay);

        var index = Random.Range(0, intensities.Length);

        foreach (HDAdditionalLightData ld in lightData)
        {
            ld.intensity = intensities[index];
        }

        randomLight = true;
    }

    public void PlayLightFlicker() 
    {
        AudioController.instance.PlayAudio(UnityCore.Audio.AudioType.LightFlicker);
    }

    public void StopAudioTrack1() 
    {
        AudioController.instance.StopAudio(UnityCore.Audio.AudioType.SoundTrack_01, true);
    }

    public void PlayWindSound() 
    {
        AudioController.instance.PlayAudio(UnityCore.Audio.AudioType.Wind);
    }

    public void PlayHeartBeatSound() 
    {
        AudioController.instance.PlayAudio(UnityCore.Audio.AudioType.HeartBeat_02, true, 1f);
    }

    public void PlaySoundTrack3() 
    {
        AudioController.instance.PlayAudio(UnityCore.Audio.AudioType.SoundTrack_03);
    }

    public void NextScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
