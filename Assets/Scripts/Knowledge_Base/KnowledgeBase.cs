using Assets.Scripts.Helpers;
using Assets.Scripts.Information.Items;
using Assets.Scripts.Information.Map;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Knowledge_Base
{
    /// <summary>
    /// Class that contains information about a specific GameWorld, all it's biomes, regions, and items
    /// </summary>
    class KnowledgeBase
    {
        private string datapath = "Data/";

        // Item types
        public Item[] items;
        public Liquid[] liquids;
        // Region information
        public Region[] regions;
        public Biome[] biomes;
        // Structures
        public StructureLoader strucLoader;


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

            // loading region specific references
            foreach(Region r in regions)
            {
                r.standard_block = items.Single(item => item.id == r.standard_block_id);
                r.standard_foilage = items.Single(item => item.id == r.standard_foilage_id);
                r.standard_liquid = liquids.Single(item => item.id == r.standard_liquid_id);
            }

            // loading biome information
            jsonString = Resources.Load<TextAsset>(datapath + "biomes").ToString();
            biomes = JsonHelper.FromJson<Biome>(jsonString);

            // loading biome specific references and fixing occurance
            foreach (Biome b in biomes)
            {
                b.standard_block = items.Single(item => item.id == b.standard_block_id);
                b.standard_foilage = items.Single(item => item.id == b.standard_foilage_id);
                b.standard_liquid = liquids.Single(item => item.id == b.standard_liquid_id);
                // occurance corrected
                b.top_occurance = ((float)b.top_occurance / 100f);
                b.bottom_occurance = ((float)b.bottom_occurance / 100f);
            }

            // loading structures
            strucLoader = new StructureLoader();
        }
    }
}
