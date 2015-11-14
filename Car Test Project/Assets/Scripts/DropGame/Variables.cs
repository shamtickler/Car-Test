using UnityEngine;
using System.Collections;

public class Variables : MonoBehaviour {

    public GameObject[] stackers;
    public GameObject currentStacker;

    public static bool Instance;


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = true;
        }
    }

    //Use this function to set the stacker from a menu
    public void SetStacker(int _stackerIndex)
    {
        currentStacker = stackers[_stackerIndex];
    }
}
