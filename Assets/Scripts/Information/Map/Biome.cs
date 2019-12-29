using System;
using Assets.Scripts.Information.Items;
using UnityEngine;

// Inheriting from the Region base class, a Biome is not a height based region but additionally a specific XxY area that will have it's own specifics.

namespace Assets.Scripts.Information.Map
{
    [Serializable]
    class Biome : Region
    {
        /*
         * Water Reservoir w/ stone
         * Bedrock w/ Large Caves
         * Basalt w/ Lava Vents
         * Basalt w/ Lava Lakes
         * Evergreen Grass w/ Trees, Flowers
         * Pirates' Cove w/ Lake (Ship in Center) : Sand
         * Knight's Castle w/ Courtyard (Castle in center) : Dirt
         * Lost Pyramid w/ Cactus (Pyramid in Center) : Sand
         * Ancient Aliens? w/ Meteor (space shuttle in center)
         * Lost Trex w/ Trees, Grass (Trex Fossil in Center)
         * Forge of the Fallen w/ Lava Forge in Center : Stone
         * Sword in the Stone w/ Trees, Grass (Sword stone in center) : Grass
         * Snow Village w/ Taiga (Village in center, lots of events) : Snow/dirt
        */

        // storage for information
        private float x_start,x_end;

        public int artifact_id;
        public Item artifact; // Certain Item

        /// <summary>
        /// Initializes a Biome providing its Charateristics
        /// </summary>
        /// <param name="_id">ID</param>
        /// <param name="_name">Name</param>
        /// <param name="_std_block_id">ID of Standard Item</param>
        /// <param name="_std_foilage_id">ID of Standard Grass Item</param>
        /// <param name="_std_liq_id">ID of Standard Liquid</param>
        /// <param name="_artifact_id">ID of Artifact Item</param>
        public Biome(int _id, string _name, int _std_block_id, int _std_foilage_id, int _std_liq_id, int _artifact_id) : base(_id, _name, _std_block_id, _std_foilage_id, _std_liq_id)
        {
            artifact_id = _artifact_id;
        }

        /// <summary>
        /// Initializes a Biome's location on a map
        /// </summary>
        /// <param name="start_pos">Start position to pull co-ordinates from.</param>
        /// <param name="end_pos">End position to pull co-ordinates from.</param>
        public void SetLocation(Vector3 start_pos, Vector3 end_pos)
        {
            x_start = start_pos.x;
            y_start = start_pos.y;
            x_end = end_pos.x;
            y_end = end_pos.y;
        }

    }
}
