using UnityEngine;
using UnityEngine.Assertions;

public class SlidingDoor : MonoBehaviour
{
	[SerializeField] private Animator animator = null;
	[SerializeField] private MorseInteractable[] morseCodes = null;

	void Start ()
	{
		Assert.IsNotNull( animator );
		Assert.IsNotNull( morseCodes );
		Assert.AreNotEqual( morseCodes.Length, 0 );
	}

	void FixedUpdate ()
	{
		bool shouldOpen = true;

		foreach ( var code in morseCodes )
		{
			if ( !code.CodeIsCorrect )
				shouldOpen = false;
		}

		if (shouldOpen)
		{
			foreach ( var code in morseCodes )
			{
				code.enabled = false;
			}

			animator.SetBool( "Open", true );
			enabled = false;
		}
	}
}
