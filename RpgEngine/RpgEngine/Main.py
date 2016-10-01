import math
print "loaded script"

class EntityDef:
    def __init__(self, texture, width, height, startFrame, tileX, tileY):
        self.Texture = texture
        self.Width = width
        self.Height = height
        self.StartFrame = startFrame
        self.TileX = tileX
        self.TileY = tileY

class Entity:
    def __init__(self, entityDef):
        self.Sprite = Sprite.Create()
        self.Texture = Texture.Find(entityDef.Texture)
        self.Height = entityDef.Height
        self.Width = entityDef.Width
        self.TileX = entityDef.TileX
        self.TileY = entityDef.TileY
        self.StartFrame = entityDef.StartFrame

        self.Sprite.Texture = self.Texture
        self.UVs = self.Texture.GenerateUVs(self.Width, self.Height)
        self.SetFrame(self.StartFrame)

    def SetFrame(self, frame):
        self.Sprite.UVs = self.UVs[frame]


#LoadScript("Map.py")

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




