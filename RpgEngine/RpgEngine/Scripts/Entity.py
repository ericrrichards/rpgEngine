class EntityDef:
    def __init__(self, texture, width, height, startFrame, tileX, tileY, layer):
        self.Texture = texture
        self.Width = width
        self.Height = height
        self.StartFrame = startFrame
        self.TileX = tileX
        self.TileY = tileY
        self.Layer = layer

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
        self.Layer = entityDef.Layer

    def SetFrame(self, frame):
        self.Sprite.UVs = self.UVs[frame]