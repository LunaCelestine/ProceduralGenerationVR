# Procedurally Generated Environments for VR Game Development

# Working Title: VirtuaBlocks

## Overview
The intent of this project is to explore the use of procedural generation algorithms to
generate terrain maps for virtual reality game development. This project will consist of a game
developed using the Unity game engine and will apply the Perlin Noise algorithm to generate
the in-game terrain using voxels. Additionally, the game will use the Windows Mixed Reality
Toolkit and will be developed and tested using a Samsung Odyssey head mounted display. The
game itself will be a 3d game in which the player is able to move throughout the world as well as
place and destroy blocks composed of voxels. The player will move about the world from a first
person perspective displayed through the virtual reality headset.

Key Features:
- Procedurally generated world consisting of voxels
- First person perspective via VR headset
- VR Motion Controls


Future Updates:
- Building and destroying cubes
- Procedural generation using algorithms other than Perlin Noise
- Enemies

Project Architecture

![Architecture](ProjectArchitecture.png?raw=true "Project Architecture")

Not yet implemented:

- CharacterController
- AudioManager
- GameManager

## Development

#### Terrain Generation

In this project, the terrain consists of a series of voxels, much like in MineCraft. A voxel is a series of 2-dimensional surfaces
that used to represent a cube. The difference between rendering a cube as a opposed to voxel is that wit ha cube, all side of the cube
are contantly being rendered whereas with voxels, only those sides that are visible to the player are rendered. This significant increases performance.

![Voxels](Voxels.png?raw=true "Voxels")

These voxels serve as the surface on which the player walks. This terrain is generated using an improved Version of the PerlinNoise algorithm.
The output of the algorithm denotes the height in number of voxels at that point on the map. The purpose of using this algorithm is to generate terrain
which has a natural topology akin to rolling hills.

A detailed explanation of the algorithm can be found here:

https://github.com/bdabdoub/PerlinNoise

The terrain consists of a series of chunks, which are simply a square area of voxels. These chunks are implemented in the chunk.cs class.
The world object loops through all points in each chunk while calling the noise class to generate a Y value which represents the
height in voxels at that point in the map. Each chunk checks to see what surfaces are surrounding in order to determine which surfaces are 
to be rendered. 

![ProjectScreenshot](projectscreenshot.png?raw=true "Voxels")

When rendered using a transparent wireframe, you can see that the bottom and unexposed sides of the voxel are not rendered.

Here it is with textures applied:


![TerrainWithTextures](texturedterrain.png?raw=true "Voxels")

#### WindowsMixedReality Integration


In order to get the mixed reality working with this project, I followed the instructions found at these two links:

https://docs.microsoft.com/en-us/windows/mixed-reality/unity-development-overview
https://github.com/Microsoft/MixedRealityToolkit-Unity

Importing the mixed reality toolkit into the project gives you access to a number or examples and prefabs that 
make working with WMR in Unity fairly simple. 

The view of the headset is rendered separately for each eye display as show below:

![View](wmrview.png?raw=true "Voxels")

The player can navigate around the terrain using the motion controllers. In order to move forward, the player simply point to
the location where they'd like to go and uses the joystick on the controller which shows a line leading to that spot. When the joystick is released,
the player teleports to that location.


![View](movement.png?raw=true "Voxels")

#### Future Updates

In future updates the player will be able to create and destroy blocks on the map in order to build structures. The player will also be able to jump using
the buttons on the motion controllers. Alternate terrain generation algorithms might also be explored, including 3-dimensional noise algorithms.



References:

http://pcg.wikidot.com/pcg-games:minecraft

https://en.wikipedia.org/wiki/Perlin_noise

https://docs.unity3d.com/Manual/wmr_sdk_overview.html

https://github.com/Microsoft/MixedRealityToolkit-Unity

https://docs.microsoft.com/en-us/windows/mixed-reality/unity-development-overview