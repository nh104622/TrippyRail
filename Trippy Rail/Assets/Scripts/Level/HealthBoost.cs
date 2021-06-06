//==============================================================================	
//Health Booster Item Script
//
//==============================================================================
using UnityEngine;
using System.Collections;

public class HealthBoost : MonoBehaviour {
	//==========================================================================
	public int healAmt = 10;
	public AudioClip powUp;
	private AudioSource audioSource;

    private void Start()
    {
		audioSource = GetComponent<AudioSource>();
    }

    //==========================================================================
    void OnTriggerEnter(Collider other){
		other.SendMessage("Heal", healAmt);
		audioSource.PlayOneShot(powUp);
		Destroy(gameObject);
	}
	//--------------------------------------------------------------------------
}
