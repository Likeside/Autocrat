using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;


namespace DefaultNamespace {
    public class Card: MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler {


        [SerializeField] TextMeshProUGUI _text;
        
        Random rand = new();
        Transform _parent;
        RectTransform _rt;
        CanvasGroup _canvasGroup;
        Canvas _canvas;
        CardSlot _slot;
        float _canvasScaleFactor;
        bool _inSlot;
        
        IEffect _effect;


        void Start() {
            _rt = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }


        public void ReturnCard() {
            transform.SetParent(_parent);
            if (_slot != null) _slot.EmptySlot();
        }

        public void ApplyEffect() {
            _effect.Apply();
        }
        

        public void Randomize(List<IEffect> _effects, Transform parent, Canvas canvas) {
            _parent = parent;
            _effect = _effects[rand.Next(_effects.Count)];
            _canvasScaleFactor = canvas.scaleFactor;
            _text.text = _effect.Description;
            _canvas = canvas;
        }

        

        public void OnBeginDrag(PointerEventData eventData) {
            Debug.Log("BeganDrag");
            transform.SetParent(_canvas.transform);
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData) {
            _canvasGroup.blocksRaycasts = true;
            if(!_inSlot) ReturnCard();
        }

        public void OnDrag(PointerEventData eventData) {
            _rt.anchoredPosition += eventData.delta / _canvasScaleFactor;
        }

        public void SetToSlot(CardSlot slot) {
            _inSlot = true;
            transform.SetParent(slot.transform);
            _slot = slot;
        }

        public void OnPointerDown(PointerEventData eventData) {
           // ReturnCard();
        }
    }
}