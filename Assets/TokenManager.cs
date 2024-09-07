using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using TMPro;
using UnityEngine.UI;

public class TokenManager : MonoBehaviour
{
    public string Address { get; private set; }
    public TextMeshProUGUI fanTokenTitleText;
    public TextMeshProUGUI fanTokenText;
    public Button ratingBtn;

    private string fanTokenAddress = "0x94B99aA7Ab54CC563C7ae488b2fa4b5ce539f387";

    // A reference to the GameObject "star Score"
    public GameObject starScoreObject;

    private StarManage starManage;
    void Start()
    {
        // Find the StarManage script on the "star Score" GameObject
        if (starScoreObject != null)
        {
            starManage = starScoreObject.GetComponent<StarManage>();

            Debug.Log(starManage);

            // Check if StarManage script exists on the GameObject
            if (starManage == null)
            {
                Debug.LogError("StarManage script not found on the star Score object.");
            }
        }
        else
        {
            Debug.LogError("star Score object is not assigned.");
        }
        FanTokenBalance();
    }

    public async void FanTokenBalance() {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract = ThirdwebManager.Instance.SDK.GetContract(fanTokenAddress);
        var result = await contract.ERC20.BalanceOf(Address);
        fanTokenText.text = result.displayValue;
    }

    public async void ClaimFanToken()
    {
        //Check star numnber
        if (starManage.scoreStar <= 0) return;

        fanTokenTitleText.text = "Claiming...";
        fanTokenText.text = "";
        ratingBtn.interactable = false;

        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract = ThirdwebManager.Instance.SDK.GetContract(fanTokenAddress);

        Debug.Log("starManage.scoreStar: " + starManage.scoreStar.ToString());

        await contract.ERC20.ClaimTo(Address, starManage.scoreStar.ToString());

        fanTokenTitleText.text = "Claimed";

        //Reduce stars
        if (starManage != null)
        {
            starManage.ResetScoreStar();
        }

        var result = await contract.ERC20.BalanceOf(Address);

        fanTokenTitleText.text = "FAN Token";
        fanTokenText.text = result.displayValue;
        ratingBtn.interactable = true;
    }
}
