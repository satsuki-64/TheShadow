using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    //���ı���
    //��ҵ�transform���
    private Transform playerTransform;
    //��ʼλ��ƫ����
    private Vector3 positionOffset;
    //�����λ�õ�����������λ����
    [Header("���λ�õ���")]
    public Vector3 displacementOffset = new Vector3();
    //������ӽǽǶȵ�������������ת��
    [Header("�����ת�Ƕȵ���")]
    public Vector3 rotateOffset = new Vector3();

    //�ƶ��������
    //λ����ֵ
    private Vector3 finalPosition;
    //��ǰ��Ӱ��λ�����ʣ�����SmoothDamp()�������и���
    private Vector3 velocity = Vector3.zero;
    //���ƽ�����������ʱ��
    [Header("�������ʱ��")]
    public float smoothTime = 1;


    // Start is called before the first frame update
    void Start()
    {
        //�ҵ���ҵ�transform���
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //�õ���ʼλ��ƫ����
        positionOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //��ת
        Quaternion rotation = Quaternion.Euler(rotateOffset);
        //λ�ø���
        finalPosition = positionOffset + playerTransform.position + displacementOffset;
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, smoothTime);
        //�Ƕȸ���
        transform.rotation = rotation;
    }
}
