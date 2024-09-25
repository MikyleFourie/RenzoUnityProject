using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    
    
    private void Start()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;//lock frame rate;

        
    }


    // Update is called once per frame
    void Update()
    {
        

    }

    //Buttons
    public void OnResumeClick()
    {
        
        
    }

    public void OnQuitGameClick()
    {
        Application.Quit();
    }

    
}
