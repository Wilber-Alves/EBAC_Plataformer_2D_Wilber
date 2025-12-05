# EBAC_Plataformer_2D_Wilber

2D platform game project as an exercise for modules 13 to 19 of the Unity developer course at EBAC.

November 4th, 2025 - Adding a basic menu to the game - The DOTween plugins were installed, and programming of the basic scripts for scenes and the menu began, in addition to some animations for the menu scene buttons. Creation of the game manager and its designation as a Singleton. A Singleton script was also created and added to the EDGEE folder for reuse in other development stages. Inclusion of the main character's spawn logic in the game manager script. (Module 13 submission - Creating a 2D platformer: Basic structure).

November 8th, 2025 - A prototype of the player's movement was created - Press the left and right arrow keys to move the character.

November 9th, 2025 - Implementation of the player's physics and jump. Adjustments to friction values ​​and jump force.

November 10th, 2025 - Modifications were made to the player's script, including the logic for running when the Z key is pressed. The camera also follows the player. The animation for the character's jump, when the Space key is pressed, has been included, decreasing the Y-axis. Pending task: include an animation that changes the Y-axis when the character touches the ground after the jump.

November 15th, 2025 - Jump animation update. Pending task completed, the character now has animations for both jumping and landing. Hitting platforms from below also activates a slight bouncing animation. Health system creation. Enemy creation for health system testing. If the player touches the enemy (passive type enemy with thorns), it will automatically die after a short delay. (Module 14 submission - Creating a 2D platformer: Movement).

November 18th, 2025 - New sprites imported from EBAC package. Start the animation classes.

December 5th, 2025 - Updates have been made to the level design to test the character's jumping, running, and landing animations. The jumping and movement mechanics have been updated. Now the character can run, turn right and left by changing the sprite, and it's possible to jump while looking in the correct direction. Additionally, it's possible to run by pressing Z and change the animation to running. Pressing A and S allows you to change the animation of the enemies (satellites); their animations were created in the animator with skinning previously done in Unity from 2D sprites. The infinite jump problem has been fixed, and now the player can jump, double jump, and if near a wall, can jump infinitely, as if in climbing mode (only works on walls). Implemented animations: Run / Idle / JumpUp / JumpDown / JumpLanding. Animation not implemented: Run1 / Death. (Module 15 submission - Creating a 2D platformer: Animation 2D).

