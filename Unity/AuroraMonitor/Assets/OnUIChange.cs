using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUIChange : MonoBehaviour
{
	public GameObject BackGround;
	public GameObject WindGroup;
	public GameObject AuroraGroup;

    // Update is called once per frame
    void Update()
    {
		Vector2 right = BackGround.GetComponent<RectTransform>().offsetMax;
		Vector2 bottom = BackGround.GetComponent<RectTransform>().offsetMin;
		if (!WindGroup.activeSelf)
		{
			right.x = 90f;
		}
		else right.x = 0f;
		if (!AuroraGroup.activeSelf)
		{
			bottom.y = 51f;
		}
		else bottom.y = 0f;
	}
}
