// This manages the animations of the Scene Fader UI element. since the  scene fader
// is a part of the level it will need to register itself with the game manager

using UnityEngine;

public class SceneFader : MonoBehaviour
{
	Animator anim;		//Reference to the Animator component
	int fadeParamID;    //The ID of the animator parameter that fades the image

	void Start()
	{
		//Get reference to Animator component
		anim = GetComponent<Animator>();

		//Get the integer hash of the "Fade" parameter.
		fadeParamID = Animator.StringToHash("Fade");

		//Register this Scene Fader with the Game Manager
		GameManager.RegisterSceneFader(this);
	}

	public void FadeSceneOut()
	{
		//Play the animation that fades the UI
		anim.SetTrigger(fadeParamID);
	}
}
