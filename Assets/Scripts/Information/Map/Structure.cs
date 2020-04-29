using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Information.Map
{
    class Structure
    {
        // A dictionary containing location and item id
        public Vector2[] spacechecks;
        public int[] Itemids;
        public List<Vector2[]> pos_array;

        // Checking the ground below said structure
        public bool ground_check = false;
        public Vector2[] groundspots;
        public int biome_id;


        public Structure(Vector2[] space, int[] items, List<Vector2[]> arrays, bool ground = false)
        {
            spacechecks = space;
            Itemids = items;
            pos_array = arrays;
            ground_check = ground;
        }
    }
}
