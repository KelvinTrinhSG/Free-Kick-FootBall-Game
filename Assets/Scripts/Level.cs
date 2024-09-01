using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{
	public int maxLevel;
	public Transform[] point;
	public Ball ball;
	//
	public bool isRun;
	public Transform[] pointMove;
	public int pointIndex;
	public float speed;
	public Goalie goalie;
	public GameObject GOALIER;

	//
	public BonusStar[] stars;
	//
	public Defende defende;
	public Transform[] pointDefende;

	// Update is called once per frame
	void Update()
	{
		if (isRun)
		{
			GOALIER.transform.position = Vector3.MoveTowards(GOALIER.transform.position, pointMove[pointIndex].position, speed);
			if (GOALIER.transform.position == pointMove[pointIndex].position)
			{
				pointIndex = 1 - pointIndex;
			}
		}
	}
	public void resetStar()
	{
		for (int i = 0; i < stars.Length; i++)
		{
			stars[i].isShow = false;
			stars[i].gameObject.SetActive(false);
		}

	}
	public void setLevel(int mLevel)
	{
		defende.gameObject.SetActive(false);
		goalie.gameObject.SetActive(false);
		goalie.shadowGoalie.SetActive(false);
		StartCoroutine("setAnim");
		resetStar();
		if (Random.Range(0, 5) == 0)
		{
			int index = Random.Range(0, stars.Length);
			stars[index].gameObject.SetActive(true);
			stars[index].isShow = true;

		}

		if (mLevel < 2)
		{
			ball.transform.position = point[0].position;
		}
		else if (mLevel < 5)
		{
			ball.transform.position = point[Random.Range(0, 3)].position;
		}
		else
		{
			int indexLevel = Random.Range(0, maxLevel);
			//int indexLevel=3;
			switch (indexLevel)
			{
				case 0:
					goalie.gameObject.SetActive(true);
					goalie.shadowGoalie.SetActive(true);
					ball.transform.position = point[Random.Range(0, 3)].position;

					if (Random.Range(0, 3) == 0)
					{ //thi cho phep nhay

					}
					isRun = true;
					break;
				case 1:
					goalie.gameObject.SetActive(true);
					goalie.shadowGoalie.SetActive(true);
					ball.transform.position = point[Random.Range(0, 3)].position;
					//	ball.transform.position=new Vector3(Random.Range(pointMove[0].position.x+30,pointMove[1].position.x-30),ball.transform.position.y,0);
					isRun = false;
					break;
				case 2:
					//goalie.gameObject.SetActive (true);
					int indexDefende = Random.Range(0, 2);
					int indexPo = 0;
					if (indexDefende == 0)
						indexPo = 2;
					else
						indexPo = 1;

					ball.transform.position = point[indexPo].position;
					defende.gameObject.SetActive(true);
					defende.transform.position = pointDefende[indexDefende].position;

					break;
				case 3:
					goalie.gameObject.SetActive(true);
					goalie.shadowGoalie.SetActive(true);
					int indexDefende2 = Random.Range(0, 2);
					int indexPo2 = 0;
					if (indexDefende2 == 0)
						indexPo2 = 2;
					else
						indexPo2 = 1;

					ball.transform.position = point[indexPo2].position;
					defende.gameObject.SetActive(true);
					defende.transform.position = pointDefende[indexDefende2].position;
					isRun = true;
					break;
				case 4:
					break;
				case 5:
					break;

			}
		}
	}
	public IEnumerator setAnim()
	{
		if (Random.Range(0, 2) == 0)
			GOALIER.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		else
			GOALIER.transform.eulerAngles = new Vector3(0f, 180f, 0f);
		goalie.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		goalie.transform.position = GOALIER.transform.position;
		goalie.anim.SetBool("isGoal", false);
		goalie.anim.SetBool("isJump", false);
		yield return new WaitForSeconds(0.1f);
		goalie.anim.SetBool("New", true);
		yield return new WaitForSeconds(0.2f);
		goalie.anim.SetBool("New", false);
		//set defende
		defende.isFail = false;
		defende.ground.SetActive(false);
	}
}
