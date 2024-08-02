using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Febucci.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter7;
    [SerializeField] private TypewriterByCharacter m_typewriterByCharacter8;
    
    [SerializeField] private CinemachineBrain _brain;
    [SerializeField] private CinemachineFreeLook _cam1;
    [SerializeField] private CinemachineFreeLook _cam2;
    [SerializeField] private CinemachineFreeLook _cam3;

    [SerializeField] private Transform _king;
    [SerializeField] private Material[] kingMaat;
    
    [SerializeField] private Transform _kingMovePos;
    [SerializeField] private GameObject[] _stones;
    
    [SerializeField] private Transform _son1;
    [SerializeField] private Transform _son1MovePos;
    
    [SerializeField] private Transform _son2;
    [SerializeField] private Transform _son2MovePos;
    
    
    
    
    
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
            _king.DOMove(_kingMovePos.position, 4f).OnComplete(() => {_king.gameObject.SetActive(false);});
            m_typewriterByCharacter4.gameObject.SetActive(false);
            m_typewriterByCharacter5.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            _son1.DOLocalRotate(new Vector3(0, 275, 0), 2f);
            _son2.DOLocalRotate(new Vector3(0, 90, 0), 2f);  //275   -   90
            yield return new WaitForSeconds(1f);
            m_typewriterByCharacter5.gameObject.SetActive(false);
            m_typewriterByCharacter6.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            _brain.m_DefaultBlend.m_Time = 3f;
            _cam3.Priority = 0;
            yield return new WaitForSeconds(1.5f);
            m_typewriterByCharacter6.gameObject.SetActive(false);
            m_typewriterByCharacter7.gameObject.SetActive(true);
            _son1.DOMove(_son1MovePos.position, 2f);
            _son2.DOMove(_son2MovePos.position, 2f);
            yield return new WaitForSeconds(2.5f);
            foreach (var stone in _stones)
            {
                yield return new WaitForSeconds(0.35f);
                stone.SetActive(true);
            }
            yield return new WaitForSeconds(2.5f);
            _blackBg.DOFade(1, 2f).OnComplete(() =>
            {
                m_typewriterByCharacter7.gameObject.SetActive(false);
                m_typewriterByCharacter8.gameObject.SetActive(true);
            });

            yield return new WaitForSeconds(4f);
            m_typewriterByCharacter8.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(2);
        }
    }
}
