using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

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

    private GameObject stacker;
    private GameObject variableObject;
    private float aberrationTime = 0.0f;

    private float height = 1;
    private float speed = 3;
    private bool spawnerActive = true;

    public float speedMultiplier = 1;
    public float playAreaWidth = 4;

    private VignetteAndChromaticAberration aberration;
    private AudioSource source;
    private StackerInfo stackerInfo;
    private SpawnerMotor spawnerMotor;
    private SmoothFollow camOffset;
    private UIController uiController;
    private List<GameObject> stackList = new List<GameObject>();

	// Use this for initialization
	void Start () {

        //Find variable object and use its stacker values from menu to select stacker
        variableObject = GameObject.FindWithTag("Variable");
        stacker = variableObject.GetComponent<Variables>().currentStacker;


        //set starting variables
        height = 1;
        spawnerMotor = spawnPoint.GetComponent<SpawnerMotor>();
        source = GetComponent<AudioSource>();
        camOffset = cam.GetComponent<SmoothFollow>();
        uiController = uiControllerObject.GetComponent<UIController>();
        lossMenu.SetActive(false);
        stackerInfo = stacker.GetComponent<StackerInfo>();
        aberration = cam.GetComponent<VignetteAndChromaticAberration>();

        aberrationTime = 1.5f;
    }
	
	// Update is called once per frame
	void Update () {

        //reset all player prefs. TESTING
        if (Input.GetKeyDown(KeyCode.R))
        {
            ActivateStacks();
        }

        //move spawnpoint back and forth whithin the play area
        spawnerMotor.Move(playAreaWidth);
        //Set the speed of the spawner
        spawnerMotor.SetSpeed(speed * speedMultiplier);
        //Controls Chromatic Aberration
        Aberrate();

        //actions on mouse click
        if (Input.GetButtonDown("Fire1") || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (spawnerActive)
            {
                //Aberrate that screen!
                aberrationTime = 0.75f;

                //Create a temperary variable to add the new stack to stackList
                GameObject _stack;

                //Play the sound effect
                source.PlayOneShot(stackerInfo.placeStack);

                //create the object we are spawning in - menuVariablesObject.GetComponent<Variables>().currentStacker
                _stack = (GameObject)Instantiate(stacker, spawnPoint.transform.position, spawnPoint.transform.rotation);

                //add the new stack (_stack) to the stackList
                stackList.Add(_stack);

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

    public void Aberrate()
    {
        if (aberrationTime > 0)
        {
            aberration.chromaticAberration = Mathf.Pow(1.1f, ((40 * aberrationTime) - 10));
            aberrationTime -= Time.deltaTime;
        }
        else if (aberrationTime <= 0)
        {
            aberrationTime = 0.0f;
            aberration.chromaticAberration = 0.0f;
        }
    }


    //brings camera back to view entire stack and shows score screen.
    public void ActivateStacks()
    {
        //ABERRATE YEAH
        aberrationTime = 1.5f;

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
    }

    void CalculateSpeed()
    {
        speed = ((height / 25) + 3);
        //speed = 3.0f;
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
