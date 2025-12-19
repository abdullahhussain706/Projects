

---

# 4D Linked List Notepad

## Overview

**4D Linked List Notepad** is a **console-based text editor** implemented in **C++** that uses a **4D doubly linked list** to store characters and lines dynamically. This project also features a **custom undo (Ctrl+Z) and redo (Ctrl+Y) functionality** using stacks to manage text snapshots.

This was a **group project** completed collaboratively by **Muhammad Abdullah** and **Faizan Ali** as part of our **Data Structures and Algorithms coursework**.

* **Language:** C++
* **Platform:** Windows Console Application (uses `<conio.h>` for key input)
* **Data Structure:** 4D doubly linked list (up, down, left, right)
* **Stack Implementation:** Custom stack for Undo/Redo

---

## Features

1. **Basic Editing**

   * Insert characters at the cursor position
   * Delete characters using Backspace
   * Insert a new line with Enter

2. **Cursor Navigation**

   * Move cursor using Arrow keys (Up, Down, Left, Right)
   * Current cursor position displayed as `|`

3. **Undo/Redo System**

   * Undo: Ctrl+Z
   * Redo: Ctrl+Y
   * Both implemented using a custom stack storing snapshots of the full text

4. **Dynamic Storage**

   * Lines and characters stored in a 4D linked list
   * No limit on the number of lines or characters

---


## How It Works

### Insertion

1. Creates a new `Node` for the character
2. Links the node with left and right neighbors
3. Moves the cursor to the new node
4. Pushes the current text snapshot to the **undo stack**
5. Clears the redo stack

### Deletion

1. Removes the character to the left of the cursor
2. Moves cursor backward
3. Pushes the current text snapshot to the **undo stack**

### Undo / Redo

* **Undo (Ctrl+Z):**

  1. Current text is pushed to the redo stack
  2. Pop the previous text from the undo stack
  3. Rebuild the linked list from the popped text

* **Redo (Ctrl+Y):**

  1. Current text is pushed to the undo stack
  2. Pop the text from the redo stack
  3. Rebuild the linked list from the popped text

---

## How to Use

1. Compile the project in any C++ IDE or compiler (Code::Blocks, Dev-C++, Visual Studio, etc.)
2. Run the executable
3. Use keyboard controls:

   * Arrow keys → move cursor
   * Backspace → delete character
   * Enter → insert new line
   * Ctrl+Z → undo
   * Ctrl+Y → redo
   * ESC → exit the program

---

## Advantages

* Efficient memory management using linked list
* Fully dynamic text storage
* No external libraries needed besides `<conio.h>`
* Supports unlimited text input in console

## Limitations

* Console-based only
* Cursor position does not persist after undo/redo
* No file save/load functionality

---

## Future Improvements

1. Add **File I/O** for saving and loading text
2. Preserve cursor position during undo/redo
3. Implement **multi-line selection and copy/paste**
4. Enhance user interface with colors or syntax highlighting

---

## Credits

* **Muhammad Abdullah** – Core Implementation, Undo/Redo Logic
* **Faizan Ali** – Linked List Design, Cursor & Navigation Handling

---


