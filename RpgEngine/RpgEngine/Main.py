import math
print "loaded script"

LoadScript("Map.py")



gTiledMap = LoadMap("larger_map.json")

gMap = Map(gTiledMap)

def Update():
    Renderer.Translate( -gMap.CamX, gMap.CamY)
    gMap.Render(Renderer)

    if IsKeyDown(Keys.Left):
        gMap.CamX -= 1
    elif IsKeyDown(Keys.Right):
        gMap.CamX += 1

    if IsKeyDown(Keys.Up):
        gMap.CamY += 1
    elif IsKeyDown(Keys.Down):
        gMap.CamY -= 1

    if IsKeyDown(Keys.Space):
        gMap.GotoTile(10,10)



