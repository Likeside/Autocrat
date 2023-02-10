using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace {
    public class CardSlot: MonoBehaviour, IDropHandler {

        RectTransform _rectTransform;
        public event Action OnCardReturned;
        public event Action<Card> OnCardDropped; 

        bool _filled;

        void Start() {
            _rectTransform = GetComponent<RectTransform>();
        }


        public void OnDrop(PointerEventData eventData) {
            Debug.Log("Dropped");
            if (eventData.pointerDrag != null && !_filled) {
                Debug.Log("setting pos");
                eventData.pointerDrag.GetComponent<Card>().SetToSlot(this);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition;
                _filled = true;
                OnCardDropped?.Invoke(eventData.pointerDrag.GetComponent<Card>());
            }
        }

        public void EmptySlot() {
            _filled = false;
            OnCardReturned?.Invoke();
        }
    }
}