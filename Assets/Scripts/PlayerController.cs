using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	private float m_Speed;

	private Rigidbody m_Rigidbody;

	Vector3 m_PreviousCameraRight;
	Vector3 m_PreviousCameraForward;

	private bool m_ButtonWasPressed;

	// Use this for initialization
	void Start () 
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
	}
	
	// FixedUpdate is called once per physics calculation
	void FixedUpdate () 
	{
		Vector3 horizontalMovement = Vector3.zero;
		Vector3 verticalMovement = Vector3.zero;

		if (!m_ButtonWasPressed) 
		{
			m_PreviousCameraRight = Camera.main.transform.parent.right;
			m_PreviousCameraForward = Camera.main.transform.parent.forward;
		}

		if(Input.GetAxis ("Horizontal") != 0) 
		{
			horizontalMovement = 
				Input.GetAxis ("Horizontal") * m_Speed * m_PreviousCameraRight;
		}
		if(Input.GetAxis ("Vertical") != 0) 
		{
			verticalMovement = 
				Input.GetAxis ("Vertical") * m_Speed * m_PreviousCameraForward;
		}

		m_Rigidbody.velocity = horizontalMovement + verticalMovement;

		m_ButtonWasPressed = Input.GetAxis ("Horizontal") != 0f || Input.GetAxis ("Vertical") != 0f;
	}
}
