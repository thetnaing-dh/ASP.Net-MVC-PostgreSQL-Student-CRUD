# Student CRUD System - ASP.NET MVC with PostgreSQL
## 📝 Project Overview
A complete Student Management System built with ASP.NET MVC and PostgreSQL, featuring CRUD operations (Create, Read, Update, Delete) with a clean architecture approach.

![alt](https://github.com/thetnaing-dh/ASP.Net-MVC-PostgreSQL-Student-CRUD/blob/5090e945b625df9049f84a7cdc5ff3ccd131ba95/StudentCRUD.png)
## ✨ Features
* ✅ Full CRUD operations for Student entities
* 🏗️ Repository pattern implementation
* 📊 Bootstrap-styled tables and forms
* 🔔 TempData alert messages with auto-dismiss
* 🧩 Clean architecture with separation of concerns
* 🐘 PostgreSQL database integration
  
## 🛠️ Technology Stack
* **Backend:** ASP.NET MVC
* **Database:** PostgreSQL
* **ORM:** Entity Framework Core
* **Frontend:** Bootstrap 5
* **JavaScript:** For alert timeout functionality

## 🗃️ Database Schema
    CREATE TABLE Students (
        Id SERIAL PRIMARY KEY,
        Name VARCHAR(100) NOT NULL,
        Phone VARCHAR(20),
        Address TEXT,
        Email VARCHAR(100)
    );

## 🚀 Getting Started
### Prerequisites
* .NET 6 SDK or later
* PostgreSQL 12+
* IDE Visual Studio 2022

### Installation
1. Clone the repository:
2. Configure the database connection in appsettings.json:

        "ConnectionStrings": {"DefaultConnection": "Server=localhost;Port=5432;Database=StudentDB;User Id=postgres;Password=yourpassword;"}
3. Apply database migrations:
add-migration initial
4. Run the application:

## 🎨 UI Components
### Alert Messages
Temporary alert messages displayed after CRUD operations with auto-dismiss: javaScript

    setTimeout(function() {
        $('.alert').alert('close');
    }, 5000);
    
## Student Table
Bootstrap-styled table with action buttons: html

    <div class="shadow p-3 mb-5 bg-white rounded">
       <table class="table table-striped table-hover table-responsive">
           <thead>
               <tr>
                   <th>Id</th>
                   <th>Student Name</th>
                   <th>Phone</th>
                   <th>Address</th>
                   <th>Edit</th>
                   <th>Action</th>
               </tr>
           </thead>
           <tbody>
               @if(Model != null && Model.Any())
               {
                   @foreach(var student in Model)
                   {
                       <tr>
                           <td>@student.Id</td>
                           <td>@student.Name</td>
                           <td>@student.Phone</td>
                           <td>@student.Address</td>
                           <td>@student.Email</td>
                           <td>
                               <a asp-action="Edit" asp-route-id="@student.Id" class="btn btn-warning">Edit</a>
                               <a asp-action="Details" asp-route-id="@student.Id" class="btn btn-info">Detials</a>
                               <a asp-action="Delete" asp-route-id="@student.Id" class="btn btn-danger">Delete</a>
                           </td>
                       </tr>
                   }
               }
               else
               {
                   <tr>
                       <td colspan="6">
                           There is no student yet.
                       </td>
                   </tr>
               }
           </tbody>
       </table>
    </div>
## 🧩 Repository Pattern Implementation
### Interface

    public interface IStudentRepository
    {
       Task<IEnumerable<Student>> GetStudents();
       Task<Student> GetStudentById(int id);
       Task Add(Student student);
       Task Update(Student student);   
       Task Delete(int id);
    }

## Implementation

    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _appDbContext;
    
        public StudentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    
        public async Task Add(Student student)
        {
            _appDbContext.Students.Add(student);
            await _appDbContext.SaveChangesAsync();
        }
    
        public async Task Delete(int id)
        {
            var student = await _appDbContext.Students.FindAsync(id);
            if(student != null)
            {
                _appDbContext.Remove(student);
                await _appDbContext.SaveChangesAsync();
            }
        }
    
        public async Task<Student> GetStudentById(int id)
        {
           return await _appDbContext.Students.FindAsync(id);
        }
    
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _appDbContext.Students.ToListAsync();
        }
    
        public async Task Update(Student student)
        {
            _appDbContext.Entry(student).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();    
        }
    }

## 📧 Contact
Thet Naing Oo : thetnaing94@gmail.com <br/>
Project Link: https://github.com/thetnaing-dh/ASP.Net-MVC-PostgreSQL-Student-CRUD
