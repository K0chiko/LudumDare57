using UnityEngine;

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
