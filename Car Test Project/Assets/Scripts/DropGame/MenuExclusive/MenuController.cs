using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private AudioClip selectSounds;
    [SerializeField]
    private Camera myCamera;

    public float aberrationTime = 0.0f;

    private AudioSource source;
    private VignetteAndChromaticAberration aberration;

    //Change this to canvas location
    private Vector3 xLoc = Vector3.zero;


	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        aberration = myCamera.GetComponent<VignetteAndChromaticAberration>();
        aberrationTime = 1.5f;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, xLoc, Time.deltaTime * movementSpeed);
        
        if (aberrationTime > 0)
        {
            aberration.chromaticAberration = Mathf.Pow(1.1f,((40 * aberrationTime) - 10));
            aberrationTime -= Time.deltaTime;
        }else if (aberrationTime <= 0)
        {
            aberrationTime = 0.0f;
            aberration.chromaticAberration = 0.0f;
        }
	}

    //Start Game
    public void StartGame()
    {
        Application.LoadLevel("MegaStacker");
    }

    //Go to StackerSelect Screen
    public void SelectStackerMenu()
    {
        xLoc.Set(0, 1200, 0);
        aberrationTime = 0.75f;
    }

    //go to settings menu
    public void SettingsMenu()
    {
        xLoc.Set(400, 0, 0);
        aberrationTime = 0.75f;
    }

    //Go to main screen
    public void MainScreen()
    {
        xLoc.Set(0, 0, 0);
        aberrationTime = 0.75f;
    }

    //clear saved data
    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
        aberrationTime = 0.75f;
    }

    public void PlaySelectSound()
    {
        source.PlayOneShot(selectSounds);
        aberrationTime = 0.75f;
    }

}
