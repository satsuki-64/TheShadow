using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicPlay : MonoBehaviour
{
	private void Start()
	{
		GameAudioManager.Instance().PlayMusic(3);
	}
}
