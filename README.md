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

## December 18th, 2025

### Enemy Hierarchy Refactoring & Advanced AI Patrol

"Cladistic Code Architecture": 
NOTE: "Applying cladistic (biology) concepts to the organization of the class hierarchy facilitated the understanding of the scope of variables and the inheritance of behaviors between passive and reactive enemies."

#### Refactored the EnemyBase system into a hierarchical structure (Inheritance) to separate behaviors:
* EnemyBase (Ancestral/Passive): Basic touch damage.
* EnemyReactive: Adds health, freezing, and animation states.
* EnemyPatrol: Adds locomotion and edge detection.
* EnemyPatrolJumper: Specialized vertical movement (Slime Blue).
#### Intelligent Patrol System: 
Implemented edge detection using Raycast2D and LayerMasks to prevent enemies from falling off platforms and avoid self-collision issues.
#### Combat Polish: 
* Modified EnemyBase to filter damage using Tags, preventing enemies from hurting each other or ice spikes.
* Adjusted collision logic to allow frozen enemies to act as solid platforms without dealing damage to the player.
#### Bug Fixes: 
Resolved NullReferenceExceptions and context errors by promoting the _isFrozen variable to the base class, ensuring visibility across all derived species.
Key Learnings:
* OOP Principles: Practical application of Inheritance (virtual/override) and access modifiers (protected).
* Spatial Awareness: Using Raycasting for environmental sensing.
* Physics Interaction: Managing complex interactions between different body types and layers.

## December 19th, 2025

### AI Patrol Optimization, Advanced Freeze Mechanics, and UI Integration

#### Finalized Patrol System: 
Successfully synchronized the GroundCheck sensor with the enemy's initial movement direction. Resolved the "floating/flipping" bug by aligning the sensor's local position with the logic-driven _direction variable.
#### Enhanced Freeze & Platform Mechanics:
Implemented a visual pulsing feedback using DOTween that triggers during the final second of the 3-second freeze duration, notifying the player of the imminent thaw.
Fixed a rendering bug by targeting the specific Body GameObject within the rigging hierarchy, ensuring variant colors (Red/Blue Slimes) are correctly preserved and restored after freezing.
#### Enabled "Frozen Platforms": 
Confirmed that enemies act as solid terrain without dealing damage while frozen, allowing for strategic navigation.
#### Coins & UI Implementation:
Integrated the ItemManager (Singleton) with a new CoinUIController using TextMeshPro.
The UI now dynamically updates in real-time as the player collects coins, using string formatting (ToString("F0")) to handle the fractional coin increment logic.
#### Code Architecture: 
Unified the interaction between ProjectileBase and EnemyReactive using GetComponentInParent, ensuring the "Ice Shatter" mechanic (breaking ice with a second shot) works across all enemy types in the hierarchy.

Key Learnings:
* Coordinate Systems: Deep understanding of Local vs. Global space during sprite flipping and translation.
* State-Driven UI: Implementing the Observer-like pattern where the Manager notifies the UI of state changes.
* Rigging Manipulation: Learning how to isolate specific mesh parts (Body) for color manipulation without affecting the entire skeletal structure.

## December 20th, 2025

### AI Awareness, Advanced Death Mechanics & Restorative Systems

Implemented a dynamic chase AI, enabling enemies to switch between patrol and run states upon detecting the player while maintaining platform safety through edge detection. I refactored the death mechanics for both player and enemies using event-driven logic (Action OnKill), ensuring animations play fully before object destruction and resolving initialization bugs. Additionally, I expanded the collectibles system by creating the HeartContainer, integrating a restorative health logic into HealthBase that allows players to recover HP capped at their maximum starting health.(Module 16 submission - Creating a 2D platformer - Items and enemies/ NOTE: The activity began on December 9th and ended on December 20th.).

## December, 28th, 2025

### Scriptable Objects & UI Refactoring

Implemented Scriptable Objects to centralize data for collectible items (coins), decoupling game values from specific scenes. Developed specialized scripts to handle dynamic UI updates for both float and int data types. Additionally, refactored the Scene and Item Manager scripts to integrate with this new architecture, improving system modularity and maintenance.

## December 29th, 2025

### Implementation of Scriptable Objects for animation and code refactoring. 

