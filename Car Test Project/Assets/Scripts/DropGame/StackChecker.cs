using UnityEngine;
using System.Collections;

public class StackChecker : MonoBehaviour {

    public float checkIterations = 10;

    [SerializeField]
    private GameObject gameControllerObject;

    private bool isStanding = true;
    private GameController gameController;
    private RaycastHit hit;
    private float startLeftDist;
    private float sensorCounter = 1;


	void Start ()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        startLeftDist = gameController.playAreaWidth / 2;
	}
	

	void Update ()
    {
        if (isStanding)
        {
            float _xDist = -startLeftDist;
            for (float i = 1; i <= checkIterations; i++)
            {
                float stepSize = (startLeftDist * 2) / checkIterations;
                Vector3 rayPosition = transform.position;
                rayPosition.x = _xDist + (i * stepSize);
                //Create the ray
                Ray sensorRay = new Ray(rayPosition, -Vector3.up);
                //Test if it hits the stack
                if (Physics.Raycast(sensorRay, out hit, 2.0f))
                {
                    sensorCounter += 1;
                }


                _xDist += stepSize;
            }
            //If no sensors hit the tower it is marked as not standing
            if (sensorCounter <= 0)
            {
                isStanding = false;
            }
            else //if any sensors hit the tower it is left as standing and the process repeats
            {
                sensorCounter = 0;
            }

        }else if (!isStanding)
        {
            gameController.ActivateStacks();
        }
	}
}
