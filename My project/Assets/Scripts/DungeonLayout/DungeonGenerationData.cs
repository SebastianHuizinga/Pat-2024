using UnityEngine;

// Allows creating DungeonGenerationData assets in the Unity Editor
[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGeneartionData/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    // Number of crawlers to be generated in the dungeon
    public int numberOfCrawlers;

    // Minimum number of iterations for dungeon generation
    public int iterationMin;

    // Maximum number of iterations for dungeon generation
    public int iterationMax;
}
