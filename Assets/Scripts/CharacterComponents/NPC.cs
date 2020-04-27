using Assets.Scripts.CharacterComponents.Parts.NPC;
using System.Collections.Generic;

namespace Assets.Scripts.CharacterComponents
{
    class NPC : Character
    {
        //
        private List<Dialogue> Chat;
        public bool isShop;

        // Constructor
        public NPC(int _ID, string _name) : base(_ID, _name)
        {

        }
    }
}
