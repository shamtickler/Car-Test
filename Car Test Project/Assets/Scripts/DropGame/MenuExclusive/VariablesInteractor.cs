using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VariablesInteractor : MonoBehaviour {

    private GameObject _variables;
    [SerializeField]
    private GameObject buyUI;
    [SerializeField]
    private Text chitCostTxt;

    [HideInInspector]
    public GameObject currentStackerBuy;
    [HideInInspector]
    public GameObject currentStacker;

	// Use this for initialization
	void Start () {
        _variables = GameObject.FindWithTag("Variable");
        DontDestroyOnLoad(_variables);
    }

    void Update()
    {
        currentStacker = _variables.GetComponent<Variables>().currentStacker;
    }


    public void SetStacker(int _stackerIndex)
    {
        //first checks to see if the stacker is unlocked
        bool _isUnlocked;
        _isUnlocked = _variables.GetComponent<Variables>().CheckIfUnlocked(_stackerIndex);
        
        //next, show purchase ui if not unlocked or select it if it is unlocked
        if (_isUnlocked)
        {
            buyUI.SetActive(false);
            _variables.GetComponent<Variables>().SetStacker(_stackerIndex);
            currentStacker = _variables.GetComponent<Variables>().currentStacker;

        }
        else
        {
            //find out what stacker it is
            //currentStackerBuy = _variables.GetComponent<Variables>().GetStacker(_stackerIndex);

            //set price visual
            //chitCostTxt.text = currentStackerBuy.GetComponent<StackerInfo>().chitCost.ToString();

            //get the price of the clicked stacker and set the text visual
            chitCostTxt.text = _variables.GetComponent<Variables>().GetStacker(_stackerIndex).GetComponent<StackerInfo>().chitCost.ToString();

            //if the buy ui is already enabled is disables it instead
            if (buyUI.activeSelf == true)
            {
                //check if the clicked stacker already has the buy menu shown, if so it hides it
                if (currentStackerBuy == _variables.GetComponent<Variables>().GetStacker(_stackerIndex))
                {
                    buyUI.SetActive(false);
                }
            }
            else
            {
                buyUI.SetActive(true);
            }
        }

        //Finally set the variable to the stacker that was just clicked for future referance in this method
        currentStackerBuy = _variables.GetComponent<Variables>().GetStacker(_stackerIndex);
    }

    public void BuyStacker()
    {
        int price;
        price = currentStackerBuy.GetComponent<StackerInfo>().chitCost;
        //compare price to total chits
        if (price <= PlayerPrefs.GetInt("Chits"))
        {
            PlayerPrefs.SetInt("Chits", PlayerPrefs.GetInt("Chits") - price);
            PlayerPrefs.SetInt(currentStackerBuy.name, 1);
            buyUI.SetActive(false);
        }

    }

}
