# ToDoApp

## Table of Contents
- [Motivation](#motivation)
- [Getting Started](#getting-started)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
    - [How to Run the Application](#how-to-run-the-application)
    - [How to Run the Tests](#how-to-run-the-tests)
- [Features](#features)
- [Notes](#notes)

## Motivation
This project was created as a working week challenge as part of an interview proces, the application is a classic ToDo application with 2 parts, Bootstrap Angular front-end and C# dotnet Entity Core back-end. This part is to demonstrate my competence with Web API development and unit-testing.

## Getting Started
You will need:
1. PostgreSQL
2. C# (Dotnet Core Entity)

### Installation
- Install PostgreSQL from [postgresql.org](https://www.postgresql.org/download/) and set up the database I reccomend todoappcomland

### Running the Application
Apply the migrations already given:
1. `cd ToDoApp-BE`
2. Run `dotnet restore`
3. Add appsetting.json in your root folder and add `{
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "ConnectionStrings": {
      "WebApiDatabase": "Host=localhost; Database=; Username=; Password=;"
    },
    "AllowedHosts": "*"
  }`
4. Run `dotnet run`

#### How to Run the Application
Explain how users can run your application, including any configuration or command-line instructions. Provide examples if possible.

#### How to Run the Tests
Describe how to run tests for your project. Include any testing frameworks or libraries that users need to install.

## Features
List and briefly describe the key features of your project.

## Notes
Due to some misunderstanding in the instructions I decided too make 2 projects, to-do-app-with-tests for demonstrating my unit testing and todo-app for demonstrating my skill using rawSQL querying 