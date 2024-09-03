using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	public Ball ball;
	public ButtonManage buttonMg1;
	public ButtonManage buttonMg2;
	public Score score;
	void Start()
	{

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Ball")
		{
			buttonMg1.StopAllCoroutines();
			buttonMg2.StopAllCoroutines();
			ball.stopShadow();
			ball.StopAllCoroutines();
			ball.addScoreBall(0);
			ball.resetBall();
		}
	}

}
