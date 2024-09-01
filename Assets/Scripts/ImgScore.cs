using UnityEngine;
using System.Collections;

public class ImgScore : MonoBehaviour
{
	public float pointY;
	public float speed;
	public Transform point;
	// Use this for initialization
	void Start()
	{
		pointY = point.position.y;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, point.position, speed);
	}
	public void setX(float value)
	{
		point.position = new Vector3(value, pointY, 0f);
	}
	public IEnumerator stopShow()
	{
		yield return new WaitForSeconds(0.7f);
		gameObject.SetActive(false);
	}
}
