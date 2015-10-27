using UnityEngine;
using System.Collections;

public class RotateStacker : MonoBehaviour {

    [SerializeField]
    private float rotateSpeed = 1;


    public float rotAngle = 360;

    private float currentRotation = 0;
    private bool rotateMe = false;


    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (rotateMe)
        {

            transform.rotation = Quaternion.Euler(currentRotation,currentRotation,currentRotation);
            currentRotation += (Time.deltaTime * rotateSpeed);
            if (currentRotation >= rotAngle)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                currentRotation = 0;
                rotateMe = false;
            }

        }

    }

    public void RotateButton()
    {
        rotateMe = true;
    }
}
