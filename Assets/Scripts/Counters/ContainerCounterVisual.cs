using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose"; //Defining a const because working with strings is risky
    
    [SerializeField] private ContainerCounter containerCounter; //Providing the reference to the counter for the event
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); //Getting the Animator component
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject; //Using the event
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE); //Setting the trigger for the animation
    }
}
