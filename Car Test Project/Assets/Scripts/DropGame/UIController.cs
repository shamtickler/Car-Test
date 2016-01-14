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
    private Text chitValue;

    private bool onChitRoutine = true;
    private float currentChits = 0;
    private float startingChits = 0;
    private float chitIncreaseAmmount = 0;
    private GameController gameController;
    public bool playScreenIsActive = true;

	// Use this for initialization
	void Start () {
        gameController = gameControllerObject.GetComponent<GameController>();
        chitValue.text = PlayerPrefs.GetInt("Chits").ToString();
        startingChits = PlayerPrefs.GetInt("Chits");
        currentChits = startingChits;
    }
	
	// Update is called once per frame
	void Update () {

        //Update currentHeight Text
        currentHeightTXT.text = gameController.GetHeight().ToString();

        //Update highscore Text
        highscoreTXT.text = PlayerPrefs.GetFloat("HighScore").ToString();

        lastRunScoreTXT.text = PlayerPrefs.GetFloat("LastRunScore").ToString();

        //if score needs to increase more run coroutine
        if (chitIncreaseAmmount > 0)
        {
            StartCoroutine(DecreaseChitBuffer());
        }
        else
        {
            StopCoroutine(DecreaseChitBuffer());
        }

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

    public void SetChitIncrease(float _chitIncreaseAmmount)
    {
        chitIncreaseAmmount = _chitIncreaseAmmount;
    }

    IEnumerator DecreaseChitBuffer()
    {
        
        if (onChitRoutine)
        {
            //set routine back to false so it doesnt all go right away
            onChitRoutine = false;
            //Increase the chit number
            chitIncreaseAmmount -= 1.0f;
            currentChits += 1.0f;
            chitValue.text = currentChits.ToString();

            //wait for a calculated period of time (time to wait increases as value get closer to actual)
            float waittime = 0.5f / chitIncreaseAmmount;
            yield return new WaitForSeconds(waittime);
            //after the wait set the routine back to true so it can run again
            onChitRoutine = true;
        }
        
    }


}
