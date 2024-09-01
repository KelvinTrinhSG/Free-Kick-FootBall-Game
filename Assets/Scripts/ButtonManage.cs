using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonManage : MonoBehaviour
{
	public int ID;
	public Button[] buttons;
	public Transform[] points;
	public bool isShow;
	public float speed;
	public BallManage shop;
	public GameObject ball;
	public int STATE;
	public QuitGame quitGame;
	public BallManage ballMg;

	// Use this for initialization
	void Start()
	{
		StartCoroutine("showIcon");
	}

	// Update is called once per frame
	void Update()
	{
		if (isShow)
		{
			transform.position = Vector3.MoveTowards(transform.position, points[0].position, speed);
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, points[1].position, speed);

		}
		if (Input.GetKeyUp(KeyCode.Escape) && ID == 1)
		{
			if (STATE == 0)
			{

				quitGame.isQuit = true;
				quitGame.gameObject.SetActive(true);
			}
			else
			{
				closeShop();
			}

		}
	}
	public IEnumerator showIcon()
	{
		foreach (Button bt in buttons)
		{
			bt.gameObject.SetActive(true);
		}
		float count = 0.0f;
		while (count <= 1f)
		{
			foreach (Button bt in buttons)
			{
				bt.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, count);
			}
			count += 0.05f;
			yield return new WaitForSeconds(0.01f);
		}
	}
	public IEnumerator hideIcon()
	{
		float count = 1f;
		while (count >= 0.0f)
		{
			foreach (Button bt in buttons)
			{
				bt.GetComponent<Image>().color = new Vector4(1f, 1f, 1f, count);
			}
			count -= 0.05f;
			yield return new WaitForSeconds(0.01f);
		}
		foreach (Button bt in buttons)
		{
			bt.gameObject.SetActive(false);
		}
	}
	public void openShop()
	{
		STATE = 1;
		ball.SetActive(false);
		shop.gameObject.SetActive(true);
		shop.updateShop();
	}
	public void closeShop()
	{
		STATE = 0;
		ball.SetActive(true);
		shop.gameObject.SetActive(false);
	}
	public void YesQuit()
	{
		Application.Quit();
	}
	public void NoQuit()
	{

		quitGame.isQuit = false;
	}
}
