using UnityEngine;
using UnityEngine.Assertions;

public class SlidingDoor : MonoBehaviour
{
	[SerializeField] private Transform lookAtPoint = null;
	[SerializeField] private Animator animator = null;
	[SerializeField] private MorseInteractable[] morseCodes = null;
	[SerializeField] private ScreenShake shake = null;
	[SerializeField] private ShowPointOfInterest show = null;
	[SerializeField] private float animationTime = 1.5f;

	[FMODUnity.EventRef]
	public string inputSound;
    FMOD.Studio.EventInstance Music;
    FMOD.Studio.ParameterInstance MusicProgress;
    private float musicProgressCount = 0;

	private bool isOpen = false;

	void Start ()
	{
		Assert.IsNotNull( lookAtPoint );
		Assert.IsNotNull( animator );
		Assert.IsNotNull( shake );
		Assert.IsNotNull( show );
		Assert.IsNotNull( morseCodes );
		Assert.AreNotEqual( morseCodes.Length, 0 );

        Music = FMODUnity.RuntimeManager.CreateInstance("event:/WordWall/WWMusic");
        Music.getParameter("Progress", out MusicProgress);
        Music.start();

	}

	void FixedUpdate ()
	{
		if ( isOpen )
			return;

		bool shouldOpen = true;
        musicProgressCount = 0;

        foreach ( var code in morseCodes )
		{
            if (!code.CodeIsCorrect)
            {
                shouldOpen = false;
            } else
            {
                musicProgressCount++;
            }
		}
        MusicProgress.setValue(musicProgressCount);


        if (shouldOpen)
		{
			foreach ( var code in morseCodes )
			{
				code.enabled = false;
			}

			float time = show.Show( lookAtPoint );
			isOpen = true;
			Invoke( "DoAnimation", time );
		}
	}

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Music.release();
    }

    private void DoAnimation()
	{
		FMODUnity.RuntimeManager.PlayOneShotAttached(inputSound, gameObject);
		shake.StartShake( animationTime );
		animator.SetBool( "Open", true );

		Invoke( "EndAnimation", animationTime );
	}

	private void EndAnimation( )
	{
		show.Return( );
	}
}
