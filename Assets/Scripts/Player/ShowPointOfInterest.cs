using UnityEngine;
using UnityEngine.Assertions;

public class ShowPointOfInterest : MonoBehaviour
{
	[SerializeField] private Transform camRig = null;
	[SerializeField] private ViewControl view = null;
	[SerializeField] private WalkControl walk = null;
	[SerializeField] private float anglesPerSec = 200f;
	[SerializeField] private float backMultiplayer = 0.5f;

	private Transform lookAtPoint = null;
	private Quaternion originalRot;
	private Quaternion neededRotation;
	private bool isRotating = false;
	private bool isReturning = false;
	private float fullRotationTime;
	private float currentRotationTime;

	void Start ()
	{
		Assert.IsNotNull( camRig );
		Assert.IsNotNull( view );
		Assert.IsNotNull( walk );
	}

	void Update ()
	{
		if (isRotating)
		{
			currentRotationTime += Time.deltaTime;
			currentRotationTime = currentRotationTime > fullRotationTime ? fullRotationTime : currentRotationTime;
			camRig.localRotation = Quaternion.Slerp( originalRot, neededRotation, currentRotationTime / fullRotationTime );

			if ( currentRotationTime >= fullRotationTime )
				isRotating = false;
		}

		if ( isReturning )
		{
			currentRotationTime += Time.deltaTime;
			currentRotationTime = currentRotationTime > fullRotationTime ? fullRotationTime : currentRotationTime;
			camRig.localRotation = Quaternion.Slerp( originalRot, neededRotation, currentRotationTime / fullRotationTime );

			if ( currentRotationTime >= fullRotationTime )
				isReturning = false;
		}
	}

	public float Show( Transform point )
	{
		Debug.Log( "Show POI" );

		if ( point == null )
		{
			Debug.LogError( "Looking. Point to look at is NULL." );
			return 0;
		}

		lookAtPoint = point;
		view.enabled = false;
		walk.enabled = false;

		originalRot = camRig.localRotation;
		camRig.LookAt( lookAtPoint );
		neededRotation = camRig.localRotation;
		camRig.localRotation = originalRot;

		isRotating = true;

		float amount = Vector3.Angle( neededRotation.eulerAngles, originalRot.eulerAngles );
		Debug.Log( "amount: " + amount );
		fullRotationTime = amount / anglesPerSec;
		currentRotationTime = 0;
		Debug.Log( "fullRotationTime: " + fullRotationTime );

		return fullRotationTime;
	}

	public void Return( )
	{
		Debug.Log( "Return Cam" );

		if ( lookAtPoint == null )
		{
			Debug.LogError( "Returning. Point to look at is NULL." );
			return;
		}

		Quaternion newOrigin = neededRotation;
		neededRotation = originalRot;
		originalRot = newOrigin;

		isReturning = true;

		float amount = Vector3.Angle( neededRotation.eulerAngles, originalRot.eulerAngles );
		Debug.Log( "amount: " + amount );
		fullRotationTime = amount / anglesPerSec * backMultiplayer;
		currentRotationTime = 0;
		Debug.Log( "fullRotationTime: " + fullRotationTime );

		Invoke( "Returned", fullRotationTime );
	}

	private void Returned( )
	{
		Debug.Log( "Returned Cam" );

		view.enabled = true;
		walk.enabled = true;
	}
}
