using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public List<GameObject> panelPrefabsForPool;
    private List<GameObject> pooledPanelObjects = new List<GameObject>();
    private Vector3 centerUiVector = new Vector3(960f,540f,0f);
    private Vector3 viewVector = new Vector3(1200f,540f,0f);

   public GameObject GetViewObjectFromPool(string objectName)
    {
        var instance = pooledPanelObjects.FirstOrDefault(obj => obj.name == objectName);
        Debug.Log("이거왜실행안돼"+instance);
        if (instance != null) // Pooled 인스턴스가 이미 있을경우
        {
            pooledPanelObjects.Remove(instance);
            instance.SetActive(true);
            return instance;
        }
        else
        {
            var prefab = panelPrefabsForPool.FirstOrDefault(obj =>obj.name == objectName);
            if (prefab != null) // pooled 인스턴스가 없을 경우
            {
                
                var newInstance =  Instantiate(prefab,viewVector,Quaternion.identity,transform.GetChild(0));
                newInstance.name = objectName;
                return newInstance;
            }
            else // prefabsForPool에 프리팹이 없을 경우
            {
                Debug.Log("오브젝트풀은 이 이름의 프리팹을 가지고있지 않습니다 : "+ objectName);
                return null;
            }
        }
    }

    public GameObject GetPanelObjectFromPool(string objectName)
    {
        var instance = pooledPanelObjects.FirstOrDefault(obj => obj.name == objectName);
                Debug.Log("이건되는데"+instance);

        if (instance != null) // Pooled 인스턴스가 이미 있을경우
        {
            pooledPanelObjects.Remove(instance);
            instance.SetActive(true);
            return instance;
        }
        else
        {
            var prefab = panelPrefabsForPool.FirstOrDefault(obj =>obj.name == objectName);
            if (prefab != null) // pooled 인스턴스가 없을 경우
            {
                
                var newInstance =  Instantiate(prefab,centerUiVector,Quaternion.identity,transform.GetChild(0));
                newInstance.name = objectName;
                return newInstance;
            }
            else // prefabsForPool에 프리팹이 없을 경우
            {
                Debug.Log("오브젝트풀은 이 이름의 프리팹을 가지고있지 않습니다 : "+ objectName);
                return null;
            }
        }
    }
    public void PoolObject(GameObject obj)
    {
        obj.SetActive(false);
        pooledPanelObjects.Add(obj);
    }

}
