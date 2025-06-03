using UnityEngine;

public class DeathMenu : PauseManager
{
    public JSONSaveLoad saveSystem;

    void Update()
    {
        if (!_isPauseGame)
        {
            if (KillzoneCheck.Instance.isDead || OxygenController.Instance.oxygen <= 0)
            {
                ButtonPause();
            }
        }


        //else ButtonResume();
    }

}
