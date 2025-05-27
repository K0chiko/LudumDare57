using UnityEngine;

public class PauseMenu : PauseManager
{
    [SerializeField] private KeyCode editorKey = KeyCode.R;
    [SerializeField] private KeyCode buildKey = KeyCode.Escape;

    private KeyCode _pauseKey;

    private void Awake()
    {
#if UNITY_EDITOR
        _pauseKey = editorKey;
#else
        _pauseKey = buildKey;
#endif
    }
    void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            if (_isPauseGame) ButtonResume();
            else ButtonPause();
        }    
    }
}
