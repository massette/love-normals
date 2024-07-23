local g = love.graphics
local k = love.keyboard
local m = love.mouse

local W, H = g.getDimensions()

local im_wall = g.newImage("assets/images/wall.png")
local im_norm = g.newImage("assets/images/normal.png")

local shader = g.newShader("assets/shaders/lights.fs")
shader:send("u_normalMap", im_norm)
shader:send("u_ambientColor", { 0.5, 0.5, 0.6 })
shader:send("u_lightColor", { 1.0, 1.0, 1.0 })

local cx, cy = 0, 0
local mx, my = 0, 0
function love.update(dt)
    mx, my = g.inverseTransformPoint(m.getPosition())
    shader:send("u_lightCoords", { mx, my, 100.0 })

    if k.isDown("left") then
        cx = cx - dt * 600
    end

    if k.isDown("right") then
        cx = cx + dt * 600
    end
    
    if k.isDown("up") then
        cy = cy - dt * 600
    end

    if k.isDown("down") then
        cy = cy + dt * 600
    end
end

function love.draw()
    g.setShader(shader)
    g.translate(cx, cy)

    g.setColor(1, 1, 1)
    g.draw(im_wall, (W - 350)/2, (H - 350)/2)

    g.setShader()
    g.setColor(1, 0, 0)
    g.circle("fill", mx,my, 3)
end