using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	private AudioSource sound01;
	private AudioSource sound02;
	private AudioSource sound03;
	private AudioSource sound04;
	
	// Use this for initialization
	void Start ()
	{
		AudioSource[] audioSources = GetComponents<AudioSource>();
		sound01 = audioSources[0];
		sound02 = audioSources[1];
		sound03 = audioSources[2];
		sound04 = audioSources[3];
		sound01.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Ita()
	{
		sound02.PlayOneShot(sound02.clip);
	}

	public void Yatta()
	{
		sound03.PlayOneShot(sound03.clip);
	}
	public void Goal()
	{
		sound04.PlayOneShot(sound04.clip);
	}
}
