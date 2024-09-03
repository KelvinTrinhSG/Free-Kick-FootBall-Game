using UnityEngine;
using System.Collections;

public class BonusStar : MonoBehaviour
{
	public bool isShow;
	public Score score;
	public ImgScore scoreImg;
	public bool isCollision;
	public GameObject startMove;
	public SoundManage soundMG;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Ball")
		{
			isShow = false;
			score.addScore(2);
			gameObject.SetActive(false);
			scoreImg.gameObject.SetActive(true);
			scoreImg.transform.position = transform.position;
			scoreImg.StartCoroutine("stopShow");
			startMove.transform.position = transform.position;
			startMove.SetActive(true);
			soundMG.playSound(4);
		}
	}

}
