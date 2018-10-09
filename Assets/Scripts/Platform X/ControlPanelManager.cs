using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class ControlPanelManager : MonoBehaviour {

	[SerializeField] ControlPanelsPuzzleManager controlPanelsPuzzleManager;
	[SerializeField] Canvas codeCanvas;
	[SerializeField] Image panelImage;
	[SerializeField] Text codeText;
	[SerializeField] Color panelWinColor;
	[SerializeField] string codeSolution;

	// private Text
	private string currentCode = "";
	private int codeSolutionLength = 0;
	private Coroutine resetCodeCoroutine = null;

	void Start () {
		codeSolutionLength = codeSolution.Length;

		codeCanvas.enabled = false;
		codeText.text = "";
		// @todo: prev state: SetWon();
	}

	void SetWon() {
		panelImage.color = panelWinColor;
		codeText.text = currentCode = codeSolution;
		codeCanvas.enabled = true;

		Disable();
	}

	public void ClickButton(int btnValue) {
		currentCode += btnValue;
		codeText.text = currentCode;

		if (HasWon()) {
			// call puzzle manager
			SetWon();

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
