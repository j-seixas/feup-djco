using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip stepSound,jumpSound,landSound,playershot,shots,hitSound,trapHit;
    static AudioSource audiosrc;

    // Start is called before the first frame update
    void Start()
    {
        stepSound = Resources.Load<AudioClip>("step");
        jumpSound = Resources.Load<AudioClip>("jump");
        landSound = Resources.Load<AudioClip>("landing"); //not done
        playershot = Resources.Load<AudioClip>("player_shot");
        shots = Resources.Load<AudioClip>("shots");
        hitSound = Resources.Load<AudioClip>("Hit");
        trapHit = Resources.Load<AudioClip>("trapHit");

        audiosrc= GetComponent<AudioSource> ();
        audiosrc.volume=0.5f;
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip){
        switch(clip){
            case "step":
                audiosrc.PlayOneShot(stepSound);
                break;
            case "jump":
                audiosrc.PlayOneShot(jumpSound);
                break;
            case "land":
                audiosrc.PlayOneShot(landSound);
                break;
            case "player_shot":
                audiosrc.PlayOneShot(playershot);
                break;
            case "shots":
                audiosrc.PlayOneShot(shots);
                break;
            case "hit":
                audiosrc.PlayOneShot(hitSound);
                break;
            case "traps_hit":
                audiosrc.PlayOneShot(trapHit);
                break;
        }
    }
}
