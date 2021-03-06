﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KKUtilities
{
    public class BaseManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T I
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                }
                return instance;
            }
            protected set
            {
                instance = value;
            }
        }
        [SerializeField]
        bool dontDestroyOnLoad = false;

        protected virtual void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Inisialize();
        }

        protected void Inisialize()
        {
            List<T> instances = new List<T>();
            instances.AddRange((T[])FindObjectsOfType(typeof(T)));

            if (I == null) I = instances[0];
            instances.Remove(I);

            if (instances.Count == 0) return;
            //あぶれ者のinstanceはデストロイ 
            foreach (T t in instances) Destroy(t.gameObject);
        }

        protected virtual void Start()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            I = null;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
