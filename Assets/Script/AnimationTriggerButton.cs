using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerButton : MonoBehaviour
{
    public Animator targetAnimator;

    public void PlayAnimation1()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger("Animasi1");
        }
        else
        {
            Debug.LogWarning("Animator tidak diset di AnimationTriggerButton!");
        }
    }

    public void PlayAnimation2()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger("Animasi2");
        }
        else
        {
            Debug.LogWarning("Animator tidak diset di AnimationTriggerButton!");
        }
    }

    public void PlayAnimation3()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger("Animasi3");
        }
        else
        {
            Debug.LogWarning("Animator tidak bisa berjalan!");
        }
    }
}
