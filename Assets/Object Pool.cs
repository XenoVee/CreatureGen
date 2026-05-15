using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IPoolable
{
	bool active { get; set; }
	void OnEnableObject();
	void OnDisableObject();
}

public class ObjectPool<T> where T : IPoolable
{
	List<T> Active = new List<T>();
	List<T> InActive = new List<T>();

	T RequestObject()
	{
		if (InActive.Count > 0)
		{
			T obj = InActive[InActive.Count - 1];
			InActive.RemoveAt(InActive.Count - 1);
			obj.OnEnableObject();
			Active.Add(obj);
			return (obj);
		}
		return (default(T));
	}

	void ReturnObject(T obj)
	{
		Active.Remove(obj);
		InActive.Add(obj);
		obj.OnDisableObject();
	}
}