using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject heightObject;
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private int stacksToCalculate = 9;

    private float height = 1;
    private float speed = 3;
    private bool spawnerActive = true;

    public float speedMultiplier = 1;
    public float playAreaWidth = 4;

    private SpawnerMotor spawnerMotor;
    private SmoothFollow camOffset;
    private List<GameObject> stackList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //set starting variables
        height = 1;
        spawnerMotor = spawnPoint.GetComponent<SpawnerMotor>();
        camOffset = cam.GetComponent<SmoothFollow>();
	}
	
	// Update is called once per frame
	void Update () {

        //move spawnpoint back and forth whithin the play area
        spawnerMotor.Move(playAreaWidth);
        //Set the speed of the spawner
        spawnerMotor.SetSpeed(speed * speedMultiplier);

        //actions on mouse click
        if (Input.GetButtonDown("Fire1") && spawnerActive)
        {
            //Create a temperary variable to add the new stack to stackList
            GameObject _stack;

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

        //on keypress activate the regidbodies on all stacks
        if (Input.GetKeyDown("f"))
        {
            ActivateStacks();
        }
	
	}

    //brings camera back to view entire stack. Perhaps show score?
    public void ActivateStacks()
    {
        //Sets camera to look at entire stack
        Vector3 _offset = new Vector3(0, -(height / 2.0f), -height);
        camOffset.ChangeOffset(_offset);

        //Re-activates all rigidbodies
        foreach (GameObject _stack in stackList){
            _stack.GetComponent<Rigidbody>().isKinematic = false;
            _stack.GetComponent<Rigidbody>().drag = 0;
        }

        //deactivate the spawner
        spawnerMotor.Stop();
        spawnerActive = false;
    }

    void CalculateSpeed()
    {
        speed = ((height / 15) + 3);
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
