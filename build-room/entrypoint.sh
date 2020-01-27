#!/usr/bin/env bash
timestamp=$(date +"%Y%m%d")
echo $timestamp

cp -a /project-code/. /build-room/
cd /build-room/
cmake -DVENDOR=espressif -DCMAKE_TOOLCHAIN_FILE=amazon-freertos/tools/cmake/toolchains/xtensa-esp32.cmake -DBOARD=esp32_wrover_kit -DCOMPILER=xtensa-esp32 -GNinja -S  . -B /build-output
cd /build-output/ 
ninja