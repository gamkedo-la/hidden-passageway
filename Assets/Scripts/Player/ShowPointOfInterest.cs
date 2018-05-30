using UnityEngine;
using UnityEngine.Assertions;

public class ShowPointOfInterest : MonoBehaviour
{
	[SerializeField] private Transform lookAtPoint = null;
	[SerializeField] private Transform cam = null;
	[SerializeField] private ViewControl view = null;
	[SerializeField] private WalkControl walk = null;
	[SerializeField] private float anglesPerSec = 90f;

	private Quaternion originalRot;
	private Quaternion neededRotation;
	private bool isRotating = false;
	private float fullRotationTime;
	private float currentRotationTime;

	void Start ()
	{
		Assert.IsNotNull( lookAtPoint );
		Assert.IsNotNull( cam );
		Assert.IsNotNull( view );
		Assert.IsNotNull( walk );
	}

	void Update ()
	{
		if (isRotating)
		{
			currentRotationTime += Time.deltaTime;
			currentRotationTime = currentRotationTime > fullRotationTime ? fullRotationTime : currentRotationTime;
			cam.localRotation = Quaternion.Slerp( originalRot, neededRotation, currentRotationTime / fullRotationTime );

			if ( currentRotationTime >= fullRotationTime )
			{
				isRotating = false;
				//cam.localRotation = originalRot;
			}
		}
	}

	public float Show()
	{
		Debug.Log( "Show POI" );
		view.enabled = false;
		walk.enabled = false;

		originalRot = cam.localRotation;
		Vector3 direction = lookAtPoint.position - transform.position;
		neededRotation = Quaternion.FromToRotation( cam.forward, direction );
		//neededRotation = Quaternion.LookRotation( lookAtPoint.position - cam.position, Vector3.up );
		isRotating = true;

		Vector3 amountOfRotation = neededRotation.eulerAngles - originalRot.eulerAngles;
		float amout = 0;

		if (amountOfRotation.x > amountOfRotation.y && amountOfRotation.x > amountOfRotation.z )
			amout = amountOfRotation.x;
		else if ( amountOfRotation.y > amountOfRotation.x && amountOfRotation.y > amountOfRotation.z )
			amout = amountOfRotation.y;
		else
			amout = amountOfRotation.z;

		fullRotationTime = amout / anglesPerSec;
		currentRotationTime = 0;

		return fullRotationTime;
	}

	public void Return( )
	{
		Debug.Log( "Return Cam" );
		cam.localRotation = originalRot;

		view.enabled = true;
		walk.enabled = true;
	}
}
