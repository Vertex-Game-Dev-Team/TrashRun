// # Systems
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


// # Unity
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] private IngameUIMnager UIManager;

    #region player
    [SerializeField] private GameObject playerObject;
    private Player player;
    private Vector3 playerSpawnPosition = new Vector3(-5f, 1.2f, 0);

    public Player Player => player;
    #endregion

    #region weapon
    [SerializeField] private GameObject[] weaponPrefabs;
    private Weapon weapon;

    public Weapon Weapon => weapon;
    #endregion

    #region monster
    [SerializeField] private GameObject bossPrefab;

    private bool isBoss;
    public bool IsBoss => isBoss;
    #endregion

    [SerializeField] private Animator hitAnim;

    private void Awake()
    {
        instance = this;

        player = playerObject.GetComponent<Player>();

        int weaponID = PlayerPrefs.GetInt(PlayerPrefsKey.WeaponID);
        GameObject weapon = Instantiate(weaponPrefabs[weaponID], playerObject.transform);
        this.weapon = weapon.GetComponent<Weapon>();
    }

    public void GetScrap(float value)
    {
        UIManager.UpGaugeValue(value);
    }

    public void SetBoostState(bool on)
    {
        player.SetBoostState(on);
        weapon.SetBoostState(on);
    }

    public void ChangeToBossStage()
    {
        if (isBoss) return;

        Vector3 spawnPos = new Vector3(Camera.main.transform.position.x + 16, 3f, 0);

        GameObject boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        isBoss = true;

        Camera.main.transform.parent.GetComponent<CameraController>().SetBossCamera();
    }

    public void ReturnAtBossStage()
    {
        isBoss = false;
    }

    public void PlayerHit()
    {
        Camera.main.transform.GetComponent<CameraShake>().StartShake(0.3f);
        hitAnim.SetTrigger("Play");
    }
}
