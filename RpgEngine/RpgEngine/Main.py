import RpgEngine
import math
print "loaded script"

gTiledMap = LoadMap("larger_map.json")

gTextureAtlas = Texture.Find(gTiledMap.TileSets[0].Image)

gUVs = gTextureAtlas.GenerateUVs(gTiledMap.TileSets[0].TileWidth, gTiledMap.TileSets[0].TileHeight)

gMap = gTiledMap.Layers[0]
gMapWidth = gMap.Width
gMapHeight = gMap.Height
gTileHeight = gTiledMap.TileSets[0].TileWidth
gTileWidth = gTiledMap.TileSets[0].TileHeight
gTiles = gMap.Data

gDisplayWidth = Renderer.ScreenWidth
gDisplayHeight = Renderer.ScreenHeight

gTop = gDisplayHeight / 2 - gTileHeight/2
gLeft = -gDisplayWidth / 2 + gTileWidth/2

def GetTile(map, rowSize, x, y):
    return map[x + y * rowSize]-1


gTileSprite = Sprite.Create()
gTileSprite.Texture = gTextureAtlas



def Update():
    for j in range(0, gMapHeight):
        for i in range(0, gMapWidth):
            tile = GetTile(gTiles, gMapWidth, i, j)
            uvs = gUVs[tile]

            
            gTileSprite.UVs = uvs;
            gTileSprite.SetPosition(gLeft + i * gTileWidth, gTop - j * gTileHeight)
            Renderer.DrawSprite(gTileSprite)

