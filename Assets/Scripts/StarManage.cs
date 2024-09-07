using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarManage : MonoBehaviour
{
	public int scoreStar;
	public Text textStar;
	public Text textScoreStar;
	// Use this for initialization
	void Start()
	{
		//PlayerPrefs.SetInt ("Star",50);
		//	if (PlayerPrefs.GetInt ("Set2") == 0) {
		//	PlayerPrefs.SetInt("Set2",1);
		//	PlayerPrefs.SetInt ("Star",0);
		//}
		scoreStar = PlayerPrefs.GetInt("Star");
		textStar.text = "x" + scoreStar;
		textScoreStar.text = "x" + scoreStar;
	}


	void OnApplicationQuit()
	{
		PlayerPrefs.SetInt("Star", scoreStar);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "StarMove")
		{
			transform.Rotate(new Vector3(0, 0, 72));
			other.gameObject.SetActive(false);
			StartCoroutine("rotationStar");
			scoreStar++;
			textStar.text = "x" + scoreStar;
			textScoreStar.text = "x" + scoreStar;
			PlayerPrefs.SetInt("Star", scoreStar);
		}
	}

	public void ResetScoreStar() {
		scoreStar = 0;
		textStar.text = "x" + 0;
		textScoreStar.text = "x" + 0;
		PlayerPrefs.SetInt("Star", 0);
	}
	public IEnumerator rotationStar()
	{
		float count = 0f;
		float rota = -6f;
		while (count <= 4f)
		{
			transform.Rotate(new Vector3(0f, 0f, -rota));
			count++;
			yield return new WaitForSeconds(0.05f);
		}
		count = 0;
		while (count <= 4f)
		{
			transform.Rotate(new Vector3(0f, 0f, rota));
			count++;
			Debug.Log("" + count);
			yield return new WaitForSeconds(0.05f);
		}
	}
}
