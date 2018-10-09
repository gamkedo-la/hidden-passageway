using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class ControlPanelManager : MonoBehaviour {

	[SerializeField] AbstractActivateable toEnable;
	[SerializeField] Canvas codeCanvas;
	[SerializeField] Image panelImage;
	[SerializeField] Text codeText;
	[SerializeField] Color panelWinColor;
	[SerializeField] string codeSolution;

	// private Text
	private string currentCode = "";
	private int codeSolutionLength = 0;
	private Coroutine resetCodeCoroutine = null;
	private string mySaveName;

	void Start () {
		codeSolutionLength = codeSolution.Length;

		codeCanvas.enabled = false;
		codeText.text = "";

        mySaveName = PlayerPrefsHelper.GetPrefsName(gameObject);
        int previousState = PlayerPrefs.GetInt(mySaveName, 0);

        if (previousState == 1) {
			SetWon();
		}
	}

	void SetWon() {
		panelImage.color = panelWinColor;
		codeText.text = currentCode = codeSolution;
		codeCanvas.enabled = true;

		PlayerPrefs.SetInt(mySaveName, 1);

		Disable();
	}

	public void ClickButton(int btnValue) {
		currentCode += btnValue;
		codeText.text = currentCode;

		if (HasWon()) {
			SetWon();
			toEnable.SendMessage("Activate");

			return;
		}

		if (currentCode.Length < codeSolutionLength) {
			return;
		}

		if (resetCodeCoroutine != null) {
			StopCoroutine(resetCodeCoroutine);
			resetCodeCoroutine = null;
		}
		resetCodeCoroutine = StartCoroutine(resetCode());
	}

	IEnumerator resetCode() {
		enabled = false;
		codeText.text += "\nWrong code";

		yield return new WaitForSeconds(1.25f);

		currentCode = "";
		codeText.text = "";
		enabled = true;
	}

    void OnTriggerEnter(Collider col)
    {
		if (!enabled) {
			return;
		}

        if (col.gameObject.tag == Tags.Player)
        {
			codeCanvas.enabled = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
		if (!enabled) {
			return;
		}

        if (col.gameObject.tag == Tags.Player)
        {
			codeCanvas.enabled = false;
		}
	}

	public bool HasWon ()
	{
		// Debug.Log("has won?" + PlayerPrefsHelper.GetPrefsName(gameObject) + " = " + (currentState?"1":"0") + "=="+(winState?"1":"0"));
		return currentCode == codeSolution;
	}

	public void Disable ()
	{
		enabled = false;
	}
}
