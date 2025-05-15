using System;
using System.Collections.Generic;
using Map;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerLetterReader : MonoBehaviour
    {
        [Header("Letter Options")] [SerializeField]
        private TMP_Text text;
        
        private bool _isReadingLetters;
        public bool IsReadingLetters => _isReadingLetters;
        private List<Letter> letters;
        private int currentLetter;

        public event EventHandler OnLettersToggle;
        public event EventHandler OnReadingLetterChanged;
        
        private void Start()
        {
            letters = GetComponent<PlayerInventory>().Letters;
        }

        public void ToggleLetters()
        {
            if(letters.Count == 0) return;
            _isReadingLetters = !_isReadingLetters;
            OnLettersToggle?.Invoke(this, EventArgs.Empty);
        }

        public void NextLetter()
        {
            if(!_isReadingLetters) return;
            if(currentLetter+1 >= letters.Count) return;
            
            currentLetter++;
            OnReadingLetterChanged?.Invoke(this, EventArgs.Empty);
        }

        public void PreviousLetter()
        {
            if(!_isReadingLetters) return;
            if(currentLetter - 1 < 0) return;
            
            currentLetter--;
            OnReadingLetterChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}