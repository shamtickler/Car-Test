using UnityEngine;
using System.Collections;

public class SpawnerMotor : MonoBehaviour {

    private bool isMovingRight = true;
    private float speed = 2;
    private bool isMoving = true;

    public void Move(float _playAreaWidth)
    {
        //Checks if spawner should be moving
        if (isMoving)
        {
            //move the spawner object
            if (isMovingRight)
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
                if (transform.position.x > _playAreaWidth)
                {
                    isMovingRight = false;
                }
            }
            else
            {
                transform.position += -Vector3.right * Time.deltaTime * speed;
                if (transform.position.x < -_playAreaWidth)
                {
                    isMovingRight = true;
                }
            }
        }
    }

    public void SetSpeed(float _speed)
    {
        //sets the speed at which the spawner moves
        speed = _speed;
    }

    public void Stop()
    {
        isMoving = false;
        GetComponent<Renderer>().enabled = false;
    }

    public void Start()
    {
        isMoving = true;
        GetComponent<Renderer>().enabled = true;
    }

}
