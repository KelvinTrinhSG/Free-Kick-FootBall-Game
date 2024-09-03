using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
				indexCurrent = index;
				updateShop();
				PlayerPrefs.SetInt("IndexBall", index);
			}

		}
		else if (index == 7)
		{
			if (PlayerPrefs.GetInt("" + index) == 0)
			{				
				PlayerPrefs.SetInt("" + index, 1);
				indexCurrent = index;
				PlayerPrefs.SetInt("IndexBall", index);
				updateShop();
			}
			else
			{
				indexCurrent = index;
				updateShop();
				PlayerPrefs.SetInt("IndexBall", index);
			}

		}
		else
		{
			if (score.highScore >= costBall[index])
			{
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
				if (score.highScore >= costBall[index])
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
}
