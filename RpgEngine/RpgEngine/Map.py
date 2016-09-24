class Map:
    def __init__(self, mapDef):
        layer = mapDef.Layers[0]
        
        self.CamX = 0
        self.CamY = 0

        self.MapDef = mapDef
        self.TextureAtlas = Texture.Find(gTiledMap.TileSets[0].Image)

        self.TileSprite = Sprite.Create()
        self.TileSprite.Texture = self.TextureAtlas

        self.Layer = layer
        self.Width = layer.Width
        self.Height = layer.Height

        self.Tiles = layer.Data
        self.TileWidth = mapDef.TileSets[0].TileWidth
        self.TileHeight = mapDef.TileSets[0].TileHeight

        self.X = -Renderer.ScreenWidth / 2 + self.TileWidth / 2
        self.Y = Renderer.ScreenHeight / 2 - self.TileHeight / 2

        self.WidthPixel = self.Width * self.TileWidth
        self.HeightPixel = self.Height * self.TileHeight

        self.UVs = self.TextureAtlas.GenerateUVs(self.TileWidth, self.TileHeight)


    def PointToTile(self, x, y):
        x += self.TileWidth / 2
        y -= self.TileHeight / 2
    
        x = max(self.X, x)
        y = min(self.Y, y)
        x = min(self.X + self.WidthPixel - 1, x)
        y = max(self.Y - self.HeightPixel + 1, y)

        tileX = math.floor((x - self.X) / self.TileWidth)
        tileY = math.floor((self.Y - y) / self.TileHeight)

        return int(tileX), int(tileY)


    def GetTile(self, x, y):
        return self.Tiles[x + y * self.Width] - 1

    def Render(self, renderer):
        tileLeft, tileBottom = self.PointToTile(self.CamX - renderer.ScreenWidth / 2, self.CamY - renderer.ScreenHeight / 2)
        tileRight, tileTop = self.PointToTile(self.CamX + renderer.ScreenWidth /2, self.CamY + renderer.ScreenWidth / 2)

        for j in range(tileTop, tileBottom):
            for i in range(tileLeft, tileRight):
                tile = self.GetTile(i,j)
                uvs = self.UVs[tile]

                self.TileSprite.UVs = uvs
                self.TileSprite.SetPosition(self.X + i * self.TileWidth, self.Y - j * self.TileHeight)
                renderer.DrawSprite(self.TileSprite)