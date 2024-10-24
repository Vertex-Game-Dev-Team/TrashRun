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
    //데이터 생성
    private UserData resetGameData = new UserData();
    //초기화 데이터 생성

    private string path = default;

    private void Awake()
    {
        #region 싱글톤
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
        userData.UserName = "테스터";

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

    // Data를 Json 저장 및 변수에 저장
    public void InitData()
    {
        string data = JsonConvert.SerializeObject(gameData, Formatting.Indented);
        File.WriteAllText(path + filename + ".json", data);

        data = File.ReadAllText(path + filename + ".json");
        gameData = JsonConvert.DeserializeObject<UserData>(data);
    }

    ////데이터 불러오기
    //public void Load()
    //{
    //    //해당 경로에 파일이 없을때 ==> 최초 접속
    //    if (File.Exists(path + filename))
    //    {
    //        string data = File.ReadAllText(path + filename + ".json");
    //        gameData = JsonConvert.DeserializeObject<UserData>(data);
    //    }
    //    else
    //    {
    //        //저장하기
    //        Save();
    //    }
    //}

    public UserData GetSaveUserData()
    {
        return gameData;
    }

    //데이터 리셋하기
    public void ResetData()
    {
        gameData = resetGameData;
        InitData();
    }
}