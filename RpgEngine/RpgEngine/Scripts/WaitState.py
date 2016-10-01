class WaitState:
    def __init__(self, character, map):
        self.Character = character
        self.Map = map
        self.Entity = character.Entity
        self.Controller = character.Controller

    def Enter(self, params):
        self.Entity.SetFrame(self.Entity.StartFrame)

    def Render(self, renderer):
        pass
    
    def Exit(self):
        pass

    def Update(self, dt):
        if IsKeyDown(Keys.Left):
            self.Controller.Change("move", (-1,0))
        elif IsKeyDown(Keys.Right):
            self.Controller.Change("move", (1,0))
        elif IsKeyDown(Keys.Up):
            self.Controller.Change("move", (0,-1))
        elif IsKeyDown(Keys.Down):
            self.Controller.Change("move", (0, 1))


class MoveState:
    def __init__(self, character, map):
        self.Character = character
        self.Map = map
        self.TileWidth = map.TileWidth
        self.Entity = character.Entity
        self.Controller = character.Controller
        self.MoveX = 0
        self.MoveY = 0
        self.Tween = Tween(0,0,1)
        self.MoveSpeed = 0.3

    def Enter(self, params):
        self.MoveX, self.MoveY = params
        pixelPos = self.Entity.Sprite.Position
        self.PixelX = pixelPos.X
        self.PixelY = pixelPos.Y
        self.Tween = Tween(0, self.TileWidth, self.MoveSpeed)

    def Exit(self):
        self.Entity.TileX += self.MoveX
        self.Entity.TileY += self.MoveY
        Teleport(self.Entity, self.Map)

    def Render(self, renderer):
        pass

    def Update(self, dt):
        self.Tween.Update(dt)

        value = self.Tween.Value
        x = self.PixelX + (value*self.MoveX)
        y = self.PixelY - (value*self.MoveY)
        self.Entity.X = x
        self.Entity.Y = y
        self.Entity.Sprite.SetPosition(x,y)

        if self.Tween.IsFinished:
            self.Controller.Change("wait", None)