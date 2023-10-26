using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerCube : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			GameObject.Find("GameManager").GetComponent<VictoryCondition>().GameCondition = GameCondition.Win;
		}
	}
}
