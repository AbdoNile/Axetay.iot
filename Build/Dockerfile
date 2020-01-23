FROM ubuntu:16.04

RUN apt-get update && apt-get upgrade -y && apt-get install -y software-properties-common apt-transport-https ca-certificates gnupg 

RUN apt-add-repository 'deb https://apt.kitware.com/ubuntu/ bionic main'
RUN apt-get update &&  \
    apt-get install -y git wget flex bison \
    gperf python python-pip python-setuptools \ 
    python-serial python-click python-cryptography \
    python-future python-pyparsing python-pyelftools \
    ninja-build ccache libffi-dev libssl-dev && \
    apt-get install --yes --upgrade  --allow-unauthenticated cmake

RUN mkdir /esp && \
    wget https://dl.espressif.com/dl/xtensa-esp32-elf-linux64-1.22.0-80-g6c4433a-5.2.0.tar.gz -O /esp/xtensa-esp32-elf-linux64-1.22.0-80-g6c4433a-5.2.0.tar.gz

WORKDIR /esp
RUN tar -xzf xtensa-esp32-elf-linux64-1.22.0-80-g6c4433a-5.2.0.tar.gz
ENV PATH="$PATH:$HOME/esp/xtensa-esp32-elf/bin"


RUN git clone https://github.com/aws/amazon-freertos.git --recurse-submodules
WORKDIR amazon-freertos

#ADD "https://www.random.org/cgi-bin/randbyte?nbytes=10&format=h" skipcache

RUN pip install --upgrade pip 
RUN pip install --user -r vendors/espressif/esp-idf/requirements.txt
RUN cmake -DVENDOR=espressif -DBOARD=esp32_wrover_kit -DCOMPILER=xtensa-esp32 -S . -B build
