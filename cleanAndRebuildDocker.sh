echo "stopping containers:"
docker ps -a | grep agiledb | awk '{print $1}' | xargs -I {} docker stop {}
echo "removing containers:"
docker ps -a | grep agiledb | awk '{print $1}' | xargs -I {} docker rm {}

./installOrRestartDocker.sh