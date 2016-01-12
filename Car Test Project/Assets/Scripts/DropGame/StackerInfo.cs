using UnityEngine;
using System.Collections;

public class StackerInfo : MonoBehaviour {

    public int chitCost = 100;
    public float stackHeight = 1;
    public AudioClip placeStack;
    public bool overrideUnlock = false;
    public bool isUnlocked = false;
    private Renderer[] rend;
    [SerializeField]
    private bool randomizeRotation = false;
    [SerializeField]
    private Material regularMat;
    [SerializeField]
    private Material blackoutMat;

    [SerializeField]
    private GameObject[] prefabObjects;

    void Start()
    {
        //
        //randomize rotation for all 6 sides if that is selected Default: false
        if (randomizeRotation == true)
        {
            float x = Random.Range(0.0f, 6.0f);
            if (x < 1.0)
            {
                transform.Rotate(0, 0, 0);
            }
            else if (x > 1.0 && x < 2.0)
            {
                transform.Rotate(90, 0, 0);
            }
            else if (x > 2.0 && x < 3.0)
            {
                transform.Rotate(180, 0, 0);
            }
            else if (x > 3.0 && x < 4.0)
            {
                transform.Rotate(-90, 0, 0);
            }
            else if (x > 4.0 && x < 5.0)
            {
                transform.Rotate(0, 90, 0);
            }
            else if (x > 5.0)
            {
                transform.Rotate(0, 180, 0);
            }

        }


        if (overrideUnlock)
        {
            PlayerPrefs.SetInt(gameObject.name, 1);
        }
        int temp = 0;

        //Set renderer referance
        rend = new Renderer[prefabObjects.Length];
        foreach(GameObject i in prefabObjects)
        {
            rend[temp] = i.GetComponent<Renderer>();
            temp++;
        }

        //set material and variales based on isUnlocked variable
        //if it is not unlocked it starts the coroutine to check when it is unlocked
        //if it is already unlocked it should not become locked again so coroutine does not need to run in this case
        if ((PlayerPrefs.GetInt(gameObject.name) == 1) || overrideUnlock)
        {
            //set local variable to true
            isUnlocked = true;
            //set material to regular
            int tempmat = 0;
            foreach (GameObject i in prefabObjects)
            {
                foreach (Material m in rend[temp].materials)
                {
                    rend[temp].materials[tempmat].CopyPropertiesFromMaterial(regularMat);
                    tempmat++;
                }
                temp++;
            }

        }
        else
        {
            isUnlocked = false;
            StartCoroutine(UpdateMaterials());
        }


   
    }


    //using a coroutine to update object material so it doesnt check every fram
    IEnumerator UpdateMaterials()
    {
        while (1 > 0)
        {

            //checks if object is unlocked
            if ((PlayerPrefs.GetInt(gameObject.name) == 1) || overrideUnlock)
            {
                isUnlocked = true;
            }
            else
            {
                isUnlocked = false;
            }



            //changes material to not be blacked out
            if (isUnlocked)
            {
                //set material to normal
                int temp = 0;
                int tempmat = 0;
                foreach (GameObject i in prefabObjects)
                {
                    foreach (Material m in rend[temp].materials)
                    {
                        rend[temp].materials[tempmat].CopyPropertiesFromMaterial(regularMat);
                        tempmat++;
                    }
                    temp++;
                }
            }
            else
            {
                //set material to blackout
                int temp = 0;
                int tempmat = 0;
                foreach (GameObject i in prefabObjects)
                {
                    foreach(Material m in rend[temp].materials)
                    {
                        rend[temp].materials[tempmat].CopyPropertiesFromMaterial(blackoutMat);
                        tempmat++;
                    }
                    temp++;
                }
            }

            yield return new WaitForSeconds(0.5f);

        }
  
    }

}
