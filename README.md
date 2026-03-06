# TowerDefense
**First session (5-6 hours)**
- decided not to use Unity Physics or Navmesh and implemented enemy navigation via Waypoints for speed and simplicity
- implemented a basic tile map from 3D Planes (initially I used Cubes, later changed to Planes with 2 Prefab models)
- implemented a simple Tower Prefab (which shoots at the closest enemy) + BuildManager
- implemented Enemy Prefab (follows path from waypoints) + EnemySpawner
- created GameManager (wave control, prepared for game win/lose system)

**Second session (4 hours)**
- created Player Economy for player lives and gold
- implemented UI Manager
- implemented game logic - Player gains gold for killing enemies and buys towers
- implemented Game Over Screen
- added Projectiles - to make the prototype more "alive" - implemented Projectile Pooling for better performance 

**Next steps**
- I hoped I would finish at least two types of towers and enemies, but I'm glad I didn't get stuck on  that - it can be achieved via other Prefabs or Scriptable Objects
(Enemies are ready for object pooling - I used it at least on Projectiles)
- the project is scalable - usage of Prefabs + UIManager, GameManager, enemy spawning, and tower building are separated 
(new types of towers and enemies can be easily added in the future)
- I tried to create at least some UI elements - there is room for more, including bars over enemies, floating texts, Prefabs for similar UI...


