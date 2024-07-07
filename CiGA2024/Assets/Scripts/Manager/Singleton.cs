using UnityEngine;


    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        public static T Instance => instance;
        protected virtual void Awake()
        {
            if (FindObjectsOfType<T>().Length > 1)
            {
                Destroy(this);
                return;
            }
            instance = this as T;
        }
        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }
