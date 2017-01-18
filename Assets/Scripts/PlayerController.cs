using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D pRigidbody;
	private BoxCollider2D pCollider;
	//private SpriteRenderer pRenderer;

	private float moveForce = 3.5f;
	private float jumpForce = 7f;
	//private float maxSpeed = 3f;

	private float moveDirection;

	private float distToGround;
	private LayerMask wormHoleLayer;

	// Use this for initialization
	void Start () {	
		pRigidbody = gameObject.GetComponent<Rigidbody2D>();
		pCollider = gameObject.GetComponent<BoxCollider2D>();
		//pRenderer = gameObject.GetComponent<SpriteRenderer>();
				
		distToGround = pCollider.bounds.extents.y;
		wormHoleLayer = 1 << LayerMask.NameToLayer("WormHole");
	}

	void FixedUpdate () {		
		pRigidbody.velocity = new Vector2(moveDirection * moveForce, pRigidbody.velocity.y);

//		if(moveDirection == 0){
//			pRigidbody.velocity = new Vector2(0, pRigidbody.velocity.y);
//		}

//		if(Mathf.Abs(pRigidbody.velocity.x) > maxSpeed) {
//			pRigidbody.velocity = new Vector2(maxSpeed * Mathf.Sign(pRigidbody.velocity.x), pRigidbody.velocity.y);
//		}
	}

	void Update () {
		moveDirection = Input.GetAxis("Horizontal");
		//float moveVelocity = moveDirection * moveForce;
		//pRigidbody.velocity = new Vector2(moveVelocity, pRigidbody.velocity.y);

		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			if(playerIsTouchingGrounding()) {
				pRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
			}
		}
	}

	private bool playerIsTouchingGrounding() {
		return Physics2D.Raycast(transform.position, -transform.up, distToGround + 0.1f);
	}

	private void InvertGravity() {

		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, Mathf.Infinity , wormHoleLayer);

		//TODO: Verificar raycastnonalloc
		if(hit.collider != null) {			
			if(hit.collider.gameObject.tag == "WormHole"){
				transform.Rotate(Vector3.forward * 180);
				pRigidbody.gravityScale = pRigidbody.gravityScale * -1;
			}
		}
	}
}
