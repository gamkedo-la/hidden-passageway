using UnityEngine;
using UnityEngine.Assertions;

public class ScreenShake : MonoBehaviour
{
	[SerializeField] private Transform cam;

	private Vector3 originalPos;
	private float shakeDuration = 0f;
	private float shakeAmount;
	private float decreaseFactor;

	void Start( )
	{
		Assert.IsNotNull( cam );
	}

	void Update( )
	{
		if ( shakeDuration > 0 )
		{
			cam.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			cam.localPosition = originalPos;
		}
	}

	public void StartShake( float shakeDuration, float shakeAmount = 0.2f, float decreaseFactor = 1.0f )
	{
		this.shakeDuration = shakeDuration;
		this.shakeAmount = shakeAmount;
		this.decreaseFactor = decreaseFactor;

		originalPos = cam.localPosition;
	}
}
