using UnityEngine;
using System.Collections;

public class PowerInfo : MonoBehaviour {

    public GameObject blackoutObject;
    public int myPowerupIndex;
    public float aquireDifficulty = 1;
    public float chitCost;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
        if (PlayerPrefs.GetInt(gameObject.transform.name.ToString()) == 1)
        {
            blackoutObject.SetActive(false);
        }
	}

    public void UnlockSelf()
    {
        PlayerPrefs.SetInt(gameObject.transform.name.ToString(), 1);
    }


}
