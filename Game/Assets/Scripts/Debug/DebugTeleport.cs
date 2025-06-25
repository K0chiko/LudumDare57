using UnityEngine;

public class DebugTeleport : MonoBehaviour
{
    [Tooltip("—сылки на точки, к которым можно телепортироватьс€")]
    public Transform[] teleportPoints;

    [Tooltip("ќбъект, который будет телепортироватьс€ (обычно игрок)")]
    public Transform target;

    void Update()
    {
        for (int i = 0; i < teleportPoints.Length && i < 9; i++) // поддержка клавиш 1Ц9
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (teleportPoints[i] != null && target != null)
                {
                    CharacterController cc = target.GetComponent<CharacterController>();
                    if (cc != null)
                    {
                        cc.enabled = false;
                        target.position = teleportPoints[i].position;
                        target.rotation = teleportPoints[i].rotation;
                        cc.enabled = true;
                    }

                }
            }
        }
    }
}
