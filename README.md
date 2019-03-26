# feup-djco
🎮 Game for FEUP Game Devlopment Course 💻

- **Preliminary Project - FeupEvac**
    * [Description](https://github.com/j-seixas/feup-djco#feupevac)
    * [Controls](https://github.com/j-seixas/feup-djco#controls)
    * [Enemies and Traps](https://github.com/j-seixas/feup-djco#enemies-and-traps)
    * [Power Ups](https://github.com/j-seixas/feup-djco#power-ups)
    * [HUD and UI](https://github.com/j-seixas/feup-djco#hud-and-ui)
    * [Additional Features](https://github.com/j-seixas/feup-djco#additional-features)
    * [Installation](https://github.com/j-seixas/feup-djco#installation)
    * [Group](https://github.com/j-seixas/feup-djco#group)
    * [References](https://github.com/j-seixas/feup-djco#references)

***
# FeupEvac
FeupEvac is a 2D platformer game where you play as a student studying at FEUP’s library. He’s startled by an explosion and suddenly has to face enemies, falling objects and holes on the floor. His goal is to exit the library towards safety. Once he does, he realizes he has fallen asleep while studying at the library. 

- [Video Trailer](https://youtu.be/UN99osXLc1o)

## Controls
- **[A]** and **[D]** move the player to left and right, respectively.
- **[W]** or **[Space]** makes the player jump.
- **[Left Mouse Button]** shoots pen drives in the same direction as your mouse pointer.
- When off the ground, **[Right Mouse Button]** hooks your mouse to a surface in the same direction as your mouse pointer and uses its cord as a rope. 
- When using the mouse hook, you can swing back and forward using **[A]** and **[D]** or rappel up and down using **[W]** and **[S]**.


## Enemies and Traps
- **Terrorists:** When near the player, these enemies will shoot him using a machine gun. They have limited bullets per magazine and a set reload time. Their accuracy is not perfect either, so the player can dodge some of the bullets. The bullets that manage to hit the player will inflict a damage of 1 health point.
- **Falling traps:** Some objects may fall from the ceiling, killing the player instantly.
- **Floor holes:** Some parts of the floor have been compromised by the initial explosion and will fall when the player steps on it.


## Power Ups
- **Health power-up:** The player regenerates 1 health point.
- **Pen power-up:** The player gains 3, 5. 8 or 10 pen drives, depending on the background color of the power up (white, blue, purple and yellow, respectively).


## HUD and UI
- **Main menu:** You can chose to play the game, adjust the volume in the options menu, check the help menu or exit the game.
- **Pause menu:** By clicking the pause button you reach the pause menu. From there, you can choose to resume the game, go back to the main menu or exit the game.
- **Health bar:** The redness of this bar displays the remaining health of the player.
- **Pen counter:** This number represents the player’s pen ammo.


## Additional features
- **Hidden path:** In level 3, you can find a hidden entrance which will lead the player to freedom.
- **Moving platforms:** Some of the long ceiling lamps didn’t fall but were left swinging from side to side. In some situations the players must jump to their top to reach the next platform.
- If the Player, while hooked with the mouse, collides with a platform or ground, the hook detaches.


## Installation
- Unzip the `DJCO-P1-G3-FeupEvac-game.zip` file into whatever directory you want. 
- Open unzipped folder.
- Execute `FeupEvac.exe`


## Group
This project was developed by group #3:
- Daniel Marques - up201503822
- João Seixas - up201505648
- Pedro Miranda - up201506574 

The work was distributed evenly and each individual effort was the same. Because of this, each member had the opportunity to learn something about game development, whether regarding visuals, animations, sounds or scripting. 


## References
* Software:
    - Game engine: [Unity](https://unity.com/)
	- Image editing: [GIMP](https://www.gimp.org/) and [Photoshop](https://www.adobe.com/products/photoshop.html)

* Sprites:
    - Player sprites: https://jesse-m.itch.io/jungle-pack
	- Enemy sprites: https://www.deviantart.com/motorroach/bang
	- Platform sprites: https://oddpotatogift.itch.io/16x16-fantasy-pack


* Sounds: https://freesound.org/
