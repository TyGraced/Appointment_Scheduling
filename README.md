🚀 Getting Started
Let’s get you up and running in just a few steps!

1️⃣ Clone the Repository
First, grab the project code:

bash

git clone [<repository-url>](https://github.com/TyGraced/Appointment_Scheduling.git)
cd Appointment_Scheduling


2️⃣ Restore Dependencies
Install the necessary libraries:

bash
dotnet restore

3️⃣ Configure the Database
Open the appsettings.json file in the Appointment_Scheduling.API folder.
Replace the placeholder connection string with your own database details.
Apply migrations to set up the database schema:

bash
cd Appointment_Scheduling.Infrastructure
dotnet ef database update

4️⃣ Build the Solution
Ensure everything is compiled and ready to go:

bash
dotnet build

5️⃣ Run the Application
Fire up the API server:

bash
cd Appointment_Scheduling.API
dotnet run
🎉 That’s it! The app will be running locally. Visit http://localhost:5000 (or the port you’ve configured) to start exploring.

🧰 Useful Tips
Testing the App: Run all tests to ensure everything works as expected:

bash
dotnet test

Documentation: The Swagger UI is a well detailed documentation for the API.
💡 Troubleshooting
Stuck on something? Here are a few common issues and their solutions:

Migrations not working? Double-check your database connection string in appsettings.json.
Server not starting? Ensure your .NET SDK version meets the minimum requirements.
If you’re still having trouble, feel free to open an issue or reach out to me.

✨ Final Notes
Thanks for checking out the Appointment Scheduling System! I hope it helps make managing appointments a breeze. If you have any feedback or suggestions, let me know - I’d love to hear from you!
