// This will hold all the information and logic for a given Region
// It will also serve as the base class for a Biome

using UnityEngine;
using Assets.Scripts.Information.Items;
using System;

namespace Assets.Scripts.Information.Map
{
    [Serializable]
    public class Region
    {
        // default values
        public int id;
        public int standard_block_id;
        public int standard_foilage_id;
        public int standard_liquid_id;
        public string name;


        // storage for information
        public float y_start, y_end;
        public Item standard_block;
        public Item standard_foilage;
        public Item standard_liquid;

        /// <summary>
        /// Initializes a Region providing its Charateristics
        /// </summary>
        /// <param name="_id">ID</param>
        /// <param name="_name">Name</param>
        /// <param name="_std_block_id">ID of Standard Item</param>
        /// <param name="_std_foilage_id">ID of Standard Grass Item</param>
        /// <param name="_std_liq_id">ID of Standard Liquid</param>
        public Region(int _id, string _name, int _std_block_id, int _std_foilage_id, int _std_liq_id)
        {
            id = _id;
            name = _name;
            standard_block_id = _std_block_id;
            standard_foilage_id = _std_foilage_id;
            standard_liquid_id = _std_liq_id;
        }
    }
}
