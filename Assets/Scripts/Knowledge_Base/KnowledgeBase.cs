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
        public RegionClassification[] region_classes;


        public KnowledgeBase()
        {
            // Initializing variables

            // loading item information
            var jsonString = Resources.Load<TextAsset>(datapath + "Items/items").ToString();
            items = JsonHelper.FromJson<Item>(jsonString);
            // loading liquid information
            jsonString = Resources.Load<TextAsset>(datapath + "Items/liquids").ToString();
            liquids = JsonHelper.FromJson<Liquid>(jsonString);

            // loading region classification information
            jsonString = Resources.Load<TextAsset>(datapath + "regionclasses").ToString();
            region_classes = JsonHelper.FromJson<RegionClassification>(jsonString);

            // loading region information
            jsonString = Resources.Load<TextAsset>(datapath + "regions").ToString();
            regions = JsonHelper.FromJson<Region>(jsonString);
            // loading region specific Items
            foreach(Region r in regions)
            {
                r.standard_block = items.Single(item => item.id == r.standard_block_id);
                r.standard_foilage = items.Single(item => item.id == r.standard_foilage_id);
                r.standard_liquid = liquids.Single(item => item.id == r.standard_liquid_id);
                r.region_class = region_classes.Single(item => item.id == r.region_class_id);
            }
            // loading biome information
            jsonString = Resources.Load<TextAsset>(datapath + "biomes").ToString();
            biomes = JsonHelper.FromJson<Biome>(jsonString);
            // loading biome specific Items
            foreach (Biome b in biomes)
            {
                b.standard_block = items.Single(item => item.id == b.standard_block_id);
                b.standard_foilage = items.Single(item => item.id == b.standard_foilage_id);
                b.standard_liquid = liquids.Single(item => item.id == b.standard_liquid_id);
                b.region_class = region_classes.Single(item => item.id == b.region_class_id);
                b.region_class = region_classes.Single(item => item.id == b.region_class_id);
            }
        }
    }
}
