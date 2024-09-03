using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{
	public bool isQuit;
	public Transform[] pointMove;
	public float speed;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (isQuit)
		{
			transform.position = Vector3.MoveTowards(transform.position, pointMove[1].position, speed);
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, pointMove[0].position, speed);
			if (transform.position == pointMove[0].position)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
