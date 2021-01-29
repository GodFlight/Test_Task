using System.Collections;
using UnityEngine;

using TrafficLight.UI;

namespace TrafficLight
{
	public class TrafficLightController : MonoBehaviour
	{
		public TrafficLightHolder trafficLightHolder;
		public TrafficLightSettings[] settings;
		[Space]
		public float timeToStart = 0f;
		public bool turnOnTrafficLightsAfterCompletion;

		private void Start()
		{
			trafficLightHolder.PrepareTrafficLights(settings);
			StartCoroutine(StartTrafficLight());
		}

		private IEnumerator StartTrafficLight()
		{
			yield return new WaitForSeconds(timeToStart);

			for (int i = 0; i < settings.Length; i++)
			{
				float startTime = Time.time;
				float endTime = startTime + settings[i].duration;

				trafficLightHolder.TurnOnTrafficLight(settings[i], i);


				if (settings[i].hasBlink)
				{
					float delta = settings[i].duration - settings[i].startBlinkAfterDurationTime;
					if (delta < 0f || delta > settings[i].duration)
					{
						settings[i].startBlinkAfterDurationTime = settings[i].duration / 3f;
					}

					StartCoroutine(BlinkTrafficLight(settings[i], endTime, i));
				}

				yield return new WaitUntil(() => Time.time >= endTime);

				if (settings[i].turnOffAfterDuration)
				{
					trafficLightHolder.TurnOffTrafficLight(settings[i], i);
				}

				Debug.Log("time for update: " + Time.time);
			}

			if (!turnOnTrafficLightsAfterCompletion)
			{
				trafficLightHolder.TurnOffTrafficLights(settings);
			}
			else
			{
				trafficLightHolder.TurnOnTrafficLights(settings);
			}

			yield return new WaitForSeconds(1f);

			trafficLightHolder.HideTrafficLights();
		}

		private IEnumerator BlinkTrafficLight(TrafficLightSettings setting, float endTime, int trafficLightIndex)
		{
			yield return new WaitForSeconds(setting.startBlinkAfterDurationTime);

			YieldInstruction waitForTurnOn = new WaitForSeconds(setting.blinkFrequency / 2f);
			YieldInstruction waitForTurnOff = new WaitForSeconds(setting.blinkFrequency / 2f);
			
			while (Time.time < endTime - (setting.blinkFrequency / 2f))
			{
				yield return waitForTurnOn;
				trafficLightHolder.TurnOnTrafficLight(setting, trafficLightIndex);

				yield return waitForTurnOff;
				trafficLightHolder.TurnOffTrafficLight(setting, trafficLightIndex);
			}

			trafficLightHolder.TurnOffTrafficLight(setting, trafficLightIndex);
		}

		private IEnumerator TurnOffTrafficLightByTime(TrafficLightSettings setting, int trafficLightIndex)
		{
			int nextTrafficLightIndex = trafficLightIndex + 1;

			if (nextTrafficLightIndex < settings.Length)
			{
				yield return new WaitForSeconds(settings[nextTrafficLightIndex].duration - 0.05f);
				trafficLightHolder.TurnOffTrafficLight(setting, trafficLightIndex);
			}
		}
	}
}



