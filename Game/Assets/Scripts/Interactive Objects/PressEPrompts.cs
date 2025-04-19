using UnityEngine;

[CreateAssetMenu(fileName = "PressEPrompts", menuName = "Scriptable Objects/PressEPrompts")]
public class PressEPrompts : ScriptableObject
{
    public string[] promptText;
    public string altPromptText;
}
