using UnityEngine;
using System.Collections;

public class SelectedObjectFind : MonoBehaviour {

    [SerializeField]
    GameObject variablesObject;
    [SerializeField]
    private GameObject buyUI;

    [Header("Selector")]
    [SerializeField]
    GameObject selectorObject;
    [SerializeField]
    Vector3 selectorOffset;

   

    private VariablesInteractor variables;
    private GameObject selectedStacker;
    private GameObject selectedBuy;

    private Vector3 moveLocation;

	// Use this for initialization
	void Start () {
        variables = variablesObject.GetComponent<VariablesInteractor>();
	}
	
	// Update is called once per frame
	void Update () {
        selectedStacker = GameObject.Find(variables.currentStacker.name);

        if (variables.currentStackerBuy != null)
        {
            selectedBuy = GameObject.Find(variables.currentStackerBuy.name);
        }

        //move the selector "arrow"
        moveLocation = selectedStacker.transform.position + selectorOffset;
        moveLocation.y = selectorObject.transform.position.y;
        selectorObject.transform.position = moveLocation;

        //move the buy gui
        moveLocation = selectedBuy.transform.position;
        buyUI.transform.position = moveLocation;
	}
}
