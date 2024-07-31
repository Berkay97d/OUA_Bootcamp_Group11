using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Febucci.UI;
using UnityEngine;
using UnityEngine.UI;

public class StartCutSceneManager : MonoBehaviour
{
    [SerializeField] private Image _blackBg;
    
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter1;
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter2;
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter3;
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter4;
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter5;
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter6;
    
    [SerializeField] private CinemachineBrain _brain;
    [SerializeField] private CinemachineFreeLook _cam1;
    [SerializeField] private CinemachineFreeLook _cam2;

    [SerializeField] private Transform _king;
    [SerializeField] private Transform _kingMovePos;
    
    
    
    
    
    private void Start()
    {
        m_typewriterByCharacter1.onTextShowed.AddListener(ShowAfterOne);
        m_typewriterByCharacter2.onTextShowed.AddListener(ShowAfterTwo);
        m_typewriterByCharacter4.onTextShowed.AddListener(ShowAfterFour);
        
        StartCutScene();
    }

    private void StartCutScene()
    {
        StartCoroutine(InnerRoutine());
        
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(3f);
            m_typewriterByCharacter1.gameObject.SetActive(true);
        }
    }

    private void ShowAfterOne()
    {
        StartCoroutine(InnerRoutine());
        
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(3f);
            m_typewriterByCharacter1.gameObject.SetActive(false);
            m_typewriterByCharacter2.gameObject.SetActive(true);
        }
    }
    
    private void ShowAfterTwo()
    {
        StartCoroutine(InnerRoutine());
        
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            _brain.m_DefaultBlend.m_Time = _brain.m_CustomBlends.m_CustomBlends[0].m_Blend.m_Time;
            _cam1.Priority = 0;
            
            _blackBg.DOFade(0, 3f);
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForSeconds(_brain.m_CustomBlends.m_CustomBlends[0].m_Blend.m_Time - 2.5f);
            
            m_typewriterByCharacter2.gameObject.SetActive(false);
            m_typewriterByCharacter3.gameObject.SetActive(true);
            

            yield return new WaitForSeconds(4f);
            
            _brain.m_DefaultBlend.m_Time = 3f;
            _cam2.Priority = 0;
            
            m_typewriterByCharacter3.gameObject.SetActive(false);
            m_typewriterByCharacter4.gameObject.SetActive(true);
        }
    }
    
    private void ShowAfterFour()
    {
        StartCoroutine(InnerRoutine());
        
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(5f);
            _king.DOMove(_kingMovePos.position, 4f);
            m_typewriterByCharacter4.gameObject.SetActive(false);
            m_typewriterByCharacter5.gameObject.SetActive(true);
            yield return new WaitForSeconds(8f);
            m_typewriterByCharacter5.gameObject.SetActive(false);
            m_typewriterByCharacter6.gameObject.SetActive(true);
        }
    }
}
