using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Rigidbody2D playerRididbody;
	public BoxCollider2D playerCollider;
	public SpriteRenderer playerRenderer;

	private float moveForce = 10;
	private float jumpForce = 10;

	private float distToGround;
	LayerMask wormHoleLayer;

	// Use this for initialization
	void Start () {	
		distToGround = playerCollider.bounds.extents.y;
		wormHoleLayer = 1 << LayerMask.NameToLayer("WormHole");
	}

	void FixedUpdate () {
		
		float moveDirection = Input.GetAxis("Horizontal") * moveForce;
		playerRididbody.velocity = new Vector2(moveDirection, playerRididbody.velocity.y);
		//playerRididbody.AddForce(Vector2.right * moveDirection);
	}

	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			if(isTouchingGrounding()) {
				playerRididbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
			}
		}
	}

	private bool isTouchingGrounding() {

		return Physics2D.Raycast(transform.position, -transform.up, distToGround + 0.1f);
	}

	void InvertGravity() {

		//TODO: Verificar raycastnonalloc
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity , wormHoleLayer);

		if(hit.collider != null) {
			if(hit.collider.gameObject.tag == "WormHole"){
				transform.Rotate(Vector3.forward * 180);
				playerRididbody.gravityScale = playerRididbody.gravityScale * -1;
			}
		}
	}
}
