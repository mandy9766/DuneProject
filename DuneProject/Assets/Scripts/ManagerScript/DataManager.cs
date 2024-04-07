using System.Collections;

using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using UnityEngine;
using System.IO;
using TMPro;


public class DataManager : Singleton<DataManager>
{
    private TextAsset data;
    private AllData datas;
    public PlayerData nowPlayer = new PlayerData();
    public SettingData nowSetting;
    public string path;
    public string settingPath;
    public int nowSlot;
    
    void Start()
    {
        path = Application.persistentDataPath +"/save";
        settingPath = path+"Setting";
    }

    void Update()
    {
        Debug.Log(nowPlayer.name);
    }
    public void dataManagerInitialize()
    {
        data = Resources.Load("Json/Dune") as TextAsset;
        datas = JsonUtility.FromJson<AllData>(data.text);
       
        Debug.Log("데이터매니저 초기 로드");
    }
    public void SaveSetting()
    {
        string nowSettingData = JsonUtility.ToJson(nowSetting);
        File.WriteAllText(settingPath,nowSettingData);
    }
    public void LoadSetting()
    {
        if(CheckSettingFileExist())
        {
            string nowSettingData = File.ReadAllText(settingPath);
            nowSetting = JsonUtility.FromJson<SettingData>(nowSettingData);
        }
        else
        {
            nowSetting = new SettingData
            {
                volume = 0.6f
            };
        }
    }
    public void SaveData()
    {
        string nowPlayerData = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path+nowSlot.ToString(),nowPlayerData);
    }
    public void LoadData()
    {
        string nowPlayerData = File.ReadAllText(path+nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(nowPlayerData);
    }
    public bool CheckFileExist(int number)
    {
        return File.Exists(path+number);
    }
    public bool CheckSettingFileExist()
    {
        return File.Exists(settingPath);
    }
    public void SlotSetting(int number)
    {
        nowSlot = number;
    }
    public float GetVolume()
    {
        return nowSetting.volume;
    }
    public void SetVolume(float value)
    {
        nowSetting.volume = value;
        SaveData();
    }
    /// <summary>
    /// 새로운 플레이어의 초기값을 설정합니다.
    /// Activated값은 모두 0
    /// 처음 보유한 유닛/스킬은 0,1 (유닛/스킬 ID값)으로 초기화 합니다.
    /// </summary>
    /// <param name="name"></param>
    public void NewDataSetting(string name)
    {
        nowPlayer.name = name;
        nowPlayer.playerDataId = nowSlot;
        nowPlayer.level = 1;
        nowPlayer.stageNum = 1;
        nowPlayer.activatedSpicyUnitId = 0;
        nowPlayer.activatedWormUnitId = 0;
        nowPlayer.activatedLandClearUnitId = 0;
        nowPlayer.activatedShoutSkillId = 0;
        nowPlayer.spicyUnitAvaiable = new List<int> {0,1};
        nowPlayer.wormUnitAvailable = new List<int> {0,1};
        nowPlayer.landClearingUnitAvailable = new List<int> {0,1};
        nowPlayer.shoutSkillAvailable = new List<int> {0,1};
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
    public void DataDelete()
    {
        File.Delete(path+nowSlot);
        DataClear();
    }
}

#region 데이터클래스

[System.Serializable]
public class AllData
{
    public MapData[] stage;
    public SoundData[] sound;
    public spicyUnit[] spicyUnit;
    public wormUnit[] wormInit;
    public landClearingUnit[] landClearUnit;
    public shoutSkill[] shoutSkill;
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
    public float volume;
}

[System.Serializable] 
public class spicyUnit
{
    public int spicyUnitId;

}

[System.Serializable] 
public class wormUnit
{
    public int wormUnitId;

}

[System.Serializable] 
public class landClearingUnit
{
    public int landClearingUnitId;

}
[System.Serializable] 
public class shoutSkill
{
    public int shoutSkillId;
}

[System.Serializable]
public class PlayerData
{
    public int playerDataId;
    public string name;
    public int level;
    public int stageNum;
    public int activatedSpicyUnitId;
    public int activatedWormUnitId;
    public int activatedLandClearUnitId;
    public int activatedShoutSkillId;


    
    public List<int> spicyUnitAvaiable;
    public List<int> wormUnitAvailable;
    public List<int> landClearingUnitAvailable;
    public List<int> shoutSkillAvailable;


}

#endregion 