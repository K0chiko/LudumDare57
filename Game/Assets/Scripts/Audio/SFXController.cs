using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXController : MonoBehaviour
{
    [Header("Ссылки на игровые сущности")]
    public AudioSource breatheSourceA;
    public AudioSource breatheSourceB;
    public AudioClip[] breathingClips;
    public OxygenController oxygenController;
    public AudioSource uiSourceSFX;
    public AudioClip[] uiAudio;
    [Tooltip("Чтобы брать состояние кнопок")]
    public GameObject shop;
    [Tooltip("Файл настроек")]
    public JSONSettingsData settingsData;

    [Space]
    [Header("Длительность затухания дыхания")]
    public float fadeDuration = 1f;

    private int currentClipIndex = 0;

   // private int currentClipIndex = -1;
    private bool isSourceAActive = true;
    private float generalVolume;
    private float musicVolume;
    private float environmentVolume;
    private float sfxVolume;

    //private float fadeTimer = 0f;

    void Start()
    {
        generalVolume = settingsData.settingsMenu.generalVolume;
        musicVolume = settingsData.settingsMenu.musicVolume;
        environmentVolume = settingsData.settingsMenu.environmentVolume;
        sfxVolume = settingsData.settingsMenu.sfxVolume;

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            float oxygenNormalized = Mathf.Clamp01(oxygenController.oxygen / oxygenController.oxygenMax);
            currentClipIndex = GetClipIndex(oxygenNormalized);
            breatheSourceA.clip = breathingClips[currentClipIndex];
            breatheSourceA.volume = generalVolume * sfxVolume;
            breatheSourceA.Play();
            breatheSourceB.volume = 0f;

            uiSourceSFX.volume = sfxVolume * generalVolume;
            uiSourceSFX.clip = uiAudio[currentClipIndex];
        }



    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            float oxygenNormalized = Mathf.Clamp01(oxygenController.oxygen / 100f);
            int newClipIndex = GetClipIndex(oxygenNormalized);

            if (newClipIndex != currentClipIndex)
            {
                currentClipIndex = newClipIndex;
                StartCoroutine(CrossfadeTo(breathingClips[currentClipIndex]));
            }
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
        AudioSource fromSource = isSourceAActive ? breatheSourceA : breatheSourceB;
        AudioSource toSource = isSourceAActive ? breatheSourceB : breatheSourceA;
        isSourceAActive = !isSourceAActive;

        toSource.clip = newClip;
        toSource.Play();

        float time = 0f;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            fromSource.volume = Mathf.Lerp(generalVolume * sfxVolume, 0f, t);
            toSource.volume = Mathf.Lerp(0f, generalVolume * sfxVolume, t);
            time += Time.deltaTime;
            yield return null;
        }

        fromSource.volume = 0f;
        fromSource.Stop();
        toSource.volume = generalVolume * sfxVolume;
    }
    #endregion
}


