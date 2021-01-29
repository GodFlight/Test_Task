using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrafficLight.UI
{
	public class TrafficLightHolder : MonoBehaviour
	{
		[SerializeField]
		private Image[] trafficLights;

		public void PrepareTrafficLights(TrafficLightSettings[] settings)	
		{
			for (int i = 0; i < settings.Length; i++)
			{
				trafficLights[i].gameObject.SetActive(true);
				trafficLights[i].color = settings[i].waitingColorOfLight;
			}
		}
	}
}

