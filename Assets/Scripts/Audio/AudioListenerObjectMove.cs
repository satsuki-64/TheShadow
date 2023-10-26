using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerObjectMove : MonoBehaviour
{
    private GameObject listenerObject;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        listenerObject = this.gameObject;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        listenerObject.transform.position = player.transform.position;
    }
}
