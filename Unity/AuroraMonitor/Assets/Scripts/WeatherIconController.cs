using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AuroraMonitor
{
	public class WeatherIconController : MonoBehaviour
	{
		public Sprite[] Images;
		public Image Image;

		public void Start()
		{
			Image.sprite = Images[1];
		}

		public void UpdateIcon(string name)
		{
			switch (name)
			{
				case "Blizzard":
					Image.sprite = Images[0];
					break;
				case "Clear":
					Image.sprite = Images[1];
					break;
				case "ClearAurora":
					Image.sprite = Images[2];
					break;
				case "Cloudy":
					Image.sprite = Images[3];
					break;
				case "DenseFog":
					Image.sprite = Images[4];
					break;
				case "ElectrostaticFog":
					Image.sprite = Images[5];
					break;
				case "HeavySnow":
					Image.sprite = Images[6];
					break;
				case "LightFog":
					Image.sprite = Images[7];
					break;
				case "LightSnow":
					Image.sprite = Images[8];
					break;
				case "PartlyCloudy":
					Image.sprite = Images[9];
					break;
				case "ToxicFog":
					Image.sprite = Images[10];
					break;
				default:
					break;
			}
		}
	}
}
