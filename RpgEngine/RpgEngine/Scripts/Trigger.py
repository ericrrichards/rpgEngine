class Trigger:
    def __init__(self, onEnter, onExit, onUse):
        emptyFunc = lambda: None
        self.OnEnter = onEnter or emptyFunc
        self.OnExit = onExit or emptyFunc
        self.OnUse = onUse or emptyFunc
