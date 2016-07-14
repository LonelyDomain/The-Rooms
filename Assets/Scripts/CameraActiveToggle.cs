using UnityEngine;
using System.Collections;

public class CameraActiveToggle : MonoBehaviour 
{
	void OnDisable()
	{
		transform.parent.gameObject.SetActive (false);
	}
	void OnEnable()
	{
		transform.parent.gameObject.SetActive (true);
	}
}
