using UnityEngine;
using System.Collections;

public class Defende : MonoBehaviour
{

	public bool isFail;
	public Ball ball;
	public GameObject ground;
	public GameObject shadow;
	public float yBall;
	public SoundManage soundMG;

	// Update is called once per frame
	void Update()
	{
		if (isFail)
		{
			Vector3 vt2 = ball.transform.position;
			shadow.transform.position = new Vector3(vt2.x, yBall - 80f, 0f);
			if (vt2.y > yBall)
				ball.transform.position = new Vector3(vt2.x, yBall, 0f);
			else
				ball.transform.position = new Vector3(vt2.x, vt2.y, 0f);
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (!isFail)
		{
			yBall = ball.transform.position.y;
			isFail = true;
			ball.stopShadow();
			ball.StopAllCoroutines();
			ground.SetActive(true);
			ball.addScoreBall(0);
			//gameObject.SetActive(true);//cho trai banh hien
			soundMG.playSound(5);
			StartCoroutine("resetGame");
		}
	}
	public IEnumerator resetGame()
	{
		yield return new WaitForSeconds(0.5f);
		ground.SetActive(false);
		ball.resetBall(); // isfail dk set false tai event



	}
}
