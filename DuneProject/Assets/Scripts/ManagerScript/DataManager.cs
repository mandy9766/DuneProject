using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using UnityEngine;
using System.IO;

public class DataManager : Singleton<DataManager>
{
    private TextAsset data;// Start is called before the first frame update
    private AllData datas;

    public TextAsset setting;
    public AllSetting settings;

    PlayerData nowPlayer = new PlayerData();
    string path;
    string fileName = "save";
    void Start()
    {
        path = Application.persistentDataPath +"/";
        print(File.Exists(path + fileName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void dataManagerInitialize()
    {
        data = Resources.Load("Json/Dune") as TextAsset;
        datas = JsonUtility.FromJson<AllData>(data.text);
        setting = Resources.Load("Json/Setting") as TextAsset;
        settings = JsonUtility.FromJson<AllSetting> (setting.text);
        SoundManager.Instance.volumeSetting();
        Debug.Log("데이터매니저 초기 로드");
    }

    public void SaveData()
    {
        string nowPlayerData = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path+fileName,nowPlayerData);
    }
    public void LoadData()
    {
        string nowPlayerData = File.ReadAllText(path+fileName);
        nowPlayer = JsonUtility.FromJson<PlayerData>(nowPlayerData);
    }
}

#region 데이터클래스

[System.Serializable]
public class AllData
{
    public MapData[] stage;
    public SoundData[] sound;
}

[System.Serializable]
public class AllSetting
{
    public SettingData[] setting;
   
}


[System.Serializable]
public class MapData
{
    public int stageID;
    public string stageName;
    public int x;
    public int y;
}

[System.Serializable]
public class SoundData
{
    public int soundId;
    public string soundName;
}

[System.Serializable]
public class SettingData
{
    public int settingId;
    public int volume;
}

[System.Serializable] 
public class spicyUnit
{
    public int spicyUnitId;
    public bool spicyUnitAvailable;

}

[System.Serializable] 
public class wormUnit
{
    public int wormUnitId;
    public bool wormUnitAvailable;

}

[System.Serializable] 
public class landClearingUnit
{
    public int landClearingUnitId;
    public bool landClearingUnitAvailable;

}
[System.Serializable] 
public class shoutSkill
{
    public int shoutSkillId;
    public bool shoutSkillAvailable;
}

[System.Serializable]
public class PlayerData
{
    public int playerDataId;
    public string name;
    public int level;
    public int stageNum;
    public int activatedSpicyUnitId;
}

#endregion 