using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	private SpriteRenderer groundRenderer;

	private Color fadedColor = new Vector4(1,1,1,.5f);
	private Color originalColor = Color.white;

	private bool isFaded;

	// Use this for initialization
	void Start () {		

		groundRenderer = GetComponent<SpriteRenderer>();
		isFaded = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Z)) {
			if(!isFaded){
				StartCoroutine(fadeGround());
			}
		}
	}

	IEnumerator fadeGround() {

		isFaded = !isFaded;
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"));
		groundRenderer.color = fadedColor;

		yield return new WaitForSeconds(.6f);

		isFaded = !isFaded;
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), false);
		groundRenderer.color = originalColor;
	}
}