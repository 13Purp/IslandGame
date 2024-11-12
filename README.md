# IslandGame


The purpose of the game is to guess which island has the biggest average height

    -The map is made out of Cells (30x30 on Easy difficulty), every Cell has a height from 0-1000
    -Cells with height of 0 represent water, 1-1000 represents land
    -Adjacent land Cells together form an Island

The gameplay loop

    -The game starts and generates a map, giving the player 3 lives and a hint
    -The player can hover his mouse over an Island to highlight it
    -The player guesses the correct Island by simply clicking on it
      -If the player has guessed wrong, the screen shakes, one life gets taken away and the game continues
      -If the player has guessed right, the Island will be highlighted in green, a new map will be generated and the score will increase
    -The player can use the hint, which will highlight two Islands in green, one of them will be the correct guess
      -If the player manages to get a streak of 3 correct guesses they will be awarded with another hint
    -The game will continue to run as long as the player has not lost all of his lives, when the player looses their last life the game restarts


## ArchipelAlgo- The twist I added

### Introduction

  Originally, as part of the challenge, the game sends a request to server and gets a matrix - out of which it produces the map
  
  The server randomly selects 1 out of 10 matrices to send - meaning there are only 10 possible maps, which makes the replayability rather low
  
  The solution? 

### **Procedural Island generation!**

![Screenshot 2024-11-12 185343](https://github.com/user-attachments/assets/90bf869b-cb26-4d79-811e-c2ae06f7265c)

### Implementation

  The tried and tested solution to the problem of procedural Island generation would be using Perlin noise. 
  However, I wanted to try something different so I decided to do it using [Cellular Automata](https://en.wikipedia.org/wiki/Cellular_automaton)

  #### How I did it

  Automata abstract Class

    -Every Cell seen on the map is an Automata
    -The Class is used to provide an Interface for updating its own state 
    -The rules that determine when and how the state should change depend on the "type" of Automata in question
      -Classes that inherit the Automata Class:
        * Conways
        * Brian
        * DayNight
        * Seeds
        * Maze
      -Those 5 are implemented currently, although it would be rather easy add any other Cellular Automata

  CellTable Class

    -Holds a matrix of Automata
    -Responsible for Updating the state of the entire matrix
    -Defines a method for Randomazing states of every Cell in the matrix

  ArchipelAlgo Class

    -Holds three CellTables, each of them representing a layer of the algorithm
    -Defines the generate method for generating the final map
      -The generate method first randomizes all three CellTables, and then updates their states a random number of times
      -It then adds the first two layers together i.e. adds the values of their states together - this reduces the amount of cells left at state 0, and leaves some cells with state-2 
      -Advances the state of layer2 by one and repeats the last step
      -Adds layer 3 to the result of the previous steps, only adding to cells whose state is higher or equal to 2
      -Finds Islands in the result
      -Fills up the state 0 cells that are surrounded by higher state cells
      -At this point, we are left with a matrix of cells with their states ranging [0,4] 
      -The algorithm then randomly upscales these states to the [0,1000] range using the states as range brackets
          - 0-0
          - 1-[[1:250]
          - 2-[251:500]
          - 3-[501:750]
          - 4-[751:1000]

  GameLogic class

    -Defines methods for drawing the map, updating game state, handling game logic and acts as a Facade for the classes above
    -Holds the paintBox used to draw the map
    -Holds the CellTable of the map
    -Generates new maps
    -Keeps track of player score, hints, lives and so on

  Form class

    -Takes inputs from the player and calls GameLogic methods to handle the logic

## Extras

The game also features a difficulty setting, which determines the size of the cell matrix <br>
And the ability to change some generation parameters (And by parameters, I mean which Automata is used for which layer) <br>
By default, all three layers are set to Conways as it seems to be the most playable that way <br>
Other combinations can produce more visualy interesting results, and some can produce invalid results  <br>
(Invalid results does not mean that the game crashes, it means that there might only be 1 Island, or cells with a negative height and so on...) <br>
The combination bellow coupled with fast generation seems to be able to produce striking results <br>
![image](https://github.com/user-attachments/assets/2278ce86-dbc1-4d08-afa8-a58c0e64e8b0)

  

  
  
