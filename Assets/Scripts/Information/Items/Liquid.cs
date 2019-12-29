using System;
using UnityEngine;

namespace Assets.Scripts.Information.Items
{
    [Serializable]
    class Liquid : Item
    {
        // Liquid is a type of Item

        /// <summary>
        /// Initialize a Liquid : It's characteristics
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Name</param>
        /// <param name="tilecolor">TileColor</param>
        /// <param name="integrity">Integrity</param>
        public Liquid(int _id, string _name, Color _tilecolor, float _integrity) : base(_id, _name, _tilecolor, _integrity)
        {

        }
    }
}
