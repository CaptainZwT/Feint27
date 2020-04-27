// This drives a NPC's shop using the Dialogue as a base class.


using System.Collections.Generic;

namespace Assets.Scripts.CharacterComponents.Parts.NPC
{
    class Shop : Dialogue
    {

        /// <summary>
        /// Initializes Shop with a Dictionary 
        /// where an action is represented by an int, and the phrase is reprsented by the string.
        /// </summary>
        /// <param name="_speech">ActionPhrase Dictionary</param>
        public Shop(Dictionary<int, string> _speech) : base(_speech)
        {

        }
    }
}
