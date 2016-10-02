class Actions:
    @staticmethod
    def Teleport(map, tileX, tileY):
        def teleport(trigger, entity):
            entity.TileX = tileX
            entity.TileY = tileY
            TeleportEntity(entity, map)
        return teleport
