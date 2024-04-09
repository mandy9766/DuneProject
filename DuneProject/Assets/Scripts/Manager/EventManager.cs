using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<EventType,List<IListener>> _listners = new Dictionary<EventType, List<IListener>>();

    /// <summary>
    /// 이벤트 매니저에 자기 자신을 리스너로 등록하는 메서드
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public void AddListener(EventType eventType,IListener listener)
    {
        List<IListener> _listenList = null;

        if (_listners.TryGetValue(eventType,out _listenList)) // 이벤트형식 키 존재하는지 검사, 존재하면 리스트에 추가
        {
            _listenList.Add(listener);
        }
        else // 없으면 새로운 리스너 리스트를 만들고 이벤트 타입으로 딕셔너리 추가
        {
            _listenList = new List<IListener>();
            _listenList.Add(listener);
            _listners.Add(eventType,_listenList);
        }

    }
    /// <summary>
    /// 이벤트가 발생했을 때 이벤트 매니저에게 알려주는 메서드
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="sender"></param>
    /// <param name="param"></param>
    public void PostNotification(EventType eventType,Component sender,object param = null)
    {
        List<IListener> _listenList = null;
        if (!_listners.TryGetValue(eventType,out _listenList))
        {
            return;
        }
        for(int i = 0;i < _listenList.Count;i++)
            _listenList?[i].OnEvent(eventType,sender,param);
    }
    public void RemoveEvent(EventType eventType) => _listners.Remove(eventType);

    /// <summary>
    /// 씬 바뀔때 메소드를 호출하자.
    /// 씬이 바뀌어서 이미 파괴된 오브젝트를 참조하면 안되므로 이 부분을 보완하는 기능
    /// </summary>
    public void RemoveRedundancies()
    {
        Dictionary<EventType,List<IListener>> newListeners = new Dictionary<EventType, List<IListener>>();

        foreach(KeyValuePair<EventType,List<IListener>> item in _listners)
        {
            for (int i = item.Value.Count -1; i>=0; i--)
            {
                if (item.Value.Equals(null))
                    item.Value.RemoveAt(i);
            }

            if(item.Value.Count> 0)
            {
                newListeners.Add(item.Key,item.Value);
            }
        }
        _listners = newListeners;
    }
   
}
