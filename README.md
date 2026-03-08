# Overview
This project implements a Library Management System using C# and Object-Oriented Programming (OOP) principles.
The system allows librarians to manage books, DVDs, borrowers, and borrowing transactions through both:
- Automatic demonstration (preloaded sample data)
- Interactive console input (user-driven actions)
This satisfies the assignment requirement to demonstrate functionality in Main() while also making the system fully usable.

## OOP Design Summary
 ### 1. Encapsulation
All classes use private fields and public properties/methods to protect data.
Examples:
- IsAvailable can only be changed through Borrow() and Return().
- Borrower details are set through the constructor and cannot be modified externally.
This ensures data integrity and prevents invalid states.

### 2. Inheritance
A core requirement was to treat Books and DVDs as Media types so the system can be extended later.
Class hierarchy:
```
Media (abstract)
│
├── Book
└── Dvd
```

- Media defines shared attributes (Id, Title, IsAvailable)
- Book and Dvd inherit from Media and add their own attributes
This reduces duplication and makes the system easy to extend (e.g., EBook, CD).

 ### 3. Polymorphism
The system uses method overriding to allow each media type to display its own details.
```
public abstract string GetDetails();
```

- Book overrides GetDetails() to include Author and Genre
- Dvd overrides GetDetails() to include Director and Runtime
The Library class can display all media without knowing their specific types.

### 4. Class Responsibilities
#### Media (Abstract Base Class)
- Shared attributes for all media
- Borrow/return behavior
- Abstract GetDetails() for polymorphism
#### Book
- Inherits from Media
- Adds Author and Genre
#### Dvd
- Inherits from Media
- Adds Director and Runtime
#### Borrower
- Represents a library user
- Stores name, address, contact number, and email
#### Library
- Manages all media and borrowers
- Generates unique IDs
- Handles:
- Adding/removing media
- Adding/removing borrowers
- Borrowing/returning items
- Displaying lists
This class acts as the system controller.

## System Capabilities
Your program now supports two modes of operation:

 1. Automatic Demonstration (Instructor Requirement)
When the program starts, it automatically:
- Adds sample books and DVDs
- Adds a sample borrower
- Borrows one item
- Displays all media
- Displays all borrowers
This ensures the system demonstrates required functionality without user input.

 2. Interactive Console Menu (User Input)
After the demonstration, the system enters an interactive loop where the librarian can:
### Media Management
- Add a new Book
- Add a new DVD
- Remove media by ID
- View all media items
### Borrower Management
- Add a new borrower
- View all borrowers
### Borrowing System
- Borrow a media item by ID
- Return a media item by ID
### Exit
- Quit the system safely
This makes the system fully functional and testable.
