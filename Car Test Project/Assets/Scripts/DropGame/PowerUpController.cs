using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PowerUpController : MonoBehaviour {

    public GameObject cam;

    public GameObject[] powerupButtons;

    public float timeSlowDuration = 3;

    private GameObject variableObject;
    private int activePowerup;
    private bool timeSlowActive;
    private float temp = 0;
	// Use this for initialization
	void Start () {

        //Find variable object and use its stacker values from menu to select stacker
        variableObject = GameObject.FindWithTag("Variable");

        SetPowerUp(variableObject.GetComponent<Variables>().activePower);

    }
	
	// Update is called once per frame
	void Update () {
	
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
        }

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
        }

    }


    public void SlowTime()
    {
        if (timeSlowActive == false)
        {
            timeSlowActive = true;
        }
    }
}
