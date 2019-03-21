using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static Dictionary<string, AudioClip> audioClips;
    public static AudioSource audiosrc;

    static SoundManager current;            //Singleton

    void Awake()
    {
        if (current != null && current != this)
		{
			//...destroy this and exit. There can only be one Sound Manager
			Destroy(gameObject);
			return;
		}

		//Set this as the current sound manager
		current = this;

        //Persist this object between scene reloads
		DontDestroyOnLoad(gameObject);   
    }

    // Start is called before the first frame update
    void Start()
    {
        audioClips = new Dictionary<string, AudioClip>();    
        audioClips.Add("step", Resources.Load<AudioClip>("step"));
        audioClips.Add("jump", Resources.Load<AudioClip>("jump"));
        audioClips.Add("land", Resources.Load<AudioClip>("landing"));
        audioClips.Add("player_shot", Resources.Load<AudioClip>("player_shot"));
        audioClips.Add("shots", Resources.Load<AudioClip>("shots"));
        audioClips.Add("hit", Resources.Load<AudioClip>("Hit"));
        audioClips.Add("traps_hit", Resources.Load<AudioClip>("trapHit"));
        audioClips.Add("usb_collect", Resources.Load<AudioClip>("usb_collect"));

        audiosrc= GetComponent<AudioSource> ();
        audiosrc.volume = 0.5f; 
    }

    public static void SetVolume(float volume)
    {
        audiosrc.volume = volume;
    } 

    public static void PlaySound(string clip){
        if (audioClips.TryGetValue(clip, out AudioClip audioClip)) {
            audiosrc.PlayOneShot(audioClip);
        }
    }
}
