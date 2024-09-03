using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	public int STATUS;
	public Vector2 oldPosition;
	public Vector2 newPosition;
	public bool isShoot;
	public bool isPlay;
	public float stepTime;
	public float stepScale;
	public float minScale;
	public float maxScale;
	public float countStep;
	public float ratioBasic;
	public float ratiotarget;
	public float factor;
	public float stepRoation;
	public Vector2 vt;
	public GameObject shadow;
	public Transform pointShadow;
	public Transform pointStart;
	public Score score;
	public ImgScore[] scoreImg;
	public int indexShowImg;
	public Level level;
	public Goalie goalie;
	public ButtonManage btMg;
	public ButtonManage btMg2;
	public SoundManage soundMg;
	public BallManage ballMg;

	// Use this for initialization
	void Start()
	{
		//		hit = new RaycastHit2D ();
		stepScale = (maxScale - minScale) / countStep;
		Debug.Log(score.score);

		level.setLevel(score.score);
		shadow.transform.position = pointShadow.position;
		ballMg.setImgBall();
	}


	void OnMouseDrag()
	{
		if (!isPlay)
		{
			shadow.transform.position = pointShadow.position;
			oldPosition = Input.mousePosition;
			isPlay = true;
		}

		if (!isShoot)
		{
			newPosition = Input.mousePosition;
			if (newPosition.y - oldPosition.y > ratioBasic)
			{
				GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				vt = Vector2.zero;
				isShoot = true;
				vt = newPosition - oldPosition;
				float ratio = vt.y / ratiotarget;
				vt = new Vector2(vt.x / ratio, vt.y / ratio);
				vt *= factor;
				if (vt != Vector2.zero)
				{
					shadow.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(vt);
					GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(vt);
					//rigidbody2D.velocity = transform.TransformDirection(vt);

					StartCoroutine("scaleBall");
				}
				else
				{
					isShoot = false;
				}
				STATUS = 1;//trang thai 0
				if (btMg.isShow = true)
				{
					hideButton();
					score.GetComponent<Text>().color = new Vector4(1f, 1f, 1f, 1f);
					soundMg.playSound(1);
				}
			}
		}
	}
	public void hideButton()
	{
		btMg.isShow = false;
		btMg2.isShow = false;
		if (score.score == 0)
		{
			btMg.StartCoroutine("hideIcon");
			btMg2.StartCoroutine("hideIcon");
		}
		score.bestScore.SetActive(false);
	}
	public void showButonNoCheck()
	{
		btMg.isShow = true;
		btMg.StartCoroutine("showIcon");
		btMg2.isShow = true;
		btMg2.StartCoroutine("showIcon");
	}

	public void showButon()
	{
		if (STATUS == 1)
		{
			btMg.isShow = true;
			btMg.StartCoroutine("showIcon");
			btMg2.isShow = true;
			btMg2.StartCoroutine("showIcon");
			STATUS = 0;
			score.resetScore();
		}
	}
	void OnMouseUp()
	{
		if (!isShoot)
			isPlay = false;
	}
	public IEnumerator scaleBall()
	{
		int count = 0;
		while (count < countStep)
		{
			Vector3 vtScale = transform.localScale;
			transform.localScale = new Vector3(vtScale.x - stepScale, vtScale.y - stepScale, maxScale);
			shadow.transform.localScale = new Vector3(vtScale.x - stepScale, vtScale.y - stepScale, maxScale);
			transform.Rotate(new Vector3(0, 0, stepRoation));
			count++;
			yield return new WaitForSeconds(stepTime);
		}
	}

	public void resetBall()
	{
		gameObject.SetActive(true);
		level.setLevel(score.score);
		transform.position = pointStart.position;

		isShoot = false;
		isPlay = false;
		GetComponent<Rigidbody2D>().Sleep ();
        transform.localScale = new Vector3(maxScale, maxScale, maxScale);
		transform.localEulerAngles = Vector3.zero;
		transform.Rotate(0f, 0f, 0f);
		oldPosition = Vector2.zero;
		newPosition = Vector2.zero;
		vt = Vector2.zero;
		transform.Rotate(Vector3.zero);
		shadow.transform.localScale = new Vector3(maxScale, maxScale, maxScale);
		shadow.transform.position = pointShadow.position;
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;

		StartCoroutine("StopRigidbody");
    }
	public void stopShadow()
	{
		shadow.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
	}
	public IEnumerator StopRigidbody()
	{
		GetComponent<Rigidbody2D>().isKinematic = true;
		yield return new WaitForFixedUpdate();
		GetComponent<Rigidbody2D>().isKinematic = false;
	}
	public void addScoreBall(int vScore)
	{
		if (vScore > 0)
		{
			score.addScore(vScore);
			indexShowImg = vScore - 1;
			StartCoroutine("showImg");
		}
		else
		{
			showButon();
		}
	}
	public IEnumerator showImg()
	{
		scoreImg[indexShowImg].gameObject.SetActive(true);
		scoreImg[indexShowImg].transform.position = new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z);
		scoreImg[indexShowImg].setX(transform.position.x);
		yield return new WaitForSeconds(0.4f);
		scoreImg[indexShowImg].gameObject.SetActive(false);
	}

}
