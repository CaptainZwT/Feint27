using System;
using Assets.Scripts.Information.Items;
using UnityEngine;

// Inheriting from the Region base class, a Biome is not a height based region but additionally a specific XxY area that will have it's own specifics.

namespace Assets.Scripts.Information.Map
{
    [Serializable]
    class Biome : Region
    {
        // characteristics
        public float top_occurance;
        public float bottom_occurance;

        /*
         * Basalt w/ Lava Vents
         * Basalt w/ Lava Lakes
         * Evergreen Grass w/ Trees, Flowers
         * Pirates' Cove w/ Lake (Ship in Center) : Sand
         * Knight's Castle w/ Courtyard (Castle in center) : Dirt
         * Lost Pyramid w/ Cactus (Pyramid in Center) : Sand
         * Ancient Aliens? w/ Meteor (space shuttle in center)
         * Lost Trex w/ Trees, Grass (Trex Fossil in Center)
         * Forge of the Fallen w/ Lava Forge in Center : Stone
        */

        public Biome(int _id, string _name, int _stdBlockId, int _stdFoilageId, int _stdLiqId, int _regionClassId, float _topOccurance, float _bottomOccurance) : base(_id, _name, _stdBlockId, _stdFoilageId, _stdLiqId, _regionClassId)
        {
            top_occurance = _topOccurance;
            bottom_occurance = _bottomOccurance;
        }

    }
}
