# Tanky-Madness

## Core Techniques

## Architecture

## Technical Challenges

## Miscellaneous 

### Object Pooling in Unity
Object Pooling is a technique where you **reuse existing objects instead of creating and destroying them repeatedly**.

This helps improve performance, especially in games with many frequently spawned objects.

#### 🪜 Step-by-Step Guide

##### 1. Define Pools
- Decide what objects you want to pool (e.g., bullets, enemies, effects)
- For each type, define:
  - A unique identifier (tag)
  - A prefab (object template)
  - A pool size (how many to pre-create)

##### 2. Initialize the Pool
- When the game starts:
  - Create a container (Dictionary) to store all pools
  - For each pool:
    - Create multiple instances of the prefab
    - Disable them (inactive state)
    - Store them in a queue (or similar structure)

##### 3. Store Objects Efficiently
- Use a structure that:
  - Groups objects by type (tag)
  - Allows quick access and reuse

👉 Common approach:
- Map each tag → a collection of reusable objects

##### 4. Spawn an Object
When you need an object:
1. Check if the pool exists
2. Take one object from the pool
3. Activate it
4. Set its:
   - Position
   - Rotation
   - Scale
5. Use it in the game

##### 5. Do NOT Return Immediately ⚠️
- Do **not** return the object to the pool right after spawning
- The object is still in use
- Returning it too early can cause:
  - Duplicate usage
  - Visual bugs
  - Logic errors

##### 6. Return Object After Use
- When the object is no longer needed:
  - Disable it
  - Put it back into the pool

👉 Examples:
- Bullet hits target
- Particle effect finishes
- Enemy dies

##### 7. Use a Global Access Point
- Make the pool accessible from anywhere (using Singleton)
- This allows other systems to:
  - Spawn objects easily
  - Return objects when done

#### 🔄 Lifecycle Summary

1. Game starts → objects are created and stored
2. Spawn request → object is taken and activated
3. Object is used in gameplay
4. Object finished → returned and disabled
5. Object becomes available again

#### 🚀 Benefits

- ✅ Better performance (less Instantiate/Destroy)
- ✅ Reduced garbage collection
- ✅ Smoother gameplay (fewer frame drops)
- ✅ Essential for mobile and real-time games

#### 💡 Best Practices

- Preload enough objects to avoid runtime allocation
- Avoid returning objects too early
- Handle cases when the pool is empty (expand or reuse safely)
- Keep pool sizes balanced (not too small, not too large)
- Use pooling for frequently spawned objects only

#### 🔥 When to Use Object Pooling

Use it when:
- Objects are created/destroyed frequently
- Performance is critical
- You notice frame drops or GC spikes

Avoid it when:
- Objects are rarely created
- Simplicity is more important than optimization

### Draw Calls & Sprite Atlas (Unity)

#### Draw Calls (Batches)

- Draw call = 1 render request to GPU  
- More draw calls → worse performance  

##### Default behavior
- ~1 draw call per sprite  
- +1 draw call for game view rendering  
#### Sprite Atlas

- Path:
  - `Assets → Create → 2D → Sprite Atlas`

##### Purpose
- Pack multiple sprites into **one texture**
- Reduce draw calls (batching)
- Improve rendering performance  
#### Important Settings

- **Include in Build**: Ensure atlas is used in final build  

- **Allow Rotation**: Rotates sprites to pack more efficiently  

- **Filter Mode**: Controls texture sampling (Point, Bilinear, etc.)

- **Maximum Texture Size**
  - Smaller size → less memory  
  - Too small → more atlas pages → more draw calls  

- **Compression (Crunch)**
  - Reduces build size  
  - May affect quality slightly  
#### Atlas Pages

- One atlas can have multiple pages  
- Ideally: **1 page**
- Multiple pages happen when:
  - Texture size too large  
  - Device limitations  

👉 More pages → more draw calls  
#### Setup

- Enable Sprite Atlas:
  - `Edit → Project Settings → Editor`
  - Enable:
    - Sprite Atlas V1 or V2  
#### Common Issues

- Atlas not working:
  - Sprites must share **same material**
  - Ensure:
    - Same shader  
    - Same texture settings  

- If bug:
  - Reimport sprites  
  - Reinstall 2D packages  
#### Best Practices

- Group sprites that are:
  - Used together  
  - Visible at the same time  

- Avoid:
  - Putting unrelated sprites in same atlas

👉 Helps reduce memory + improve batching  
