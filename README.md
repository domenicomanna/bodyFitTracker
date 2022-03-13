Body Fitness Tracker
======
A web application that allows you to track your body measurements and body fat percentage. The core focus of this app is to help the user achieve his/her fitness goals.

Installation
------

#### Prerequisites
 * [.Net Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
 * [MySql Server](https://dev.mysql.com/downloads/mysql/)
 * [Node.js](https://nodejs.org/en/)

#### 1. Clone the project

#### 2. Create a mysql user that has all permissions

#### 3. Set up app secrets for the API
 The app secrets are that database connection, the secret key to use for JWT, and email credentials. 
 1. Go to the Api folder
 ```
 cd server/Api
 ```
 2. Run the following commands
 ```
 dotnet user-secrets set 'TokenKey' 'superSecretKeyGoesHere'
 dotnet user-secrets set 'DbConnection' 'Server=localhost; Database=bodyFitTracker; Uid=yourUsername; Password=yourPassword'
 dotnet user-secrets set 'EmailSettings:Username' 'yourEmailAddress'
 dotnet user-secrets set 'EmailSettings:Password' 'yourPassword' 
 ```
 3. Verify the secrets were created by running:
 ```
 dotnet user-secrets list
 ```

 **Important:** If you are using Gmail for the email account, then you must [enable less secure app access](https://support.google.com/accounts/answer/6010255).


#### 4. Run the api
```
cd server/Api
dotnet watch run
```
 The api should be listening on localhost:5000

#### 5. Run the client app
```
cd clientApp
npm install
npm start
```


