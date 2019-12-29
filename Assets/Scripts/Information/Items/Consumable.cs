using UnityEngine;

namespace Assets.Scripts.Information.Items
{
    class Consumable : Item
    {

        /// <summary>
        /// Initialize a Consumable : It's characteristics
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Name</param>
        /// <param name="tilecolor">TileColor</param>
        /// <param name="integrity">Integrity</param>
        public Consumable(int id, string name, Color tilecolor, float integrity) : base(id, name, tilecolor, integrity)
        {

        }
    }
}
