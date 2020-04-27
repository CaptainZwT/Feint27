using Assets.Scripts.CharacterComponents.Parts;
using Assets.Scripts.CharacterComponents.Parts.Journal;
using Assets.Scripts.Information.Items;
using Assets.Scripts.Information.Spells;

namespace Assets.Scripts.CharacterComponents
{
    class Player : Character
    {
        // initializing variables
        private Journal _journal;
        private Equipment _equipment;
        private Pouch _pouch;

        // Character Selection choices
        public Item chosen_starting_item;
        public Spell chosen_starting_spell;

        // Constructor
        public Player(int _ID, string _name) : base(_ID, _name)
        {

        }
    }
}
