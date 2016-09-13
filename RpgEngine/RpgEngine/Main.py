import RpgEngine
print "loaded script"


Renderer.AlignText("center", "bottom" )

def Update():
	Renderer.AlignText("center", "bottom" )
	Renderer.DrawText2D(0,0, "Hello World bottom")
	Renderer.AlignText("center", "center" )
	Renderer.DrawText2D(0,0, "Hello World center")
	Renderer.AlignText("center", "top" )
	Renderer.DrawText2D(0,0, "Hello World top")

