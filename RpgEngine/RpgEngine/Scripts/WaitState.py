class WaitState:
    def __init__(self, character, map):
        self.Character = character
        self.Map = map
        self.Entity = character.Entity
        self.Controller = character.Controller
        self.FrameResetSpeed = 0.05
        self.FrameCount = 0


    def Enter(self, params):
        self.FrameCount = 0
        

    def Render(self, renderer):
        pass
    
    def Exit(self):
        pass

    def Update(self, dt):
        if self.FrameCount != -1:
            self.FrameCount += dt
            if self.FrameCount >= self.FrameResetSpeed:
                self.FrameCount = -1
                self.Entity.SetFrame(self.Entity.StartFrame)

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

        self.Anim = Animation(List[int]([self.Entity.StartFrame]))

    def Enter(self, params):
        moveX, moveY = params

        

        self.MoveX = moveX
        self.MoveY = moveY
        frames = None
        if self.MoveX == -1:
            frames = self.Character.AnimLeft
        elif self.MoveX == 1:
            frames = self.Character.AnimRight
        elif self.MoveY == -1:
            frames = self.Character.AnimUp
        elif self.MoveY == 1:
            frames = self.Character.AnimDown

        self.Anim.Frames = frames


        pixelPos = self.Entity.Sprite.Position
        self.PixelX = pixelPos.X
        self.PixelY = pixelPos.Y
        self.Tween = Tween(0, self.TileWidth, self.MoveSpeed)

        targetX = self.Entity.TileX + moveX
        targetY = self.Entity.TileY + moveY
        if self.Map.IsBlocked(0, targetX, targetY):
            self.MoveX = 0
            self.MoveY = 0
            self.Entity.SetFrame(self.Anim.Frame)
            self.Controller.Change("wait", None)

    def Exit(self):
        self.Entity.TileX += self.MoveX
        self.Entity.TileY += self.MoveY
        Teleport(self.Entity, self.Map)

    def Render(self, renderer):
        pass

    def Update(self, dt):
        self.Anim.Update(dt)
        self.Entity.SetFrame(self.Anim.Frame)

        self.Tween.Update(dt)

        value = self.Tween.Value
        x = self.PixelX + (value*self.MoveX)
        y = self.PixelY - (value*self.MoveY)
        self.Entity.X = x
        self.Entity.Y = y
        self.Entity.Sprite.SetPosition(x,y)

        if self.Tween.IsFinished:
            self.Controller.Change("wait", None)