using TMPro;
using UnityEngine;

namespace DefaultNamespace {
    public class Number: MonoBehaviour {
        [SerializeField] TextMeshProUGUI _textMeshProUGUI;

        int _number;    

        
        public void Modify(int modificator) {
            _number += modificator;
            _textMeshProUGUI.text = _number.ToString();
        }
    }
}