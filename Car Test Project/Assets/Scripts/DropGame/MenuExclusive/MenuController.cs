using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private AudioClip selectSounds;
    [SerializeField]
    private Camera myCamera;
    [SerializeField]
    private Text chitValue;

    private AdController adControl;
    private AudioSource source;

    private bool isHome = true;

    //Change this to canvas location
    private Vector3 xLoc = Vector3.zero;


	// Use this for initialization
	void Start () {
        adControl = GetComponent<AdController>();
        source = GetComponent<AudioSource>();
        StartCoroutine(DisplayChits());
    }

    // Update is called once per frame
    void Update () {
        transform.position = Vector3.Lerp(transform.position, xLoc, Time.deltaTime * movementSpeed);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isHome == true)
            {
                Application.Quit();
            }
            else
            {
                isHome = true;
                xLoc.Set(0, 0, 0);
            }
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
        isHome = false;
    }

    //go to settings menu
    public void SettingsMenu()
    {
        xLoc.Set(400, 0, 0);
        isHome = false;
    }

    //Go to main screen
    public void MainScreen()
    {
        xLoc.Set(0, 0, 0);
        isHome = true;
    }

    //clear saved data
    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
        Application.LoadLevel(Application.loadedLevel);
    }

    public void PlaySelectSound()
    {
        source.PlayOneShot(selectSounds);
    }

    private IEnumerator DisplayChits()
    {
        while (100 > 5)
        {
            chitValue.text = PlayerPrefs.GetInt("Chits").ToString();
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    public void PlayRewardedAds()
    {
        adControl.ShowRewardedAd();
    }
}
