# AngularNetCoreVetApp

## Overview
This is a sample project that integrates an Angular front-end with a .NET Core back-end. It serves as a demonstration of managing and displaying mock veterinary appointment data.

## Prerequisites
- Node.js (version 18 or later)
- Angular CLI (version 15 or later)
- .NET SDK (version 8.0 or later)
- Git

## Setup Instructions
1. Clone the repository.
   
   ```bash
   git clone https://github.com/<your-username>/AngularNetCoreVetApp.git
   cd AngularNetCoreVetApp
   ```

2. Install dependencies.

   For Angular:
   ```bash
   cd AngularVetApp
   npm install
   ```
   
   For .NET Core: Restore NuGet packages automatically when building the solution in Visual Studio or using:
   
   ```bash
   dotnet restore
   ```
   
4. Run the project.

   Start the back-end API.

   ```bash
   cd NetVet.API
   dotnet run
   ```

   Start the Angular front-end.
   ```bash
   cd AngularVetApp
   ng serve
   ```
   
5. Open your browser and navigate to:
   
   API: http://localhost:5058/swagger (for API testing)
   
   Front-end: http://localhost:4200

## Features
* Displays mock veterinary appointment data.
* Supports filtering appointments by date and pet name.
* Demonstrates dependency injection, component-based architecture, and API integration.
