#!/bin/sh

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[0;33m'

up()
{
    echo "${GREEN}Starting $1..."
    docker compose -f $1 up -d
    echo ""
}

down()
{
    echo "${RED}Stopping $1"
    # docker compose -f $1 down --remove-orphans
    docker compose -f $1 down
}

echo "${GREEN}Starting services..."

if [ $# -eq 0 ]
then
    echo "${YELLOW}No default args provided"
    echo "${YELLOW}Only 'up' and 'down' are accepted"
    echo "${YELLOW}Default 'up' will be executed"
    echo ""
    sh ./StartCommonInfrastructure.sh
fi

SERVICES=$(find . -type f -name "*.sln")

for SERVICE in $SERVICES
do
    SERVICE_PATH=$(dirname $SERVICE)

    case $@ in 
        up)
            up $SERVICE_PATH/docker-compose.yml
            ;;
        
        down)
            down $SERVICE_PATH/docker-compose.yml
            ;;
        
        *)
            up $SERVICE_PATH/docker-compose.yml
            ;;
    esac
    
done