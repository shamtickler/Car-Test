using UnityEngine;
using System.Collections;

public class VariablesInteractor : MonoBehaviour {

    private GameObject _variables;

    public GameObject currentStacker;

	// Use this for initialization
	void Start () {
        _variables = GameObject.FindWithTag("Variable");
        DontDestroyOnLoad(_variables);
    }

    void Update()
    {
        currentStacker = _variables.GetComponent<Variables>().currentStacker;
    }
	
public void SetStacker(int _stackerIndex)
    {
        _variables.GetComponent<Variables>().SetStacker(_stackerIndex);
    }

}
