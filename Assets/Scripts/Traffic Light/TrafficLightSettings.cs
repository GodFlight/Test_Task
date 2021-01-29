using UnityEngine;

namespace TrafficLight
{
	[CreateAssetMenu (menuName = "Traffic Light Settings")]
	public class TrafficLightSettings : ScriptableObject
	{
		[Range(0f, 25f)]
		public float duration;
		public bool turnOffAfterDuration;
		public bool hasBlink;
		public float startBlinkAfterDurationTime;
		public float blinkFrequency;
		public Color waitingColorOfLight;
		public Color readyColorOfLight;
	}
}

