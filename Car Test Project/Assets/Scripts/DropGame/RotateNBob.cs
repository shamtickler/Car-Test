using UnityEngine;
using System.Collections;

public class RotateNBob : MonoBehaviour {

    [SerializeField]
    private float bobHeight = 1.0f;
    [SerializeField]
    private float bobSpeed = 1.0f;
    [SerializeField]
    private float rotateSpeed;

    private Vector3 floatY;
    private float originalY;
	
    void Start()
    {
        originalY = transform.position.y;
    }

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.Self);
        //transform.Translate()
        floatY = transform.position;
        floatY.y = originalY + (Mathf.Sin(Time.time * bobSpeed) * bobHeight);
        transform.position = floatY;
    }
}
