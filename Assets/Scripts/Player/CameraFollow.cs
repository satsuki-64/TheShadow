using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    //核心变量
    //玩家的transform组件
    private Transform playerTransform;
    //初始位置偏移量
    private Vector3 positionOffset;
    //摄像机位置调整量，三轴位移量
    [Header("相机位置调整")]
    public Vector3 displacementOffset = new Vector3();
    //摄像机视角角度调整量，三轴旋转量
    [Header("相机旋转角度调整")]
    public Vector3 rotateOffset = new Vector3();

    //移动后处理变量
    //位移终值
    private Vector3 finalPosition;
    //当前摄影机位移速率，，由SmoothDamp()函数自行更改
    private Vector3 velocity = Vector3.zero;
    //完成平滑处理所需的时间
    [Header("跟随后处理时间")]
    public float smoothTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        //找到玩家的transform组件
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //得到初始位置偏移量
        positionOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //旋转
        Quaternion rotation = Quaternion.Euler(rotateOffset);
        //位置更新
        finalPosition = positionOffset + playerTransform.position + displacementOffset;
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, smoothTime);
        //角度更新
        transform.rotation = rotation;
    }
}
