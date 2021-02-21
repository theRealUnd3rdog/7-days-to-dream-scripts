using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start() 
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        CameraShaker.Instance.ShakeOnce(0.5f, 0.1f, 1f, 3f);
    }

    public void StartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
