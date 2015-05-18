The following contains descriptions of the elements in the the-moose-U5 repository.


Table of contents


Section A: Repository Structure
1 Assets
1.1 Blender
1.2 Fonts
1.3 Materials
1.4 Models
1.5 Resources
1.6 Scripts
1.7 Standard Assets
1.8 Textures
2. Project Settings
3. .gitignore


Section B: Scripts Overview
1. Assets/Scripts Overview
2. Audio Scripts
3. Behavior Scripts
4. Controller Scripts
5. Events Scripts
6. Manager Scripts
7. UnitTests Scripts
8. Utils Scripts
9. Other Scripts


Section C: Other Notes
1. Connection Utilities


Section A: Repository Structure


1. Assets


This folder contains all assets and script needed for the game.


1.1 Blender: This folder contains various blender created .fbx models. 


1.2 Fonts: This folder contains fonts used within the game. 


1.3 Materials: This folder contains various materials used within the game. 


1.4 Models


This folder contains various models used within the game as well as materials for those models. This folder is partially partitioned into subdirectories that contain additional models and materials. 


1.5 Resources


This folder contains the prefabs, scenes and sounds used in the game. The prefabs are in the prefabs folder, the scenes are in the scenes folder, and the sounds are in the sounds folder.


1.6 Scripts: This folder and its scripts are described in Section B.






1.7 Standard Assets


This folder contains any Unity Standard Assets used in the game. Unity Standard Assets are implementations offered by unity for anyone to use. We use Unity’s Standard first person character controller. It is slightly modified to support pause functionality. 


1.8 Textures: This folder contains various textures used within the game. 


2. Project Settings


This game contains global settings for the game such as game inputs in InputManager.asset and build settings in EditorBuildSettings.asset. See the Unity user manual for more information on the specifics of each of these files. These files are managed and updated by Unity when changes are made in the relevant area of the Unity editor. 


3. .gitignore


This contains files and folders that git will ignore when committed. 


Section B: Scripts Overview


1. Assets/Scripts Overview


This folder contains all scripts used within the game that did not originate from Unity’s Standard Assets. The folder structure is defined as follows:


Audio: The audio folder contains audio related scripts. For example, this folder contains the script that adds audio to the player’s droid ship. 


Behaviours: The behaviors folder contains scripts that are attached game prefabs to provide behaviors. For example, the rotation behavior script when attached to an object causes the object to rotate and the computer behavior is attached to the computers within the mothership to provide the press space to enter level functionality.


Controllers: The controllers folder contains scripts that provide user control to objects. For example, the script that converts user input into the player’s droid ship actions is in this folder.


Events: The events folder contains the various events that are used within the game. For example, there is a PauseEvent script, when a new PauseEvent is published using an instance of the EventPublisher the OnPause function is called in all scripts within the scene that contain that function. 


Managers: This folder contains managers that have general logic needed for a certain area for the game. For example, the GUIManager contains the GUI implementation and the LevelManager contains logic needed for the game’s levels. 


UnitTests: This folder contains commented example unit tests. NUnit is needed to run any unit tests. 


Utils: This folder contains utility scripts. In this folder there is currently an FPSLoggingUtilitiy and various external server communication scripts. 


Other scripts: There are also scripts that do not reside in any of the above folders. These are just scripts that do not fit anywhere else. 




2. Audio Scripts


ShipAudio: This script is used to add audio to the player’s droid ship


3. Behavior Scripts


BeamBehavior: implements the tractor beam
BusterBhv: behavior for the cluster buster rocket
CartoonBehaviour: implements a slight bob to a game object
CheckpointBehaviour: behavior for check points
ClusterBhv: behavior for molecule cluster
CollectableBehaviour: behavior for collectable molecules
ComputerBehaviour: behavior for the level select computers in the mothership
DamagerBehaviour: behavior for damaging obstacles
DialogueMarkerBehaviour: behavior for tutorial level dialogue markers
FacingCameraBehaviour: behavior for the players camera
KillSwitchBehaviour: implements the death animation
MovementBehaviour: not used currently
RotationBehaviour: implements a rotation to a game object
ShipBehaviour: implements many of the ships behaviors. For example, damage, death, updating of health, energy, and the jump drive. 
SparkBehaviour: implements sparks when colliding with walls


4. Controller Scripts


ShipController: this script is used to convert the players user input into in game actions. This controls the droid ships motion as well as the tractor beam and cluster buster in game mechanics.


5. Events Scripts


CollectableEvent: this script calls all OnCollect or OnDecollect functions in scripts within the scene when published. This is called when a player collides with a collectable.
DamageEvent: this script calls all OnDamage functions in scripts within the scene when published. This is used when a player receives damage.
DeathEvent: this script calls all OnDeath functions in scripts within the scene when published. This is used when a player dies. 
EventPublisher: an instance of this is needed to publish new events. When an event is published using this, the corresponding function is executed in all scripts that contain that function with in the scene.
GameEvent: the base event everything extends
PauseEvent: this script calls all OnPause functions in scripts within the scene when published. This is used to pause all moving objects and also show the pause screen if necessary.
RespawnEvent: this script calls all OnRespawn functions in scripts within the scene when published. It is used to trigger a player’s respawn. 
ShowMouseEvent: this script calls all OnShowMouse functions in scripts within the scene when published. It is used to show and hide the player’s mouse cursor. 


6. Manager Scripts


GUIManager: contains the GUI implementation logic
LevelManager: contains the general logic needed for the tutorial level and other levels.


7. UnitTests Scripts


ConnectionUtilityTest: contans example Nunit tests for the connection util scripts. NUnit dll is needed for any unit tests of this format. 


8. Utils Scripts


ConnectionUtility: Performs GET/POST http requests using a DataContractJsonSerializer and the ServerRequest, ServerResponse objects
FPSLoggingUtility: Prints FPS measurements to console when it exists within a scene. This is useful for monitoring FPS on lab machines.
ServerRequest: DataContractJsonSerializable server request object
ServerResponse: DataContractJsonSerializable server response object


9. Other Scripts


GameOver: text and buttons for the game over scene
LevelLoader: used to load additional scenes and provide a loading screen with fun fact if necessary
MainMenu: adds highlight color change to start game in the start menu scene
ObjectFinder: utility script to create the Utilities game object if it doesn’t exist 
StateObj: state object for players health, energy, score and molecules collected


Empty or will soon be removed


GameHUD: old HUD implementation, to be removed 
GameManager: empty at the moment, should be in the managers folder
LevelSelectHUD: not used


Section C: Other Notes


1. Connection Utilities


The included ServerRequest, ServerResponse and ConnectionUtility files are all commented out. The files create http web requests with a SHA256 HMAC using data contract files, JsonDataContractSerializer, and HttpWebRequests. They are commented out as the these files required additional appropriately versioned dlls that caused only monodevelop to fail to compile the game due to version issues ( the unity compiler uses a different and custom version of mono and compiled fine ).


The needed dlls were previously included in a branch of the Unity 4 version of the game, but since server communication was deprioritized, these files have not been used or tested in the Unity 5 version. The Unity 4 version required the following dlls: System.Json, System.Runtime.Serialization, System.ServiceModel.Web, System.ServiceModel, System.Web.Services. Unity 5 may have an easier way to provide this server communication functionality.