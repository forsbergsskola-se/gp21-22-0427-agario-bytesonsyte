# Assignment Task List

### TCP & UDP Assignments:

- [x] Time Server implemented
- [x] Time Client implemented
- [x] Open Word Server implemented
- [x] Open word Client implemented

*comments: scenes and scripts for these are found within Agario project:* 
>*Scenes -> Assignments*  
>*Scripts -> Assignments*

#

### Agar.io Player Spawning:

- [x] `Agario` scene added to project
- [x] `FIELD_SIZE` sets the play area 
- [x] `CLAMP_POSITION` of Player to within this Field
- [x] `PLAYER_SPAWN` instantiates player 
  - [x] to a `RANDOM_POSITION` 

*comments: see Spawner.cs for spawning logic and PlayerMovement.cs for active player clamping logic*

#

### Agar.io Player Tracking:

- [x] `FOLLOW_CAMERA` tracks player
- [x] `PLAYER_VISUAL` shows player in-game as a circle
- [x] `PLAYER_INPUT` tracks where player wants to move to
- [x] `MOUSE_POSITION` player moves towards mouse's `VECTOR_DIRECTION`
  - [ ] \(Optional) or `WASD_INPUT` used instead

*comments: see CameraFollow.cs and PlayerMovement.cs. Spawner.cs uses prefabs for visuals *

#

### Agar.io Orb Collection:

- [x] `SPAWN_ORBS` instantiates orbs within map's bounds
  - [x] to a `RANDOM_POSITION`
  - [x] within an `UPDATE_LOOP`
- [x] `COLLECT_ORB` player can eat these orbs
  - [x] to `UPDATE_VISUALS` & grow in size
  - [x] and `INCREASE_SCORE` by one

*comments: see Spawner.cs for SpawnFood() logic, which spawns a set amount within the playuing field & then more in an update if the orb/ food count falls below this inital set amount*  
*Consume.cs handles orb/ food collection and updates the IncreaseScore function in PlayerScore.cs*

#

### Agar.io Enemy/Player Collision:

- [x] `DISTANCE_CHECK` sees if players are overlapping & can be eaten
- [x] `PLAYER_RESPAWN` instantiates smaller player at random pos with 0 score
- [x] `INCREASE_SCORE` of the bigger player with the smaller one's total score
- [ ] `PLAYER_LEADERBOARD` tracks & ranks high scores of players
- [x] `PLAYER_NAMES` shows the names of each player on their circles 
  - [ ] + leaderboard entry 

*comments: Consume.cs uses collision to then compare player scales, then destroy rival if bigger than them and update Score.cs accordingly*  
*Spawner.cs detects if players are destroyed and respawns accordingly*
*Score.cs starts with score at 0 & keeps track of highscore for the leaderboard*

#

### Agar.io Networking:

- [ ] `CONNECT_GAME` for players to join server
- [ ] `DISCONNECT_GAME` disconnects players from server
- [ ] `CURRENTLY_CONNECTED_PLAYERS` tracks active players
- [ ] `OTHER_PLAYERS_BROADCAST` updates player positions & scores to others
- [ ] `CHEAT_PROTECTION` exists in some form
- [ ] `INTERPOLATE` player positions to protect against transportation if lagging occurs

*comments:*

#

### 100% Completion:

- [ ] One final checkbox to rule them all
