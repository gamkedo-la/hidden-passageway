using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour {
	bool isOpen;
	bool isAnimating;
	[SerializeField]
	RectTransform target;
	float animationTimeMax = 0.5f;
	float currAnimTime = 0f;
	Vector2 openMin;
	Vector2 openMax;
	Vector2 closeMin;
	Vector2 closeMax;

	Vector2 startMin, startMax, endMin,endMax;
	// Use this for initialization
	void Awake (){
		openMax = new Vector2(0.74f, 0.84f);
		openMin = new Vector2(0.26f, 0.17f);
		closeMax = new Vector2(0.953f, 0.871f);
		closeMin = new Vector2(0.953f, 0.871f);
		Set(closeMin, closeMax);
		isOpen = false;
	}

	public void SetToOpen(){
		startMin = closeMin;
		startMax = closeMax;
		endMin = openMin;
		endMax = openMax;
		currAnimTime = 0f;
		isAnimating = true;
		
	}

	public void SetToClose(){
		endMin = closeMin;
		endMax = closeMax;
		startMin = openMin;
		startMax = openMax;
		currAnimTime = 0f;
		isAnimating = true;		
	}

	public void ToggleMenu(){
		if(!isAnimating){
			if(isOpen){
				SetToClose();
			}else{
				SetToOpen();
			}
		}
	}

	void Start () {
		
	}
	
	public void QuitApplication(){
		if(!isAnimating){
			Application.Quit();
		}
	}

	public void BackToHub(){
		if(!isAnimating){
			SceneManager.LoadScene("MainHub");
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			ToggleMenu();
		}
		//Provide a way to unlock cursor when in esc menu
		if (Input.GetKeyUp(KeyCode.T)) {
			Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ?
				CursorLockMode.None : CursorLockMode.Locked;			
		}

		if(isAnimating){
			currAnimTime += Time.deltaTime;
			if(currAnimTime < animationTimeMax){
				Vector2 currMin = Vector2.Lerp(startMin, endMin, currAnimTime / animationTimeMax);
				Vector2 currMax = Vector2.Lerp(startMax, endMax, currAnimTime / animationTimeMax);
				Set(currMin, currMax);
			}else{
				Set(endMin, endMax);
				isAnimating = false;
				isOpen = !isOpen;
			}
		}
	}

	void Set(Vector2 anchorMin, Vector2 anchorMax){
		target.anchorMin = anchorMin;
		target.anchorMax = anchorMax;
		target.anchoredPosition = Vector2.zero;
		target.sizeDelta = Vector2.zero;
	}
}
