using UnityEngine;
using System.Collections;

public class GravityControl : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {

		other.SendMessage("InvertGravity");
	}
}