Scriptable Objects were developed to manage the character's jump animations, utilizing five distinct float variables for precise control. A new script was created to centralize player variables, specifically focusing on movement speed and jump animation logic. A significant refactoring was performed on the player script to integrate these Scriptable Objects, which now allows for the handling of two different movement states. Additionally, the boss script received minor formatting updates for better code readability, and the scene underwent subtle environmental modifications. 

#### Creation of a variant player prefab and Update player, health and destroy helper scripts: 
Minor adjustments to the code to try and prevent the player from shooting themselves. With the implementation of the scriptable objects for the player variants, unfortunately the player can not longer activate the weapon sprite and without it, they cannot shoot. When manually activated in the Unity inspector, it does not recognize the character's direction and always shoots in the same direction. This did not occur before; I have to fix this bug.(NEEDS FIXING!)

## January 3rd, 2026

### Advanced HUD Integration, I-Frames & Systems Synchronization
NOTE: The bug from the December 29th has been fixed.

* #### Unified HUD Architecture:
Developed a centralized HUDController utilizing Scriptable Objects to monitor multiple game states. Implemented a dynamic Health Bar (UI Slider) and a Coin counter, ensuring complete decoupling between the gameplay logic and the User Interface.
* #### Inventory Visualization:
Created simple inventory slot within the HUD that triggers visual feedback (Weapon Icon) upon the collection of the Ice Wand, utilizing static state checks for real-time updates.
* #### Invincibility Frames (I-Frames) & Visual Feedback: 
Implemented a momentary invincibility system in HealthBase to enhance player experience. Developed a specialized "Blink" effect that manages sprite transparency (Alpha) while preserving original prefab colors and details through a color caching system.
* #### Hierarchical Bug Fixes:
Refactored the EnemyBase and EnemyReactive inheritance chain to resolve object destruction conflicts. Fixed a critical bug where reactive enemies (Satellites) would persist in the scene after death, ensuring proper memory cleanup and animation synchronization.
* #### System Refactoring:
Integration of ItemManager and HealthBase with the new Scriptable Objects. NOTE: In the item manager script, the `public void AddCoins` function needs to display the amount value as a float, because the `int` was including double the collected coins, even when colliding with only one coin. To solve the problem, I had to include an increment of 0.5, so that for each coin collected, the game adds 0.5 twice to form a value of 1 in the HUD.

(Module 17 submission - Creating a 2D platformer - Working with Scriptable Objects/ NOTE: The activity began on December 28th and ended on January 3th).

### Inclusion of new particle visual effects - VFX

Coin particles, heart container, weapon, and projectiles have been updated, with sprites created in Aseprite. A sparkle sprite and a snowflake sprite were added. Minor adjustments were made to the scene, such as changing overlapping layers and adjusting all prefabs containing the particle system.

<img width="96" height="96" alt="VFX_SnowFlake_Sprite" src="https://github.com/user-attachments/assets/a6fbb671-7348-4805-90b9-5d3aa1be428f" />

<img width="96" height="96" alt="VFX_Sparkle_Sprite" src="https://github.com/user-attachments/assets/16ae79f2-e855-4609-9d72-4c8766ec06d8" />

A dust sprite (dark and white) was created in Aseprite to provide the visual effect for the character's running. Minor modifications to the scenery were made solely to test the visual effects, bringing the items closer to the character's spawn point.

<img width="150" height="96" alt="image" src="https://github.com/user-attachments/assets/a2c19c8c-1a6b-4133-8f77-efcd2b9544cd" />

<img width="96" height="96" alt="VFX_Dust_Sprite" src="https://github.com/user-attachments/assets/611e155d-966b-499c-b627-a2f003b2b88b" />

<img width="96" height="96" alt="VFX_WhiteDust_Sprite" src="https://github.com/user-attachments/assets/7650218f-355e-46c5-9785-55b3032f9c7e" />

## January, 6th, 2026

### Particle System (Dust) Implementation 

Added dust visual effects (VFX) for running mechanics (activated with Z + Directional), double jump, and landing. The commit includes updated player and variant prefabs, as well as animation triggering logic. Note: Adjustments to the running dust layer sorting are pending.

## January, 7th, 2026

### VFX System Overhaul & Ice Jump Implementation:

Transitioned from a direct particle reference to a prefab-based instantiation system for all movement VFX (run, walk, and jump), resolving previous rendering issues and improving performance. This update introduces a new "Double Ice Jump" effect by integrating ice crystals into the dust material and includes fine-tuning of particle behaviors for sprinting and basic movement, alongside minor scene adjustments for better visual consistency.

