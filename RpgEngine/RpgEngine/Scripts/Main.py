import math
print "loaded script"




#LoadScript("Map.py")
LoadScript("Entity.py")

gTiledMap = TileMap.LoadMap("small_room.json")

gMap = Map(gTiledMap)

gMap.GotoTile(5,5)

heroDef = EntityDef("walk_cycle.png", 16, 24, 8, 10, 2)
gHero = Entity(heroDef)


def Teleport(entity, map):
    pos = map.GetTileFoot(entity.TileX, entity.TileY)
    entity.Sprite.SetPosition(pos.X, pos.Y + entity.Height/2)


Teleport(gHero, gMap)



def Update():
    Renderer.Translate( -gMap.CamX, -gMap.CamY)
    gMap.Render(Renderer)
    Renderer.DrawSprite(gHero.Sprite)

    if IsKeyDown(Keys.Left):
        gHero.TileX -= 1
        Teleport(gHero, gMap)
    elif IsKeyDown(Keys.Right):
        gHero.TileX += 1
        Teleport(gHero, gMap)

    if IsKeyDown(Keys.Up):
        gHero.TileY -= 1
        Teleport(gHero, gMap)
    elif IsKeyDown(Keys.Down):
        gHero.TileY += 1
        Teleport(gHero, gMap)




