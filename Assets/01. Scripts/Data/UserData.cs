public class UserData
{
    //������ ������
    //Vector3, MonoBehaviour class ���� �Ұ�

    public string UserName = "Dummy";
    public int UserProfile = 0;
    public int Coin = 0;
    public int Crystal = 0;

    //public int MAXSettingValue = 100;
    //public int BGMSettingValue = 100;
    //public int SFXSettingValue = 100;

    public WeaponData[] WeaponDataList = new WeaponData[4];

    public UserData()
    {
        WeaponDataList = new WeaponData[4];
        for (int i= 0; i < WeaponDataList.Length; i++)
        {
            WeaponDataList[i] = new WeaponData();
        }
    }
}

public class WeaponData
{
    public bool IsUnlock = false;
    public int UpgradeCount = 0;
}