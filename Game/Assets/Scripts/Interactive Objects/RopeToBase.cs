using StarterAssets;
using UnityEngine;
using System.Collections;

public class RopeToBase : MonoBehaviour
{
    public GameObject player;
    private GameManager gameManager;
    private BoxCollider boxCollider;
    private UpgradeManager upgradeManager;

    public AudioClip airTankFillUpClip;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        upgradeManager = GameObject.Find("GameManager").GetComponent<UpgradeManager>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            gameManager.saleWindow.SetActive(true);
            upgradeManager.isUpgrade = true;
            gameManager.oxygen = gameManager.oxygenMax;

            // Play sound of air tank filling up
            AudioSource airTankFillUpSFX = player.AddComponent<AudioSource>();
            airTankFillUpSFX.clip = airTankFillUpClip;
            airTankFillUpSFX.Play();
        }
    }
}
