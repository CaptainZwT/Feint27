using System.Collections.Generic;
using UnityEngine;

// This will handle the logic to drive Dialogue if the Char is an NPC
// It will also form the base class for when a NPC has a Shop

namespace Assets.Scripts.Character.Parts.NPC
{
    class Dialogue
    {
        private Dictionary<int, string> speech;

        /// <summary>
        /// Initializes Dialogue with a Dictionary 
        /// where an action is represented by an int, and the phrase is reprsented by the string.
        /// </summary>
        /// <param name="_speech">ActionPhrase Dictionary</param>
        public Dialogue(Dictionary<int, string> _speech)
        {
            speech = _speech;
        }
    }
}
