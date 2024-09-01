using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SoundManage : MonoBehaviour
{
	public AudioSource[] audioS;
	public bool isSound;
	public bool isMelody;
	public Sprite[] img;
	public Button btSound;
	public Button btMelody;
	// Use this for initialization
	void Start()
	{

		string Sound = PlayerPrefs.GetString("Sound");
		if (Sound == "True")
		{
			isSound = true;
		}
		setImgSound();
		string melody = PlayerPrefs.GetString("Melody");
		if (melody == "True")
		{
			isMelody = true;
		}
		setImdMelody();
	}

	public void playSound(int index)
	{
		for (int i = 1; i < audioS.Length; i++)
		{
			if (i != index)
			{
				if (audioS[i].isPlaying)
					audioS[i].Stop();
			}
			else
			{
				if (isSound)
					audioS[i].Play();
				//Debug.Log("Play "+i);

			}
		}
	}
	public void disableSound()
	{
		if (!isSound)
		{
			for (int i = 1; i < audioS.Length; i++)
			{
				if (audioS[i].isPlaying)
					audioS[i].Stop();
			}
		}
	}
	public void playBgSound()
	{
		if (isMelody)
			audioS[0].Play();
		else
			audioS[0].Stop();
	}
	public void changeSound()
	{
		isSound = !isSound;
		PlayerPrefs.SetString("Sound", isSound + "");
		if (!isSound)
			disableSound();
		setImgSound();
	}
	public void changMelody()
	{
		isMelody = !isMelody;
		PlayerPrefs.SetString("Melody", isMelody + "");
		setImdMelody();
	}
	public void setImgSound()
	{
		if (isSound)
		{
			btSound.GetComponent<Image>().sprite = img[0];
		}
		else
			btSound.GetComponent<Image>().sprite = img[1];
	}
	public void setImdMelody()
	{
		playBgSound();
		if (isMelody)
		{
			btMelody.GetComponent<Image>().sprite = img[2];

		}
		else
			btMelody.GetComponent<Image>().sprite = img[3];
	}
}
