#!/usr/bin/env bash
timestamp=$(date +"%Y%m%d")
echo $timestamp

cd /amazon-freertos
cmake -DVENDOR=espressif -DBOARD=esp32_wrover_kit -DCOMPILER=xtensa-esp32 -GNinja -S  . -B /build-output
cd /build-output 
ninja