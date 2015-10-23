using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField]
    private Text currentHeightTXT;
    [SerializeField]
    private Text lastRunScoreTXT;
    [SerializeField]
    private Text highscoreTXT;
    [SerializeField]
    private GameObject newHighScoreIcon;
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

        //Update highscore Text
        highscoreTXT.text = PlayerPrefs.GetFloat("HighScore").ToString();

        lastRunScoreTXT.text = PlayerPrefs.GetFloat("LastRunScore").ToString();
	}

    public void NewHighScore()
    {
        newHighScoreIcon.active = true;
    }

    //Go to main menu
    public void GoToMenu()
    {
        Application.LoadLevel("MegaStackerMenu");
    }
}
