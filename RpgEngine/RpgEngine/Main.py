import RpgEngine
import math
print "loaded script"

gTextureAtlas = Texture.Find("atlas.png")
gMap = [
    0,0,0,0,4,5,6,0,
    0,0,0,0,4,5,6,0,
    0,0,0,0,4,5,6,0,
    2,2,2,2,10,5,6,0,
    8,8,8,8,8,8,9,0,
    0,0,0,0,0,0,0,0,
    0,0,0,0,0,0,1,2
]
gMapWidth = 8
gMapHeight = 7

def GetTile(map, rowSize, x, y):
    return map[x + y * rowSize]

topLeftTile = GetTile(gMap, gMapWidth, 0,0)
bottomRightTile = GetTile(gMap, gMapWidth, gMapWidth-1, gMapHeight-1)


gTileHeight = 32
gTileWidth = 32

gDisplayWidth = Renderer.ScreenWidth
gDisplayHeight = Renderer.ScreenHeight

gTop = gDisplayHeight / 2 - gTileHeight/2
gLeft = -gDisplayWidth / 2 + gTileWidth/2


gTileSprite = Sprite.Create()
gTileSprite.Texture = gTextureAtlas

gUVs = gTextureAtlas.GenerateUVs(gTileWidth)

def Update():
    for j in range(0, gMapHeight):
        for i in range(0, gMapWidth):
            tile = GetTile(gMap, gMapWidth, i, j)
            uvs = gUVs[tile];
            
            gTileSprite.UVs = uvs;
            gTileSprite.SetPosition(gLeft + i * gTileWidth, gTop - j * gTileHeight)
            Renderer.DrawSprite(gTileSprite)

