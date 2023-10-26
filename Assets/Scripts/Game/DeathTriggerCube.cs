using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerCube : MonoBehaviour
{
	[Header("死亡迷雾Trigger往前移动速度")]
	public float DeathTriggerSpeed;
	
	public AudioSource audioSource;

	private GameObject player;
	private void Start()
	{
		audioSource = this.gameObject.GetComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.Play();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			GameObject.Find("GameManager").GetComponent<VictoryCondition>().GameCondition = GameCondition.Fail;
			GameAudioManager.Instance().PlaySound(3);
		}
	}
	
	private void Update()
	{
		this.gameObject.transform.position += new Vector3(0, 0, DeathTriggerSpeed);
	}
}
