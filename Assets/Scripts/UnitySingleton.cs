using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleton <T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;

    public static T Instance
    {
        get { return instance ?? (instance = FindObjectOfType<T>()); }
    }

}
