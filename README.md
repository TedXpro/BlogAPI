# Blog Backend API

This is the backend API for the Blog application. It provides endpoints to manage blog posts, comments, and users.

## Features

- User authentication and authorization
- CRUD operations for blog posts
- CRUD operations for comments
- User profile management

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- MongoDB Server
- JWT for authentication

## Getting Started

### Prerequisites

- .NET 6 SDK
- SQL Server

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/BlogBackendAPI.git
    ```
2. Navigate to the project directory:
    ```bash
    cd BlogBackendAPI
    ```
3. Update the connection string in `appsettings.json`:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
    }
    ```
4. Apply migrations and update the database:
    ```bash
    dotnet ef database update
    ```
5. Run the application:
    ```bash
    dotnet run
    ```

## API Endpoints

### Authentication

- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Authenticate a user and get a token

### Blog Posts

- `GET /api/posts` - Get all blog posts
- `GET /api/posts/{id}` - Get a single blog post by ID
- `POST /api/posts` - Create a new blog post
- `PUT /api/posts/{id}` - Update a blog post by ID
- `DELETE /api/posts/{id}` - Delete a blog post by ID

### Comments

- `GET /api/posts/{postId}/comments` - Get all comments for a blog post
- `POST /api/posts/{postId}/comments` - Add a comment to a blog post
- `DELETE /api/comments/{id}` - Delete a comment by ID

### Users

- `GET /api/users/{id}` - Get user profile by ID
- `PUT /api/users/{id}` - Update user profile by ID

## License

This project is licensed under the MIT License.

## Contact

For any questions or feedback, please contact [yourname@example.com](mailto:yourname@example.com).