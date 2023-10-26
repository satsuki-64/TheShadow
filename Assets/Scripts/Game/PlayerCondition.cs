using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCondition : MonoBehaviour
{
	#region 单例
	private static PlayerCondition instance;
    public static PlayerCondition GetInstance() 
    {
        if (instance == null)  
        {
            Debug.LogWarning("PlayerCondition实例不存在");
            instance = new PlayerCondition();
            Debug.LogWarning("创建PlayerCondition的实例");
        }
        
        return instance;
    }

	private void Awake()
	{
        instance = this;
    }

	#endregion

	[Header("最大体力值")]
    public int MaxEnergyValue = 30;

	[Header("当前体力值")]
	[SerializeField]
    private int energyValue = 30;

    public int EnergyValue 
    {
        get 
        { 
            return this.energyValue; 
        }
        set 
        {
            if (value > MaxEnergyValue) 
            {
                this.energyValue = MaxEnergyValue;
                return;
            }

            if (value < -2)  
            {
                this.energyValue = -2;
                return;
            }

            this.energyValue = value;
            Debug.Log(energyValue);
            return;
        }
    }

	[Header("能量回复时间间隔")]
    public int EnergyRate = 1;

	[Header("单次能量回复数值")]
    public int EnergyNumber = 1;

	
	[SerializeField]
    private float time = 0;

    public Text energyText;

    [SerializeField]
    public GameObject procObject = null;
    private Text text = null;
    private Image imageProc;

	[SerializeField]
    private float amout;

    private void Start()
	{
        procObject = GameObject.Find("StatusUIProc");

        if (procObject!=null)
        {
            Debug.Log("statusPanel is not null");
        }

        imageProc = procObject.GetComponent<Image>();
        text = GameObject.Find("StatusUIProcText").GetComponent<Text>();
    }

	private void Update()
	{
        time += Time.deltaTime;

        if (time>= EnergyRate) 
        {
            EnergyValue += EnergyNumber;
            time = 0;
        }

        UpdateFillAmoutToUI();
    }

    /// <summary>
    /// Set the FillAmoutUI Number
    /// </summary>
    private void UpdateFillAmoutToUI() 
    {
        float energyNow = energyValue;
        float energyMax = MaxEnergyValue;
        amout = energyNow / energyMax;
        //Debug.Log(amout);
        imageProc.fillAmount = amout;

        text.text = energyNow.ToString();
    }
}
