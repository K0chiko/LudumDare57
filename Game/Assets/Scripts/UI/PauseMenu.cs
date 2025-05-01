using System.Diagnostics.Contracts;
using StarterAssets;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : PauseManager
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_isPauseGame) ButtonResume();
            else ButtonPause();
        }    
    }
}
