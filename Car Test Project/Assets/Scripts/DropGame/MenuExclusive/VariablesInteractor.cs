using UnityEngine;
using System.Collections;

public class VariablesInteractor : MonoBehaviour {

    private GameObject _variables;

	// Use this for initialization
	void Start () {
        _variables = GameObject.FindWithTag("Variable");
        DontDestroyOnLoad(_variables);
    }
	
public void SetStacker(int _stackerIndex)
    {
        _variables.GetComponent<Variables>().SetStacker(_stackerIndex);
    }
}
