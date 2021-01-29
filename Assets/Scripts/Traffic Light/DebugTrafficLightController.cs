using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

namespace TrafficLight.UI
{
	public class DebugTrafficLightController : MonoBehaviour
	{
		public TrafficLightController trafficLightController;
		public GameObject debugCanvas;
		public TextMeshProUGUI currentStateText;
		public TextMeshProUGUI durationStateText;
		public TextMeshProUGUI turnOffAfterDurationText;
		public TextMeshProUGUI hasBlinkText;
		public TextMeshProUGUI startBlinkAfterBeginTimeText;
		public TextMeshProUGUI blinkFrequencyText;

		private readonly string stateText = "Current State: ";
		private readonly string durationText = "Duration Of State: ";
		private readonly string turnOffAfterDuration = "Turn Off After Duration: ";
		private readonly string hasBlink = "Has Blink: ";
		private readonly string startBlinkAfterBeginTime = "Start Blink After Begin Time: ";
		private readonly string blinkFrequency = "Blink Frequency: ";

		private void Awake()
		{
			Assert.IsNotNull(trafficLightController);
			Assert.IsNotNull(debugCanvas);
			Assert.IsNotNull(currentStateText);
			Assert.IsNotNull(durationStateText);
			Assert.IsNotNull(turnOffAfterDurationText);
			Assert.IsNotNull(startBlinkAfterBeginTimeText);
			Assert.IsNotNull(startBlinkAfterBeginTimeText);
			Assert.IsNotNull(hasBlinkText);
		}

		private void LateUpdate()
		{
			if (trafficLightController.DebugMode)
			{
				if (!debugCanvas.active)
				{
					debugCanvas.SetActive(true);
				}

				if (trafficLightController.CurrentState == null)
				{
					currentStateText.text = "None";
					durationStateText.gameObject.SetActive(false);
					turnOffAfterDurationText.gameObject.SetActive(false);
					hasBlinkText.gameObject.SetActive(false);
					startBlinkAfterBeginTimeText.gameObject.SetActive(false);
					blinkFrequencyText.gameObject.SetActive(false);
					return;
				}
				else
				{
					durationStateText.gameObject.SetActive(true);
					turnOffAfterDurationText.gameObject.SetActive(true);
					hasBlinkText.gameObject.SetActive(true);
				}
				
				currentStateText.text = stateText + trafficLightController.CurrentState.name;
				durationStateText.text = durationText + trafficLightController.CurrentState.duration;
				turnOffAfterDurationText.text = turnOffAfterDuration + trafficLightController.CurrentState.turnOffAfterDuration;
				hasBlinkText.text = hasBlink + trafficLightController.CurrentState.hasBlink;
				
				if (trafficLightController.CurrentState.hasBlink)
				{
					startBlinkAfterBeginTimeText.gameObject.SetActive(true);
					blinkFrequencyText.gameObject.SetActive(true);
					startBlinkAfterBeginTimeText.text = startBlinkAfterBeginTime + trafficLightController.CurrentState.startBlinkAfterBeginTime;
					blinkFrequencyText.text = blinkFrequency + trafficLightController.CurrentState.blinkFrequency;
				}
				else
				{
					startBlinkAfterBeginTimeText.gameObject.SetActive(false);
					blinkFrequencyText.gameObject.SetActive(false);
				}


			}
			else if (debugCanvas.active)
			{
				debugCanvas.SetActive(false);
			}
		}
	}
}
