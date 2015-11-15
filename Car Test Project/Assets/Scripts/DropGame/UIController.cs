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
    [SerializeField]
    private Vector3 heightOffset = new Vector3(0,945,0);
    [SerializeField]
    private GameObject[] movingComponents;

    private Vector3[] movingComponentsStartingOffset;

    private GameController gameController;
    public bool playScreenIsActive = true;
    private int x = 0;


	// Use this for initialization
	void Start () {
        gameController = gameControllerObject.GetComponent<GameController>();
        movingComponentsStartingOffset = new Vector3[movingComponents.Length];
       
        foreach(GameObject item in movingComponents)
        {
            movingComponentsStartingOffset[x] = item.transform.position;
            x += 1;
        }
        x = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //Update currentHeight Text
        currentHeightTXT.text = gameController.GetHeight().ToString();

        //Update highscore Text
        highscoreTXT.text = PlayerPrefs.GetFloat("HighScore").ToString();

        lastRunScoreTXT.text = PlayerPrefs.GetFloat("LastRunScore").ToString();

        //controls the moving between restart ui and more stats ui
        if (playScreenIsActive)
        {
            foreach(GameObject item in movingComponents)
            {
                //Debug.Log(item.gameObject.name);
                item.transform.position = Vector3.Lerp(transform.position, movingComponentsStartingOffset[x], Time.deltaTime * 1);
                x += 1;
            }
        }
        else
        {
            foreach (GameObject item in movingComponents)
            {
                item.transform.position = Vector3.Lerp(transform.position, movingComponentsStartingOffset[x] + heightOffset, Time.deltaTime * 1);
                x += 1;
            }
        }

        x = 0;
	}

    public void NewHighScore()
    {
        newHighScoreIcon.SetActive(true);
    }

    //Go to main menu
    public void GoToMenu()
    {
        Application.LoadLevel("MegaStackerMenu");
    }

    //View extra stats
    public void ViewMoreStats()
    {
        if (playScreenIsActive)
        {
            playScreenIsActive = false;
        }
        else
        {
            playScreenIsActive = true;
        }
    }

}
