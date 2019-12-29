using Assets.Scripts.Knowledge_Base;
using Assets.Scripts.MapConstruction;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Initializing Variables
    KnowledgeBase knowledgebase;
    World CurrentTerrain;

    /// <summary>
    /// Generates a World to accompany the Game.
    /// </summary>
    public void CreateWorld()
    {
        knowledgebase = new KnowledgeBase();

        if (CurrentTerrain != null)
        {
            CurrentTerrain.Clear();
        }

        CurrentTerrain = new World(knowledgebase, 300, 700, 0.1f);

    }

    /// <summary>
    /// Function that is called when the game first loads on this scene
    /// </summary>
    private void Start()
    {
        CreateWorld();
    }
}
