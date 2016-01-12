using UnityEngine;
using System.Collections;

public class Variables : MonoBehaviour {

    public GameObject[] stackers;
    public GameObject currentStacker;

    public int activePower = 0;
    public string[] powerNameList;

    public static bool Instance;



    void Awake()
    {
        Application.targetFrameRate = 30;
        DontDestroyOnLoad(transform.gameObject);
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = true;
            currentStacker = stackers[(int)PlayerPrefs.GetFloat("CurrentStacker")];
        }

    }

    //Use this function to set the stacker from a menu
    public void SetStacker(int _stackerIndex)
    {
        currentStacker = stackers[_stackerIndex];
        PlayerPrefs.SetFloat("CurrentStacker", _stackerIndex);
    }

    //check if the stacker is unlocked or not and tells variableinteractor
    public bool CheckIfUnlocked(int _stackerIndex)
    {
        bool _isUnlocked;
        if (PlayerPrefs.GetInt(stackers[_stackerIndex].name) > 0)
        {
            _isUnlocked = true;
        }
        else
        {
            _isUnlocked = false;
        }

        //returns the verdict to variableinteractor
        return _isUnlocked;
    }

    //Use this function to access the array
    public GameObject GetStacker(int _stackerIndex)
    {
        //GameObject _requestedStacker;
        //_requestedStacker = stackers[_stackerIndex];
        return stackers[_stackerIndex];
    }

    public void SetPower(int _power)
    {
        activePower = _power;
    }

}
