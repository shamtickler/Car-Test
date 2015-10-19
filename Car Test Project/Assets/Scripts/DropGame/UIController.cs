using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private Text currentHeightTXT;
    [SerializeField]
    private GameObject gameControllerObject;

    private GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = gameControllerObject.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {

        //Update currentHeight Text
        currentHeightTXT.text = gameController.GetHeight().ToString();

	}
}
