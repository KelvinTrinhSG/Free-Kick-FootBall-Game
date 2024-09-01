using UnityEngine;
using System.Collections;

public class Goalie : MonoBehaviour
{
	public Animator anim;
	public GameObject shadow;
	public Ball ball;
	public Transform pointNewShadow;
	public bool isCollision;
	public Goal goal;
	public Level level;
	public GameObject goalObject;
	public LayerMask ground;
	public float radius;
	public bool isJump;
	public Transform pointBegin;
	public GameObject shadowGoalie;
	public SoundManage soundMG;
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		isJump = Physics2D.OverlapCircle(ball.transform.position, radius, ground);
		if (isJump && Random.Range(0, 5) == 0)
		{
			anim.SetBool("isJump", true);
		}
		if (isCollision)
		{
			Vector3 vt = shadow.transform.position;
			shadow.transform.position = new Vector3(transform.position.x, vt.y, vt.z);
		}
		shadowGoalie.transform.position = new Vector3(transform.position.x, shadowGoalie.transform.position.y, 0);
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (!goal.isGoal)
		{
			if (other.collider.tag == "Ball")
			{
				isCollision = true;
				anim.SetBool("isGoal", true);
				anim.SetBool("New", false);
				ball.gameObject.SetActive(false);
				Vector3 vtShadow = shadow.transform.position;
				shadow.transform.position = new Vector3(vtShadow.x, pointNewShadow.position.y, vtShadow.z);
				ball.stopShadow();
				level.isRun = false;
				ball.addScoreBall(0);
				soundMG.playSound(5);

				StartCoroutine("resetGame");
			}
		}
	}
	public IEnumerator resetGame()
	{
		yield return new WaitForSeconds(0.5f);
		goalObject.SetActive(false);

		isCollision = false;
		ball.gameObject.SetActive(true);
		//transform.position = new Vector3 (0f,0f,0f);
		ball.resetBall();

	}
	public IEnumerator setAnim()
	{
		anim.SetBool("isGoal", true);
		yield return new WaitForSeconds(0.1f);
		anim.SetBool("New", false);
	}
}
