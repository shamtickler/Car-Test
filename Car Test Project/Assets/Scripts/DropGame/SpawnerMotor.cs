using UnityEngine;
using System.Collections;

public class SpawnerMotor : MonoBehaviour {

    private bool isMovingRight = true;
    private float speed = 2;
    private bool isMoving = true;

    private Vector3 movementLocX = Vector3.zero;

    public void Move(float _playAreaWidth)
    {


        //sets location
        movementLocX = transform.position;
        //Checks if spawner should be moving
        if (isMoving)
        {
            //move the spawner object
            if (isMovingRight)
            {
                movementLocX.x += Time.deltaTime * speed;
                if (transform.position.x >= _playAreaWidth)
                {
                    isMovingRight = false;
                    movementLocX.x = _playAreaWidth -(transform.position.x - _playAreaWidth);
                }
            }
            else
            {
                movementLocX.x -= Time.deltaTime * speed;
                if (transform.position.x <= -_playAreaWidth)
                {
                    isMovingRight = true;
                    movementLocX.x = -_playAreaWidth - (transform.position.x + _playAreaWidth);
                }
            }
            //sets the spawners location equal to the calculated position
            transform.position = movementLocX;
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
       // movementLocX.y = transform.position.y;
    }

    public void MoveUsingSin(float _playAreaWidth)
    {
        movementLocX = transform.position;
        movementLocX.x = _playAreaWidth * Mathf.Sin(Mathf.PI * Time.time * speed);
        transform.position = movementLocX;
    }

}
