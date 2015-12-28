using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationCenter : MonoBehaviour {
	
	public delegate void defaultNotificationDelegate(string notification);

	private static NotificationCenter _defaultInstance = null;
	public bool destroyOnLoad = true;

	public static NotificationCenter defaultInstance {
		get {
			if (_defaultInstance == null && FindObjectOfType<NotificationCenter>() == null)
			{
				_defaultInstance = new GameObject("NotificationCenter", typeof(NotificationCenter)).GetComponent<NotificationCenter>();
			} else if (_defaultInstance == null) {
				_defaultInstance = FindObjectOfType<NotificationCenter>();
			}
			return _defaultInstance;
		}
	}

	void Start () {
		if (_defaultInstance == null)
		{
			_defaultInstance = this;
		}
		if (!destroyOnLoad)
		{
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	private Dictionary<string, List<defaultNotificationDelegate>> _notificationMap;

	public NotificationCenter()
	{
		_notificationMap = new Dictionary<string, List<defaultNotificationDelegate>>();
	}

	public void AddObserver(string notification, defaultNotificationDelegate notificationDelegate)
	{
		if (!_notificationMap.ContainsKey(notification))
		{
#if DEBUG
			Debug.Log("Adding observer: " + notification + ", " + notificationDelegate.ToString());
#endif
			_notificationMap.Add(notification, new List<defaultNotificationDelegate>());
		}

		_notificationMap[notification].Add(notificationDelegate);
	}

	public void RemoveObserver(string notification, defaultNotificationDelegate notificationDelegate)
	{
		if (!_notificationMap.ContainsKey(notification))
		{
			return;
		}

		if (!_notificationMap[notification].Contains(notificationDelegate))
		{
			return;
		}

		_notificationMap[notification].Remove(notificationDelegate);
	}

	public void PostNotification(string notification)
	{
#if DEBUG
		Debug.Log("Recieved notification: " + notification);
#endif
		foreach(KeyValuePair<string, List<defaultNotificationDelegate>> l in _notificationMap)
		{
			//Debug.Log(l.Key);
		}

		if (_notificationMap.ContainsKey(notification))
		{
			List<defaultNotificationDelegate> observers = _notificationMap[notification];
			for (int i = 0; i < observers.Count; i++)
			{
				if (observers[i] != null)
				{
					observers[i](notification);
				} else {
					observers.RemoveAt(i);
				}
			}
		}
	}

	public void Clear()
	{
		PostNotification ("AboutToClear");
		foreach(KeyValuePair<string, List<defaultNotificationDelegate>> l in _notificationMap)
		{
			l.Value.Clear();
		}

		_notificationMap.Clear ();
	}
}
