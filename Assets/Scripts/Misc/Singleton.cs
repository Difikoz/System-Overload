using UnityEngine;

namespace WinterUniverse
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _staticInstance;
        public static T StaticInstance => _staticInstance;

        [SerializeField] private bool _dontDestroyOnLoad = true;

        protected virtual void Awake()
        {
            if (StaticInstance == null)
            {
                _staticInstance = (T)this;
            }
            else if (StaticInstance != (T)this)
            {
                Destroy(gameObject);
                return;
            }
            if (_dontDestroyOnLoad && !transform.parent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}