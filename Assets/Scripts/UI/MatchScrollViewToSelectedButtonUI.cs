using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class MatchScrollViewToSelectedButtonUI : MonoBehaviour
    {
        [SerializeField] private GameObject _currentSelected;
        [SerializeField] private GameObject _previousSelected;
        [SerializeField] private RectTransform _currentSelectedTransform;
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private ScrollDirection _scrollDirection;
        [SerializeField] private List<GameObject> _ignoredToSelectionObjects = new();

        private void Update()
        {
            _currentSelected = EventSystem.current.currentSelectedGameObject;
            if (_currentSelected != null && _currentSelected != _previousSelected && !_ignoredToSelectionObjects.Contains(_currentSelected))
            {
                _previousSelected = _currentSelected;
                _currentSelectedTransform = _currentSelected.GetComponent<RectTransform>();
                SnapTo();
            }
        }

        private void SnapTo()
        {
            Canvas.ForceUpdateCanvases();
            Vector2 newPosition = _scrollRect.transform.InverseTransformPoint(_contentPanel.position) - _scrollRect.transform.InverseTransformPoint(_currentSelectedTransform.position);
            if (_scrollDirection == ScrollDirection.Horizontal)
            {
                newPosition.y = 0f;
            }
            else if (_scrollDirection == ScrollDirection.Vertical)
            {
                newPosition.x = 0f;
            }
            _contentPanel.anchoredPosition = newPosition;
        }
    }
}