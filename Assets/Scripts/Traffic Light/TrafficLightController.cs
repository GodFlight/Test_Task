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
		private TrafficLightSettings currentState = null;
		
		public bool DebugMode { get { return debugMode;} }
		public TrafficLightSettings CurrentState { get { return currentState; } }

		private void Awake()
		{
			currentState = null;
			Assert.IsNotNull(trafficLightHolder);
			if (settings.Length > 8)
			{
				Debug.LogError("Traffic light elements cannot be more than 8");
			}
		}

		public void StartTrafficLights()
		{
			trafficLightHolder.PrepareTrafficLights(settings);
			StartCoroutine(Begin());
		}

		private IEnumerator Begin()
		{
			yield return new WaitForSeconds(timeToStart);

			for (int i = 0; i < settings.Length; i++)
			{
				currentState = settings[i];
				float startTime = Time.time;
				float endTime = startTime + settings[i].duration;

				trafficLightHolder.TurnOnTrafficLight(settings[i], i);
				if (debugMode)
				{
					Debug.Log("Current tarffic light: " + currentState.name);
				}

				if (settings[i].hasBlink)
				{
					float delta = settings[i].duration - settings[i].startBlinkAfterBeginTime;
					if (delta < 0f || delta > settings[i].duration)
					{
						settings[i].startBlinkAfterBeginTime = settings[i].duration / 3f;
					}

					StartCoroutine(BlinkTrafficLight(settings[i], endTime, i));
				}

				yield return new WaitUntil(() => Time.time >= endTime);

				if (settings[i].turnOffAfterDuration)
				{
					trafficLightHolder.TurnOffTrafficLight(settings[i], i);
				}

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

			currentState = null;
			trafficLightHolder.HideTrafficLights();
		}

		private IEnumerator BlinkTrafficLight(TrafficLightSettings setting, float endTime, int trafficLightIndex)
		{
			yield return new WaitForSeconds(setting.startBlinkAfterBeginTime);

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
