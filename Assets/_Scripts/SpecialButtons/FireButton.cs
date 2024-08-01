using System;
using System.Collections;
using System.Collections.Generic;
using TurnSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FireButton : MonoBehaviour
{
    [SerializeField] private Button _fireButton;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Color _disableColor;
    
    public static event Action<bool> OnFireButtonClick;

    private bool m_isFireSelected;

    private void Start()
    {
        _buttonImage.color = _disableColor;
        m_isFireSelected = false;
        
        _fireButton.onClick.AddListener(OnFireButtonClic);
    }

    private void OnFireButtonClic()
    {
        Debug.Log("BUTTON TIKLANDIII");

        if (TurnController.SharedInstance.GetCurrentTeamTurn() == Team.White)
        {
            Debug.Log("BEYAZ TAŞ ATEŞ EDEMEZ");
            return;
        }
        
        if (!m_isFireSelected)
        {
            m_isFireSelected = true;
            OnFireButtonClick?.Invoke(m_isFireSelected);
            
            _buttonImage.color = Color.white;
            
            return;
        }

        _buttonImage.color = _disableColor;
        
        m_isFireSelected = false;
        OnFireButtonClick?.Invoke(m_isFireSelected);
    }
}
