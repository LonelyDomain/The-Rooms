using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour 
{
	[SerializeField]
	private Camera m_CameraToActivate;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player" || Camera.main == m_CameraToActivate)
			return;

		Camera oldMainCamera = Camera.main;

		oldMainCamera.gameObject.SetActive(false);
		oldMainCamera.tag = "Untagged";

		m_CameraToActivate.gameObject.SetActive(true);
		m_CameraToActivate.tag = "MainCamera";
	}
}
