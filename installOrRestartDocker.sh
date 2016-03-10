#!/bin/bash
numberOfAgileDbContainers="$(docker ps -a | grep agiledb | wc -l)"
if [ $numberOfAgileDbContainers -gt 0 ]
then
	echo "Found existing agiledb containers restarting agiledbweb"
	docker ps -a | grep agiledbweb | awk '{print $1}' | xargs -I {} docker restart {}
else
	echo "No existing agiledb docker instances creating them now using $(pwd) as build directory"
	docker build -t agiledb . 
	echo "creating and starting rabbit"
	docker run -d --hostname agiledbrabbit --name agiledbrabbit -p 8080:15672 rabbitmq:3-management
	echo "waiting for rabbit to start"
	# wait for rabbit to start cant be bothered polling the endpoints
	sleep 10s
    echo "creating and starting agiledbweb"
	docker run -d -p 9000:9000  --link agiledbrabbit --name agiledbweb agiledb
fi

