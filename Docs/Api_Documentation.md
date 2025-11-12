# Documentation for To-Do Api.

## Table of Contents

- [Overview](#overview)
- [Authentication](#authentication)
- [Endpoints](#endpoints)
    * [Users Endpoints](#users-endpoints)
    * [Tasks Endpoints](#tasks-endpoints)
- [DTOs](#dtos)
- [Exception Handling](#exception-handling)
- [Logging](#logging)
- [Models](#models)



---
## Overview {#overview}
---

This Mandatory assignment is an API for a To-Do task list. At this time the usecase for this API is purely learning. When testing you will be launched into scalar by default where you can test the different endpoints. 


---

### Technologies

- ASP.NET Core
- MySQL
- EF Core
- BCrypt
- Serilog
- Basic Authentication


---

### Project Structur

Dette er en foreløbig oversikt, stemmer ikke, men en placeholder for å teste programmet jeg brukte for å lage det.  


```  
Arbeidskrav2/
├─ Docs/
├─ src/
│  ├─ TodoApi/
│  │  ├─ Controllers/
│  │  ├─ Data/
│  │  ├─ DTOs/
│  │  ├─ Middleware/
│  │  ├─ Models/
│  │  │  ├─ User.cs
│  │  │  ├─ TodoTask.cs
│  │  ├─ Program.cs
```



---
## Authentication {#authentication}
---

This API implements **Basic Authentication** as its user verification method.  
The authentication process works by encoding the string `username:password` into **Base64**, which is then added to the `Authorization` header in the following format:
```
Authorization: Basic <base64encoded_username:password>
```
If authentication fails, the server returns a `401 Unauthorized` response, indicating that valid credentials are required to access the requested resource.  
All endpoints require authentication, **except** the endpoint responsible for creating a new user.

Once a user has been created, authentication is required for all subsequent requests.  
For operations involving user-specific data — such as managing tasks associated with their account — **authorization** is also enforced. This ensures that only the owner of a given resource can modify or access it.

If a user attempts to access or modify a task that does not belong to them, the API will return a `403 Forbidden` response, indicating that the user lacks the necessary permissions to perform the requested action.




---
## Endpoints
---

Below the endpoints used for this API is show and described using tables. It will have them sorted into a table for User Endoints and one for Task Endpoints

---

### Users Endpoints

| Method | Endpoint | Description | Auth Required | Authorization |  
|:------:|:---------|:------------|:-------------:|:-------------:|  
| POST | /api/v1/users/register | Register new user | No | No |   
| GET | /api/v1/users/me | Info about user | Yes | No | 

#### POST /api/v1/users/register
**request** `RegisterUserDto`  
**Response** `UserResponseDto` + auth string —for ease of use

**Example Request** 
```json
{ 
    "username": "bobby", 
    "password": "hemmelig!" 
}
```

**Example Response**  
**Status:** `201 Created`  
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "username": "bobby",
  "createdAt": "2025-11-04T20:45:00Z",
  "authString": "Basic Ym9iYnk6aGVtbWVsaWch"
}
```



### Tasks Endpoints

| Method | Endpoint | Description | Auth Required | Authorization |   
|:------:|:---------|:------------|:-------------:|:-------------:|   
| GET | /api/v1/tasks | All tasks listed | Yes | No | 
| POST | /api/v1/tasks/register | Register task | Yes | No | 
| PUT | /api/v1/task/{taskId} | Update task | Yes | Yes | 
| DELETE  | /api/v1/task/{taskId} | Delete task | Yes | Yes | 



---
## DTOs
---

### RegisterUserDto

| Field | Type | Required | Validation | 
|:------|:----:|:--------:|:-----------|
| Username | string | Yes | MaxLength(50) |
| Password | string | Yes | MinLength(6) |



---
## Exception Handling
---

asdasda


---
## Logging {#logging}
---

asdasdas


---
## Models {#models}
---

**User**

```C#
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)] 
    public string Username { get; set; } = String.Empty;
    
    [Required]
    public string HashedPassword { get; set; } = String.Empty;
    
    
    public DateTime CreatedAt { get; set; }
    
    public ICollection<TodoTask> Tasks { get; set; } = new HashSet<TodoTask>();
}
```

**TodoTask**
```C#
public class TodoTask
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = String.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; } 
    
    public DateTime? DueDate { get; set; }
    
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}
```
