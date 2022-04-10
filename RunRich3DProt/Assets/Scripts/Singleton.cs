using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
   
    private static T instance;
    public static T Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
}
