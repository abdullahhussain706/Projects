#include <iostream>
#include <conio.h>  // For _getch()
#include <string>
#include <cstdlib>  // For system("cls")

using namespace std;

// Node structure for 4D linked list
struct Node {
    char data;         // Character data
    Node* up;          // Link to previous line
    Node* down;        // Link to next line
    Node* left;        // Link to previous character in the same line
    Node* right;       // Link to next character in the same line

    // Constructor to initialize the node
    Node(char c) : data(c), up(NULL), down(NULL), left(NULL), right(NULL) {}
};

// Notepad class to manage the 4D linked list
class Notepad {
private:
    Node* head;           // Head of the text
    Node* currentLine;    // Current line in the text
    Node* currentChar;    // Current character in the line
    int cursorPos;        // Track horizontal position of the cursor for up and down movement

public:
    Notepad() {
        head = NULL;
        currentLine = NULL;
        currentChar = NULL;
        cursorPos = 0;
    }

    // Initialize the notepad with a single empty node
    void initNotepad() {
        Node* newNode = new Node(' '); // Start with a blank node
        head = newNode;
        currentLine = newNode; 
        currentChar = newNode;
    }

    // Function to move cursor up
    void moveUp() {
        if (currentLine && currentLine->up) {
            Node* temp = currentLine->up;  // Move to the line above
            int tempCursorPos = cursorPos; // Store the current horizontal position
            currentLine = temp;
            currentChar = currentLine;

            // Try to move to the same horizontal position on the new line
            while (currentChar && tempCursorPos > 0 && currentChar->right) {
                currentChar = currentChar->right;
                tempCursorPos--;
            }
        }
    }

    // Function to move cursor down
    void moveDown() {
        if (currentLine && currentLine->down) {
            Node* temp = currentLine->down; // Move to the line below
            int tempCursorPos = cursorPos;  // Store the current horizontal position
            currentLine = temp;
            currentChar = currentLine;

            // Try to move to the same horizontal position on the new line
            while (currentChar && tempCursorPos > 0 && currentChar->right) {
                currentChar = currentChar->right;
                tempCursorPos--;
            }
        }
    }

    // Function to move cursor left
    void moveLeft() {
        if (currentChar && currentChar->left) {
            currentChar = currentChar->left;
            cursorPos--; // Decrease cursor position
        }
    }

    // Function to move cursor right
	void moveRight() {
	    if (currentChar && currentChar->right) {
	        currentChar = currentChar->right;
	        cursorPos++; // Increase cursor position
	    } 
	    // If at the end of the line, move to the start of the next line
	    else if (currentLine && currentLine->down) {
	        currentLine = currentLine->down; // Move to the next line
	        currentChar = currentLine;       // Move to the start of the next line
	        cursorPos = 0;                   // Reset cursor position to 0
	    }
	}

    // Function to insert a character
    void insertCharacter(char c) {
        Node* newNode = new Node(c);
        newNode->up = currentLine->up;
        newNode->down = currentLine->down;
        newNode->left = currentChar;
        newNode->right = currentChar->right;

        if (currentChar->right) {
            currentChar->right->left = newNode; // Link the right neighbor
        }
        currentChar->right = newNode;  // Link currentChar to the new node
        currentChar = newNode;         // Move to the new node
        cursorPos++;                   // Move cursor right
    }

    // Function to handle Backspace and delete across lines
    void deleteCharacter() {
        if (currentChar && currentChar->left) {  // If not at the start of the line
            Node* temp = currentChar;
            currentChar = currentChar->left;
            currentChar->right = temp->right;  // Skip over the node to be deleted

            if (temp->right) {
                temp->right->left = currentChar;  // Maintain left link of the next node
            }
            delete temp;
            cursorPos--; // Update the cursor position
        } 
        else if (currentLine->up) {  // If at the start of the line, go to the previous line
            Node* tempLine = currentLine;
            moveUp();  // Move cursor to the previous line
            currentChar = currentLine;
            cursorPos = 0;
            while (currentChar && currentChar->right) {
                currentChar = currentChar->right;  // Move to the end of the previous line
                cursorPos++;
            }

            // If there's something to delete in the previous line, delete it
            if (currentChar && currentChar->left) {
                deleteCharacter();  // Recursively delete the character from the previous line
            } else {
                currentLine->down = tempLine->down;  // Remove the empty line
                if (tempLine->down) {
                    tempLine->down->up = currentLine;  // Maintain the link
                }
                delete tempLine;
            }
        }
    }

