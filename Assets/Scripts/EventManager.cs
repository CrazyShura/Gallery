using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public static class EventManager
{
	public static EventHandler NoMoreImagesToLoad = new EventHandler();
}
#region Support
public class EventHandler<N>
{
	#region Fields
	List<UnityEvent<N>> Invokers = new List<UnityEvent<N>>();
	List<UnityAction<N>> Listeners = new List<UnityAction<N>>();
	#endregion

	#region Properties
	public bool IsListenersZero
	{
		get
		{
			if (Listeners.Count == 0)
				return true;
			else
				return false;
		}
	}
	#endregion

	#region Methods
	public void AddInvoker(UnityEvent<N> invoker)
	{
		if (!Invokers.Contains(invoker))
		{
			Invokers.Add(invoker);
			foreach (UnityAction<N> listener in Listeners)
			{
				invoker.AddListener(listener);
			}
		}
	}
	public void AddListener(UnityAction<N> action)
	{
		if (!Listeners.Contains(action))
		{
			Listeners.Add(action);
			foreach (UnityEvent<N> button in Invokers)
			{
				button.AddListener(action);
			}
		}
	}
	public void RemoveListener(UnityAction<N> action)
	{
		Listeners.Remove(action);
		foreach (UnityEvent<N> button in Invokers)
		{
			button.RemoveListener(action);
		}
	}
	public void ClearAllListeners()
	{
		Listeners.Clear();
		foreach (UnityEvent<N> invoker in Invokers)
		{
			invoker.RemoveAllListeners();
		}
	}
	#endregion
}
public class EventHandler
{
	#region Fields
	List<UnityEvent> Invokers = new List<UnityEvent>();
	List<UnityAction> Listeners = new List<UnityAction>();
	#endregion

	#region Properties
	public bool IsListenersZero
	{
		get
		{
			if (Listeners.Count == 0)
				return true;
			else
				return false;
		}
	}
	#endregion

	#region Methods
	public void AddInvoker(UnityEvent invoker)
	{
		if (!Invokers.Contains(invoker))
		{
			Invokers.Add(invoker);
			foreach (UnityAction listener in Listeners)
			{
				invoker.AddListener(listener);
			}
		}
	}
	public void AddListener(UnityAction action)
	{
		if (!Listeners.Contains(action))
		{
			Listeners.Add(action);
			foreach (UnityEvent button in Invokers)
			{
				button.AddListener(action);
			}
		}
	}
	public void RemoveListener(UnityAction action)
	{
		Listeners.Remove(action);
		foreach (UnityEvent button in Invokers)
		{
			button.RemoveListener(action);
		}
	}
	public void ClearAllListeners()
	{
		Listeners.Clear();
		foreach (UnityEvent invoker in Invokers)
		{
			invoker.RemoveAllListeners();
		}
	}
	#endregion
}
#endregion