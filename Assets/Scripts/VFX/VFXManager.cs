using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    [Header("火焰特效")]
    public VisualEffect FireEffect;
    [Header("火苗特效")]
    public VisualEffect FlameEffect;
    [Header("鬼魂特效")]
    public VisualEffect GhostEffect;
    [Header("爆炸特效")]
    public VisualEffect BoomEffect;

    [Header("爆炸特效物体")]
    public GameObject BoomObj;

    [Header("父物体")]
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
