//==============================================================================	
//PlayerShip Script
//
//==============================================================================	
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class PlayerShip : MonoBehaviour {
	//==========================================================================	
	public GameObject explosion;
	
	public int score = 0;
	public GameObject menuHolder;
	public int collisionDamage = 10;
	public float flickerTimeMax = 1.0f;
	public float flickerRate = 0.1f;
	public GameObject shipModel;

	private bool damaged = false;
	private float flickerTimer;
	private float flickerInterval;
	private float intervalTime = 0.5f; // Handle invulnerability delay time
	private float invulnerableExpire;	// Handle when invulnerability expires
	private Color baseColor;
	//private bool invulnerable;
	public AudioClip hitSound;
	public AudioClip destroySound;
	public GameObject pauseMenu;
	private bool invulnerable = false;	// handle time in between getting hit
	
	//--------------------------------------------------------------------------
	//Health Bar Attributes
	public int maxHealth = 50;
	private int health;
	private Rect box = new Rect(200, 200, 200, 20);
	private Texture2D frame;
	private Texture2D background;
	private Texture2D foreground;
	private bool paused=false;
	public float rotateSpeed=3.0f;
	

	//==========================================================================
	void Start(){
		//GetComponent<Renderer>().material.color = Color.blue;
		health = maxHealth;
		flickerTimer = 0.0f;
		flickerInterval = 0.0f;
		//baseColor = GetComponent<MeshRenderer>().material.color;
		//invulnerable = false;
		
		frame 	   = new Texture2D(1, 1, TextureFormat.RGB24, false);
		background = new Texture2D(1, 1, TextureFormat.RGB24, false);
		foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);
		frame.SetPixel(0, 0, Color.black);
		background.SetPixel(0, 0, Color.red);
		foreground.SetPixel(0, 0, Color.blue);
		frame.Apply();
		background.Apply();
		foreground.Apply();
		paused = false;
	}
	
	//--------------------------------------------------------------------------
	void Update() {

		// Handle game pausing
		if (Input.GetKeyDown ("p") && !paused) {
			pauseMenu.SetActive(true);
			PauseGame(true);
			paused = true;
		} else if (Input.GetKeyDown("p") && paused)
        {
			pauseMenu.SetActive(false);
			PauseGame(false);
			paused = false;
        }




		if (Input.GetKey ("r")) {	// Rotate while holding down the r key
			Vector3 rotationVector = transform.rotation.eulerAngles;
			rotationVector.z += rotateSpeed;	// rotate by x amount of degrees
			transform.rotation = Quaternion.Euler(rotationVector);
		}


		if( health <= 0 ){
			health = 0;
			Kill();
		} else if (health > maxHealth)
        {
			health = maxHealth;
        }

		if(damaged){

			DamageFlash();
		}
		
		


	}

	// Handle pausing/unpausing
    public void PauseGame(bool value)
    {
		if (value)
        {
			Time.timeScale = 0.0f;
        } else
        {
			Time.timeScale = 1.0f;
        }
    }

    //--------------------------------------------------------------------------
    void OnGUI() {
		GUI.contentColor = Color.red;
		GUI.Label(new Rect(10, Screen.height - 150, 150, 100), "Total Score: " + score);

		GUI.contentColor = Color.white;
		GUI.Label(new Rect(10, Screen.height - 125, 150, 100), "Speed ");
		GUI.DrawTexture(new Rect(8, Screen.height - 52, box.width + 4, box.height + 4), frame, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(10, Screen.height - 50, box.width, box.height), background, ScaleMode.StretchToFill);
		GUI.DrawTexture(new Rect(10, Screen.height - 50, box.width*health/maxHealth, box.height), foreground, ScaleMode.StretchToFill);
	}
	
	//--------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		if((other.CompareTag("Indestructible") || other.CompareTag("Destructible") || other.CompareTag("EnemyBullet")) && !invulnerable) {
			ApplyDamage(collisionDamage);
			damaged = true;
			invulnerable = true;
			
		}

		if (other.CompareTag("Item"))
						Destroy (other.gameObject);

	}
	
	//--------------------------------------------------------------------------
	//Reduces health by incoming damage value.
	void ApplyDamage(int damage) {
		health -= damage;
		GetComponent<AudioSource>().PlayOneShot (hitSound);
		if (health <= 0) {
			Kill ();
		}
	}

	//--------------------------------------------------------------------------
	public void Heal(int mod){
		Debug.Log("health added");
		health += mod;
	}
	
	//--------------------------------------------------------------------------
	//Handle death animations/effects and remove object when destroyed.
	void Kill() {
		GameObject destruction;
		GetComponent<AudioSource>().PlayOneShot (destroySound);
		destruction = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		Destroy(this.gameObject);
	}
	
	//--------------------------------------------------------------------------
	public void SetScore (int inScore) {
		score += inScore;
	}
	
	//--------------------------------------------------------------------------
	// Handle invulnerability in between hits
	void DamageFlash(){	
		
		
		//shipModel.GetComponent<Renderer>().material.color = Color.red;
		flickerTimer += Time.deltaTime;
		if(flickerTimer < flickerTimeMax){
			flickerInterval += Time.deltaTime;
			if(flickerInterval >= flickerRate){
				flickerInterval = 0.0f;
			}
		}
		else{
			damaged = false;
			flickerTimer = 0.0f;
			//shipModel.GetComponent<Renderer>().material.color = baseColor;
			invulnerable = false;	// ship is no longer invulnerable
		}
		
	}
	//--------------------------------------------------------------------------
}