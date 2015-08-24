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
	public AudioMixerSnapshot PeaceSnapshot;
	public AudioMixerSnapshot BattleSnapshot;
	public AudioMixerSnapshot InGameSnapshot;
	public AudioMixerSnapshot GameOverSnapshot;

	public float BattleSnapshotLength = 16;
	public float bgmBPM = 124;

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

	private float returnToPeaceTime = 0;

	public void Awake()
	{
		if(_instance != null)
		{
			Destroy(this);
			return;
		}
		
		_instance = this;
	}

	public void Update(){
		if (returnToPeaceTime != 0 && Time.time >= returnToPeaceTime) {
			returnToPeaceTime = 0;
			PeaceSnapshot.TransitionTo(1);
		}
	}

	public AudioController(){
		sfxPlayer = new AudioSource ();
	}

	private float beatsToSeconds(float beats){
		return beats * 60/bgmBPM;
	}

	//such repetition, wow.
	public void ToTitleSnapshot(float s = 0){
		TitleSnapshot.TransitionTo (s);
	}

	public void ToInGameSnapshot(float s = 0){
		ToPeaceSnapshot ();
	}
	
	public void ToPeaceSnapshot(float beats = 0){
		PeaceSnapshot.TransitionTo (beatsToSeconds(beats));
	}

	public void ToBattleSnapshot(float beats = 0){
		BattleSnapshot.TransitionTo (beatsToSeconds(beats));
		returnToPeaceTime = Time.time + beatsToSeconds(BattleSnapshotLength);
	}

	public void ToGameOverSnapshot(float s = 0){
		returnToPeaceTime = 0;
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
