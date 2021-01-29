using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
	public class RestartButton : MonoBehaviour
	{
		public void Restart()
		{
			SceneManager.LoadScene(0);
		}
	}
}
