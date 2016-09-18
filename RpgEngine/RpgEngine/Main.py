import RpgEngine
print "loaded script"

gLeft = -Renderer.ScreenWidth / 2
gTop = Renderer.ScreenHeight / 2

gGrassTexture = Texture.Find("grass_tile.png")
gTileHeight = gGrassTexture.Height
gTileWidth = gGrassTexture.Width

gTileSprite = Sprite.Create()
gTileSprite.Texture = gGrassTexture
gTileSprite.SetPosition(gLeft + gTileWidth/2, gTop - gTileHeight/2)

def Update():
	Renderer.DrawSprite(gTileSprite)

