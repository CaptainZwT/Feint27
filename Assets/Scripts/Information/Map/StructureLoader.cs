using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Information.Map
{
    class StructureLoader
    {

        public Structure getTree(Vector2 startpos)
        {
            int[] itemids = 
            {
                6, 3
            };

            Vector2[] check =
            {
               new Vector2(startpos.x+4, startpos.y + 3),
               new Vector2(startpos.x-4, startpos.y + 3),

               new Vector2(startpos.x+4, startpos.y + 4),
               new Vector2(startpos.x-4, startpos.y + 4),

               new Vector2(startpos.x+3, startpos.y + 5),
               new Vector2(startpos.x-3, startpos.y + 5),

               new Vector2(startpos.x, startpos.y + 7),
               new Vector2(startpos.x, startpos.y + 8),
            };


            Vector2[] root =
            {
                new Vector2(startpos.x, startpos.y + 1),
                new Vector2(startpos.x, startpos.y + 2),
                new Vector2(startpos.x, startpos.y + 3),
                new Vector2(startpos.x, startpos.y + 4),
                new Vector2(startpos.x, startpos.y + 5),
                new Vector2(startpos.x, startpos.y + 6),
            };

            Vector2[] leaves =
            {
                new Vector2(startpos.x+1, startpos.y + 3),
                new Vector2(startpos.x+2, startpos.y + 3),
                new Vector2(startpos.x+3, startpos.y + 3),
                new Vector2(startpos.x-1, startpos.y + 3),
                new Vector2(startpos.x-2, startpos.y + 3),
                new Vector2(startpos.x-3, startpos.y + 3),

                new Vector2(startpos.x+1, startpos.y + 4),
                new Vector2(startpos.x+2, startpos.y + 4),
                new Vector2(startpos.x+3, startpos.y + 4),
                new Vector2(startpos.x-1, startpos.y + 4),
                new Vector2(startpos.x-2, startpos.y + 4),
                new Vector2(startpos.x-3, startpos.y + 4),

                new Vector2(startpos.x+1, startpos.y + 5),
                new Vector2(startpos.x+2, startpos.y + 5),
                new Vector2(startpos.x-1, startpos.y + 5),
                new Vector2(startpos.x-2, startpos.y + 5),

                new Vector2(startpos.x+1, startpos.y + 6),
                new Vector2(startpos.x-1, startpos.y + 6),
            };

            List<Vector2[]> slots = new List<Vector2[]>() { root, leaves };

            Structure tree = new Structure(check, itemids, slots);

            return tree;
        }

        public Structure getCactus(Vector2 startpos)
        {
            int height = Random.Range(1, 4);

            int[] itemids =
            {
                3
            };

            Vector2[] check =
            {
               new Vector2(startpos.x + 1, startpos.y+1),
               new Vector2(startpos.x - 1, startpos.y+1),
               new Vector2(startpos.x, startpos.y + height + 1)
            };

            Vector2[] root = new Vector2[height];

            for (int h = 0; h < height; h++)
            {
                root[h] = new Vector2(startpos.x, startpos.y + (h+1));
            }


            List<Vector2[]> slots = new List<Vector2[]>() { root };

            Structure tree = new Structure(check.ToArray(), itemids, slots);

            return tree;
        }

        public StructureLoader()
        {
        }
    }
}
