// This will control the logic for each character. 
// Driving the various pices that make up either the player, or the NPC

namespace Assets.Scripts.CharacterComponents
{
    class CharacterControl
    {
        public Character character;

        /// <summary>
        /// Initializes a new CharacterControl by providing a Character.
        /// </summary>
        /// <param name="_char">Character to be Controlled</param>
        public CharacterControl(Character _char)
        {
            character = _char;
        }
    }
}
