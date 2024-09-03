using UnityEngine;
using System.Collections;

public class StarMove : MonoBehaviour
{
	public Transform targetPoint;
	public float speed;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed);
	}
}
