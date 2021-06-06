//==============================================================================	
//Ship Movement Script
//
//==============================================================================
using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour{
	//==========================================================================
	public float movementSpeed = 5.0f;
	public bool inverted = true;
	
	//==========================================================================
	void Start(){
		GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	//--------------------------------------------------------------------------
	void Update (){
		float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
		float vertical = Input.GetAxis("Vertical") * movementSpeed;
		
		if(inverted){
			vertical *= -1;
		}
		
		GetComponent<Rigidbody>().velocity = new Vector3(horizontal, vertical, 0);
	}
	//--------------------------------------------------------------------------
}