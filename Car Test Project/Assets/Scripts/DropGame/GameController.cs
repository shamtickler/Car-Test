using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject uiControllerObject;
    [SerializeField]
    private GameObject heightObject;
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject lossMenu;
    [SerializeField]
    private int stacksToCalculate = 9;

    private float height = 1;
    private float speed = 3;
    private bool spawnerActive = true;

    public float speedMultiplier = 1;
    public float playAreaWidth = 4;

    private AudioSource source;
    private StackerInfo stackerInfo;
    private SpawnerMotor spawnerMotor;
    private SmoothFollow camOffset;
    private UIController uiController;
    private List<GameObject> stackList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //set starting variables
        height = 1;
        spawnerMotor = spawnPoint.GetComponent<SpawnerMotor>();
        source = GetComponent<AudioSource>();
        stackerInfo = spawnObject.GetComponent<StackerInfo>();
        camOffset = cam.GetComponent<SmoothFollow>();
        uiController = uiControllerObject.GetComponent<UIController>();
        lossMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        //move spawnpoint back and forth whithin the play area
        spawnerMotor.Move(playAreaWidth);
        //Set the speed of the spawner
        spawnerMotor.SetSpeed(speed * speedMultiplier);

        //actions on mouse click
        if (Input.GetButtonDown("Fire1") || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (spawnerActive)
            {
                //Create a temperary variable to add the new stack to stackList
                GameObject _stack;

                //Play the sound effect
                source.PlayOneShot(stackerInfo.placeStack);

                //create the object we are spawning in - spawnObject
                _stack = (GameObject)Instantiate(spawnObject, spawnPoint.transform.position, spawnPoint.transform.rotation);

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
	
        //reset all player prefs. TESTING
        if (Input.GetKeyDown("r"))
        {
            PlayerPrefs.DeleteAll();
        }

	}

    //brings camera back to view entire stack. Perhaps show score?
    public void ActivateStacks()
    {
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
