using UnityEngine;

namespace TrafficLight
{
	[CreateAssetMenu (menuName = "Traffic Light Settings")]
	public class TrafficLightSettings : ScriptableObject
	{
		[Range(0f, 25f)]
		public float duration;
		public bool inUISpace;
		public bool inRealSpace = false;
		public Sprite spriteOfTrafficLight;
		public Color colorOfLight;
	}
}

