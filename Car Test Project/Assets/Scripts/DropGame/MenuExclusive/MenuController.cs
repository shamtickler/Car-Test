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


    private AudioSource source;

    //Change this to canvas location
    private Vector3 xLoc = Vector3.zero;


	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        StartCoroutine(DisplayChits());
    }

    // Update is called once per frame
    void Update () {
        transform.position = Vector3.Lerp(transform.position, xLoc, Time.deltaTime * movementSpeed);
        
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
    }

    //go to settings menu
    public void SettingsMenu()
    {
        xLoc.Set(400, 0, 0);
    }

    //Go to main screen
    public void MainScreen()
    {
        xLoc.Set(0, 0, 0);
    }

    //clear saved data
    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
    }

    public void PlaySelectSound()
    {
        source.PlayOneShot(selectSounds);
    }

    private IEnumerator DisplayChits()
    {
        chitValue.text = PlayerPrefs.GetInt("Chits").ToString();
        yield return new WaitForSeconds(1);
    }
}
