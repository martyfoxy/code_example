using System.Collections.Generic;
using UnityEngine;

namespace Code.Utils
{
	/// <summary>
	/// Обобщенный пул с возможностью прогрева
	/// </summary>
    public sealed class Pool<T> where T: MonoBehaviour
    {
	    private readonly Stack<T> _inactiveStack;
	    private readonly T _prefab;
	    
	    private int _nextId = 0;
	    private Transform _poolRoot;

	    public Pool(T prefab, int warmCount = 0)
	    {
		    _inactiveStack = new Stack<T>(warmCount);
		    _prefab = prefab;

		    Warm(warmCount);
	    }

	    private void Warm(int warmCount)
		{
			_poolRoot = new GameObject($"Pool_{typeof(T)}").transform;
			
			for (var i = 0; i < warmCount; i++)
				_inactiveStack.Push(SpawnNew());
		}

		public T Spawn(Transform pivot)
		{
			var instance = SpawnOrGet();

			instance.transform.SetParent(pivot, false);
			instance.gameObject.SetActive(true);
			
			return instance;
		}

		public T Spawn(Vector3 position)
		{
			var instance = SpawnOrGet();

			instance.transform.position = position;
			instance.gameObject.SetActive(true);
			
			return instance;
		}

		public void Despawn(T obj)
		{
			obj.transform.SetParent(_poolRoot.transform, false);
			obj.gameObject.SetActive(false);
			_inactiveStack.Push(obj);
		}

		private T SpawnOrGet()
		{
			T instance;
			
			if (_inactiveStack.Count == 0)
				instance = SpawnNew();
			else
				instance = _inactiveStack.Pop();
			return instance;
		}

		private T SpawnNew()
		{
			var instance = Object.Instantiate(_prefab, _poolRoot.transform, false);
			instance.name = _prefab.name + "_" + _nextId++;
			instance.transform.localPosition = Vector3.zero;
			instance.gameObject.SetActive(false);
			return instance;
		}
    }
}