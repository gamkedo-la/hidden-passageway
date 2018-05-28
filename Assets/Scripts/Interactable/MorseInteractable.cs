using UnityEngine;
using UnityEngine.Assertions;

public class MorseInteractable : MonoBehaviour
{
	[SerializeField] private GameObject[] codes = null;
	[SerializeField] private int correctCode = -1;

	public bool CodeIsCorrect;// { get; private set; }

	private int element = 0;

	void Start ()
	{
		Assert.IsNotNull( codes );
		Assert.AreNotEqual( codes.Length, 0 );

		do
		{
			element = Random.Range( 0, codes.Length);

		} while ( element == correctCode );
		codes[element].SetActive( true );
	}

	void Update ()
	{

	}

	public void triggerAction( )
	{
		if ( !enabled )
			return;

		element++;
		element = element >= codes.Length ? 0 : element;

		foreach ( GameObject code in codes )
		{
			code.SetActive( false );
		}
		codes[element].SetActive( true );

		CodeIsCorrect = element == correctCode;
	}
}
