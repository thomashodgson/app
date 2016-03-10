#!/bin/bash
cd "$(dirname "$0")"

numberOfContainers="$(docker ps -a | grep app_ | wc -l)"
if [ $numberOfContainers -gt 0 ]
then
	echo "Found existing  containers restarting webb"
	docker ps -a | grep app_web | awk '{print $1}' | xargs -I {} docker restart {}
	echo "Found existing  containers restarting worker"
	docker ps -a | grep app_worker | awk '{print $1}' | xargs -I {} docker restart {}
else
	echo "No existing docker instances creating them now using"
	echo "creating and starting rabbit"
	docker run -d --hostname app_rabbit --name app_rabbit -p 8080:15672 rabbitmq:3-management

	echo "waiting for rabbit to start"
	# wait for rabbit to start cant be bothered polling the endpoints
	sleep 10s

	echo "creating app_web"
	docker build -t app_web source/webserver/bin/${1:-Debug}
    echo "starting app_web"
	docker run -d -p 9000:9000  --link app_rabbit --name app_web app_web
	

	echo "creating app_worker"
	docker build -t app_worker source/worker/bin/${1:-Debug}
    echo "starting app_worker"
	docker run -d --link app_rabbit --name app_worker app_worker
fi

