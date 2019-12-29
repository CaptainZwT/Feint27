// This will serve as the base class containing information about a character

using Assets.Scripts.Character.Parts;
using Assets.Scripts.Character.Parts.Journal;

namespace Assets.Scripts.Character
{
    class Character
    {
        // charateristics
        public int ID;
        private bool isplayer;
        public string name;

        // initialziing variables
        private Journal _journal;
        private Inventory _inventory;
        private Pouch _pouch;

        /// <summary>
        /// Initializes a new Character : It's charateristics and components
        /// </summary>
        /// <param name="_ID">ID</param>
        /// <param name="_isplayer">Character is Player or NPC</param>
        /// <param name="_name">Name of Character</param>
        public Character(int _ID, bool _isplayer, string _name)
        {
            ID = _ID;
            isplayer = _isplayer;
            name = _name;
        }
    }
}
