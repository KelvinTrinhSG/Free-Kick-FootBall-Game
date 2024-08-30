using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveLogo : MonoBehaviour
{
	public Image[] logos;
	public Transform[] points;
	public float speed;
	//public Image img;
	public float stepColor;
	public float timeDelay;
	// Use this for initialization
	void Start()
	{
		StartCoroutine("showLogo");
	}

	// Update is called once per frame
	void Update()
	{
		logos[0].transform.position = Vector3.MoveTowards(logos[0].transform.position, points[0].transform.position, speed);
		logos[1].transform.position = Vector3.MoveTowards(logos[1].transform.position, points[1].transform.position, speed);
	}

    public IEnumerator showLogo()
	{
		float count = 0f;
		while (count <= 1f)
		{
			logos[0].color = new Vector4(1f, 1f, 1f, count);
			logos[1].color = new Vector4(1f, 1f, 1f, count);
			logos[2].color = new Vector4(1f, 1f, 1f, count);
			count += stepColor;
			yield return new WaitForSeconds(timeDelay);
		}
		SceneManager.LoadScene(1);
	}
}
