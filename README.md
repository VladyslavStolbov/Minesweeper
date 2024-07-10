# Minesweeper

## MVP:

- [X] Grid Generation: Generate a grid of tiles representing the game board.
- [X] Tile Placement: Place mines randomly on the grid.
- [X] Tile Interaction: Allow players to reveal tiles and flag potential mines.
- [X] Game Logic: Implement the core game rules, including revealing adjacent tiles and ending the game when a mine is clicked.
- [X] Win Condition: Check for win condition when all non-mine tiles are revealed.
- [ ] Web Compatibility: Ensure the game can be played seamlessly in a web browser.

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
- [X] Game Logic:
   - [X] Write functions to handle revealing adjacent tiles when a tile with no adjacent mines is clicked.
   - [X] Implement game over conditions when a mine is clicked.
- [X] Win Condition:
   - [X] Write a function to check for the win condition when all non-mine tiles are revealed.
   - [X] Display a victory message when the win condition is met.
- [ ] Web Compatibility:
   - [ ] Optimize the game for web deployment.
   - [ ] Test the game in web browsers to ensure compatibility and performance.
- [ ] Polish:
    - [ ] Refactor code for readability and optimization.
    - [ ] Add sound effects and animations to enhance user experience.
    - [ ] Make any necessary adjustments based on user feedback.
