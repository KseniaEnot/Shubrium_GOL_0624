using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corsina : MonoBehaviour
{
    AudioSource m_AudioSource;
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            m_AudioSource.Play();
            //отключенеи возможности вытащить из корзины
            //засчитать задание
        }

    }
}
