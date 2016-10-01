﻿import math
print "loaded script"




#LoadScript("Map.py")
LoadScript("Entity.py")
LoadScript("StateMachine.py")
LoadScript("WaitState.py")

gTiledMap = TileMap.LoadMap("small_room.json")

gMap = Map(gTiledMap)

gMap.GotoTile(5,5)

class Character:
    def __init__(self, entity):
        self.Entity = entity
        self.Controller =  StateMachine({
            "wait": lambda: self.WaitState,
            "move": lambda: self.MoveState
        })
        self.WaitState = WaitState(self, gMap)
        self.MoveState = MoveState(self, gMap)
        self.Controller.Change("wait", None)




heroDef = EntityDef("walk_cycle.png", 16, 24, 8, 10, 2)


gHero = Character(Entity(heroDef))


def Teleport(entity, map):
    pos = map.GetTileFoot(entity.TileX, entity.TileY)
    entity.Sprite.SetPosition(pos.X, pos.Y + entity.Height/2)


Teleport(gHero.Entity, gMap)


def Update():
    dt = GetDeltaTime()

    Renderer.Translate( -gMap.CamX, -gMap.CamY)
    gMap.Render(Renderer)
    Renderer.DrawSprite(gHero.Entity.Sprite)

    gHero.Controller.Update(dt)



