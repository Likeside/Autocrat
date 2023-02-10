using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EventDropper : MonoBehaviour {
    [SerializeField] GameObject _eventPrefab;
    [SerializeField] Transform _eventsParent;

    [SerializeField] GameObject _cardPrefab;
    [SerializeField] Transform _cardsParent;

    [SerializeField] Canvas _canvas;
    
    [SerializeField] int _maxEvents;
    [SerializeField] int _maxCards;

    [SerializeField] List<EffectBase> _cardEffectsObjs;
    [SerializeField] List<EffectBase> _eventEffectsObjs;

    List<GameEvent> _events = new();
    List<Card> _cards = new();

    List<IEffect> _cardEffects;
    List<IEffect> _eventEffects;

    void Start() {
        _cardEffects = new List<IEffect>();
        _eventEffects = new List<IEffect>();

        foreach (var effect in _cardEffectsObjs) {
            _cardEffects.Add(effect);
        }

        foreach (var effect in _eventEffectsObjs) {
            _eventEffects.Add(effect);
        }
    }
    void LaunchEvents() {
        if(_events.Count != 0) return;
        for (int i = 0; i < _maxEvents; i++) {
            var ev = Instantiate(_eventPrefab, _eventsParent);
            _events.Add(ev.GetComponent<GameEvent>());
            ev.GetComponent<GameEvent>().Randomize(_eventEffects);
        }
        LaunchCards();
    }
    void LaunchCards() {
        for (int i = 0; i < _maxCards; i++) {
            var card = Instantiate(_cardPrefab, _cardsParent);
            _cards.Add(card.GetComponent<Card>());
            card.GetComponent<Card>().Randomize(_cardEffects, _cardsParent, _canvas);
        }
    }

    
     void CloseEvents() {
        foreach (var ev in _events) {
            ev.Conclude(out bool success);
            if (!success) {
                Debug.Log("Not successful");
                return;
            }
            Destroy(ev.gameObject);
        }

        foreach (var card in _cards) {
            Destroy(card.gameObject);
        }
        _events.Clear();
        _cards.Clear();
        
    }

    public void Turn() {
        CloseEvents();
        LaunchEvents();
    }
}
