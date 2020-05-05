// This will serve as the base class containing information about a character

using Assets.Scripts.CharacterComponents.Parts;
using UnityEngine;

namespace Assets.Scripts.CharacterComponents
{
    class Character : MonoBehaviour
    {
        // charateristics

        // initialziing components to drive character
        public Inventory inventory;
        public Pouch pouch;
        public CharacterControl char_control;

        // initializing stats
        public float maxHealth, health, maxMana, mana, credits;
        public float engineSpeed, engineAcceleration, engineFlightAccel;

        public float armor, cooldownReduction;
        public float spellPower, alignment;


        public void Awake()
        {
            char_control = GetComponent<CharacterControl>();
        }

        public void Start()
        {
            // base stats
            maxHealth = 100;
            health = maxHealth;
            maxMana = 50;
            mana = maxMana;
            credits = 0;
            armor = 0;
            alignment = 0;

            // base spell stats
            cooldownReduction = 0;
            spellPower = 0;

            // base movement stats
            engineAcceleration = 2f;
            engineFlightAccel = 2f;
            engineSpeed = 0f;
        }
    }
}
