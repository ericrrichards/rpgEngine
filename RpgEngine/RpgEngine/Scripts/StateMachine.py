class EmptyState:
    def Render(self, renderer):
        pass
    def Update(self, dt):
        pass
    def Enter(self, params):
        pass
    def Exit(self):
        pass


class StateMachine:
    def __init__(self, states ):
        
        self.States = states or {}
        self.CurrentState = EmptyState()


    def Change(self, stateName, params):
        assert(self.States.has_key(stateName))
        self.CurrentState.Exit()
        self.CurrentState = self.States[stateName]()
        self.CurrentState.Enter(params)

    def Update(self, dt):
        self.CurrentState.Update(dt)

    def Render(self, renderer):
        self.CurrentState.Render(renderer)
