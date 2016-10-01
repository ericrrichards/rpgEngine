import math
print "loaded script"


#LoadScript("Map.py")

gTiledMap = TileMap.LoadMap("small_room.json")

gMap = Map(gTiledMap)

gMap.GotoTile(5,5)

def Update():
    Renderer.Translate( -gMap.CamX, -gMap.CamY)
    gMap.Render(Renderer)

    if IsKeyDown(Keys.Left):
        gMap.CamX -= 1
    elif IsKeyDown(Keys.Right):
        gMap.CamX += 1

    if IsKeyDown(Keys.Up):
        gMap.CamY += 1
    elif IsKeyDown(Keys.Down):
        gMap.CamY -= 1




