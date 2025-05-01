using UnityEngine;

public class DeathMenu : PauseManager
{

    private bool _isDead = false;

    public JSONSaveLoad saveSystem;

/*    void Start()
    {
        
    }
*/

    void Update()
    {
        if (_isDead)
        {
            ButtonPause();
        }
        else ButtonResume();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Killzone"))
        {
            _isDead = true;
        }
    }

    private void DeathMenuOn()
    {
        ButtonResume();
        
        //saveSystem.LoadGame();
    }
}
