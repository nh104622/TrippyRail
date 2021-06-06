//==============================================================================	
//Power Up Script
//
//==============================================================================
using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	//==========================================================================
	public string weapon;
	public AudioClip powUp;

	//==========================================================================
	void OnTriggerEnter(Collider other){
		Transform wpnHandler = other.transform.Find("WeaponHandler");

		if (wpnHandler != null) {
			wpnHandler.SendMessage ("SetWeapon", weapon);
			GetComponent<AudioSource>().PlayOneShot (powUp);
			Destroy (this.gameObject);
		}

	}
	//--------------------------------------------------------------------------
}
