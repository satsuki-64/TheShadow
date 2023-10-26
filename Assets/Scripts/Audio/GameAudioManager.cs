using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
	private static GameAudioManager instance;
	public static GameAudioManager Instance() 
	{
		if (instance != null) 
		{
			return instance;
		}

		Debug.Log("AudioManager Îª¿Õ£¡");
		return null;
	}

	public AudioSource AudioSource_Music;
	public AudioSource AudioSource_Sound;

	public AudioClip[] Music_AudioClips;
	public AudioClip[] Sound_AudioClips;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			GameObject.Destroy(this.gameObject);
			return;
		}

		AudioSource_Music = this.gameObject.AddComponent<AudioSource>();
		AudioSource_Sound = this.gameObject.AddComponent<AudioSource>();

		AudioSource_Music.volume = 0.8f;
	}

	public void PlayMusic(int musicIndex) 
	{
		AudioSource_Music.clip = Music_AudioClips[musicIndex];
		AudioSource_Music.Play();
	}

	public void PlaySound(int soundIndex) 
	{
		AudioSource_Sound.clip = Sound_AudioClips[soundIndex];
		AudioSource_Sound.Play();
	}
}
