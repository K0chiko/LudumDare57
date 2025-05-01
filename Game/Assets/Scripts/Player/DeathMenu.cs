using UnityEngine;

public class DeathMenu : PauseManager
{

    private bool _isDead = false;

    public JSONSaveLoad saveSystem;


    void Update()
    {
        if (_isDead || GameManager.Instance.oxygen <= 0)
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

}
