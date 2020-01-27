docker-compose -f ./docker-compose.debug.yml -f ./docker-compose.yml down 
docker-compose -f ./docker-compose.debug.yml -f ./docker-compose.yml up -d
container_id=$(docker ps -a -q -l)
echo $container_id
winpty docker exec -it $container_id  "/bin/bash" 