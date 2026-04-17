# Tanky-Madness

## Core Techniques

## Architecture

## Technical Challenges

## Miscellaneous 

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
