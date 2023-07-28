using UnityEngine.XR.Interaction.Toolkit;

public class ClimbableCollider : XRBaseInteractable
{
    // Method is triggered when a XRBaseInteractor enters the collision zone of the climbable collider
    // This is usually when the user grabs or holds onto the climbable collider
    [System.Obsolete] 
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor); 

        // If the interactor is an XRDirectInteractor (indicating direct physical interaction in VR)
        if (interactor is XRDirectInteractor)
        {
            // Retrieve the ActionBasedController component from the interactor and set it as the current climbing hand in the PlayerClimbingManager
            PlayerClimbingManager.climbingHand = interactor.GetComponent<ActionBasedController>();
        }
    }

    // Method is triggered when a XRBaseInteractor exits the collision zone of the climbable collider
    // This is usually when the user releases or lets go of the climbable collider
    [System.Obsolete] 
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor); 

        // If the interactor is an XRDirectInteractor (indicating direct physical interaction in VR)
        if (interactor is XRDirectInteractor)
        {
            // If the current climbing hand in the PlayerClimbingManager exists and its name matches the name of the interactor
            if (PlayerClimbingManager.climbingHand && PlayerClimbingManager.climbingHand.name == interactor.name)
            {
                // Set the current climbing hand in the PlayerClimbingManager to null
                PlayerClimbingManager.climbingHand = null;
            }
        }
    }
}