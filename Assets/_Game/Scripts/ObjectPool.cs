using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool> {

	Dictionary<GameObject, LinkedList<GameObject>> _poolDictionary = new Dictionary<GameObject, LinkedList<GameObject>>();
	Dictionary<GameObject, GameObject>  _allocatedDictionary = new Dictionary<GameObject, GameObject>();
	Dictionary<string, GameObject>  _prefabPathDictionary = new Dictionary<string, GameObject>();


	public void AddToPool(GameObject prefab,int count){
		for(int i=0;i<count;i++){
			GameObject instance = InstantiateGo(prefab);
			_allocatedDictionary[instance] = prefab;
			Release(instance);
		}
	}

	public GameObject Allocate(string prefabPath){

		GameObject prefab;

		if(!_prefabPathDictionary.ContainsKey(prefabPath)){
			prefab = Resources.Load(prefabPath) as GameObject;
			_prefabPathDictionary[prefabPath]=prefab;
		}
		else
			prefab = _prefabPathDictionary[prefabPath];

		return Allocate(prefab);
	}

	public GameObject Allocate(GameObject prefab){
		return Allocate(prefab,Vector3.zero,Quaternion.identity);
	}

	public GameObject Allocate(GameObject prefab,Vector3 postion,Quaternion rotation){
		//Debug.Log("ObjectPool.Allocate "+prefab);

		if(!_poolDictionary.ContainsKey(prefab)){
			_poolDictionary[prefab] = new LinkedList<GameObject>();
		}

		GameObject ret;
		if(_poolDictionary[prefab].Count==0){
			ret = InstantiateGo(prefab,postion,rotation);
		}
		else{
			ret = _poolDictionary[prefab].First.Value;
			ret.transform.position = postion;
			ret.transform.rotation = rotation;
			_poolDictionary[prefab].RemoveFirst();
		}

		_allocatedDictionary[ret] = prefab;
		ret.SetActive(true);
		return ret;
	}

	GameObject InstantiateGo(GameObject obj){
		return InstantiateGo(obj,Vector3.zero,Quaternion.identity);
	}

	GameObject InstantiateGo(GameObject obj,Vector3 postion,Quaternion rotation){
		GameObject ret;
		ret = Instantiate(obj,postion,rotation) as GameObject;
		ret.transform.parent = gameObject.transform;
		return ret;
	}

	public void Release(GameObject instance){
		GameObject prefab = _allocatedDictionary[instance];
		//Debug.Log("ObjectPool.Release "+prefab);

		if(!_poolDictionary.ContainsKey(prefab)){
			_poolDictionary[prefab] = new LinkedList<GameObject>();
		}

		_allocatedDictionary.Remove(instance);
		instance.SetActive(false);
		_poolDictionary[prefab].AddLast(instance);
	}

	public void Clean(){
		foreach(KeyValuePair<GameObject, LinkedList<GameObject>> entry in _poolDictionary){

			foreach (var item in entry.Value) {
				Destroy(item);
			}

//			for(int i=0;i<entry.Value.Count;i++){
//				Destroy(entry.Value[i]);
//			}
		}

		List<GameObject> objToDestroy = new List<GameObject>();

		foreach(KeyValuePair<GameObject, GameObject> entry in _allocatedDictionary)
			objToDestroy.Add(entry.Key);

		for(int i=objToDestroy.Count-1;i>=0;i--)
			Destroy(objToDestroy[i]);

		_poolDictionary.Clear();
		_allocatedDictionary.Clear();
		_prefabPathDictionary.Clear();

//		for(int i=gameObject.transform.childCount-1;i>=0;i--){
//			Destroy(gameObject.transform.GetChild(i).gameObject);
//		}
	}

	void DebugPrint(){
		Debug.Log("============DebugPrint");
		foreach(KeyValuePair<GameObject, LinkedList<GameObject>> entry in _poolDictionary)
		{
			Debug.Log("========="+entry.Key+" "+entry.Value.Count);
		}
	}

}
