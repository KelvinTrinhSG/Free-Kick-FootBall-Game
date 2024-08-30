using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLogo : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		if (PlayerPrefs.GetInt("SetSound") == 0)
		{
			PlayerPrefs.SetInt("SetSound", 1);
			PlayerPrefs.SetString("Sound", "True");
			PlayerPrefs.SetString("Melody", "True");
		}
		if (PlayerPrefs.GetString("Sound") == "True")
		{
			GetComponent<AudioSource>().Play();
		}
	}
}
