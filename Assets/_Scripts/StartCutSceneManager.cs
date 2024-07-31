using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class StartCutSceneManager : MonoBehaviour
{
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter1;
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter2;

    private void Start()
    {
        m_typewriterByCharacter1.onTextShowed.AddListener(ShowNextText);
    }

    private void ShowNextText()
    {
        StartCoroutine(InnerRoutine());
        
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(1f);
            m_typewriterByCharacter1.gameObject.SetActive(false);
            m_typewriterByCharacter2.gameObject.SetActive(true);
        }
    }
}
