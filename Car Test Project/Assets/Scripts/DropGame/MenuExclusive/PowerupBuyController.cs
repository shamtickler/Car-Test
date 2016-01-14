using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerupBuyController : MonoBehaviour {

    public GameObject[] powerupTransformList;
    public GameObject halo = null;
    private GameObject variablesObject;
    public GameObject buyMenu;
    public Text priceText;

    private GameObject purchasablePower;
    private RectTransform purchasablePowerRect;

    private int purchasablePowerIndex;
    private bool buyMenuisShowing = false;

	// Use this for initialization
	void Start () {
	variablesObject = GameObject.FindWithTag("Variable");
    }
	
	void Update () {
        //Set buy menu position to whatever object is buyable.
        buyMenu.transform.position = powerupTransformList[purchasablePowerIndex].transform.position;

        // if there is no active power then simply hide the halo
        if (PlayerPrefs.HasKey("ActivePower"))
        {
            halo.SetActive(true);
        }
        else
        {
            halo.SetActive(false);
        }
     halo.transform.position = powerupTransformList[PlayerPrefs.GetInt("ActivePower")].transform.position;





        //end Update
    }



    public void SetPurchasable(int _powerIndex)
    {
        GameObject _power = variablesObject.GetComponent<Variables>().powerupList[_powerIndex];
        //checks menu status and shows or hides acordingly
        if(buyMenuisShowing && purchasablePower == GameObject.Find(_power.gameObject.name))
        {
            buyMenuisShowing = false;
            buyMenu.SetActive(false);
        }
        else
        {
            buyMenu.SetActive(true);
            buyMenuisShowing = true;
            priceText.text = _power.GetComponent<PowerInfo>().chitCost.ToString();
        }
        //sets the power to what the button passed through
        purchasablePower = GameObject.Find(_power.gameObject.name);
        purchasablePowerIndex = _powerIndex;
    }

    public void BuyPower()
    {
        if (PlayerPrefs.GetInt("Chits") >= purchasablePower.GetComponent<PowerInfo>().chitCost)
        {
            PlayerPrefs.SetInt("Chits", (PlayerPrefs.GetInt("Chits") - (int)purchasablePower.GetComponent<PowerInfo>().chitCost));
            purchasablePower.GetComponent<PowerInfo>().UnlockSelf();
            buyMenu.SetActive(false);
            buyMenuisShowing = false;
            PlayerPrefs.SetInt("ActivePower", purchasablePower.GetComponent<PowerInfo>().myPowerupIndex);
        }
    }
}
