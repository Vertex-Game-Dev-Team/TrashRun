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
    public static DataManager instance;

    [Header("DataManagerInfo")]
    [SerializeField] private string filename = default;

    public Data gameData = new Data();
    //데이터 생성
    private Data resetGameData = new Data();
    //초기화 데이터 생성

    private string path = default;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/";
    }

    //데이터 저장
    public void Save()
    {
        string data = JsonConvert.SerializeObject(gameData);
        File.WriteAllText(path + filename, data);
    }

    //데이터 불러오기
    public void Load()
    {
        //해당 경로에 파일이 없을때 ==> 최초 접속
        if (File.Exists(path + filename))
        {
            string data = File.ReadAllText(path + filename);
            gameData = JsonConvert.DeserializeObject<Data>(data);
        }
        else
        {
            //저장하기
            Save();
        }
    }

    //데이터 리셋하기
    public void ResetData()
    {
        gameData = resetGameData;
        Save();
    }
}

public class Data
{
    //저장할 데이터
    //Vector3, MonoBehaviour class 저장 불가

    public string userName = "00000";
    public int uesrProfile = 0;
    public int coin = 0;
    public int crystal = 0;

    public float musicSettingValue = 100;
    public float sfxSettingValue = 100;
    List<WeaponData> weaponDatas = new List<WeaponData>();

    public Data()
    {
        for(int i = 0; i < weaponDatas.Count; i++)
        {
            weaponDatas.Add(new WeaponData());
        }
    }

    public class WeaponData
    {
        int upgradeCount = 0;
    }

}

