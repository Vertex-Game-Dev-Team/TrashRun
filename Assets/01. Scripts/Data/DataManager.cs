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
    //������ ����
    private Data resetGameData = new Data();
    //�ʱ�ȭ ������ ����

    private string path = default;

    private void Awake()
    {
        #region �̱���
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

    //������ ����
    public void Save()
    {
        string data = JsonConvert.SerializeObject(gameData);
        File.WriteAllText(path + filename, data);
    }

    //������ �ҷ�����
    public void Load()
    {
        //�ش� ��ο� ������ ������ ==> ���� ����
        if (File.Exists(path + filename))
        {
            string data = File.ReadAllText(path + filename);
            gameData = JsonConvert.DeserializeObject<Data>(data);
        }
        else
        {
            //�����ϱ�
            Save();
        }
    }

    //������ �����ϱ�
    public void ResetData()
    {
        gameData = resetGameData;
        Save();
    }
}

public class Data
{
    //������ ������
    //Vector3, MonoBehaviour class ���� �Ұ�

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

