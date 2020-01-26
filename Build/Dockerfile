FROM ubuntu:18.04

RUN apt-get update && apt-get upgrade -y && apt-get install -y software-properties-common apt-transport-https ca-certificates gnupg wget
RUN wget https://apt.kitware.com/keys/kitware-archive-latest.asc  -O kitware-archive-latest.asc && apt-key add kitware-archive-latest.asc
RUN ls
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

RUN git clone https://github.com/aws/amazon-freertos.git /amazon-freertos --recurse-submodules

RUN pip install --upgrade pip 
RUN pip install --user -r /amazon-freertos/vendors/espressif/esp-idf/requirements.txt

ADD  ./entrypoint.sh /etc/entrypoint.sh

ENTRYPOINT ["/bin/bash", "/etc/entrypoint.sh" ]