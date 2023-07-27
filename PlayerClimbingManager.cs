using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerClimbingManager : MonoBehaviour
{
    private CharacterController characterController;
    private ContinuousMoveProviderBase continuousMovement;

    // A static field that represents the player's hand or game object with the XR controller component 
    public static ActionBasedController climbingHand;

    // Fields to store previous hand state and calculate climbing velocity
    private ActionBasedController previousHand;

    private Vector3 previousPos;
    private Vector3 currentVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        continuousMovement = GetComponent<ContinuousMoveProviderBase>();
    }

    // FixedUpdate is called once per physics update in Unity
    void FixedUpdate()
    {
        if (climbingHand) // Check if a climbing hand has been assigned by a ClimbInteractable instance
        {
            // If the hand is moving, calculate the velocity and disable continuous movement
            if (previousHand == null || climbingHand.name != previousHand.name)
            {
                previousHand = climbingHand;
                previousPos = climbingHand.positionAction.action.ReadValue<Vector3>();
            }
            continuousMovement.enabled = false; // Disables continuous movement when the user is climbing 
            Climb(); // Perform the climbing movement
        }
        else
        {
            // If the hand is not climbing, enable continuous movement
            continuousMovement.enabled = true;
        }
    }

    // The Climb method performs the climbing movement
    private void Climb()
    {
        // Calculate the current velocity of the hand by comparing the current and previous position of the hand
        currentVelocity = (climbingHand.positionAction.action.ReadValue<Vector3>() - previousPos) / Time.deltaTime;
        // Apply the opposite of the calculated velocity to the character controller, effectively creating the climbing motion
        characterController.Move(transform.rotation * -currentVelocity * Time.deltaTime);

        // Update the previous position to the current position for the next frame
        previousPos = climbingHand.positionAction.action.ReadValue<Vector3>();
    }
}
