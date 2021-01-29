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

		public void TurnOnTrafficLight(TrafficLightSettings setting, int indexOfTrafficLight)
		{
			trafficLights[indexOfTrafficLight].color = setting.readyColorOfLight;
		}

		public void TurnOffTrafficLight(TrafficLightSettings setting, int indexOfTrafficLight)
		{
			trafficLights[indexOfTrafficLight].color = setting.waitingColorOfLight;
		}

		public void TurnOffTrafficLights(TrafficLightSettings[] settings)
		{
			for (int i = 0; i < settings.Length; i++)
			{
				trafficLights[i].color = settings[i].waitingColorOfLight;
			}
		}

		public void TurnOnTrafficLights(TrafficLightSettings[] settings)
		{
			for (int i = 0; i < settings.Length; i++)
			{
				trafficLights[i].color = settings[i].readyColorOfLight;
			}
		}

		public void HideTrafficLights()
		{
			foreach (Image lightTrafficImage in trafficLights)
			{
				lightTrafficImage.gameObject.SetActive(false);
			}
		}
	}
}

