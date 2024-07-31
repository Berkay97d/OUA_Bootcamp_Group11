using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Grid_System
{
    /// <summary>
    ///   <para>
    /// Visual action of grid object will make inside this class
    /// /// </para>
    /// </summary>
    public class TileVisual : MonoBehaviour
    {
        [SerializeField] private float _selectedTilePulseSpeed;
        
        private Tile m_myTile;
        private GridObject m_myGridObject;
        private Tween m_selectedTween;
        private Vector3 m_defaultScale;

        
        private void Start()
        {
            m_myTile = GetComponent<Tile>();
            m_myGridObject = m_myTile.GetGridObject();
            m_defaultScale = new Vector3(0.95f, m_myTile.GetThickness(), 0.95f);
            
            m_myGridObject.OnSelectedStatusChanged += OnSelectedStatusChanged;

            HighlightActions.OnHighlightTiles += HighlightTiles;
            HighlightActions.OnClearTiles += ClearHighlightTiles;
        }

        private void HighlightTiles(List<GridObject> gridObjects, Color highlightColor)
        {
            if (gridObjects.Contains(m_myGridObject))
            {
                GetComponent<MeshRenderer>().material.color = highlightColor;
            }
        }

        private void ClearHighlightTiles()
        {
            GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
        }

        private void OnDestroy()
        {
            m_myGridObject.OnSelectedStatusChanged -= OnSelectedStatusChanged;
        }

        private void OnSelectedStatusChanged(bool isSelected)
        {
            if (!isSelected)
            {
                m_selectedTween.Kill();
                transform.localScale = m_defaultScale;
                return;
            }
            
            PlaySelectedAnimation();
        }

        private void PlaySelectedAnimation()
        {
            ScaleUp();
        }

        private void ScaleUp()
        {
            m_selectedTween = transform.DOScale(new Vector3(m_defaultScale.x, m_defaultScale.y + 0.25f, m_defaultScale.z), _selectedTilePulseSpeed).OnComplete((ScaleDown));
        }

        private void ScaleDown()
        {
            m_selectedTween = transform.DOScale(m_defaultScale, _selectedTilePulseSpeed).OnComplete(ScaleUp);
        }
        
    }
}