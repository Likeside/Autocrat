using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;


namespace DefaultNamespace {
    public class GameEvent: MonoBehaviour {


        [SerializeField] CardSlot _cardSlot;
        [SerializeField] TextMeshProUGUI _text;
        Random rand = new();


        Card _currentCard;
        IEffect _effect;

        void Start() {
            _cardSlot.OnCardReturned += RemoveCard;
            _cardSlot.OnCardDropped += SetCurrentCard;
        }

        public void SetCurrentCard(Card card) {
            Debug.Log("Card set");
            if(_currentCard != null) _currentCard.ReturnCard();
            RemoveCard();
            _currentCard = card;
        }

        public void RemoveCard() {
            _currentCard = null;
        }

        public void Conclude(out bool success) {
            success = _currentCard != null;
            if (success) {
                _currentCard.ApplyEffect();
                _effect.Apply();
            }
        }
        

        public void Randomize(List<IEffect> _effects) {
            _effect = _effects[rand.Next(_effects.Count)];
            _text.text = _effect.Description;
        }

        void OnDestroy() {
            _cardSlot.OnCardReturned -= RemoveCard;
            _cardSlot.OnCardDropped -= SetCurrentCard;
        }
    }
}