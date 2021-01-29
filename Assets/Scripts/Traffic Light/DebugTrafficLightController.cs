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

		private readonly string stateText = "Current State: ";

		private void Awake()
		{
			Assert.IsNotNull(trafficLightController);
			Assert.IsNotNull(currentStateText);
			Assert.IsNotNull(debugCanvas);
		}

		private void LateUpdate()
		{
			if (trafficLightController.DebugMode)
			{
				if (!debugCanvas.active)
				{
					debugCanvas.SetActive(true);
				}
				currentStateText.text = stateText + trafficLightController.CurrentStateName;
			}
			else if (debugCanvas.active)
			{
				debugCanvas.SetActive(false);
			}
		}
	}
}
