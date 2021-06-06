//==============================================================================	
//Weapon Handler Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;

public class WeaponHandler : MonoBehaviour {
	//==========================================================================	
	Transform currWpn;
	Transform newWpn;
	string defWpn="Autocannon";

	//==========================================================================
	void Start(){
		SetWeapon(defWpn);
	}

	void Update () {
		if (currWpn == null)
						SetWeapon (defWpn);


	}

	//----------------------------------------------------=----------------------
	void SetWeapon(string weapon){
		if (currWpn) {
			//	currWpn.SendMessage ("Deactivate");
			currWpn.SendMessage ("Activate");
		}

		newWpn = transform.Find (weapon);
		if (newWpn) {
			newWpn.SendMessage ("Activate");
			currWpn = newWpn;
		}

	}
	
	//--------------------------------------------------------------------------
}