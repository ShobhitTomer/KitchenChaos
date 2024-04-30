using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter; //giving reference to the clearcounter
    [SerializeField] private GameObject[] visualGameObjectArray; //giving reference to the selected counter visual - Now changing it to an array

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) //Defines what happens when the event is fired
    {
        if (e.selectedCounter == baseCounter) //if the selected counter that we have in front is equal to the clear counter that raycast gives us then it is true
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true); //Activates the visual
        }

    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false); //Hide the visual
        }

    }
}
