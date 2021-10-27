using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public event Action<string> OnAnimationEvent = delegate { };
    public void OnAnimationIvent(string eventName)
    {
        OnAnimationEvent(eventName);
    }
}
