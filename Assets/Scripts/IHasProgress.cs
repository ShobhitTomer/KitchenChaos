using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged; //Defined an event handler for whenever the progress changes
    public class OnProgressChangedEventArgs : EventArgs
    //This is the information that the event will send as argument
    {
        public float progressNormalized;
    }
}
