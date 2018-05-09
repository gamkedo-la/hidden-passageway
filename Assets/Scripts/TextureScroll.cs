using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour {
	private float scrollSpeed = 0.1f;
	private float oscEffect = 0.05f;
	private float oscRate = 0.6f;
	private float offAxisPerc = 0.7f;

	private float slowItAllDown = 0.3f;

	private float scrollAmt = 0.0f;

	private Renderer rend;

	void Start() {
		rend = GetComponent<Renderer>();
	}

	void Update() {
		scrollAmt += slowItAllDown * Time.deltaTime * (scrollSpeed + Mathf.Cos(Time.time * oscRate) * oscEffect);
		rend.material.SetTextureOffset("_MainTex", new Vector2(scrollAmt, scrollAmt*offAxisPerc));
		rend.material.SetTextureOffset("_BumpMap", new Vector2(scrollAmt, scrollAmt*offAxisPerc));
	}
}
