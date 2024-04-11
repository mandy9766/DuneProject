using System.Collections;

using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;


public class DataManager : Singleton<DataManager>
{
    private TextAsset dataJson;
    private AllData allData;
    public PlayerData nowPlayer = new PlayerData();
    public SettingData nowSetting;
    public string path;
    public string settingPath;
    public int nowSlot;
    
    void Start()
    {
        DataManagerInitialize();
        DataPreprocessing();
    }

    void Update()
    {
        Debug.Log(nowPlayer.name);
    }
    public void DataManagerInitialize()
    {
        path = Application.persistentDataPath +"/save";
        settingPath = path+"Setting";
        dataJson = Resources.Load("Json/DuneJson") as TextAsset;
        allData = JsonUtility.FromJson<AllData>(dataJson.text);
        Debug.Log("데이터매니저 초기 로드");
    }
    /// <summary>
    /// 1.EmptyPlace 좌표값 전처리
    /// </summary>
    public void DataPreprocessing()
    {
        // EmptyPlace 좌표값 전처리
        foreach(StageData stage in allData.stage)
        {
            if(stage.emptyPlace == "")
            {
                stage._emptyPlaceCoordinates = null;
            }
            else
            {
                stage._emptyPlaceCoordinates = new List<Coordinate>();
                string[] splitDatas = stage.emptyPlace.Split('/');
                foreach (string splitData in splitDatas)
                {
                    string[] secondSplitDatas = splitData.Split('.');
                    Coordinate cor = new Coordinate(int.Parse(secondSplitDatas[0]),int.Parse(secondSplitDatas[1]));
                    stage._emptyPlaceCoordinates.Add(cor);
                }
            }
        }
    }
    public StageData[] GetStageData()
    {
        return allData.stage;
    }
    public int GetLastStageNum()
    {
        Debug.Log(allData.stage[allData.stage.Length-1].stageName);
        return allData.stage[allData.stage.Length-1].stageNum;
    }
    public int GetLastStageId()
    {
        return allData.stage[allData.stage.Length-1].stageId;
    }
    public int GetStageCount(int num)
    {
        int count =0;
        foreach(StageData stageData in allData.stage)
        {
            if(stageData.stageNum == num)
                count ++;
        }
        return count;
    }
    public bool IsThisStage(int id,int num)
    {
        if(allData.stage[id].stageNum == num)
            return true;
        else
            return false;
    }
    public string GetStageName(int id)
    {
        foreach(StageData stage in allData.stage)
        {
            if(stage.stageId == id)
                return stage.stageName;
        }
        Debug.Log("ID에 맞는 스테이지 없음");
        return null;
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
    public StageData[] stage;
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
public class StageData
{
    public int stageId;
    public int stageNum;
    public string stageName;
    public int horizontalSize;
    public int verticlaSize;
    public string emptyPlace;
    public List<Coordinate> _emptyPlaceCoordinates;
    public int playerStartPointX;
    public int playerStartPointY;
    public int enemyStartPointX;
    public int enemyStartPointY;
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