//System
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;

//Unity
using Unity.VisualScripting;

//UnityEngine
using UnityEngine;

//Json
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("DataManagerInfo")]
    private readonly string filename = "UserData";

    private UserData gameData = new UserData();
    //������ ����
    private UserData resetGameData = new UserData();
    //�ʱ�ȭ ������ ����

    private string path = default;

    private void Awake()
    {
        #region �̱���
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/";

        #region TEST
        UserData userData = new UserData();
        userData.Crystal = 9999;
        userData.Coin = 3333;
        userData.UserName = "�׽���";

        WeaponData weaponData1 = new WeaponData();
        weaponData1.UpgradeCount = 9;

        userData.WeaponDataList[0] = weaponData1;

        WeaponData weaponData4 = new WeaponData();
        weaponData4.UpgradeCount = 3;

        userData.WeaponDataList[3] = weaponData4;

        gameData = userData;

        InitData();
        #endregion
    }

    // Data�� Json ���� �� ������ ����
    public void InitData()
    {
        string data = JsonConvert.SerializeObject(gameData, Formatting.Indented);
        File.WriteAllText(path + filename + ".json", data);

        data = File.ReadAllText(path + filename + ".json");
        gameData = JsonConvert.DeserializeObject<UserData>(data);
    }

    ////������ �ҷ�����
    //public void Load()
    //{
    //    //�ش� ��ο� ������ ������ ==> ���� ����
    //    if (File.Exists(path + filename))
    //    {
    //        string data = File.ReadAllText(path + filename + ".json");
    //        gameData = JsonConvert.DeserializeObject<UserData>(data);
    //    }
    //    else
    //    {
    //        //�����ϱ�
    //        Save();
    //    }
    //}

    public UserData GetSaveUserData()
    {
        return gameData;
    }

    //������ �����ϱ�
    public void ResetData()
    {
        gameData = resetGameData;
        InitData();
    }
}