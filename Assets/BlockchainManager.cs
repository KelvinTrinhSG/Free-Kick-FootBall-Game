using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlockchainManager : MonoBehaviour
{
    public string Address { get; private set; }

    public Button nftButton;
    public Button playButton;

    public TextMeshProUGUI nftButtonText;
    public TextMeshProUGUI playButtonText;

    string NFTAddressSmartContract = "0x90f654652002Cc97979b3f220a5583A4ee8d1396";

    private void Start()
    {
        nftButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
    }

    public async void Login()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        Debug.Log(Address);
        Contract contract = ThirdwebManager.Instance.SDK.GetContract(NFTAddressSmartContract);
        List<NFT> nftList = await contract.ERC721.GetOwned(Address);
        if (nftList.Count == 0)
        {
            nftButton.gameObject.SetActive(true);
        }
        else
        {
            playButton.gameObject.SetActive(true);
        }
    }

    public async void ClaimNFTPass()
    {
        nftButtonText.text = "Claiming...";
        nftButton.interactable = false;
        var contract = ThirdwebManager.Instance.SDK.GetContract(NFTAddressSmartContract);
        var result = await contract.ERC721.ClaimTo(Address, 1);
        nftButtonText.text = "Claimed NFT Pass!";
        nftButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    string Portugal = "0x04de39e4757d00fdaed113508185BcCee4bA0dAa";
    string UnitedKingdom = "0xeeEe22E4dbcD77396af398fcA6c5540452f3EF62";
    string Spain = "0x7747E5BDBd0Ddd4e81f8c6A8BdD9AcADdCAa38b1";
    string France = "0xafc7f70C47c80d6E00A46AAd2552990398f23451";
    string Germany = "0x4CdeeB2150DF1204Bb8983B1241bbf965687b8eF";
    string Italy = "0x234119e62AcB9EF6f3eBAD011dB41bABBE0757DD";
    string VietNam = "0xFdAB6df69D8965B51c1374A145377e9917F32F48";
    string SouthAfrica = "0x89207E56688410871D9EBeB8D0192A9F58DA6C38";
    string Brazil = "0x10CEba92c84f3bDC558B706e3D5925B98b4484F1";
    string Argentina = "0x8484cEc1D4475352eB775B72a9C1903CecD4bc12";

    public async void CheckNFT()
    {
        List<string> addressList = new List<string>();

        addressList.Add(Portugal);
        addressList.Add(UnitedKingdom);
        addressList.Add(Spain);
        addressList.Add(France);
        addressList.Add(Germany);
        addressList.Add(Italy);
        addressList.Add(VietNam);
        addressList.Add(SouthAfrica);
        addressList.Add(Brazil);
        addressList.Add(Argentina);

        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        int index = 1;
        foreach (string nftaddress in addressList)
        {
            Debug.Log(nftaddress);
            Contract nftcontract = ThirdwebManager.Instance.SDK.GetContract(nftaddress);
            List<NFT> nftList = await nftcontract.ERC721.GetOwned(Address);
            if (nftList.Count == 0)
            {
               
            }
            else
            {
                PlayerPrefs.SetInt("" + index, 1);
            }
            index++;
        }       
    }
}
