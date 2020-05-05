using Assets.Scripts.Information.Spells;
using System.Collections.Generic;

namespace Assets.Scripts.CharacterComponents.Parts
{
    class Pouch
    {
        // initialzing variables
        public List<Spell> spells;

        /// <summary>
        /// Initializes spells with a empty list of Spells.
        /// </summary>
        public Pouch()
        {
            spells = new List<Spell>();
        }
    }
}
