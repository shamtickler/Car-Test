using UnityEngine;
using System.Collections;

public class SpawnerMotor : MonoBehaviour {

    private bool isMovingRight = true;
    private float speed = 2;

    public void Move(float _playAreaWidth)
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

    public void SetSpeed(float _speed)
    {
        //sets the speed at which the spawner moves
        speed = _speed;
    }

}
