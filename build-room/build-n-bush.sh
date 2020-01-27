#!/usr/bin/env bash

build_number="$1"
docker_image_name="axetay-buildroom-amazonfreertos-esp32"
repositoryname="101584550521.dkr.ecr.us-east-1.amazonaws.com/amazonfreertos-esp32-builder"

if [ -z "$build_number" ]
then
    echo "please pass a build number as parameter. Terminating..";
    exit ;
fi

aws ecr get-login --no-include-email --region us-east-1 | sh

docker build . -t $docker_image_name:$build_number 

docker tag $docker_image_name:$build_number $repositoryname:$build_number

#docker push $repositoryname:$build_number