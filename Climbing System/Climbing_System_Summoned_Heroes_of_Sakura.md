# Climbing System for Summoned Heroes of Sakura

Using the XR Interaction Toolkit 1.00 pre 6, the PlayerClimbingManager and ClimbableCollider scripts work together to create climbing functionality in Unity 2020.3.18f. These scripts communicate using the PlayerClimbingManager climbingHand static field, identifying the hand currently engaged in the climbing action. Climbing was crucial to the Summoned Heroes of Sakura VR project. The game's main objective is to climb the Collsuss Lump and strike him with the Battle Axe three times to win. I created the climbing scripts before we started prototyping, ensuring that we could make climbing work before we began development, as the game's core idea would not work without it.

## Climbable Collider Script Overview:

The Climbable Collider script is attached to the in-game object with a collider. After attaching the script, you must populate the collider and interaction manager in the inspector.



When a user interacts with a climbable object, the ClimbableCollider script detects direct interactions on the climbable object through the OnSelectEntered method. This method will confirm that the player's controller object (interactor) has an XRDirectInteractor script attached. Once confirmed, the method retrieves the ActionBasedController from the player's controller (interactor) and assigns it to the PlayerClimbingManager.climbingHand. The ActionBasedController lets the PlayerClimbingManager know which hand is trying to climb.

An XRDirectInteractor will only function if the player presses the grab button on the controller, so the code explained above would get called whenever a user presses or releases the grip button. When the player releases the grip button currently climbing, the climbingHand variable will be set to null. Setting the climbingHand varible to null tells the PlayerClimbingManager that the climbing action has ended with that hand.


## Climbing Manager Script Overview:

The Climbing Manager script is attached to the XR-rig/player, and nothing needs to be populated in the inspector.



The PlayerClimbingManager script receives and processes this information during physics updates in the FixedUpdate method. If the climbingHand field is not null, this indicates active climbing, and the script will calculate the hand's velocity and position and make the climbing action accordingly.

Essentially, the PlayerClimbingManager script utilizes the information provided by the ClimbableCollider script to control the execution of the climbing mechanics. On the other hand, the ClimbableCollider script translates user interactions into data that the PlayerClimbingManager can utilize through the climbingHand field. 
##

