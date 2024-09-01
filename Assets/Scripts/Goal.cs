using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
	public bool isGoal;
	public GameObject columnGround;
	public Ball ball;
	public GameObject shadow;
	public Transform pointNewShadow;
	public float minCollision;
	public float maxCollision;
	//public BoxCollider2D boxCollision;
	public Goalie goalie;
	public GameObject wall;
	public SoundManage soundMG;
	// Use this for initialization

	// Update is called once per frame
	void Update()
	{
		if (isGoal)
		{
			Vector3 vt = shadow.transform.position;
			shadow.transform.position = new Vector3(ball.transform.position.x, vt.y, vt.z);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Ball")
		{
			isGoal = true;
			ball.stopShadow();
			try
			{
				Vector3 vtShadow = shadow.transform.position;
				shadow.transform.position = new Vector3(vtShadow.x, pointNewShadow.position.y, vtShadow.z);
			}
			catch { }
			ball.StopAllCoroutines();
			ball.addScoreBall(1);
			GetComponent<BoxCollider2D>().enabled = false;
			columnGround.SetActive(true);
			wall.SetActive(true);
			goalie.GetComponent<BoxCollider2D>().enabled = false;
			goalie.GetComponent<SpriteRenderer>().sortingOrder = 5;
			soundMG.playSound(3);
			StartCoroutine("resetGame");
		}
	}
	public IEnumerator resetGame()
	{
		float value = Random.Range(minCollision, maxCollision);
		GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x, value);
		yield return new WaitForSeconds(0.5f);
		isGoal = false;
		wall.SetActive(false);
		columnGround.SetActive(false);
		ball.resetBall();
		GetComponent<BoxCollider2D>().enabled = true;
		goalie.GetComponent<BoxCollider2D>().enabled = true;
		goalie.GetComponent<SpriteRenderer>().sortingOrder = 3;
	}
}
