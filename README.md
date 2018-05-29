# AdvJam2018
Adventure Jam 2018 is over, you can get the source code here!

All the code was made from scratch, with a couple snippets I borrow from preivous prototype projects.

The game is a platformer with an adventure game touch.

It uses a Interaction dialogue system based on scriptable objects. 

You can make a dialogue sheet that calls events.

Create Dialogue/ActorResponseSheet
  An Actor Response Sheet has a speech node list. 
  Here you place dialogue text like so
    Node 1
      actor name : boss
      text : hello
    Node 2
      actor name : Billy
      text : Good morning

Events are used to change global variables and move the game along. 

Basic run down.

Assets/Data/GlobalVariables - This scriptable object stores all variables that might be interacted with in game. Add a variable into the Variables (leave the dynamic list alone).
The dynamic list is used in runtime and the Variables list is the initial template. To reset the runtime variables to match the template variables click
Level Editor -> Build Variables.
