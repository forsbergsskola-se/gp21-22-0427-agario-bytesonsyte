# Assignment Task List

### TCP & UDP Assignments:

- [ ] Time Server implemented
- [ ] Time Client implemented
- [ ] Open Word Server implemented
- [ ] Open word Client implemented

*comments:*

#

### Agar.io Player Spawning:

- [ ] `Agario` scene added to project
- [ ] `FIELD_SIZE` sets the play area 
- [ ] `CLAMP_POSITION` of Player to within this Field
- [ ] `PLAYER_SPAWN` instantiates player 
  - [ ] to a `RANDOM_POSITION` 

*comments:*

#

### Agar.io Player Tracking:

- [ ] `FOLLOW_CAMERA` tracks player
- [ ] `PLAYER_VISUAL` shows player in-game as a circle
- [ ] `PLAYER_INPUT` tracks where player wants to move to
- [ ] `MOUSE_POSITION` player moves towards mouse's `VECTOR_DIRECTION`
  - [ ] or `WASD_INPUT` used instead

*comments:*

#

### Agar.io Orb Collection:

- [ ] `SPAWN_ORBS` instantiates orbs within map's bounds
  - [ ] to a `RANDOM_POSITION`
  - [ ] within an `UPDATE_LOOP`
- [ ] COLLECT_ORB player can eat these orbs
  - [ ] to `UPDATE_VISUALS` & grow in size
  - [ ] and `INCREASE_SCORE` by one

*comments:*

#

### Agar.io Enemy/Player Collision:

- [ ] `DISTANCE_CHECK` sees if players are overlapping & can be eaten
- [ ] `PLAYER_RESPAWN` instantiates smaller player at random pos with 0 score
- [ ] `INCREASE_SCORE` of the bigger player with the smaller one's total score
- [ ] `PLAYER_LEADERBOARD` tracks & ranks high scores of players
- [ ] `PLAYER_NAMES` shows the names of each player on their circles + leaderboard entry 

*comments:*

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
