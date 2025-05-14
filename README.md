# Netolewx - Movie Watching Platform

![.NET](https://img.shields.io/badge/.NET-5.0%2B-512BD4)
![MVC](https://img.shields.io/badge/Architecture-MVC-brightgreen)
![C#](https://img.shields.io/badge/Language-C%23-239120)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927)

> A comprehensive web application for browsing, watching, and maintaining watchlists of movies built with ASP.NET MVC.

## 📋 Table of Contents

- [Project Overview](#-project-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Technology Stack](#-technology-stack)
- [Installation](#-installation)
- [Usage](#-usage)
- [Screenshots](#-screenshots)
- [Contributing](#-contributing)
- [License](#-license)

## 🎬 Project Overview

Netolewx is a feature-rich movie watching platform developed using ASP.NET MVC. The application allows users to discover movies, create personalized watchlists, and enjoy streaming content in a user-friendly interface. This project implements a 3-layer architecture separating business logic, data access, and presentation layers for maintainable and scalable code.

## ✨ Features

- **User Authentication & Authorization**
  - Secure registration and login
  - Role-based access control
  - Account management
  
- **Movie Catalog**
  - Browse movies by categories, genres, years
  - Advanced search functionality
  - Detailed movie information and metadata
  
- **Personalized Watchlist**
  - Add/remove movies to watchlist
  - Track watched status
  - Custom categorization of saved content
  
- **Content Streaming**
  - High-quality video playback
  - Adaptive streaming capabilities
  - Playback history and resume watching

- **User Interactions**
  - Ratings and reviews
  - Recommendations based on viewing history
  - Social sharing options

## 🏗️ Architecture

The application follows a standard N-tier architecture with three main layers:

```
Netolewx/
│
├── BLL/                  # Business Logic Layer
│   ├── Services/         # Business services
│   ├── Models/           # Business models
│   └── Interfaces/       # Service interfaces
│
├── DAL/                  # Data Access Layer
│   ├── Repositories/     # Data repositories
│   ├── Context/          # Database context
│   └── Entities/         # Database entities
│
└── Netolewx/             # Presentation Layer (MVC)
    ├── Controllers/      # MVC controllers
    ├── Views/            # Razor views
    ├── Models/           # View models
    ├── wwwroot/          # Static files (CSS, JS, images)
    └── Areas/            # Feature areas (Admin, User, etc.)
```

### Layer Responsibilities:

1. **Business Logic Layer (BLL)**
   - Implements core application logic
   - Manages transactions and business rules
   - Handles validation and processing

2. **Data Access Layer (DAL)**
   - Provides data access abstractions
   - Manages entity relationships
   - Handles database operations

3. **Presentation Layer (Netolewx)**
   - Implements the MVC pattern
   - Renders UI components
   - Processes user input

## 🛠️ Technology Stack

- **Backend**
  - ASP.NET MVC
  - C#
  - Entity Framework Core
  - SQL Server

- **Frontend**
  - HTML5, CSS3, JavaScript
  - Bootstrap
  - jQuery
  - AJAX

- **Authentication**
  - ASP.NET Identity

- **Tools & DevOps**
  - Visual Studio
  - Git
  - Azure DevOps (optional)

## 📥 Installation

### Prerequisites

- Visual Studio 2019 or newer
- .NET 5.0+ SDK
- SQL Server (or SQL Server Express)

### Setup Steps

1. **Clone the repository**

```bash
git clone https://github.com/ELZAYDA/Netolewx.git
cd Netolewx
```

2. **Restore dependencies**

```bash
dotnet restore
```

3. **Set up the database**

```bash
cd DAL
dotnet ef database update
```

4. **Build the solution**

```bash
dotnet build
```

5. **Run the application**

```bash
cd ../Netolewx
dotnet run
```

## 🚀 Usage

1. **Access the application**
   - Navigate to `https://localhost:5001` in your web browser

2. **User Registration/Login**
   - Create a new account or login with existing credentials

3. **Browse Movies**
   - Explore the movie catalog by navigating through categories
   - Use the search function to find specific titles

4. **Managing Watchlist**
   - Add movies to your watchlist by clicking the "Add to Watchlist" button
   - Access your watchlist from the user dashboard
   - Remove items or mark them as watched

5. **Admin Features** (for admin accounts)
   - Access the admin panel via `/admin`
   - Manage movies, users, and site content

## 📸 Screenshots

### Homepage
![Homepage](/api/placeholder/800/450)

### Movie Details
![Movie Details](/api/placeholder/800/450)

### Watchlist Management
![Watchlist](/api/placeholder/800/450)

## 🤝 Contributing

We welcome contributions to improve Netolewx! To contribute:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please ensure your code follows our coding standards and includes appropriate tests.

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

<p align="center">
  Developed with ❤️ by <a href="https://github.com/ELZAYDA">Ahmed Nabil (ELZAYDA)</a>
</p>
