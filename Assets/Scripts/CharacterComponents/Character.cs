// This will serve as the base class containing information about a character

using Assets.Scripts.CharacterComponents.Parts;
using UnityEngine;

namespace Assets.Scripts.CharacterComponents
{
    class Character : MonoBehaviour
    {
        // charateristics
        public int ID;

        // initialziing variables
        private Inventory _inventory;

        // initializing stats
        public float maxHealth, health, maxMana, mana, credits;
        private float armor, cooldownReduction;
        private float spellPower, alignment;

        /// <summary>
        /// Initializes a new Character : It's charateristics and components
        /// </summary>
        /// <param name="_ID">ID</param>
        /// <param name="_name">Name of Character</param>
        public Character(int _ID, string _name)
        {
            ID = _ID;
            name = _name;

            // base stats
            maxHealth = 100;
            health = maxHealth;
            maxMana = 50;
            mana = maxMana;
            credits = 0;
            armor = 0;
            cooldownReduction = 0;
            spellPower = 0;
            alignment = 0;
        }
    }
}
