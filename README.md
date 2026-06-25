# Student Management System (.NET MAUI)

An android application built with **.NET MAUI** to manage student information. The project demonstrates a clean separation of concerns using the **MVVM (Model-View-ViewModel)** architectural pattern.

## Features
- **Student Management**: Add, edit, and delete student records.
- **Data Persistence**: Uses **SQLite** for local storage.
- **MVVM Architecture**: 
  - **Models**: Simple POCO classes representing the Student entity.
  - **ViewModels**: Handles business logic, data validation, and state management using `INotifyPropertyChanged`.
  - **Views**: XAML-based UI with data binding to ViewModels.
- **Dependency Injection**: Utilizes .NET's built-in DI container for managing services and repositories.

## Tech Stack
- **Framework**: .NET MAUI (net8.0-android)
- **Database**: SQLite (via `sqlite-net-pcl`)
- **Architecture**: MVVM, Dependency Injection
- **Development Environment**: VS Code / Ubuntu

## Key Learnings
- Implementation of `ICommand` and `ObservableCollection` for responsive UI updates.
- Decoupling UI logic (Code-Behind) from business logic (ViewModels).
- Handling form validation and database operations asynchronously (`async/await`).

---

## How to Run
1. Ensure you have the **.NET 8 SDK** and **.NET MAUI workload** installed.
2. Clone the repository: `git clone <repository-url>`
3. Build the project:
   ```bash
   dotnet build -f net8.0-android
