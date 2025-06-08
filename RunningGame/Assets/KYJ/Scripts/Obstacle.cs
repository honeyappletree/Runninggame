using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Animation anim;

    public void PlayAnim()
    {
        if (anim != null)
        {
            anim.Play();
        }

        Destroy(gameObject, 3f);
    }
}
