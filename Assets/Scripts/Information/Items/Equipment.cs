using UnityEngine;

namespace Assets.Scripts.Information.Items
{
    class Equipment : Item
    {

        /// <summary>
        /// Initialize a Equipment : It's characteristics
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Name</param>
        /// <param name="tilecolor">TileColor</param>
        /// <param name="integrity">Integrity</param>
        public Equipment(int id, string name, Color tilecolor, float integrity) : base(id, name, tilecolor, integrity)
        {

        }
    }
}
