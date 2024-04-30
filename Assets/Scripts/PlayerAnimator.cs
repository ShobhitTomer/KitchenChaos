using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator; 

    private const string IS_WALKING = "IsWalking"; //Variable name we are using inside our Animator

    [SerializeField] private Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking()); //it changes the value of isWalking according to IsWalking bool in Player class
    }
}
