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

    private Vector3 moveLocation;

	// Use this for initialization
	void Start () {
        variables = variablesObject.GetComponent<VariablesInteractor>();
	}
	
	// Update is called once per frame
	void Update () {
        selectedStacker = GameObject.Find(variables.currentStacker.name);
        moveLocation = selectedStacker.transform.position + selectorOffset;
        moveLocation.y = selectorObject.transform.position.y;
        selectorObject.transform.position = moveLocation;
	}
}
