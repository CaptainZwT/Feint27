using System;
using UnityEngine;

// This handles the logic for a base Item

namespace Assets.Scripts.Information.Items
{
    [Serializable]
    public class Item
    {
        // Charateristics of an Item
        public int id;
        public string name;
        public Color tilecolor;
        public float integrity; // in kg m/sec^2, aka Newton
        //

        // Initialize an Item : It's characteristics
        public Item(int _id, string _name, Color _tilecolor, float _integrity)
        {
            id = _id;
            name = _name;
            tilecolor = _tilecolor;
            integrity = _integrity;
        }
    }
}
