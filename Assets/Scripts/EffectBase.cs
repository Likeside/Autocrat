using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DefaultNamespace {
    public class EffectBase: MonoBehaviour, IEffect {

        [SerializeField] string _text;
        [SerializeField] Number _number;
        [SerializeField] int _modificator;


        public string Description => _text;


        public virtual void Apply() {
            Debug.Log("Effect applied");
            _number.Modify(_modificator);
        }
    }
}