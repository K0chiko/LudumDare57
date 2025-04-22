using UnityEngine;

public class StartScript : MonoBehaviour
{
    public GameObject startUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startUI.gameObject.SetActive(false);
        }
    }
}
