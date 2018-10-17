using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EscMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	bool isOpen;
    bool isMouseOver = false;
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
        ShowOrHideBabies(false);
	}

    void ShowOrHideBabies(bool showThem)
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(showThem);
        }
    }

	public void SetToOpen(){
        ShowOrHideBabies(true);
		startMin = closeMin;
		startMax = closeMax;
		endMin = openMin;
		endMax = openMax;
		currAnimTime = 0f;
		isAnimating = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = (Cursor.lockState == CursorLockMode.None);
	}

	public void SetToClose(){
		endMin = closeMin;
		endMax = closeMax;
		startMin = openMin;
		startMax = openMax;
		currAnimTime = 0f;
		isAnimating = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (Cursor.lockState == CursorLockMode.None);
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("The cursor entered the selectable UI element.");
        isMouseOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("The cursor left the selectable UI element.");
        isMouseOver = false;
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

    public void ResetCart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void BackToHub(){
		if(!isAnimating){
            SceneWarp.fromScene = SceneManager.GetActiveScene().name;
			SceneManager.LoadScene("MainHub");
		}
	}

	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Tab)){
			ToggleMenu();
		}
        /*
		//Provide a way to unlock cursor when in esc menu
		if (Input.GetKeyUp(KeyCode.T)) {
			Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ?
				CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = (Cursor.lockState == CursorLockMode.None);
		}*/

        //Debug.Log(EventSystem.current.IsPointerOverGameObject());
        //bool noUIcontrolsInUse = EventSystem.current.currentSelectedGameObject == null;
        //Debug.Log(noUIcontrolsInUse);

		if(isAnimating){
			currAnimTime += Time.deltaTime;
			if(currAnimTime < animationTimeMax){
				Vector2 currMin = Vector2.Lerp(startMin, endMin, currAnimTime / animationTimeMax);
				Vector2 currMax = Vector2.Lerp(startMax, endMax, currAnimTime / animationTimeMax);
				Set(currMin, currMax);
			}else{
				Set(endMin, endMax);
				isAnimating = false;
                if (isOpen) // was open
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = (Cursor.lockState == CursorLockMode.None);
                    ShowOrHideBabies(false);
                }
				isOpen = !isOpen;
			}
        } else if (Input.GetButtonDown("Fire1") && EventSystem.current.IsPointerOverGameObject()==false && isOpen)
        {
            SetToClose();
        }
	}

	void Set(Vector2 anchorMin, Vector2 anchorMax){
		target.anchorMin = anchorMin;
		target.anchorMax = anchorMax;
		target.anchoredPosition = Vector2.zero;
		target.sizeDelta = Vector2.zero;
	}
}
