using UnityEngine;
using UnityEngine.SceneManagement;

public class BreathAudioController : MonoBehaviour
{
    [Header("Ссылки на игровые сущности")]
    public AudioSource breatheSourceA;
    public AudioSource breatheSourceB;
    //public AudioClip[] breathingClips;
    public OxygenController oxygenController;
    public SettingsMenu settingsMenu;


    [Space]
    [Header("Длительность затухания дыхания")]
    public float fadeDuration = 1f;

    private int currentClipIndex = 0;

   // private int currentClipIndex = -1;
    private bool isSourceAActive = true;


    //private float fadeTimer = 0f;

    void Start()
    {


        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            float oxygenNormalized = Mathf.Clamp01(oxygenController.oxygen / oxygenController.oxygenMax);
            currentClipIndex = GetClipIndex(oxygenNormalized);
            //breatheSourceA.clip = breathingClips[currentClipIndex];
            breatheSourceA.clip = SoundManager.PlayBreathSound(SoundType.BREATH, currentClipIndex);
            breatheSourceA.volume = 1f;
            breatheSourceA.Play();
            breatheSourceB.volume = 0f;

/*            uiSourceSFX.volume = settingsMenu.sfxVolume * settingsMenu.generalVolume;
            uiSourceSFX.clip = uiAudio[currentClipIndex];*/
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
                StartCoroutine(CrossfadeTo(SoundManager.PlayBreathSound(SoundType.BREATH, currentClipIndex)));
            }
        }
         
    }




    #region ShopAudio
    public void UpgradeAudio(bool isUpgrade)
    {
        if (isUpgrade)
        {
            SoundManager.PlaySound(SoundType.SUCCESS);
        }
        else
        {
            SoundManager.PlaySound(SoundType.ERROR);
        }
/*        uiSourceSFX.clip = uiAudio[isUpgrade ? 1 : 0];
        uiSourceSFX.Play();*/
    }

    #endregion

    #region Fading
    int GetClipIndex(float oxygenNormalized)
    {
/*        if (oxygenNormalized <= 0.05) {
            return breathingClips.Length - 1;
        }*/
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


