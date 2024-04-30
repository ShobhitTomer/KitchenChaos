using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut"; //Defining a const because working with strings is risky
    
    [SerializeField] private CuttingCounter cuttingCounter; //Providing the reference to the counter for the event
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); //Getting the Animator component
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
