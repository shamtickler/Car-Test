using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject uiControllerObject;
    [SerializeField]
    private GameObject heightObject;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject lossMenu;
    [SerializeField]
    private int stacksToCalculate = 9;
    [SerializeField]
    private GameObject adControllerObject;
    [SerializeField]
    private float playsBeforeAdShows = 4.0f;

    private GameObject stacker;
    private GameObject variableObject;

    private int chits = 0;
    private float height = 0;
    private float speed;
    private bool spawnerActive = true;

    public float playAreaWidth = 4;

    private AudioSource source;
    private StackerInfo stackerInfo;
    private SpawnerMotor spawnerMotor;
    private SmoothFollow camOffset;
    private UIController uiController;
    private AdController adController;
    private List<GameObject> stackList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //sets "chits" to ammount of money
        chits = PlayerPrefs.GetInt("Chits");

        //Set Score screen to 0 opacity
        lossMenu.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.1f, false);

        //Find variable object and use its stacker values from menu to select stacker
        variableObject = GameObject.FindWithTag("Variable");
        stacker = variableObject.GetComponent<Variables>().currentStacker;


        //set starting variables and script referances
        height = 0;
        spawnerMotor = spawnPoint.GetComponent<SpawnerMotor>();
        source = GetComponent<AudioSource>();
        camOffset = cam.GetComponent<SmoothFollow>();
        uiController = uiControllerObject.GetComponent<UIController>();
        lossMenu.SetActive(false);
        stackerInfo = stacker.GetComponent<StackerInfo>();
        adController = adControllerObject.GetComponent<AdController>();

        //set the starting speed of the spawner motor
        CalculateSpeed();
    }
	
	// Update is called once per frame
	void Update () {

        //move spawnpoint back and forth whithin the play area
        spawnerMotor.Move(playAreaWidth);
        //spawnerMotor.MoveUsingSin(playAreaWidth);

        //actions on mouse click
        if (Input.GetButtonDown("Fire1") || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (spawnerActive)
            {

                //Create a temperary variable to add the new stack to stackList
                GameObject _stack;

                //Play the sound effect
                source.PlayOneShot(stackerInfo.placeStack);

                //create the object we are spawning in - menuVariablesObject.GetComponent<Variables>().currentStacker
                _stack = (GameObject)Instantiate(stacker, spawnPoint.transform.position, spawnPoint.transform.rotation);

                //add the new stack (_stack) to the stackList
                stackList.Add(_stack);

                //set some properties of the new stack and remove some components
               StackerInfo sis;
                sis = _stack.GetComponent<StackerInfo>();
                Destroy(sis);

                //add one to the height
                height += 1;

                heightObject.transform.position += Vector3.up;
                spawnPoint.transform.position += Vector3.up;

                //disable the rigidbody on the cube down reaching the "stacksToCalculate" variable
                if (stackList.Count > stacksToCalculate)
                {
                    stackList[(int)(height - 2.0f - stacksToCalculate)].GetComponent<Rigidbody>().isKinematic = true;
                }

                //Increase spawner speed
                CalculateSpeed();
            }
        }
	
	}


    //brings camera back to view entire stack and shows score screen.
    public void ActivateStacks()
    {
        //recaculates and saves new amount of chits
        PlayerPrefs.SetInt("Chits", (chits += (int)height));
        uiController.SetChitIncrease(height);
        
        //Sets camera to look at entire stack
        Vector3 _offset = new Vector3(0, -(height / 2.0f), -height);
        camOffset.ChangeOffset(_offset);

        //deactivate the spawner
        spawnerMotor.Stop();
        spawnerActive = false;

        //Check and set HighScore
        if (height > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", height);
            PlayerPrefs.SetFloat("LastRunScore", height);
            uiController.NewHighScore();
        }
        else
        {
            PlayerPrefs.SetFloat("LastRunScore", height);
        }

        //Show ending screen UI
        lossMenu.SetActive(true);
        lossMenu.GetComponent<Image>().CrossFadeAlpha(1.0f, 3.0f, false);
        //Add one to total lifetime playcount
        PlayerPrefs.SetFloat("LifetimePlays", PlayerPrefs.GetFloat("LifetimePlays") + 1.0f);
        //Figure out if an ad should show or not and then show one
        PlayerPrefs.SetFloat("PlaysSinceAd", PlayerPrefs.GetFloat("PlaysSinceAd") + 1.0f);
        if (PlayerPrefs.GetFloat("PlaysSinceAd") >= playsBeforeAdShows)
        {
            adController.ShowAd();
            PlayerPrefs.SetFloat("PlaysSinceAd", 0.0f);
        }
        
    }

    void CalculateSpeed()
    {
        speed = ((height / 25) + 2.5f) * 1.6f;
        //speed = ((height / 50) + 1.0f);   //use this one when using sin function, hopefully wont need to use sin function

        //Tells the spawner motor the speed that is calculated
        spawnerMotor.SetSpeed(speed);
    }

    public float GetHeight()
    {
        return height;
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