    // Function to handle the Enter key and create a new line
	void insertNewLine() {
	    // Create a new blank node for the new line
	    Node* newLine = new Node(' '); 
	    newLine->up = currentLine;
	    newLine->down = currentLine->down;
	
	    if (currentLine->down) {
	        currentLine->down->up = newLine;  // Link the current line down pointer
	    }
	    currentLine->down = newLine;  // Link the current line to the new line
	
	    // Move characters after the cursor to the new line
	    Node* temp = currentChar->right;
	    currentChar->right = NULL;  // Break the link to the rest of the line
	    
	    // Transfer characters to the new line
	    Node* newLineChar = newLine;
	    while (temp) {
	        Node* next = temp->right;  // Store the next character
	        newLineChar->right = temp; // Move the current character to the new line
	        temp->left = newLineChar;  // Adjust the left pointer
	        newLineChar = temp;        // Move to the next character
	        temp = next;               // Move to the next character in the old line
	    }
	
	    // Update the current position to the new line
	    currentLine = newLine;
	    currentChar = newLine;
	    cursorPos = 0;  // Reset cursor position
	}

    // Function to clear the entire text (undo operation)
    void clearNotepad() {
        while (head != NULL) {
            Node* tempLine = head;
            while (tempLine != NULL) {
                Node* tempChar = tempLine;
                tempLine = tempLine->right;
                delete tempChar;
            }
            head = head->down;
        }
        initNotepad(); // Reinitialize the notepad to an empty state
    }

    // Function to display the notepad content with a vertical cursor at the current position
	void display() {
	    Node* tempLine = head;  // Start from the first line
	    while (tempLine != NULL) {
	        Node* tempChar = tempLine;  // Start from the first character in the line
	        while (tempChar != NULL) {
	            // Display the character, and if it's the current character, show the cursor after it
	            if (tempChar == currentChar) {
	                cout << tempChar->data << '|';  // Display the character followed by the cursor
	            } else {
	                cout << tempChar->data;  // Otherwise just display the character
	            }
	            tempChar = tempChar->right;  // Move to the next character
	        }
	        cout << endl;  // Move to the next line after printing all characters in the current line
	        tempLine = tempLine->down;  // Move to the next line
	    }
	    cout << endl;
	}

    // Function to handle user input from console
    void handleInput() {
        while (true) {
            int input = _getch(); // Get a single character input from user

            if (input == 27) { // Escape key to exit
                break;
            } else if (input == 224) { // Arrow keys (Windows specific extended key codes)
                input = _getch(); // Get the actual arrow key
                if (input == 72) { // Up arrow
                    moveUp();
                } else if (input == 80) { // Down arrow
                    moveDown();
                } else if (input == 75) { // Left arrow
                    moveLeft();
                } else if (input == 77) { // Right arrow
                    moveRight();
                }
            } else if (input == 8) { // Backspace key
                deleteCharacter();
            } else if (input == 13) { // Enter key
                insertNewLine();
            } else if (input == 26) { // Ctrl+Z for undo (clear notepad)
                clearNotepad();
            } else if (input == 127) { // Ctrl+Z as an alternative (ASCII DEL key)
                clearNotepad();
            } else {
                insertCharacter(static_cast<char>(input));
            }

            system("cls"); // Clear the screen to refresh the display
            display();     // Display the notepad content
        }
    }
};

int main() {
    Notepad notepad;
    notepad.initNotepad();
    cout << "Simple Notepad (Press ESC to exit)\n";
    notepad.display(); // Display initial content
    notepad.handleInput();
    return 0;
}