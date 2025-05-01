#Task Bloom – To-Do List Application

**Task Bloom** is a lightweight task management application built using **C#**, **ASP.NET Core**, and **Blazor WebAssembly**.  
It allows users to create, view, update, delete, and filter tasks efficiently.

---

##  Features

- Add new tasks with **Title**, **Description**, **Due Date**, and **Priority**
- Edit or delete existing tasks
- Mark tasks as complete
- Filter tasks by **All**, **Completed**, or **Pending**

---

##  How to Run This Project

### 1. Clone the Repository

```bash
git clone https://github.com/Sfiso0808/Task-Bloom.git
```

### 2. Navigate to the Project Folder

```bash
cd "Task Bloom"
```

### 3. Open in Visual Studio

- Open `TaskBloom.sln` in **Visual Studio 2022** or later.

### 4. Run the Backend API

- Set `TaskBloomAPI` as the **startup project**
- Run the project (typically starts on `https://localhost:5001`)

### 5. Run the Frontend Blazor Client

- Set `TaskBloomingClient` as the **startup project**
- Run it to launch the Blazor WebAssembly UI

---

## Deployment (Azure or Other)

### Backend API (e.g. Azure App Service)

1. Right-click `TaskBloomAPI` → **Publish** → **Azure** → **App Service**
2. Select your Azure subscription and deploy the API.

### Frontend (e.g. Netlify or Azure Static Web Apps)

1. Publish `TaskBloomingClient` to a static hosting service like Netlify or Azure Static Web Apps.

### Database (Railway or Azure Database for MySQL)

1. Use Railway or Azure MySQL for production database hosting.
2. Update the connection string in `appsettings.json` or environment variables.

---

## Testing

Unit tests are written using **xUnit** and located in:

- `TaskBloomAPI.Tests/`
- `TaskBloomingClient/TaskBloomAPI.Tests/`

### Run tests via command line:
```bash
cd TaskBloomAPI.Tests
dotnet test
```

Or use **Test Explorer** in Visual Studio.

---

## Project Structure

| Folder / File             | Description                      |
|---------------------------|----------------------------------|
| `TaskBloomAPI/`           | ASP.NET Core Web API             |
| `TaskBloomingClient/`     | Blazor WebAssembly frontend      |
| `TaskBloomAPI.Tests/`     | API unit tests (xUnit)           |
| `TaskBloom.sln`           | Solution file                    |

---

## Live Deployment

| Layer     | Platform | URL                                     |
|-----------|----------|------------------------------------------|
| **Backend**  | Render   | [https://taskbloomapi.onrender.com](https://taskbloomapi.onrender.com) |
| **Frontend** | Netlify  | `https://kaleidoscopic-lily-3c4c9b.netlify.app/` |
| **Database** | Railway  | `https://railway.app/project/...`        |
