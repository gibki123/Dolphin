﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler Instance;
    [System.Serializable]
    class Pool {
        public GameObject prefab;
        public string tag;
        public int size;
    }

    [SerializeField]
    private List<Pool> pools;

    [SerializeField]
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    // Initializing GameObjects from pools
    // Searching for appropriate tagged GameObjects change position to default position and
    // spawn objects as much as it is written in inspector
    public void Awake() {
        Instance = this;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools) {
            Queue<GameObject> objects = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objects.Enqueue(obj);
                obj.transform.SetParent(transform);
            }
            poolDictionary.Add(pool.tag, objects);
        }
    }

    // This can be used for spawning objects in certain position you can spawn only 
    // maximum value of objects which are written in inspector
    public GameObject SpawnFromPool(string tag, Vector3 position) {
        if (poolDictionary.ContainsKey(tag)) {
            if (poolDictionary.Count != 0) {
                GameObject spawningObject = poolDictionary[tag].Dequeue();
                spawningObject.SetActive(true);
                spawningObject.transform.position = position;
                return spawningObject;
            }
            else {
                Debug.LogWarning("poolDistionary contains 0 objects");
                return null;
            }
        }
        else {
            Debug.LogWarning("Pool with this tag:" + tag + "doesn't exists");
            return null;
        }

    }

    public void DisableFromPool(GameObject obj) {
        obj.SetActive(false);
        poolDictionary[obj.transform.tag].Enqueue(obj);
    }
}