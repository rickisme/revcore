#!/bin/sh
xbuild KopiLua.Net40.sln /p:Configuration=Release
cd tests/
nunit-console KopiLua.Tests.dll
