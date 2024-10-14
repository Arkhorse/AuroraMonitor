using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AuroraProgressController : MonoBehaviour
{
	public TextMeshProUGUI Text;
	public void OnSliderChanged(float value)
	{
		Text.text = value.ToString();
	}
}
