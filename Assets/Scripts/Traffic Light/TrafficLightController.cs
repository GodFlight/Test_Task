using System.Collections;
using UnityEngine.Assertions;
using UnityEngine;

using TrafficLight.UI;

namespace TrafficLight
{
	public class TrafficLightController : MonoBehaviour
	{
		[Header("Traffic Lights settings")]
		[SerializeField]
		private TrafficLightHolder trafficLightHolder;
		[SerializeField]
		private TrafficLightSettings[] settings;
		[Space]
		[SerializeField]
		private float timeToStart = 0f;
		[SerializeField]
		private bool turnOnTrafficLightsAfterCompletion = false;
		[Space]
		[SerializeField]
		private bool debugMode = false;
		private string currentStateName = "None";
		
		public bool DebugMode { get { return debugMode;} }
		public string CurrentStateName { get { return currentStateName; } }

		private void Start()
		{
			Assert.IsNotNull(trafficLightHolder);
			trafficLightHolder.PrepareTrafficLights(settings);
			StartCoroutine(StartTrafficLight());
		}

		private IEnumerator StartTrafficLight()
		{
			yield return new WaitForSeconds(timeToStart);

			for (int i = 0; i < settings.Length; i++)
			{
				currentStateName = settings[i].name;
				float startTime = Time.time;
				float endTime = startTime + settings[i].duration;

				trafficLightHolder.TurnOnTrafficLight(settings[i], i);
				if (debugMode)
				{
					Debug.Log("Current tarffic light: " + currentStateName);
				}

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

				currentStateName = "Completion";

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
	}
}
