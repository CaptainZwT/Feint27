using Assets.Scripts.Information.Spells;
using System.Collections.Generic;

namespace Assets.Scripts.Character.Parts
{
    class Pouch
    {
        // initialzing variables
        private List<Spell> spells;

        /// <summary>
        /// Initializes spells with a empty list of Spells.
        /// </summary>
        public Pouch()
        {
            spells = new List<Spell>();
        }
    }
}
