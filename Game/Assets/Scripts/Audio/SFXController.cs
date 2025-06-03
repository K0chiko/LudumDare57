using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioSource sourceA;
    public AudioSource sourceB;
    public AudioClip[] breathingClips;
    public OxygenController gameManager;
    public float fadeDuration = 1f;

    [Tooltip("Чтобы брать состояние кнопок")]
    public GameObject shop;

    public AudioSource uiSourceSFX;
    public AudioClip[] uiAudio;

    private int currentClipIndex = 0;

   // private int currentClipIndex = -1;
    private bool isSourceAActive = true;
    private float sfxVolume;

    //private float fadeTimer = 0f;

    void Start()
    {
        float oxygenNormalized = Mathf.Clamp01(gameManager.oxygen / gameManager.oxygenMax);
        currentClipIndex = GetClipIndex(oxygenNormalized);
        sourceA.clip = breathingClips[currentClipIndex];
        sourceA.volume = 1f;
        sourceA.Play();
        sourceB.volume = 0f;

        uiSourceSFX.volume = sfxVolume;
        uiSourceSFX.clip = uiAudio[currentClipIndex];
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

    public void ChangeVolume(float sfxVolume)
    {
        uiSourceSFX.volume = sfxVolume / 100f;
        Debug.Log(uiSourceSFX.volume);
    }


    #region ShopAudio
    public void UpgradeAudio(bool isUpgrade)
    {
        uiSourceSFX.clip = uiAudio[isUpgrade ? 1 : 0];
        uiSourceSFX.Play();
    }

    #endregion

    #region Fading
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
    #endregion
}


