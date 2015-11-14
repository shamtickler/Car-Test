using UnityEngine;
using System.Collections;

public class StackerInfo : MonoBehaviour {

    public AudioClip placeStack;

    [SerializeField]
    private bool randomizeRotation = false;

    void Start()
    {
        if (randomizeRotation)
        {
            float x = Random.Range(0.0f, 6.0f);
            if (x < 1.0)
            {
                transform.Rotate(0,0,0);
            }else if (x>1.0 && x < 2.0)
            {
                transform.Rotate(90,0,0);
            }
            else if (x > 2.0 && x < 3.0)
            {
                transform.Rotate(180,0,0);
            }
            else if (x > 3.0 && x < 4.0)
            {
                transform.Rotate(-90,0,0);
            }
            else if (x > 4.0 && x < 5.0)
            {
                transform.Rotate(0,90,0);
            }
            else if (x > 5.0)
            {
                transform.Rotate(0,180,0);
            }

        }
    }

}
