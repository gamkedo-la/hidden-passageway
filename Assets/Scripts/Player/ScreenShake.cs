using UnityEngine;
using UnityEngine.Assertions;

public class ScreenShake : MonoBehaviour
{
	[SerializeField] private Transform cam;
	[SerializeField] private float shakeAmount = 0.7f;
	[SerializeField] private float decreaseFactor = 1.0f;

	private Vector3 originalPos;
	private float shakeDuration = 0f;

	void Start( )
	{
		Assert.IsNotNull( cam );
	}

	void OnEnable( )
	{
		originalPos = cam.localPosition;
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

	public void StartShake( float duration )
	{
		shakeDuration = duration;
	}
}
