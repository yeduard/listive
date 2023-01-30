# LISTIVE
Simple TO-DO list application using events and microservices.

## Run the application
Execute the following `.sh` files under `src/services`

### StartCommonInfrastructure.sh
Starts the common infrastructure images like SQL Server and RabbitMQ

### StartServices.sh
Starts all the services

| Name | Port |
| :---: | :---: |
| Login | 5001 |
| Notification | 5002 |

## Test the application
Load the `listive_postman_collection.json` file into your postman.