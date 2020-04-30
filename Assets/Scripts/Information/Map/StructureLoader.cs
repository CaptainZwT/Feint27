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
                6, 20
            };

            Vector2[] check =
            {
               new Vector2(startpos.x+4, startpos.y + 3),
               new Vector2(startpos.x-4, startpos.y + 3),

               new Vector2(startpos.x+4, startpos.y + 4),
               new Vector2(startpos.x-4, startpos.y + 4),

               new Vector2(startpos.x, startpos.y + 9),
            };


            Vector2[] root =
            {
                new Vector2(startpos.x, startpos.y + 1),
                new Vector2(startpos.x, startpos.y + 2),
                new Vector2(startpos.x, startpos.y + 3),
                new Vector2(startpos.x, startpos.y + 4),
                new Vector2(startpos.x, startpos.y + 5),
                new Vector2(startpos.x, startpos.y + 6),
                new Vector2(startpos.x, startpos.y + 7),
                new Vector2(startpos.x, startpos.y + 8),
            };

            Vector2[] leaves =
            {
                new Vector2(startpos.x+1, startpos.y + 4),
                new Vector2(startpos.x+2, startpos.y + 4),
                new Vector2(startpos.x+3, startpos.y + 4),
                new Vector2(startpos.x-1, startpos.y + 4),
                new Vector2(startpos.x-2, startpos.y + 4),
                new Vector2(startpos.x-3, startpos.y + 4),

                new Vector2(startpos.x+1, startpos.y + 5),
                new Vector2(startpos.x+2, startpos.y + 5),
                new Vector2(startpos.x+3, startpos.y + 5),
                new Vector2(startpos.x-1, startpos.y + 5),
                new Vector2(startpos.x-2, startpos.y + 5),
                new Vector2(startpos.x-3, startpos.y + 5),

                new Vector2(startpos.x+1, startpos.y + 6),
                new Vector2(startpos.x+2, startpos.y + 6),
                new Vector2(startpos.x+3, startpos.y + 6),
                new Vector2(startpos.x-1, startpos.y + 6),
                new Vector2(startpos.x-2, startpos.y + 6),
                new Vector2(startpos.x-3, startpos.y + 6),

                new Vector2(startpos.x+1, startpos.y + 7),
                new Vector2(startpos.x+2, startpos.y + 7),
                new Vector2(startpos.x-1, startpos.y + 7),
                new Vector2(startpos.x-2, startpos.y + 7),

                new Vector2(startpos.x+1, startpos.y + 8),
                new Vector2(startpos.x-1, startpos.y + 8),

                new Vector2(startpos.x,   startpos.y + 9),
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
               new Vector2(startpos.x + 2, startpos.y+1),
               new Vector2(startpos.x - 2, startpos.y+1),
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

        public Structure getPyramid(Vector2 startpos)
        {
            int[] itemids =
            {
                18, 17, 19
            };

            Vector2[] check =
            {

               new Vector2(startpos.x  , startpos.y+2),
               new Vector2(startpos.x+1, startpos.y+2),
               new Vector2(startpos.x-1, startpos.y+2),

               new Vector2(startpos.x, startpos.y+6),
            };

            Vector2[] baseblk =
            {
                new Vector2(startpos.x+1, startpos.y+1),
                new Vector2(startpos.x+2, startpos.y+1),
                new Vector2(startpos.x+3, startpos.y+1),
                new Vector2(startpos.x+4, startpos.y+1),
                new Vector2(startpos.x-1, startpos.y+1),
                new Vector2(startpos.x-2, startpos.y+1),
                new Vector2(startpos.x-3, startpos.y+1),
                new Vector2(startpos.x-4, startpos.y+1),

                new Vector2(startpos.x+2, startpos.y+2),
                new Vector2(startpos.x-2, startpos.y+2),
                new Vector2(startpos.x+3, startpos.y+2),
                new Vector2(startpos.x-3, startpos.y+2),

                new Vector2(startpos.x+1, startpos.y+3),
                new Vector2(startpos.x-1, startpos.y+3),

                new Vector2(startpos.x+2, startpos.y+3),
                new Vector2(startpos.x-2, startpos.y+3),

                new Vector2(startpos.x, startpos.y+4),
                new Vector2(startpos.x+1, startpos.y+4),
                new Vector2(startpos.x-1, startpos.y+4),
            };

            Vector2[] chests =
            {
                new Vector2(startpos.x, startpos.y+1),
            };

            Vector2[] gold =
            {
                new Vector2(startpos.x, startpos.y+5),
            };


            List<Vector2[]> slots = new List<Vector2[]>() { baseblk, chests, gold };

            Structure tree = new Structure(check.ToArray(), itemids, slots, true);

            tree.groundspots = new Vector2[]
            {
                new Vector2(startpos.x  , startpos.y),
                new Vector2(startpos.x+1, startpos.y),
                new Vector2(startpos.x+2, startpos.y),
                new Vector2(startpos.x+3, startpos.y),
                new Vector2(startpos.x+4, startpos.y),
                new Vector2(startpos.x-1, startpos.y),
                new Vector2(startpos.x-2, startpos.y),
                new Vector2(startpos.x-3, startpos.y),
                new Vector2(startpos.x-4, startpos.y),
            };

            tree.biome_id = 2;

            return tree;
        }

        public Structure getStoneCabin(Vector2 startpos)
        {
            int[] itemids =
            {
                0, 6
            };

            Vector2[] check =
            {
            };

            Vector2[] stonebase =
            {
                // base floor
                new Vector2(startpos.x, startpos.y+1),
                new Vector2(startpos.x+1, startpos.y+1),
                new Vector2(startpos.x+2, startpos.y+1),
                new Vector2(startpos.x+3, startpos.y+1),
                new Vector2(startpos.x-1, startpos.y+1),
                new Vector2(startpos.x-2, startpos.y+1),
                new Vector2(startpos.x-3, startpos.y+1),

                // center pillar
                new Vector2(startpos.x, startpos.y+2),
                new Vector2(startpos.x, startpos.y+3),
                new Vector2(startpos.x, startpos.y+4),
                new Vector2(startpos.x, startpos.y+5),
                new Vector2(startpos.x, startpos.y+6),

                // left pillar
                new Vector2(startpos.x+3, startpos.y+2),
                new Vector2(startpos.x+3, startpos.y+3),
                new Vector2(startpos.x+3, startpos.y+4),
                new Vector2(startpos.x+3, startpos.y+5),

                // right pillar
                new Vector2(startpos.x-3, startpos.y+2),
                new Vector2(startpos.x-3, startpos.y+3),
                new Vector2(startpos.x-3, startpos.y+4),
                new Vector2(startpos.x-3, startpos.y+5),

                // center 2nd story floor
                new Vector2(startpos.x+1, startpos.y+4),
                new Vector2(startpos.x+2, startpos.y+4),
                new Vector2(startpos.x+4, startpos.y+4),
                new Vector2(startpos.x-1, startpos.y+4),
                new Vector2(startpos.x-2, startpos.y+4),
                new Vector2(startpos.x-4, startpos.y+4),

                new Vector2(startpos.x+1, startpos.y+5),
                new Vector2(startpos.x-1, startpos.y+5),
            };

            Vector2[] woodbase =
            {
                // left arc
                new Vector2(startpos.x+5, startpos.y+4),
                new Vector2(startpos.x+4, startpos.y+5),
                new Vector2(startpos.x+3, startpos.y+6),
                new Vector2(startpos.x+2, startpos.y+5),

                // right arc
                new Vector2(startpos.x-5, startpos.y+4),
                new Vector2(startpos.x-4, startpos.y+5),
                new Vector2(startpos.x-3, startpos.y+6),
                new Vector2(startpos.x-2, startpos.y+5),

                // center arc
                new Vector2(startpos.x-1, startpos.y+6),
                new Vector2(startpos.x+1, startpos.y+6),
                new Vector2(startpos.x,   startpos.y+7),

                // filling wood on left and right sides
                new Vector2(startpos.x+1, startpos.y+2),
                new Vector2(startpos.x+2, startpos.y+2),
                new Vector2(startpos.x+1, startpos.y+3),
                new Vector2(startpos.x+2, startpos.y+3),

                new Vector2(startpos.x-1, startpos.y+2),
                new Vector2(startpos.x-2, startpos.y+2),
                new Vector2(startpos.x-1, startpos.y+3),
                new Vector2(startpos.x-2, startpos.y+3),
            };


            List<Vector2[]> slots = new List<Vector2[]>() { stonebase, woodbase };

            Structure tree = new Structure(check.ToArray(), itemids, slots, true);

            tree.groundspots = new Vector2[]
            {
                new Vector2(startpos.x  , startpos.y),
                new Vector2(startpos.x+1, startpos.y),
                new Vector2(startpos.x+2, startpos.y),
                new Vector2(startpos.x+3, startpos.y),
                new Vector2(startpos.x-1, startpos.y),
                new Vector2(startpos.x-2, startpos.y),
                new Vector2(startpos.x-3, startpos.y),
            };

            tree.biome_id = 1;

            return tree;
        }

        public Structure getPineTree(Vector2 startpos)
        {
            int[] itemids =
            {
                15, 21, 7
            };

            Vector2[] check =
            {
               new Vector2(startpos.x+4, startpos.y + 2),
               new Vector2(startpos.x-4, startpos.y + 2),
               new Vector2(startpos.x+5, startpos.y + 2),
               new Vector2(startpos.x-5, startpos.y + 2),

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
                new Vector2(startpos.x, startpos.y + 7),
            };

            Vector2[] leaves =
            {
                new Vector2(startpos.x+1, startpos.y+2),
                new Vector2(startpos.x+2, startpos.y+2),
                new Vector2(startpos.x+3, startpos.y+2),
                new Vector2(startpos.x-1, startpos.y+2),
                new Vector2(startpos.x-2, startpos.y+2),
                new Vector2(startpos.x-3, startpos.y+2),

                new Vector2(startpos.x+1, startpos.y+3),
                new Vector2(startpos.x+2, startpos.y+3),
                new Vector2(startpos.x-1, startpos.y+3),
                new Vector2(startpos.x-2, startpos.y+3),

                new Vector2(startpos.x+1, startpos.y+4),
                new Vector2(startpos.x-1, startpos.y+4),
                new Vector2(startpos.x+2, startpos.y+4),
                new Vector2(startpos.x-2, startpos.y+4),

                new Vector2(startpos.x+1, startpos.y+5),
                new Vector2(startpos.x-1, startpos.y+5),

                new Vector2(startpos.x+1, startpos.y+6),
                new Vector2(startpos.x-1, startpos.y+6),
            };

            Vector2[] snow =
            {
            };

            List<Vector2[]> slots = new List<Vector2[]>() { root, leaves, snow };

            Structure tree = new Structure(check, itemids, slots);

            return tree;
        }

        public Structure getGrave(Vector2 startpos)
        {

            int[] itemids =
            {
                0
            };

            Vector2[] check =
            {
               new Vector2(startpos.x + 1, startpos.y+1),
               new Vector2(startpos.x - 1, startpos.y+1),

               new Vector2(startpos.x + 2, startpos.y+2),
               new Vector2(startpos.x - 2, startpos.y+2),

               new Vector2(startpos.x + 2, startpos.y+3),
               new Vector2(startpos.x - 2, startpos.y+3),
               new Vector2(startpos.x + 3, startpos.y+3),
               new Vector2(startpos.x - 3, startpos.y+3),

               new Vector2(startpos.x,     startpos.y+5)
            };

            Vector2[] root =
            {
                new Vector2(startpos.x, startpos.y+1),
                new Vector2(startpos.x, startpos.y+2),
                new Vector2(startpos.x, startpos.y+3),
                new Vector2(startpos.x, startpos.y+4),

                new Vector2(startpos.x+1, startpos.y+3),
                new Vector2(startpos.x-1, startpos.y+3),
            };


            List<Vector2[]> slots = new List<Vector2[]>() { root };

            Structure tree = new Structure(check.ToArray(), itemids, slots);

            return tree;
        }

        public Structure getFlower(Vector2 startpos)
        {
            int dice_color = Random.Range(1, 100);

            int flower_color;

            if (dice_color < 30)
            {
                flower_color = 24; // red
            }
            else if (dice_color < 60)
            {
                flower_color = 25; // blue
            }
            else
            {
                flower_color = 26; // yellow
            }

            int[] itemids =
            {
                20, flower_color
            };

            Vector2[] check =
            {
               new Vector2(startpos.x + 1, startpos.y+1),
               new Vector2(startpos.x - 1, startpos.y+1),
               new Vector2(startpos.x + 2, startpos.y+1),
               new Vector2(startpos.x - 2, startpos.y+1),
               new Vector2(startpos.x, startpos.y + 3)
            };

            Vector2[] root =
            {
                new Vector2(startpos.x, startpos.y+1)
            };

            Vector2[] flower =
            {
                new Vector2(startpos.x, startpos.y+2)
            };


            List<Vector2[]> slots = new List<Vector2[]>() { root, flower };

            Structure tree = new Structure(check.ToArray(), itemids, slots);

            return tree;
        }

        public Structure getStoneCrypt(Vector2 startpos)
        {
            int[] itemids =
            {
                0, 22, 8, 28
            };

            Vector2[] check =
            {
                new Vector2(startpos.x+4, startpos.y+1),
                new Vector2(startpos.x-4, startpos.y+1),

                new Vector2(startpos.x+2, startpos.y+10),
                new Vector2(startpos.x-2, startpos.y+10),
                new Vector2(startpos.x, startpos.y+12),
            };

            Vector2[] stonedec =
            {
                // cross
                new Vector2(startpos.x, startpos.y+7),
                new Vector2(startpos.x, startpos.y+8),
                new Vector2(startpos.x, startpos.y+9),
                new Vector2(startpos.x, startpos.y+10),
                new Vector2(startpos.x+1, startpos.y+10),
                new Vector2(startpos.x-1, startpos.y+10),
                new Vector2(startpos.x, startpos.y+11),
            };

            Vector2[] dioritebase =
            {
                // base floor
                new Vector2(startpos.x+1, startpos.y+1),
                new Vector2(startpos.x+2, startpos.y+1),
                new Vector2(startpos.x+3, startpos.y+1),
                new Vector2(startpos.x-1, startpos.y+1),
                new Vector2(startpos.x-2, startpos.y+1),
                new Vector2(startpos.x-3, startpos.y+1),

                new Vector2(startpos.x+1, startpos.y+2),
                new Vector2(startpos.x+2, startpos.y+2),
                new Vector2(startpos.x+3, startpos.y+2),
                new Vector2(startpos.x-1, startpos.y+2),
                new Vector2(startpos.x-2, startpos.y+2),
                new Vector2(startpos.x-3, startpos.y+2),

                new Vector3(startpos.x,   startpos.y+3),
                new Vector2(startpos.x+1, startpos.y+3),
                new Vector2(startpos.x+2, startpos.y+3),
                new Vector2(startpos.x+3, startpos.y+3),
                new Vector2(startpos.x-1, startpos.y+3),
                new Vector2(startpos.x-2, startpos.y+3),
                new Vector2(startpos.x-3, startpos.y+3),

                new Vector3(startpos.x,   startpos.y+4),
                new Vector2(startpos.x+1, startpos.y+4),
                new Vector2(startpos.x+2, startpos.y+4),
                new Vector2(startpos.x-1, startpos.y+4),
                new Vector2(startpos.x-2, startpos.y+4),

                new Vector2(startpos.x, startpos.y+5),
                new Vector2(startpos.x+1, startpos.y+5),
                new Vector2(startpos.x-1, startpos.y+5),
            };

            Vector2[] basaltdec =
            {
                // stone base deco
                new Vector2(startpos.x+3, startpos.y+4),
                new Vector2(startpos.x-3, startpos.y+4),

                new Vector2(startpos.x+2, startpos.y+5),
                new Vector2(startpos.x-2, startpos.y+5),

                new Vector2(startpos.x, startpos.y+6),
                new Vector2(startpos.x+1, startpos.y+6),
                new Vector2(startpos.x-1, startpos.y+6),
            };

            Vector2[] granitedec =
            {
                // door
                new Vector2(startpos.x, startpos.y+1),
                new Vector2(startpos.x, startpos.y+2),

            };


            List<Vector2[]> slots = new List<Vector2[]>() { stonedec, dioritebase, basaltdec, granitedec };

            Structure tree = new Structure(check.ToArray(), itemids, slots, true);

            tree.groundspots = new Vector2[]
            {
                new Vector2(startpos.x  , startpos.y),
                new Vector2(startpos.x+1, startpos.y),
                new Vector2(startpos.x+2, startpos.y),
                new Vector2(startpos.x+3, startpos.y),
                new Vector2(startpos.x-1, startpos.y),
                new Vector2(startpos.x-2, startpos.y),
                new Vector2(startpos.x-3, startpos.y),
            };

            tree.biome_id = 6;

            return tree;
        }

        public Structure getWoodWindmill(Vector2 startpos)
        {
            int[] itemids =
            {
                22, 15, 28, 6, 29, 30
            };

            Vector2[] check =
            {
                new Vector2(startpos.x, startpos.y+15),
                new Vector2(startpos.x+4, startpos.y+1),
                new Vector2(startpos.x+5, startpos.y+1),
                new Vector2(startpos.x-4, startpos.y+1),
                new Vector2(startpos.x-5, startpos.y+1),

                new Vector2(startpos.x+4, startpos.y+3),
                new Vector2(startpos.x+5, startpos.y+3),
                new Vector2(startpos.x-4, startpos.y+3),
                new Vector2(startpos.x-5, startpos.y+3),

                new Vector2(startpos.x+7, startpos.y+9),
                new Vector2(startpos.x-7, startpos.y+9),
                new Vector2(startpos.x+8, startpos.y+9),
                new Vector2(startpos.x-8, startpos.y+9),
                new Vector2(startpos.x+9, startpos.y+9),
                new Vector2(startpos.x-9, startpos.y+9),
            };

            Vector2[] dioritebase =
            {
                // floor
                new Vector2(startpos.x+1, startpos.y+1),
                new Vector2(startpos.x+2, startpos.y+1),
                new Vector2(startpos.x+3, startpos.y+1),
                new Vector2(startpos.x-1, startpos.y+1),
                new Vector2(startpos.x-2, startpos.y+1),
                new Vector2(startpos.x-3, startpos.y+1),

                // base
                new Vector2(startpos.x+1, startpos.y+2),
                new Vector2(startpos.x+2, startpos.y+2),
                new Vector2(startpos.x-1, startpos.y+2),
                new Vector2(startpos.x-2, startpos.y+2),

                new Vector2(startpos.x, startpos.y+3),
                new Vector2(startpos.x+1, startpos.y+3),
                new Vector2(startpos.x+2, startpos.y+3),
                new Vector2(startpos.x-1, startpos.y+3),
                new Vector2(startpos.x-2, startpos.y+3),

                new Vector2(startpos.x, startpos.y+4),
                new Vector2(startpos.x+1, startpos.y+4),
                new Vector2(startpos.x-1, startpos.y+4),
                new Vector2(startpos.x-2, startpos.y+4),

                new Vector2(startpos.x, startpos.y+5),
                new Vector2(startpos.x-1, startpos.y+5),

                new Vector2(startpos.x, startpos.y+6),
                new Vector2(startpos.x-1, startpos.y+6),

                new Vector2(startpos.x-1, startpos.y+7),

                new Vector2(startpos.x+1, startpos.y+8),
                new Vector2(startpos.x-1, startpos.y+8),

                new Vector2(startpos.x+1, startpos.y+10),
                new Vector2(startpos.x-1, startpos.y+10),

            };

            Vector2[] darkwood_fan =
            {
                // base details
                new Vector2(startpos.x+3, startpos.y+2),

                new Vector2(startpos.x-3, startpos.y+2),
                new Vector2(startpos.x-3, startpos.y+3),
                new Vector2(startpos.x-3, startpos.y+4),

                new Vector2(startpos.x-2, startpos.y+5),
                new Vector2(startpos.x-3, startpos.y+5),

                new Vector2(startpos.x-2, startpos.y+6),
            };

            Vector2[] granite =
            {
                new Vector2(startpos.x  , startpos.y+1),
                new Vector2(startpos.x  , startpos.y+2),
            };

            Vector2[] oakwood_fan =
            {
                // fan
                new Vector2(startpos.x+2, startpos.y+4),
                new Vector2(startpos.x+1, startpos.y+5),
                new Vector2(startpos.x+1, startpos.y+6),

                new Vector2(startpos.x, startpos.y+7),
                new Vector2(startpos.x-5, startpos.y+7),

                new Vector2(startpos.x, startpos.y+8),
                new Vector2(startpos.x-3, startpos.y+8),
                new Vector2(startpos.x-4, startpos.y+8),

                new Vector2(startpos.x+1, startpos.y+9),
                new Vector2(startpos.x+2, startpos.y+9),
                new Vector2(startpos.x-1, startpos.y+9),
                new Vector2(startpos.x-2, startpos.y+9),

                new Vector2(startpos.x, startpos.y+10),
                new Vector2(startpos.x+3, startpos.y+10),
                new Vector2(startpos.x+4, startpos.y+10),

                new Vector2(startpos.x, startpos.y+11),
                new Vector2(startpos.x+5, startpos.y+11),

                new Vector2(startpos.x-1, startpos.y+12),
                new Vector2(startpos.x-1, startpos.y+13),
                new Vector2(startpos.x-2, startpos.y+14),

                new Vector2(startpos.x-3, startpos.y+15),
            };

            Vector2[] darkRedWool =
            {
                new Vector2(startpos.x, startpos.y+9),
            };

            Vector2[] whiteWool =
            {
                // mill sail
                new Vector2(startpos.x+3, startpos.y+3),
                new Vector2(startpos.x+3, startpos.y+4),
                new Vector2(startpos.x+3, startpos.y+5),

                new Vector2(startpos.x+2, startpos.y+5),

                new Vector2(startpos.x+2, startpos.y+6),
                new Vector2(startpos.x-4, startpos.y+6),
                new Vector2(startpos.x-5, startpos.y+6),
                new Vector2(startpos.x-6, startpos.y+6),

                new Vector2(startpos.x+2, startpos.y+7),
                new Vector2(startpos.x+1, startpos.y+7),
                new Vector2(startpos.x-2, startpos.y+7),
                new Vector2(startpos.x-3, startpos.y+7),
                new Vector2(startpos.x-4, startpos.y+7),

                new Vector2(startpos.x-2, startpos.y+8),

                new Vector2(startpos.x+2, startpos.y+10),

                new Vector2(startpos.x+2, startpos.y+11),
                new Vector2(startpos.x+3, startpos.y+11),
                new Vector2(startpos.x+4, startpos.y+11),
                new Vector2(startpos.x-1, startpos.y+11),
                new Vector2(startpos.x-2, startpos.y+11),

                new Vector2(startpos.x-2, startpos.y+12),
                new Vector2(startpos.x-2, startpos.y+13),
                new Vector2(startpos.x-3, startpos.y+13),

                new Vector2(startpos.x-3, startpos.y+14),

            };

            // diorite
            // dark wood
            // granite
            // oak wood
            // dark red wool
            // white wool

            List<Vector2[]> slots = new List<Vector2[]>() { dioritebase, darkwood_fan, granite, oakwood_fan, darkRedWool, whiteWool };

            Structure tree = new Structure(check.ToArray(), itemids, slots, true);

            tree.groundspots = new Vector2[]
            {
                new Vector2(startpos.x  , startpos.y),
                new Vector2(startpos.x+1, startpos.y),
                new Vector2(startpos.x+2, startpos.y),
                new Vector2(startpos.x+3, startpos.y),
                new Vector2(startpos.x-1, startpos.y),
                new Vector2(startpos.x-2, startpos.y),
                new Vector2(startpos.x-3, startpos.y),
            };

            tree.biome_id = 1;

            return tree;
        }

        public Structure getPantheon(Vector2 startpos)
        {
            int[] itemids =
            {
                22, 0
            };

            Vector2[] check =
            {
                new Vector2(startpos.x+10, startpos.y+1),
                new Vector2(startpos.x-10, startpos.y+1),

                new Vector2(startpos.x+9, startpos.y+6),
                new Vector2(startpos.x-9, startpos.y+6),

                new Vector2(startpos.x, startpos.y+7),
            };

            Vector2[] dioritebase =
            {
                // base
                new Vector2(startpos.x, startpos.y+1),
                new Vector2(startpos.x+1, startpos.y+1),
                new Vector2(startpos.x+2, startpos.y+1),
                new Vector2(startpos.x+3, startpos.y+1),
                new Vector2(startpos.x+4, startpos.y+1),
                new Vector2(startpos.x+5, startpos.y+1),
                new Vector2(startpos.x+6, startpos.y+1),
                new Vector2(startpos.x+7, startpos.y+1),
                new Vector2(startpos.x+8, startpos.y+1),
                new Vector2(startpos.x+9, startpos.y+1),
                new Vector2(startpos.x-1, startpos.y+1),
                new Vector2(startpos.x-2, startpos.y+1),
                new Vector2(startpos.x-3, startpos.y+1),
                new Vector2(startpos.x-4, startpos.y+1),
                new Vector2(startpos.x-5, startpos.y+1),
                new Vector2(startpos.x-6, startpos.y+1),
                new Vector2(startpos.x-7, startpos.y+1),
                new Vector2(startpos.x-8, startpos.y+1),
                new Vector2(startpos.x-9, startpos.y+1),

                new Vector2(startpos.x, startpos.y+2),
                new Vector2(startpos.x+1, startpos.y+2),
                new Vector2(startpos.x+2, startpos.y+2),
                new Vector2(startpos.x+3, startpos.y+2),
                new Vector2(startpos.x+4, startpos.y+2),
                new Vector2(startpos.x+5, startpos.y+2),
                new Vector2(startpos.x+6, startpos.y+2),
                new Vector2(startpos.x+7, startpos.y+2),
                new Vector2(startpos.x+8, startpos.y+2),
                new Vector2(startpos.x-1, startpos.y+2),
                new Vector2(startpos.x-2, startpos.y+2),
                new Vector2(startpos.x-3, startpos.y+2),
                new Vector2(startpos.x-4, startpos.y+2),
                new Vector2(startpos.x-5, startpos.y+2),
                new Vector2(startpos.x-6, startpos.y+2),
                new Vector2(startpos.x-7, startpos.y+2),
                new Vector2(startpos.x-8, startpos.y+2),

                // roof
                new Vector2(startpos.x, startpos.y+5),
                new Vector2(startpos.x+1, startpos.y+5),
                new Vector2(startpos.x+2, startpos.y+5),
                new Vector2(startpos.x+3, startpos.y+5),
                new Vector2(startpos.x+4, startpos.y+5),
                new Vector2(startpos.x+5, startpos.y+5),
                new Vector2(startpos.x+6, startpos.y+5),
                new Vector2(startpos.x+7, startpos.y+5),
                new Vector2(startpos.x-1, startpos.y+5),
                new Vector2(startpos.x-2, startpos.y+5),
                new Vector2(startpos.x-3, startpos.y+5),
                new Vector2(startpos.x-4, startpos.y+5),
                new Vector2(startpos.x-5, startpos.y+5),
                new Vector2(startpos.x-6, startpos.y+5),
                new Vector2(startpos.x-7, startpos.y+5),
            };

            Vector2[] stone =
            {
                // pillars
                new Vector2(startpos.x-6, startpos.y+3),
                new Vector2(startpos.x-6, startpos.y+4),

                new Vector2(startpos.x-3, startpos.y+3),
                new Vector2(startpos.x-3, startpos.y+4),

                new Vector2(startpos.x, startpos.y+3),
                new Vector2(startpos.x, startpos.y+4),

                new Vector2(startpos.x+3, startpos.y+3),
                new Vector2(startpos.x+3, startpos.y+4),

                new Vector2(startpos.x+6, startpos.y+3),
                new Vector2(startpos.x+6, startpos.y+4),

                // roof details
                new Vector2(startpos.x-1, startpos.y+6),
                new Vector2(startpos.x-2, startpos.y+6),
                new Vector2(startpos.x-4, startpos.y+6),
                new Vector2(startpos.x-5, startpos.y+6),

                new Vector2(startpos.x+1, startpos.y+6),
                new Vector2(startpos.x+2, startpos.y+6),
                new Vector2(startpos.x+4, startpos.y+6),
                new Vector2(startpos.x+5, startpos.y+6),
            };

            List<Vector2[]> slots = new List<Vector2[]>() { dioritebase, stone };

            Structure tree = new Structure(check.ToArray(), itemids, slots, true);

            tree.groundspots = new Vector2[]
            {
                new Vector2(startpos.x-1, startpos.y),
                new Vector2(startpos.x-2, startpos.y),
                new Vector2(startpos.x-3, startpos.y),
                new Vector2(startpos.x-4, startpos.y),
                new Vector2(startpos.x-5, startpos.y),
                new Vector2(startpos.x-6, startpos.y),
                new Vector2(startpos.x-7, startpos.y),
                new Vector2(startpos.x-8, startpos.y),
                new Vector2(startpos.x  , startpos.y),
                new Vector2(startpos.x+1, startpos.y),
                new Vector2(startpos.x+2, startpos.y),
                new Vector2(startpos.x+3, startpos.y),
                new Vector2(startpos.x+4, startpos.y),
                new Vector2(startpos.x+5, startpos.y),
                new Vector2(startpos.x+6, startpos.y),
                new Vector2(startpos.x+7, startpos.y),
                new Vector2(startpos.x+8, startpos.y),
            };

            tree.biome_id = 3;

            return tree;
        }

        public Structure getSkyscraper(Vector2 startpos)
        {
            int[] itemids =
            {
                28, 0
            };

            Vector2[] check =
            {
                new Vector2(startpos.x+3, startpos.y+1),
                new Vector2(startpos.x+4, startpos.y+1),
                new Vector2(startpos.x-3, startpos.y+1),
                new Vector2(startpos.x-4, startpos.y+1),

                new Vector2(startpos.x+3, startpos.y+16),
                new Vector2(startpos.x+4, startpos.y+16),
                new Vector2(startpos.x-3, startpos.y+16),
                new Vector2(startpos.x-4, startpos.y+16),

                new Vector2(startpos.x, startpos.y+20),
            };

            Vector2[] granitebase =
            {
                // base
                new Vector2(startpos.x, startpos.y+1),
                new Vector2(startpos.x-2, startpos.y+1),
                new Vector2(startpos.x+1, startpos.y+1),
                new Vector2(startpos.x+2, startpos.y+1),

                new Vector2(startpos.x, startpos.y+2),
                new Vector2(startpos.x-2, startpos.y+2),
                new Vector2(startpos.x+1, startpos.y+2),
                new Vector2(startpos.x+2, startpos.y+2),

                new Vector2(startpos.x, startpos.y+3),
                new Vector2(startpos.x-1, startpos.y+3),
                new Vector2(startpos.x-2, startpos.y+3),
                new Vector2(startpos.x+1, startpos.y+3),
                new Vector2(startpos.x+2, startpos.y+3),

                new Vector2(startpos.x, startpos.y+4),
                new Vector2(startpos.x-1, startpos.y+4),
                new Vector2(startpos.x-2, startpos.y+4),
                new Vector2(startpos.x+1, startpos.y+4),
                new Vector2(startpos.x+2, startpos.y+4),

                new Vector2(startpos.x, startpos.y+5),
                new Vector2(startpos.x-1, startpos.y+5),

                new Vector2(startpos.x, startpos.y+6),
                new Vector2(startpos.x-1, startpos.y+6),

                new Vector2(startpos.x, startpos.y+7),
                new Vector2(startpos.x-1, startpos.y+7),
                new Vector2(startpos.x-2, startpos.y+7),
                new Vector2(startpos.x+1, startpos.y+7),
                new Vector2(startpos.x+2, startpos.y+7),

                new Vector2(startpos.x, startpos.y+8),
                new Vector2(startpos.x+1, startpos.y+8),

                new Vector2(startpos.x, startpos.y+9),
                new Vector2(startpos.x+1, startpos.y+9),

                new Vector2(startpos.x, startpos.y+10),
                new Vector2(startpos.x-1, startpos.y+10),
                new Vector2(startpos.x-2, startpos.y+10),
                new Vector2(startpos.x+1, startpos.y+10),
                new Vector2(startpos.x+2, startpos.y+10),

                new Vector2(startpos.x, startpos.y+11),
                new Vector2(startpos.x-1, startpos.y+11),

                new Vector2(startpos.x, startpos.y+12),
                new Vector2(startpos.x-1, startpos.y+12),

                new Vector2(startpos.x, startpos.y+13),
                new Vector2(startpos.x-1, startpos.y+13),
                new Vector2(startpos.x-2, startpos.y+13),
                new Vector2(startpos.x+1, startpos.y+13),
                new Vector2(startpos.x+2, startpos.y+13),

                new Vector2(startpos.x, startpos.y+14),
                new Vector2(startpos.x+1, startpos.y+14),

                new Vector2(startpos.x, startpos.y+15),
                new Vector2(startpos.x+1, startpos.y+15),

                new Vector2(startpos.x, startpos.y+16),
                new Vector2(startpos.x-1, startpos.y+16),
                new Vector2(startpos.x-2, startpos.y+16),
                new Vector2(startpos.x+1, startpos.y+16),
                new Vector2(startpos.x+2, startpos.y+16),

                new Vector2(startpos.x, startpos.y+17),
                new Vector2(startpos.x-1, startpos.y+17),
                new Vector2(startpos.x+1, startpos.y+17),

                new Vector2(startpos.x, startpos.y+18),
                new Vector2(startpos.x, startpos.y+19),
            };

            Vector2[] stone =
            {
                // door
                new Vector2(startpos.x-1, startpos.y+1),
                new Vector2(startpos.x-1, startpos.y+2),

                // windows
                // tier 1
                new Vector2(startpos.x-2, startpos.y+5),
                new Vector2(startpos.x+1, startpos.y+5),
                new Vector2(startpos.x+2, startpos.y+5),

                new Vector2(startpos.x-2, startpos.y+6),
                new Vector2(startpos.x+1, startpos.y+6),
                new Vector2(startpos.x+2, startpos.y+6),

                // tier 2
                new Vector2(startpos.x-2, startpos.y+8),
                new Vector2(startpos.x-1, startpos.y+8),
                new Vector2(startpos.x+2, startpos.y+8),

                new Vector2(startpos.x-2, startpos.y+9),
                new Vector2(startpos.x-1, startpos.y+9),
                new Vector2(startpos.x+2, startpos.y+9),

                // tier 1
                new Vector2(startpos.x-2, startpos.y+11),
                new Vector2(startpos.x+1, startpos.y+11),
                new Vector2(startpos.x+2, startpos.y+11),

                new Vector2(startpos.x-2, startpos.y+12),
                new Vector2(startpos.x+1, startpos.y+12),
                new Vector2(startpos.x+2, startpos.y+12),

                // tier 2
                new Vector2(startpos.x-2, startpos.y+14),
                new Vector2(startpos.x-1, startpos.y+14),
                new Vector2(startpos.x+2, startpos.y+14),

                new Vector2(startpos.x-2, startpos.y+15),
                new Vector2(startpos.x-1, startpos.y+15),
                new Vector2(startpos.x+2, startpos.y+15),
            };

            List<Vector2[]> slots = new List<Vector2[]>() { granitebase, stone };

            Structure tree = new Structure(check.ToArray(), itemids, slots, true);

            tree.groundspots = new Vector2[]
            {
                new Vector2(startpos.x-1, startpos.y),
                new Vector2(startpos.x-2, startpos.y),
                new Vector2(startpos.x  , startpos.y),
                new Vector2(startpos.x+1, startpos.y),
                new Vector2(startpos.x+2, startpos.y),
            };

            tree.biome_id = 5;

            return tree;
        }

        public Structure getDioriteColumn(Vector2 startpos)
        {
            int[] itemids =
            {
                22, 0
            };

            Vector2[] check =
            {
                new Vector2(startpos.x-4, startpos.y+1),
                new Vector2(startpos.x+4, startpos.y+1),
                new Vector2(startpos.x, startpos.y+5),
            };

            Vector2[] dioritedec =
            {
                // base
                new Vector2(startpos.x-1, startpos.y+1),
                new Vector2(startpos.x, startpos.y+1),
                new Vector2(startpos.x+1, startpos.y+1),

                new Vector2(startpos.x-1, startpos.y+4),
                new Vector2(startpos.x, startpos.y+4),
                new Vector2(startpos.x+1, startpos.y+4),
            };

            Vector2[] stonebase =
            {
                // stone base
                new Vector2(startpos.x, startpos.y+2),
                new Vector2(startpos.x, startpos.y+3),

            };

            List<Vector2[]> slots = new List<Vector2[]>() { dioritedec, stonebase };

            Structure tree = new Structure(check.ToArray(), itemids, slots, true);

            tree.groundspots = new Vector2[]
            {
                new Vector2(startpos.x-1, startpos.y),
                new Vector2(startpos.x  , startpos.y),
                new Vector2(startpos.x+1, startpos.y),
            };

            tree.biome_id = 3;

            return tree;
        }

        public StructureLoader()
        {
        }
    }
}
