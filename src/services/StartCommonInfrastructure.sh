#!/bin/sh

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[0;33m'

up()
{
    echo "${GREEN}Starting common infra..."
    docker compose -f common-infrastructure.yml up -d
    echo ""
}

down()
{
    echo "${RED}Stopping common infra..."
    docker compose -f common-infrastructure.yml down --remove-orphans
    echo ""
}

if [ $# -eq 0 ]
then
    echo "${YELLOW}No default args provided"
    echo "${YELLOW}Only 'up' and 'down' are accepted"
    echo "${YELLOW}Default 'up' will be executed"
    echo ""
fi

case $@ in 
    up)
        up
        ;;
    
    down)
        down
        ;;
    
    *)
        up
        ;;
esac