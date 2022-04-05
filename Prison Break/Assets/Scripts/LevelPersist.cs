using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPersist : MonoBehaviour
{
    void Awake()
    {
        int numLP = FindObjectsOfType<LevelPersist>().Length;
        if (numLP > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
