using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PowerUpController : MonoBehaviour {

    public GameObject cam;
    public GameObject gameController;
    public GameObject circularBar;
    public Image fillBar;

    public GameObject[] powerupButtons;

    public float timeSlowDuration = 3;

    private float fillBarAmmount;
    private GameController GC;
    private GameObject variableObject;
    private int activePowerup;
    private bool timeSlowActive;
    private float temp = 0;
	// Use this for initialization
	void Start () {

        //Set GC - "GameController" Script refrence
        GC = gameController.GetComponent<GameController>();

        //Find variable object and use its stacker values from menu to select stacker
        variableObject = GameObject.FindWithTag("Variable");
        temp = variableObject.GetComponent<Variables>().activePower;
        GameObject tempObject = variableObject.GetComponent<Variables>().powerupList[(int)temp];
        //check to see if the object is unlocked, only really matters for 0 because it is the default
        if (PlayerPrefs.GetInt(tempObject.name) == 1)
        {
            SetPowerUp(variableObject.GetComponent<Variables>().activePower);

        }
        else
        {
            HideCircleBar();
        }

    }
	
	// Update is called once per frame
	void Update () {

        //Only allow a powerup to be used when the bar is full
        if (fillBarAmmount < 1)
        {
            powerupButtons[activePowerup].GetComponent<Button>().interactable = false;
        }
        else
        {
            powerupButtons[activePowerup].GetComponent<Button>().interactable = true;
        }


        if (timeSlowActive == true)
        {
            //set game speed to sloooooo motion
            Time.timeScale = ((temp / timeSlowDuration) + 0.5f);
            temp += Time.deltaTime;

            //calculate slope for vignette effect
            float m = (0.25f) / (0f - (timeSlowDuration / 2f));
            cam.GetComponent<VignetteAndChromaticAberration>().intensity = (m * temp) + 0.25f;

            //check to see if the slomo effect is done
            if (temp >= (timeSlowDuration / 2))
            {
                temp = 0;
                timeSlowActive = false;
                Time.timeScale = 1;
                cam.GetComponent<VignetteAndChromaticAberration>().intensity = 0.0f;
            }
        }//time slow active if stament over


        //update power meter graphic
        fillBar.fillAmount = fillBarAmmount;
	}

    public void SetPowerUp(int _powerup)
    {
        activePowerup = _powerup;

        //show only the active powerup button
      for(int i = 0; i < powerupButtons.Length; i++)
        {
            if (activePowerup == i)
            {
                powerupButtons[i].SetActive(true);
            }
            else
            {
                powerupButtons[i].SetActive(false);
            }
        }

    }
    
    public void HideAllButtons()
    {
        //hides fillbar
        fillBar.gameObject.SetActive(false);
        //hides all the buttons
        for (int i = 0; i < powerupButtons.Length; i++)
        {
            powerupButtons[i].SetActive(false);
        }
    }


    public void SlowTime()
    {
        if (timeSlowActive == false)
        {
            timeSlowActive = true;
        }

        //clear power meter
        fillBarAmmount = 0;
    }

    public void Undo()
    {
        GC.LowerHeightBy(1);
        Destroy(GC.stackList[(int)GC.GetHeight() -1 ].gameObject);

        //clear power meter
        fillBarAmmount = 0;
    }

    public void DoubleStacks()
    {


        //clear power meter
        fillBarAmmount = 0;
    }
    
    public void HideCircleBar()
    {
        circularBar.SetActive(false);
    }

    public void AddToPowerMeter(float _ammount)
    {
        if (_ammount < 1)
        {
            fillBarAmmount += ((-_ammount) / 5 + 0.2f) / variableObject.GetComponent<Variables>().powerupList[activePowerup].GetComponent<PowerInfo>().aquireDifficulty;
        }
    }
}
