# Top-Down Perspective Game ("Legend Of Zelda" Style)

## Overview
This is a top-down perspective action-adventure game inspired by "The Legend of Zelda." The game is developed in Unity and includes core systems such as dungeon generation, event handling, combat mechanics, and more. This README file provides an overview of the project, the architecture, key components, and implementation details.

## Table of Contents
1. Project Architecture
2. Dungeon Generation
3. Event Handling and Interactions
4. Player Combat System
5. Sound Effects
6. Summary

## 1. Project Architecture
The game is structured with a modular architecture to ensure reusability and scalability.

- **Game Manager**: Manages the main game loop, initializes dungeons, and controls player and enemy interactions.
- **Dungeon Generator**: Generates procedural rooms and corridors using Unity's Tilemap system.
- **Event System**: Manages in-game interactions such as opening doors and interacting with objects.
- **Combat System**: Handles player and enemy combat, including attack mechanics.

The scripts are organized into the following directories:

- `Scripts/Generators` for dungeon generation.
- `Scripts/Player` for player mechanics.
- `Scripts/Interactions` for managing interactions with game objects.

## 2. Dungeon Generation
### Overview
The **DungeonGenerator** script handles the procedural generation of rooms and corridors, ensuring diversity and playability.

### Code Example
```csharp
public class DungeonGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    private List<GameObject> generatedRooms = new List<GameObject>();

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < 5; i++) // Example loop to generate rooms
        {
            Vector3 position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            GameObject room = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], position, Quaternion.identity);
            generatedRooms.Add(room);
        }
    }
}
```
### Explanation
- Generates rooms at random positions using prefabs.
- Uses the `roomPrefabs` array to create different types of rooms (e.g., Boss room, Loot room).

## 3. Event Handling and Interactions
### Overview
The **EventHandling** script manages interactions with game objects, including doors, levers, treasure chests, and destructible walls.

### Code Example
```csharp
public class EventHandling : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("Object interacted with!");
        // Additional logic such as opening doors or giving items
    }

    public void DestroyWall()
    {
        Debug.Log("Wall destroyed!");
        // Logic for destroying the wall, such as playing an animation and removing the wall object
        Destroy(gameObject);
    }
}
```
### Explanation
- `OnTriggerEnter2D` detects when the player enters an object's interaction zone.
- The `Interact` method implements behaviors such as opening doors or collecting items.
- The `DestroyWall` method provides logic for destructible walls.

## 4. Player Combat System
### Overview
The **PlayerCombat** script enables melee and ranged attacks, with animations for different types of attacks.

### Code Example
```csharp
public class PlayerCombat : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 1.5f;
    public LayerMask enemyLayers;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, attackRange, enemyLayers);
        if (hit)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }
}
```
### Explanation
- The `Attack` method is triggered by player input.
- A raycast is used to detect enemies within attack range, calling `TakeDamage` on the enemy if found.

## 5. Sound Effects
### Overview
The game includes sound effects for enhancing the player experience, including interaction feedback and background ambiance.

- **Interaction Sound Effect**: Played when the player interacts with an object, providing auditory feedback.

  ```csharp
  public class EventHandling : MonoBehaviour
  {
      public AudioClip interactionSound;
      private AudioSource audioSource;

      void Start()
      {
          audioSource = GetComponent<AudioSource>();
      }

      public void Interact()
      {
          Debug.Log("Object interacted with!");
          audioSource.PlayOneShot(interactionSound);
          // Additional logic such as opening doors or giving items
      }
  }
  ```
  - The `audioSource.PlayOneShot(interactionSound)` provides immediate feedback during player interaction.

- **Background Sound**: A looping background sound effect enhances immersion.

  ```csharp
  public class BackgroundSound : MonoBehaviour
  {
      public AudioClip backgroundMusic;
      private AudioSource audioSource;

      void Start()
      {
          audioSource = GetComponent<AudioSource>();
          audioSource.clip = backgroundMusic;
          audioSource.loop = true;
          audioSource.Play();
      }
  }
  ```
  - The `BackgroundSound` script plays the background music on a loop to set the tone for the game.

## 6. Summary
This document provides an overview of the technical architecture of the game, including dungeon generation, event handling, combat mechanics, and sound effects. The modular design ensures reusability and ease of maintenance, and the detailed code examples illustrate the implementation of each key feature.

For more detailed code and additional information, refer to the respective script files included in the repository.


https://github.com/user-attachments/assets/59aaf638-c822-492c-a9b3-a444eda3823e


Video : 
Technical document: 
[Technical Documentation_ Top-Down Perspective Game (_Legend Of Zelda_ Style).pdf](https://github.com/user-attachments/files/17610148/Technical.Documentation_.Top-Down.Perspective.Game._Legend.Of.Zelda_.Style.pdf)

