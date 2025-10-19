Object Pool Pattern
Utilized to create the bullet system, pooling the bullets to be reused over and over again to reduce lag and creating and destroying.
Builder Pattern
Utilized to create the enemy structure, an enemy spawner, their behaviors etc. as well as being able to be tracked for the underlying observer system for the score.
Observer Pattern
Utilized for the scoring system for when an enemy is shot with the bullet system, it collects that data and increases the score by one. Observes the enemy script and bullet script to call the data that the enemy has been shot to add to the score.
