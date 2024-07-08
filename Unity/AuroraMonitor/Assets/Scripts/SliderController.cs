using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AuroraMonitor
{
	public class SliderController : MonoBehaviour
	{
		int progress = 0;
		public Slider Slider;
		public Text Text;
		public void OnSliderChanged(float value)
		{
			Text.text = $"{value}%";
		}

		public void UpdateProgress()
		{
			progress++;
			Slider.value = progress;
		}
	}
}
