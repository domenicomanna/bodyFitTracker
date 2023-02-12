# Body Fitness Tracker

A web application that allows you to track your body measurements and body fat percentage. The core focus of this app is to help the user achieve his/her fitness goals.

## Installation / Running locally

#### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Postgres](https://www.postgresql.org/download/)
- [Node.js](https://nodejs.org/en/)

### Running the Server

1. Clone the project: `git clone https://github.com/domenicomanna/bodyFitTracker.git`

2. Configure Postgres

   1. On your Postgres server, switch to the postgres user: `sudo -i -u postgres`
   2. Switch to the sql client: `psql`
   3. Create a database user: `CREATE USER "bodyFitnessTrackerDBUser" WITH password 'yourPassword';`
   4. Create a database to store the application data: `CREATE DATABASE "bodyFitnessTrackerDB" OWNER "bodyFitnessTrackerDBUser";`
   5. Finally, exit the database client: `exit;`

3. Create a `.env` file in the `server` folder and fill it in with the variable names present in `server/.env.keep`

   **Important:** If you are using Gmail for the account to send emails from, then you must [create an app password](https://support.google.com/accounts/answer/185833?hl=en), and use that for the email password value in the `.env` file

4. Run the api
   ```
   cd server/src/Api
   dotnet watch run
   ```
   The api should be listening on https://localhost:5000/

### Running the Client App

1. First, follow the instructions for running the server

2. `cd` to the correct directory: `cd clientApp`

3. Install dependencies: `npm install`

4. Start the app: `npm start`

5. The client app should be listening on http://localhost:3000/
