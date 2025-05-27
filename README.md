### How do I get set up? ###
This is a Docker project, please make sure you have Docker installed on your machine and boot things up with the commands below.

#### Installing Docker ####
Instructions for installing Docker can be found here: https://docs.docker.com/get-started/

#### Running the Project ####
The docker-compose.yml file needed to boot everything up is found in the `client-dashboard/` directory of this repository. To boot, simply navigate to this directory in your terminal and execute the following command

`docker-compose -f docker-compose.yml up`

The first time you run this, this will build your Containers and boot everything up.

#### Some Helpful Tips ####
* After making backend code changes, make sure you run `docker-compose -f docker-compose.yml build` to rebuild and then `docker-compose -f docker-compose.yml up` again in order to see your updates. Hot reloading should be implemented on the frontend, so you won't need to do this when updating your Vue code.
* Your database and user will be created on your Container's initialization, and won't be recreated after this, no matter how many times the Containner is upped. If you need to blow away your Docker container and do a fresh installation (e.g. when you add or modify SQL scripts for your tables), you can run the following commands
	1. `docker-compose -f docker-compose.yml down`
	2. `docker rm -f $(docker ps -a -q)`
	3. `docker volume rm $(docker volume ls -q)` (Note: this will blow away any data in your DB)
	4. `docker-compose -f docker-compose.yml up`
* A helpful .NET tool called Swagger is installed in the backend project. This will let you view and test your API in the browser without a dedicated frontend. To access this tool, navigate to the following URL once your Containers are running: <http://localhost:5000/swagger/index.html>

### What this project contains ###

#### Database ####
* A table to store Client records
* A table to store Phone numbers
* Clients can come with any number of phone numbers

#### API ####
The following endpoints are created

* GetClient - Returns a single client record, given some identifier
* GetAllClients - Returns all client records from the database
* CreateClient - Creates a new client record and stores it in database
* UpdateClient - Updates information on a client record
* ArchiveClient - Archives a client record without deleting it

* GetPhone - Returns a single phone record, given some identifier
* GetAllPhones - Returns all phone numbers from a given client identifier
* CreatePhone - Creates a new phone number that is then tied to a Client
* UpdatePhone - Updates information on a client record
* DeletePhone - Deletes a phone record from a client

#### UI ####
The following pages are created

* Client Dashboard
	* Create New button is present. Clicking this button will display a form that will allow the user to create a new client.
	* Displays all unarchived clients
	* Each existing client can be clicked on, which will route the user to a page in which client data can be updated
* Client Management Page
	* Client name and phone number information is editable on this page
	* A save button will persist the updates and return you back to the dashboard
	* There is also an archive button in this page that will archive the Client record and return you to the dashboard
