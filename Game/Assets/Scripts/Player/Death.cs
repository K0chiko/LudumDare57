using UnityEngine;

public class Death : MonoBehaviour
{

    public JSONSaveLoad saveSystem;

/*    void Start()
    {
        
    }


    void Update()
    {

    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Killzone"))
        {
            saveSystem.LoadGame();
        }
    }
}
