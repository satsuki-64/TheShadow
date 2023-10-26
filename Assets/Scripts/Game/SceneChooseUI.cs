using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SceneChooseUI : MonoBehaviour
{
    public int uiIndex;
    public int leftIndex;
    public int rightIndex;

	public int UINumber;

    public RectTransform[] rectTransforms;

	public RectTransform rectTransformLeft;
	public RectTransform rectTransformMiddle;
	public RectTransform rectTransformRight;
	private void Start()
	{
		uiIndex = 0;
		leftIndex = -1;
		rightIndex = 1;
	}

	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.A)) 
		//{
		//	uiIndex += 1;
		//	leftIndex += 1;
		//	rightIndex += 1;

		//	rectTransforms[leftIndex] = rectTransformLeft;
		//	rectTransforms[uiIndex] = rectTransformMiddle;
		//	rectTransforms[rightIndex] = rectTransformRight;
		//}

		//if (Input.GetKeyDown(KeyCode.D))
		//{
		//	uiIndex -= 1;
		//	leftIndex -= 1;
		//	rightIndex -= 1;

		//	rectTransforms[leftIndex] = rectTransformLeft;
		//	rectTransforms[uiIndex] = rectTransformMiddle;
		//	rectTransforms[rightIndex] = rectTransformRight;
		//}
	}
}
