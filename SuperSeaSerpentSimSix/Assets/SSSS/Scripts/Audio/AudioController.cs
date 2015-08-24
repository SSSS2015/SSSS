using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
	private static AudioController _instance;
	public static AudioController Instance {
		get{
			return _instance;
		}
	}

	//Snapshots
	public AudioMixerSnapshot TitleSnapshot;
	public AudioMixerSnapshot InGameSnapshot;
	public AudioMixerSnapshot GameOverSnapshot;

	//SoundPlayers
	public AudioSource sfxPlayer;
	public AudioSource roarPlayer;
	public AudioSource wavesPlayer;

	//Clips
	public AudioClip sfx_pickup_fish;
	public AudioClip sfx_pickup_crate;
	public AudioClip sfx_hurt;
	public AudioClip sfx_menu_confirm;
	public AudioClip sfx_splash;

	public AudioClip[] roars;

	public void Awake()
	{
		if(_instance != null)
		{
			Destroy(this);
			return;
		}
		
		_instance = this;
	}

	public AudioController(){
		sfxPlayer = new AudioSource ();
	}

	//such repetition, wow.
	public void ToTitleSnapshot(float s = 0){
		TitleSnapshot.TransitionTo (s);
	}

	public void ToInGameSnapshot(float s = 0){
		InGameSnapshot.TransitionTo (s);
	}

	public void ToGameOverSnapshot(float s = 0){
		GameOverSnapshot.TransitionTo (s);
	}
	
	public void PlaySfx(AudioClip clip){
		if (clip != null) {
			sfxPlayer.clip = clip;
			sfxPlayer.Play();
		}
	}

	public void PlayPickupFishSfx(){
		PlayRoarSfx ();
		PlaySfx (sfx_pickup_fish);
	}

	public void PlayPickupCrateSfx(){
		PlayRoarSfx ();
		PlaySfx (sfx_pickup_crate);
	}

	public void PlayHurtSfx(){
		PlaySfx (sfx_hurt);
	}

	public void PlayRoarSfx(){
		AudioClip clip = (roars [Random.Range (0, roars.Length)]);
		roarPlayer.clip = clip;
		roarPlayer.Play ();
	}

	public void PlayMenuConfirmSfx(){
		PlaySfx (sfx_menu_confirm);
	}

	public void PlaySplashSfx(){
		if (sfx_splash != null) {
			wavesPlayer.PlayOneShot(sfx_splash);
		}
	}
}
