using UnityEngine;
using System.Collections;

public class SelectedObjectFind : MonoBehaviour {

    [SerializeField]
    GameObject variablesObject;
    [SerializeField]
    GameObject selectorObject;
    [SerializeField]
    Vector3 selectorOffset;

    private VariablesInteractor variables;
    private GameObject selectedStacker;


	// Use this for initialization
	void Start () {
        variables = variablesObject.GetComponent<VariablesInteractor>();
	}
	
	// Update is called once per frame
	void Update () {
        selectedStacker = GameObject.Find(variables.currentStacker.name);
        selectorObject.transform.position = selectedStacker.transform.position + selectorOffset;
	}
}
