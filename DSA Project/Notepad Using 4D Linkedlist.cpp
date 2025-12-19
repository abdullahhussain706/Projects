#include <iostream>
#include <conio.h>
#include <string>
#include <cstdlib>

using namespace std;

// Node structure for 4D linked list
struct Node {
    char data;
    Node* up;
    Node* down;
    Node* left;
    Node* right; 
    Node(char c) : data(c), up(NULL), down(NULL), left(NULL), right(NULL) {}
};

struct TextStack {
    string text;
    TextStack* next;
};

class RedoStack {
    TextStack* top;
public:
    RedoStack() { top = NULL; }

    void push(string t) {
        TextStack* n = new TextStack;
        n->text = t;
        n->next = top;
        top = n;
    }

    bool empty() {
        return top == NULL;
    }

    string pop() {
        TextStack* temp = top;
        string t = temp->text;
        top = temp->next;
        delete temp;
        return t;
    }
};


// Notepad class to manage the 4D linked list
class Notepad {
private:
    Node* head;
    Node* currentLine;
    Node* currentChar;
    int cursorPos;
    RedoStack redoStack;

public:
    Notepad() {
        head = NULL;
        currentLine = NULL;
        currentChar = NULL;
        cursorPos = 0;
    }

    // Initialize the notepad with a single empty node
    void initNotepad() {
        Node* newNode = new Node(' ');
        head = newNode;
        currentLine = newNode; 
        currentChar = newNode;
    }

    // Function to move cursor up
    void moveUp() {
        if (currentLine && currentLine->up) {
            Node* temp = currentLine->up;
            int tempCursorPos = cursorPos;
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
            Node* temp = currentLine->down;
            int tempCursorPos = cursorPos;
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
            cursorPos--;
        }
    }

    // Function to move cursor right
	void moveRight() {
	    if (currentChar && currentChar->right) {
	        currentChar = currentChar->right;
	        cursorPos++;
	    } 
	    // If at the end of the line, move to the start of the next line
	    else if (currentLine && currentLine->down) {
	        currentLine = currentLine->down;
	        currentChar = currentLine;
	        cursorPos = 0;
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
            currentChar->right->left = newNode;
        }
        currentChar->right = newNode;
        currentChar = newNode;
        cursorPos++;
    }

    // Function to handle Backspace and delete across lines
    void deleteCharacter() {
        if (currentChar && currentChar->left) {
            Node* temp = currentChar;
            currentChar = currentChar->left;
            currentChar->right = temp->right;

            if (temp->right) {
                temp->right->left = currentChar;
            }
            delete temp;
            cursorPos--;
        } 
        else if (currentLine->up) {
            Node* tempLine = currentLine;
            moveUp();
            currentChar = currentLine;
            cursorPos = 0;
            while (currentChar && currentChar->right) {
                currentChar = currentChar->right;
                cursorPos++;
            }

            // If there's something to delete in the previous line, delete it
            if (currentChar && currentChar->left) {
                deleteCharacter();
            } else {
                currentLine->down = tempLine->down;
                if (tempLine->down) {
                    tempLine->down->up = currentLine;
                }
                delete tempLine;
            }
        }
    }

    // Function to handle the Enter key and create a new line
	void insertNewLine() {
	    Node* newLine = new Node(' '); 
	    newLine->up = currentLine;
	    newLine->down = currentLine->down;
	
	    if (currentLine->down) {
	        currentLine->down->up = newLine;
	    }
	    currentLine->down = newLine;

	    
	    Node* temp = currentChar->right;
	    currentChar->right = NULL;
	    // Transfer characters to the new line
	    Node* newLineChar = newLine;
	    while (temp) {
	        Node* next = temp->right;
	        newLineChar->right = temp;
	        temp->left = newLineChar;
	        newLineChar = temp;
	        temp = next;
	    }
	
	    
	    currentLine = newLine;
	    currentChar = newLine;
	    cursorPos = 0;
	}


    string getAllText() {
        string text;
        Node* line = head;
        while (line) {
            Node* ch = line->right;
            while (ch) {
                text += ch->data;
                ch = ch->right;
            }
            if (line->down) text += '\n';
            line = line->down;
        }
        return text;
    }


    void rebuildFromText(string text) {
        clearNotepad();
        for (char c : text) {
            if (c == '\n')
                insertNewLine();
            else
                insertCharacter(c);
        }
    }

    // Function to clear the entire text (undo operation)
     void clearNotepad() {
        while (head) {
            Node* ch = head;
            while (ch) {
                Node* next = ch->right;
                delete ch;
                ch = next;
            }
            head = head->down;
        }
        initNotepad();
    }

    // Function to display the notepad content with a vertical cursor at the current position
	void display() {
	    Node* tempLine = head;
	    while (tempLine != NULL) {
	        Node* tempChar = tempLine;
	        while (tempChar != NULL) {
	            if (tempChar == currentChar) {
	                cout << tempChar->data << '|';
	            } else {
	                cout << tempChar->data;
	            }
	            tempChar = tempChar->right;
	        }
	        cout << endl;
	        tempLine = tempLine->down;
	    }
	    cout << endl;
	}

    // Function to handle user input from console
    void handleInput() {
        while (true) {
            int input = _getch();

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
            }
            else if (input == 26) {   // Ctrl+Z
                redoStack.push(getAllText());
                clearNotepad();
            }
            else if (input == 25) {   // Ctrl+Y
                if (!redoStack.empty())
                    rebuildFromText(redoStack.pop());
            }
            else {
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
    notepad.display();
    notepad.handleInput();
    return 0;
}