using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    [Header("������Ч")]
    public VisualEffect FireEffect;
    [Header("������Ч")]
    public VisualEffect FlameEffect;
    [Header("�����Ч")]
    public VisualEffect GhostEffect;
    [Header("��ը��Ч")]
    public VisualEffect BoomEffect;

    [Header("��ը��Ч����")]
    public GameObject BoomObj;

    [Header("������")]
    public GameObject stone;
   
    

    // Start is called before the first frame update
    void Start()
    {
        BoomEffect.pause = true;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            VFXMethod.GetInstance().AddEffect(BoomObj, stone);
            
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            BoomEffect.playRate = 100;
        }
    }

    public void ObjDelete(GameObject boomObj)
    {
        GameObject.Destroy(boomObj);
    }
}
