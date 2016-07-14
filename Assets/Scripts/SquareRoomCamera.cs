using UnityEngine;
using System.Collections;

public class SquareRoomCamera : MonoBehaviour 
{
	[SerializeField, Tooltip("The incremental change in angle when the proper button is pressed")]
	private float m_DeltaAngle;
	[SerializeField, Tooltip("A speed coefficient for camera smoothing")]
	private float m_CameraSpeed;

	// Camera object attached somewhere to this one
	private Camera m_Camera;

	// The rotation angle that the camera wants to be at
	private Vector3 m_TargetRotation;
	// The difference between the camera's current rotation and where it needs to be
	private Vector3 m_OffsetRotation;

	// Whether or not the left or right arrow key was pressed down this frame
	private bool m_HorizontalWasPressed;
	// Whether or not to smoothly transition from the current rotation to the target
	private bool m_SmoothTransition;

	// Use this for initialization
	void Awake () 
	{
		// Set the target rotation to the current one when the game starts
		m_TargetRotation = transform.eulerAngles;

		m_Camera = GetComponentInChildren<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!m_Camera.gameObject.activeInHierarchy) 
		{
			m_OffsetRotation = Vector3.zero;
			m_TargetRotation = new Vector3 (m_TargetRotation.x, 0f, m_TargetRotation.z);
			transform.eulerAngles = m_TargetRotation;		
		}

		float key = Input.GetAxisRaw ("Pan Camera");

		// If a button was not pressed last frame
		if (!m_HorizontalWasPressed && m_Camera.gameObject.activeInHierarchy)
		{
			// Right
			if (key > 0f) 
				Rotate (Direction.Right);		
			// Left
			if (key < 0f) 
				Rotate (Direction.Left);

			if (m_SmoothTransition) 
			{
				// Get the difference between where the camera should be looking and where it is currently
				m_OffsetRotation = m_TargetRotation - transform.eulerAngles;

				m_SmoothTransition = false;
			}
		}

		m_OffsetRotation = 
			new Vector3 (
				m_OffsetRotation.x,		// Don't change this value
				Mathf.LerpAngle(m_OffsetRotation.y, 0f, Time.deltaTime * m_CameraSpeed),	// Smoothly decrease 'm_OffsetRotation.y' until it reaches 0
				m_OffsetRotation.z);	// Don't change this value

		if(Vector3.Distance(m_OffsetRotation, Vector3.zero) < 0.1f)
			m_OffsetRotation = Vector3.zero;

		transform.eulerAngles = m_TargetRotation - m_OffsetRotation;

		// True when the left or right arrow key is pressed down, false otherwise
		m_HorizontalWasPressed = key != 0;
	}

	// Create an enumeration to easily differentiate between a left or right rotation
	private enum Direction {Left, Right};

	// Quick function used to rotate 'm_TargetRotation' by 'm_DeltaAngle' based on the enum passed in
	void Rotate(Direction direction)
	{
		// Looks at the 'direction' variable to decide case statements
		switch (direction) 
		{
		case Direction.Left: 
			Rotate (m_TargetRotation.y + m_DeltaAngle);
			break;
		case Direction.Right:
			Rotate (m_TargetRotation.y - m_DeltaAngle);
			break;		
		}
	}
	void Rotate(float newAngle)
	{
		// Set the rotational value to a new Vector 3
		m_TargetRotation = 
			new Vector3(
				m_TargetRotation.x,
				newAngle,
				m_TargetRotation.z);

		// In case 'm_TargetRotation.y' is ever more than 360, make sure it gets scaled back down
		if (m_TargetRotation.y >= 360f)
			m_TargetRotation = 
				new Vector3 (
					m_TargetRotation.x,
					m_TargetRotation.y - 360,
					m_TargetRotation.z);

		// In case 'm_TargetRotation.y' is ever negative, make sure it gets moved back up
		if (m_TargetRotation.y < 0f)
			m_TargetRotation = 
				new Vector3 (
					m_TargetRotation.x,
					m_TargetRotation.y + 360,
					m_TargetRotation.z);

		// Enable a smooth Transition to the new value
		m_SmoothTransition = true;
	}
}
