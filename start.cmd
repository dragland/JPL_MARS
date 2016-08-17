@ECHO OFF
REM   Jet Propulsion Laboratory
REM   Virtual Reality for Mars Rovers | Summer 2016
REM   Davy Ragland | dragland@stanford.edu
REM   Victor Ardulov | victorardulov@gmail.com
REM   Oleg Pariser | Oleg.Pariser@jpl.nasa.gov

"C:\Program Files\Unity\Editor\Unity.exe" -quit -batchmode -serial XX-XXXX-XXXX-XXXX-XXXX-XXXX -username "XXXX" -password "XXXX" -projectPath "%~dp0\JPL_MARS_VR" -executeMethod MeshToAssetBuilder.buildTiles

DEL /F /Q /S JPL_MARS_VR\Assets\Swap\*
COPY /Y JPL_MARS_VR\Assets\Bundles\* C:\wamp64\www\Bundles\