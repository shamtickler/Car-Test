using UnityEngine;
using System.Collections;

public class StackJump : MonoBehaviour {

    [SerializeField]
    private float multiplier = 2;

    private bool isJumping = false;
    private Vector3 movement = Vector3.zero;
    private float x = 0;
    private Vector3 startingPosition = Vector3.zero;



    void Start()
    {
        startingPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (isJumping)
        {
            movement.y = Mathf.Sin((x * multiplier * Mathf.PI));
            transform.position += movement;
            x += Time.deltaTime;
            if (x >= 0.5f)
            {
                isJumping = false;
                movement.y = 0;
                transform.position = startingPosition;
            }

        }
        
	}

    public void JumpTheCube()
    {
        isJumping = true;
        x = 0;
    }

}
