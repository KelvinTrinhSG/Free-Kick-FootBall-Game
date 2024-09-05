using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Thirdweb;
using System.Collections.Generic;

public class BallManage : MonoBehaviour
{
	public ChangeBall[] changeBalls;
	public int[] costBall;
	public Score score;
	public StarManage starManage;
	public Sprite[] imgLock;
	public Sprite[] imgUnlock;
	public GameObject[] textCost;
	public int indexCurrent;
	public GameObject circleBall;
	public Ball ball;
	public GameObject ballGoalie;

	// Use this for initialization
	void Start()
	{
		//for (int i=0; i<10; i++) {
		//	PlayerPrefs.SetInt(""+i,0);
		//}


		indexCurrent = PlayerPrefs.GetInt("IndexBall");
		updateShop();
	}
	public void setImgBall()
	{
		indexCurrent = PlayerPrefs.GetInt("IndexBall");
		ball.GetComponent<SpriteRenderer>().sprite = imgUnlock[indexCurrent];
		ballGoalie.GetComponent<SpriteRenderer>().sprite = imgUnlock[indexCurrent];

	}
	public void unLockBall(int index)
	{
		if (index == 0)
		{
			indexCurrent = index;
			circleBall.transform.position = changeBalls[indexCurrent].transform.position;
			PlayerPrefs.SetInt("IndexBall", index);
			updateShop();
		}
		else if (index <= 6)
		{
			if (PlayerPrefs.GetInt("" + index) == 0)
			{
				if (starManage.scoreStar >= costBall[index])
				{
					ClaimNFT(index);
					starManage.scoreStar -= costBall[index];
					starManage.textStar.text = "" + starManage.scoreStar;
					starManage.textScoreStar.text = "" + starManage.scoreStar;
					PlayerPrefs.SetInt("" + index, 1);
					indexCurrent = index;
					updateShop();
					PlayerPrefs.SetInt("IndexBall", index);
				}
			}
			else
			{
				ClaimNFT(index);
				indexCurrent = index;
				updateShop();
				PlayerPrefs.SetInt("IndexBall", index);
			}

		}
		else if (index == 7)
		{
			if (PlayerPrefs.GetInt("" + index) == 0)
			{
				ClaimNFT(index);
				PlayerPrefs.SetInt("" + index, 1);
				indexCurrent = index;
				PlayerPrefs.SetInt("IndexBall", index);
				updateShop();
			}
			else
			{
				ClaimNFT(index);
				indexCurrent = index;
				updateShop();
				PlayerPrefs.SetInt("IndexBall", index);
			}
		}
		else
		{
			if (PlayerPrefs.GetInt("" + index) == 0)
			{
				if (score.highScore >= costBall[index])
				{
					ClaimNFT(index);
					PlayerPrefs.SetInt("" + index, 1);
					indexCurrent = index;
					updateShop();
					PlayerPrefs.SetInt("IndexBall", index);
				}
			}
			else {
				ClaimNFT(index);
				indexCurrent = index;
				updateShop();
				PlayerPrefs.SetInt("IndexBall", index);
			}
		}
	}
	public void updateShop()
	{
		int index = 0;
		foreach (ChangeBall ch in changeBalls)
		{
			if (index == 0)
			{
			}
			else if (index <= 7)
			{
				if (PlayerPrefs.GetInt("" + index) == 0)
				{
					ch.GetComponent<Image>().sprite = imgLock[index];
					textCost[index].SetActive(true);
				}
				else
				{
					ch.GetComponent<Image>().sprite = imgUnlock[index];
					textCost[index].SetActive(false);
				}
			}
			else
			{
				Debug.Log("x" + index);
				if ( (score.highScore >= costBall[index]) || PlayerPrefs.GetInt("" + index) > 0 )
				{
					ch.GetComponent<Image>().sprite = imgUnlock[index];
					textCost[index].SetActive(false);
				}
				else
				{
					ch.GetComponent<Image>().sprite = imgLock[index];
					textCost[index].SetActive(true);
				}
			}
			index++;
		}

		circleBall.transform.position = changeBalls[indexCurrent].transform.position;
		if (indexCurrent != 0)
		{
			ball.GetComponent<SpriteRenderer>().sprite = imgUnlock[indexCurrent];
			ballGoalie.GetComponent<SpriteRenderer>().sprite = imgUnlock[indexCurrent];
		}
		else
		{
			ball.GetComponent<SpriteRenderer>().sprite = imgUnlock[0];
			ballGoalie.GetComponent<SpriteRenderer>().sprite = imgUnlock[indexCurrent];
		}

	}

	public string Address { get; private set; }
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

	public Text ClaimingStatusText;

	public async void ClaimNFT(int indexBall)
    {
		Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
		if (indexBall == 1)
		{			
			Contract contractPortugal = ThirdwebManager.Instance.SDK.GetContract(Portugal);
			List<NFT> nftList = await contractPortugal.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractPortugal.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 2)
		{
			Contract contractUnitedKingdom = ThirdwebManager.Instance.SDK.GetContract(UnitedKingdom);
			List<NFT> nftList = await contractUnitedKingdom.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractUnitedKingdom.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 3)
		{
			Contract contractSpain = ThirdwebManager.Instance.SDK.GetContract(Spain);
			List<NFT> nftList = await contractSpain.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractSpain.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 4)
		{
			Contract contractFrance = ThirdwebManager.Instance.SDK.GetContract(France);
			List<NFT> nftList = await contractFrance.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractFrance.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 5)
		{
			Contract contractGermany = ThirdwebManager.Instance.SDK.GetContract(Germany);
			List<NFT> nftList = await contractGermany.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractGermany.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 6)
		{
			Contract contractItaly = ThirdwebManager.Instance.SDK.GetContract(Italy);
			List<NFT> nftList = await contractItaly.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractItaly.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 7)
		{
			Contract contractVietNam = ThirdwebManager.Instance.SDK.GetContract(VietNam);
			List<NFT> nftList = await contractVietNam.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractVietNam.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 8)
		{
			Contract contractSouthAfrica = ThirdwebManager.Instance.SDK.GetContract(SouthAfrica);
			List<NFT> nftList = await contractSouthAfrica.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractSouthAfrica.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 9)
		{
			Contract contractBrazil = ThirdwebManager.Instance.SDK.GetContract(Brazil);
			List<NFT> nftList = await contractBrazil.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractBrazil.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}
		else if (indexBall == 10)
		{
			Contract contractArgentina = ThirdwebManager.Instance.SDK.GetContract(Argentina);
			List<NFT> nftList = await contractArgentina.ERC721.GetOwned(Address);
			if (nftList.Count == 0)
			{
				ClaimingStatusText.text = "Claiming NFT!";
				await contractArgentina.ERC721.ClaimTo(Address, 1);
				ClaimingStatusText.text = "Claimed";
			}
			else
			{
				ClaimingStatusText.text = "Owned";
				return;
			}
		}


    }
}
