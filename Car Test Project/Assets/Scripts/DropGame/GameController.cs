using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject heightObject;
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private GameObject spawnPoint;

    public float height = 1;
    public float speed = 1;
    public float playAreaWidth = 4;

    private SpawnerMotor spawnerMotor;

	// Use this for initialization
	void Start () {
        height = 1;
        spawnerMotor = spawnPoint.GetComponent<SpawnerMotor>();
        spawnerMotor.SetSpeed(speed);
	}
	
	// Update is called once per frame
	void Update () {

        //move spawnpoint back and forth whithin the play area
        spawnerMotor.Move(playAreaWidth);

        //actions on mouse click
        if (Input.GetButtonDown("Fire1") || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //create the object we are spawning in - spawnObject
            Instantiate(spawnObject, spawnPoint.transform.position, spawnPoint.transform.rotation);

            //add one to the height, or score
            height += 1;

            heightObject.transform.position += Vector3.up;
            spawnPoint.transform.position += Vector3.up;
        }
	
	}
}
