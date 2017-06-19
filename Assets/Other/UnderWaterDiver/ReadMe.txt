Underwater Scuba Diver Pro is a fully controllable underwater scuba diving model that can run on land or swim in water.

To add Scuba Diver Pro to your project, follow these instructions: 
-Import the entire package and then drag in the 'DiverFirstPerson' or any other Prefab into your scene.
-Either add the 'WaterSurface' Prefab into your scene or create a new one.
-If you add a new water model, to make the diver interact with the water, add a collider underneath the water and set it's 'IsTrigger' to true.
-Change the Tag of the water with the collider to "Water".

When adding a new Diver prefab with either First or Third Person camera's, you will need to do the following for each camera:
-Drill down to the game object with the Camera Script attached (either 'Third Person Camera' or 'First Person Camera').
-On the Sun Shafts component, assign a directional light that is your scenes sun to the 'Shafts Caster' inspector field. Turn on the Sun Shafts component to add Sun Shafts to the camera.
-Optionally, add a 'Caustic Light' directional light game object to the 'Camera Underwater' script so that when camera goes underwater, it will turn the caustic light on or off.
-Optionally, when under water, the camera will turn it's 'Depth of Field' component on. Currently, the camera will focus on the 'head' of the player, to change this, add the transform into the 'Focus on Transform' field.

Diver keyboard inputs in water:
- 'WASD': Move player
- 'C': Sink
- 'Space': Float up
- 'Shift': Swim slowly
- 'V' : Toggle Camera

Version 1.1 Update
-Added New First Person camera mode with new 'DiverFirstPerson' prefab.
-Added a Camera Manager to easily toggle between First and Third person modes. This can be added with the 'DiverWithCameraManager' prefab.
-Diver controller can now interact with multiple bodies of water with differing Water Height levels, diver automatically changes buoyancy water level to match that of the body of water.
-Improved quality of Example Scene with added Camera Effects and new high resolution textures and models.
-Modified and improved Diver Controller scripts to work with First Person mode. When in First Person mode, diver will now backpedal when swimming backwards in water.