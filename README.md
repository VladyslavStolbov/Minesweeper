# Minesweeper

## MVP:

- [X] Grid Generation: Generate a grid of tiles representing the game board.
- [X] Tile Placement: Place mines randomly on the grid.
- [X] Tile Interaction: Allow players to reveal tiles and flag potential mines.
- [ ] Game Logic: Implement the core game rules, including revealing adjacent tiles and ending the game when a mine is clicked.
- [ ] Win Condition: Check for win condition when all non-mine tiles are revealed.
- [ ] Web Compatibility: Ensure the game can be played seamlessly in a web browser.
- [ ] Android Compatibility: Optimize the game for Android devices, considering screen sizes and touch controls.
- [ ] Player Testing: Conduct testing by allowing players to play the game and provide feedback for improvements.

## TODO:

- [X] Setup Unity Project:
   - [X] Create a new Unity project.
   - [X] Set up necessary folders for scripts, prefabs, and assets.
- [X] Create Tile Prefab:
   - [X] Design a prefab for a single tile with visual representation for unrevealed, revealed, flagged, and mine states.
   - [X] Attach a script to manage tile behavior (e.g., revealing, flagging).
- [X] Generate Grid:
   - [X] Write a script to generate a grid of tiles based on the desired size (rows and columns).
   - [X] Instantiate tile prefabs and position them accordingly.
- [X] Place Mines:
   - [X] Write a script to randomly place a specified number of mines on the grid.
   - [X] Ensure no two mines are placed in the same location.
- [ ] Tile Interaction:
   - [X] Implement mouse click detection on tiles.
   - [X] Write logic to handle left-click to reveal a tile and right-click to flag/unflag a tile as a potential mine.
   - [ ] Logic to left and right click together for revealing all cells around number without flags.
- [ ] Game Logic:
   - [X] Write functions to handle revealing adjacent tiles when a tile with no adjacent mines is clicked.
   - [ ] Implement game over conditions when a mine is clicked.
- [ ] Win Condition:
   - [ ] Write a function to check for the win condition when all non-mine tiles are revealed.
   - [ ] Display a victory message when the win condition is met.
- [ ] UI Implementation:
   - [ ] Design and implement UI elements such as a restart button, timer, and mine counter.
   - [ ] Display messages for game over and victory conditions.
   - [ ] Create a start menu.w
   - [ ] Create a game over menu.
- [ ] Android Compatibility (Primary):
   - [ ] Adapt UI elements for Android devices, considering different screen sizes and resolutions.
   - [ ] Implement touch controls for mobile gameplay.
- [ ] Web Compatibility:
   - [ ] Optimize the game for web deployment.
   - [ ] Test the game in web browsers to ensure compatibility and performance.
- [ ] Player Testing:
    - [ ] Deploy the game to players for testing.
    - [ ] Gather feedback and iterate on improvements based on player suggestions.
    - [ ] Conduct further testing to ensure a smooth and enjoyable gameplay experience.
- [ ] Testing:
   - [ ] Test the game extensively to ensure all features work as intended.
   - [ ] Give a try to test another people my game.
   - [ ] Debug any issues that arise during testing.
- [ ] Polish:
    - [ ] Refactor code for readability and optimization.
    - [ ] Add sound effects and animations to enhance user experience.
    - [ ] Make any necessary adjustments based on user feedback.
