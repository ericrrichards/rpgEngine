def TeleportEntity(entity, map):
    pos = map.GetTileFoot(entity.TileX, entity.TileY)
    entity.Sprite.SetPosition(pos.X, pos.Y + entity.Height / 2)