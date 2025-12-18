# EBAC_Plataformer_2D_Wilber

  2D platform game project as an exercise for modules 13 to 19 of the Unity developer course at EBAC.

## November 4th, 2025
Adding a basic menu to the game - The DOTween plugins were installed, and programming of the basic scripts for scenes and the menu began, in addition to some animations for the menu scene buttons. Creation of the game manager and its designation as a Singleton. A Singleton script was also created and added to the EDGEE folder for reuse in other development stages. Inclusion of the main character's spawn logic in the game manager script. (Module 13 submission - Creating a 2D platformer: Basic structure).

## November 8th, 2025
A prototype of the player's movement was created - Press the left and right arrow keys to move the character.

## November 9th, 2025
Implementation of the player's physics and jump. Adjustments to friction values ​​and jump force.

## November 10th, 2025
Modifications were made to the player's script, including the logic for running when the Z key is pressed. The camera also follows the player. The animation for the character's jump, when the Space key is pressed, has been included, decreasing the Y-axis. Pending task: include an animation that changes the Y-axis when the character touches the ground after the jump.

## November 15th, 2025
Jump animation update. Pending task completed, the character now has animations for both jumping and landing. Hitting platforms from below also activates a slight bouncing animation. Health system creation. Enemy creation for health system testing. If the player touches the enemy (passive type enemy with thorns), it will automatically die after a short delay. (Module 14 submission - Creating a 2D platformer: Movement).

## November 18th, 2025
New sprites imported from EBAC package. Start the animation classes.

## December 5th, 2025
Updates have been made to the level design to test the character's jumping, running, and landing animations. The jumping and movement mechanics have been updated. Now the character can run, turn right and left by changing the sprite, and it's possible to jump while looking in the correct direction. Additionally, it's possible to run by pressing Z and change the animation to running. Pressing A and S allows you to change the animation of the enemies (satellites); their animations were created in the animator with skinning previously done in Unity from 2D sprites. The infinite jump problem has been fixed, and now the player can jump, double jump, and if near a wall, can jump infinitely, as if in climbing mode (only works on walls). Implemented animations: Run / Idle / JumpUp / JumpDown / JumpLanding. Animation not implemented: Run1 / Death. (Module 15 submission - Creating a 2D platformer: Animation 2D).

## December 9th, 2025 
Updates collectible coins to the project, including coin sprites, a 3D coin prefab, and a dedicated folder for collectible items. Key features include an animation script for items with rotation and vertical movement, a base script for collision detection and item destruction, and a script for collecting coins that interacts with an ItemManager to track the collected amount. The ItemManager script monitors coin collection, though a known issue currently causes it to count two coins per collection instead of one (NEEDS FIXING). Collectible coins have also been added to the scene. Arts also added to the scene.

## December 12th, 2025
The prototype level design was created using three new sprites made in Aseprite (Pixel art): an ice crystal, a cave, and a stone block. Animations were included for enemies and collectible items. NOTE: the same coin animation script was included for enemies, but the collection function was disabled. Access to the singleton was included in the item manager script. Furthermore, the coin counting problem was solved; it was enough to change the coin quantity variable (int to float) in the "Add Coins" function from an integer equal to 1 to a floating-point number equal to 0.5. Because if I can't understand why it's adding double, I'll force the logic to add half of that, to get 1, until I figure out the reason for the error. But it's solved. (PROBLEM SOLVED!).

## December 14th, 2025
The coin sprite was enhanced with refined 3D edge lines and new particle effects. Player and enemy animations were updated for increased fluidity. Enemy prefabs were remade to alter colors and sizes, categorizing them as passive (spikes, satellites) and reactive (slimes). And the level design are remade with new environments, items, and enemy placements. Flash damage feedback implemented. 

## December 17th, 2025
Implementation of the weapon collection and projectile firing system. A container-based prefab structure was introduced for the projectiles to ensure that the animations (managed by the Animator) flip correctly based on the player's direction via localScale. A new 'Weapon' folder was created to house the three essential scripts: InventoryBase (handles collection and equipping logic), ProjectileBase (defines projectile movement), and WeaponBase (manages shooting mechanics and cooldowns). A particle system was also added to the collectible item for better visual feedback during pickup.

### Implementation of the Ice Wand & Combat System Improvements (EXTRA).

#### Ice Wand Mechanics: Implemented a projectile system with a 15% probability-based freeze effect using Random.Range.
#### Physics & Prefabs: Reorganized projectiles into a Container-based structure to fix Animator flip issues. Added Rigidbody2D (Kinematic) and Triggers to ensure correct collision detection.
#### Freeze Logic: Created a freezing state that paralyzes enemy movement (RigidbodyType2D) and animations (animator.speed) for a set duration. 
#### Feedback & Colors: Integrated FlashColor (DOTween) with the freeze state to ensure sprites return to their correct colors. NOTE: Some color transition issues persist when hitting frozen enemies, occasionally showing the original prefab colors instead of variants. (NEEDS FIXING!)
#### Bug Fixes: Resolved NullReferenceException by updating prefab references in the WeaponBase and improving component communication.
