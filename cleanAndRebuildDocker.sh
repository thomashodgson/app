
configuration=${1:-Debug}
echo "stopping containers:"
docker ps -a | grep app_ | awk '{print $1}' | xargs -I {} docker stop {}
echo "removing containers:"
docker ps -a | grep app_ | awk '{print $1}' | xargs -I {} docker rm {}

./installOrRestartDocker.sh $configuration
