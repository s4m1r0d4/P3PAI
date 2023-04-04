#!/usr/bin/bash

set -xe

dotnet test
cd bin/Debug
rm *.json
rm *.xml