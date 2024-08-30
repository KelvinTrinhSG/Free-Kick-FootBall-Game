using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScale : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		Camera.main.aspect = 1280f / 800f;
	}
}
