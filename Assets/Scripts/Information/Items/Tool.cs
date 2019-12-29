using UnityEngine;

namespace Assets.Scripts.Information.Items
{
    class Tool : Item
    {
        // Tool is a type of Item

        /// <summary>
        /// Initialize a Tool : It's characteristics
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Name</param>
        /// <param name="tilecolor">TileColor</param>
        /// <param name="integrity">Integrity</param>
        public Tool(int id, string name, Color tilecolor, float integrity) : base(id, name, tilecolor, integrity)
        {

        }
    }
}
