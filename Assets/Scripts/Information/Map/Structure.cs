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

        public Structure(Vector2[] space, int[] items, List<Vector2[]> arrays)
        {
            spacechecks = space;
            Itemids = items;
            pos_array = arrays;
        }
    }
}
