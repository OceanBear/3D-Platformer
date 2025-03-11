This project implements a simple platformer game featuring:

**Free-Look Camera:**  
Use a Cinemachine free-look camera to look around. The camera’s orientation determines the player's forward direction.  
*(Player controls: use the mouse to rotate the view.)*

**Movement:**  
The player moves relative to the camera's rotation, ensuring smooth and intuitive navigation in a 3D environment.  
*(Player controls: use W, A, S, D to move.)*

**Jumping:**  
The controller supports jumping when the player is grounded, as well as double-jumps when airborne.  
*(Player controls: press the Space key to jump or double-jump.)*

**Dash:**  
A dash mechanic is implemented that propels the player along the horizontal plane (X and Z directions). When no movement input is provided, the dash is performed in the direction the camera is facing; otherwise, it follows the input direction. A cooldown period prevents repeated dashes.  
*(Player controls: click the right mouse button to dash in the direction of keyboard input or, if no input is provided, dash in the camera's forward direction.)*

**Coin Collection & Scoring:**  
Coins are scattered across the environment. When the player collides with a coin, it disappears and increments the score displayed in the UI.

**Environment:**  
The game includes a large flat plane with platforms, boxes, and invisible walls to create a controlled play area.


https://github.com/user-attachments/assets/eb7e3196-ade4-475f-bbc5-ed788e1aa84e

