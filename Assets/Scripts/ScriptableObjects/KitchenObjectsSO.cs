using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectsSO : ScriptableObject //We are directly using public modifiers here because we will not be writing anything in our scriptable objects
{
    public Transform prefab; 
    public Sprite sprite;
    public string objectName;

}
