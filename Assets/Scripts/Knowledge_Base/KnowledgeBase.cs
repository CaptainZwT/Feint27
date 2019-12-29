using Assets.Scripts.Helpers;
using Assets.Scripts.Information.Items;
using Assets.Scripts.Information.Map;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Knowledge_Base
{
    /// <summary>
    /// Class that contains information about a specific GameWorld, all it's biomes, regions, and items
    /// </summary>
    class KnowledgeBase
    {
        private string datapath = "Data/";

        public Item[] items;
        public Region[] regions;
        public Biome[] biomes;
        public Liquid[] liquids;


        public KnowledgeBase()
        {
            // Initializing variables

            // loading item information
            var jsonString = Resources.Load<TextAsset>(datapath + "Items/items").ToString();
            items = JsonHelper.FromJson<Item>(jsonString);
            // loading liquid information
            jsonString = Resources.Load<TextAsset>(datapath + "Items/liquids").ToString();
            liquids = JsonHelper.FromJson<Liquid>(jsonString);
            // loading region information
            jsonString = Resources.Load<TextAsset>(datapath + "regions").ToString();
            regions = JsonHelper.FromJson<Region>(jsonString);
            // loading region specific Items
            foreach(Region r in regions)
            {
                r.standard_block = items[r.standard_block_id];
                r.standard_foilage = items[r.standard_foilage_id];
                r.standard_liquid = liquids[r.standard_liquid_id];
            }
            // loading biome information
            jsonString = Resources.Load<TextAsset>(datapath + "biomes").ToString();
            biomes = JsonHelper.FromJson<Biome>(jsonString);
            // loading region specific Items
            foreach (Biome b in biomes)
            {
                b.standard_block = items[b.standard_block_id];
                b.standard_foilage = items[b.standard_foilage_id];
                b.standard_liquid = liquids[b.standard_liquid_id];
                b.artifact = items[b.artifact_id];
            }
        }
    }
}
