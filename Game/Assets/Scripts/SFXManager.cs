using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource sourceA;
    public AudioSource sourceB;
    public AudioClip[] breathingClips;
    public GameManager gameManager;
    public float fadeDuration = 1f;



    private int currentClipIndex = -1;
    private bool isSourceAActive = true;
    private float fadeTimer = 0f;

    void Start()
    {
        // ������������� ������� �����
        float oxygenNormalized = Mathf.Clamp01(gameManager.oxygen / 100f);
        currentClipIndex = GetClipIndex(oxygenNormalized);
        sourceA.clip = breathingClips[currentClipIndex];
        sourceA.volume = 1f;
        sourceA.Play();
        sourceB.volume = 0f;
    }

    void Update()
    {
        float oxygenNormalized = Mathf.Clamp01(gameManager.oxygen / 100f);
        int newClipIndex = GetClipIndex(oxygenNormalized);

        if (newClipIndex != currentClipIndex)
        {
            currentClipIndex = newClipIndex;
            StartCoroutine(CrossfadeTo(breathingClips[currentClipIndex]));
        }
    }

    int GetClipIndex(float oxygenNormalized)
    {
        if (oxygenNormalized <= 0.05) {
            return breathingClips.Length - 1;
        }
        return Mathf.Clamp(4 - Mathf.FloorToInt(oxygenNormalized * 5f), 0, 4);
    }

    System.Collections.IEnumerator CrossfadeTo(AudioClip newClip)
    {
        AudioSource fromSource = isSourceAActive ? sourceA : sourceB;
        AudioSource toSource = isSourceAActive ? sourceB : sourceA;
        isSourceAActive = !isSourceAActive;

        toSource.clip = newClip;
        toSource.Play();

        float time = 0f;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            fromSource.volume = Mathf.Lerp(1f, 0f, t);
            toSource.volume = Mathf.Lerp(0f, 1f, t);
            time += Time.deltaTime;
            yield return null;
        }

        fromSource.volume = 0f;
        fromSource.Stop();
        toSource.volume = 1f;
    }
}


