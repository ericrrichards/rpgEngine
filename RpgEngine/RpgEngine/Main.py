import RpgEngine
import math
print "loaded script"


LoadScript("Map.py")



gTiledMap = LoadMap("larger_map.json")

gMap = Map(gTiledMap)

def Update():
    gMap.Render(Renderer)

