using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : class {
    public static T Instance => instance;

    [Header("Singleton")]
    [SerializeField] protected bool isPersistant = false;

    protected virtual void Awake() {
        if(Singleton<T>.instance != null) {
            Debug.LogError($"There are more than one <b>\"{typeof(T).Name}\"</b> singleton.", gameObject);
            GameObject.Destroy(this);
            return;
        }
        Singleton<T>.instance = this as T;

        if(isPersistant) {
            if(transform.parent != null) {
                transform.parent = null;
                Debug.LogWarning($"Singleton <b>\"{typeof(T).Name}\"</b> is a child, it has been removed from his parent.", gameObject);
            }
            DontDestroyOnLoad(this);
        }
    }

    protected virtual void OnDestroy() {
        if(!isPersistant) ResetInstance();
    }

    public static void ResetInstance() => Singleton<T>.instance = null;

    public void DestroyInstance() {
        GameObject.Destroy(gameObject);
        ResetInstance();
    }

    private static T instance;
}
