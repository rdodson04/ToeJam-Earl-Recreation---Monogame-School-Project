using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

public class ItemSpawner
{
    private readonly Queue<Vector2> spawnNodes = new();
    private readonly List<Item> activeItems = new();

    private const int MaxActiveItems = 5;
    private float spawnTimer = 0f;
    private const float SpawnInterval = 10f; // seconds

    public void AddSpawnNode(Vector2 node) => spawnNodes.Enqueue(node);

    public void Update(GameTime gameTime, Player player, Stage stage)
    {
        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Spawn new item periodically
        if (spawnTimer >= SpawnInterval && activeItems.Count < MaxActiveItems)
        {
            spawnTimer = 0f;
            SpawnRandomItem(stage);
        }

        // Update & remove picked items
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            Item item = activeItems[i];

            if (player.TryPickup(item))
            {
                activeItems.RemoveAt(i);
                continue;
            }

            item.Update(gameTime, player);
        }
    }

    private void SpawnRandomItem(Stage stage)
    {
        if (spawnNodes.Count == 0) return;

        Vector2 node = spawnNodes.Dequeue();
        spawnNodes.Enqueue(node);

        // Don't spawn on blocked tiles
        if (stage.IsBlocked(node))
            return;

        Rectangle rect = new Rectangle((int)node.X, (int)node.Y, 32, 32);

        // Example: Present factory
        Item item = ItemFactory.CreateRandomItem(rect);
        activeItems.Add(item);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var item in activeItems)
            item.Draw(spriteBatch);
    }

    public void Clear() => activeItems.Clear();
}
