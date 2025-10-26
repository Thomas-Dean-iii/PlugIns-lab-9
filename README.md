Lab 9
Object Pool Pattern
Utilized to create the bullet system, pooling the bullets to be reused over and over again to reduce lag and creating and destroying.
Builder Pattern
Utilized to create the enemy structure, an enemy spawner, their behaviors etc. as well as being able to be tracked for the underlying observer system for the score.
Observer Pattern
Utilized for the scoring system for when an enemy is shot with the bullet system, it collects that data and increases the score by one. Observes the enemy script and bullet script to call the data that the enemy has been shot to add to the score.

Lab 10
To implement the Save/Load feature, we updated the enemy spawner script to clear existing enemies and spawn enemies at the saved position upon loading, whilst resuming spawning afterwards. We also implemented a SaveLoadManager which acts as the central controller which references our ScoreSaver, TransformSaver, ScoreManager, & EnemySpawner scripts as well as the player instance in which when the S key is pressed, Save() is called to save score and positions of enemies/player and when the L key is pressed, Load() is called to restore saved data of score and enemy and player locations. TransformSaver saves the transforms of the player and enemies which is called by our SaveLoadManager. Our ScoreManager was also updated to be called by our Manager to save and load the score at the time of saving. When the scene is initially loaded, there is no saved data until the Load() function is activated by pressing L, which intiializes the saved data file.
