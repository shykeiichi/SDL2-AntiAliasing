using static SDL2.SDL;
using System;

IntPtr window;
IntPtr renderer;

bool is_running = true;

SDL_WindowFlags flags = SDL_WindowFlags.SDL_WINDOW_OPENGL;

if(SDL_Init(SDL_INIT_EVERYTHING) == 0) {
    SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "2");
}

Console.WriteLine("SDL Initialized");

window = SDL_CreateWindow("Anti Aliasing", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 1280, 720, flags);

Console.WriteLine("Window Created");

renderer = SDL_CreateRenderer(window, 0, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
SDL_SetRenderDrawBlendMode(renderer, SDL_BlendMode.SDL_BLENDMODE_BLEND);

Console.WriteLine("Renderer Created");

double angle = 0;

int renderQuality = 0;

while(is_running) {

    angle += 0.01;
    
    while (SDL_PollEvent(out SDL_Event events) != 0) {
        switch(events.type) {
            case SDL_EventType.SDL_QUIT:
            is_running = false;
            break;
            case SDL_EventType.SDL_KEYDOWN:
            switch(events.key.keysym.sym) {
                case SDL_Keycode.SDLK_1:
                renderQuality = 0;
                break;
                case SDL_Keycode.SDLK_2:
                renderQuality = 1;
                break;
                case SDL_Keycode.SDLK_3:
                renderQuality = 2;
                break;
            }
            break;
        }
    }

    switch(renderQuality) {
        case 0:
            SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "0");
            break;
        case 1:
            SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "1");
            break;
        case 2:
            SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "2");
            break;
    }

    IntPtr tex = SDL_CreateTexture(renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, 500, 500);
    SDL_SetRenderTarget(renderer, tex);

    SDL_RenderClear(renderer);

    SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

    SDL_Rect rect = new SDL_Rect();
    rect.x = 50;
    rect.y = 50;
    rect.w = 400;
    rect.h = 400;

    SDL_RenderFillRect(renderer, ref rect);

    SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);

    SDL_SetRenderTarget(renderer, IntPtr.Zero);

    SDL_FRect destrect;
    destrect.x = 50;
    destrect.y = 720 / 2 - 250;
    destrect.w = 500;
    destrect.h = 500;

    SDL_RenderCopyExF(renderer, tex, IntPtr.Zero, ref destrect, angle, IntPtr.Zero, SDL_RendererFlip.SDL_FLIP_NONE);
    SDL_DestroyTexture(tex);

    SDL_SetHint(SDL_HINT_RENDER_SCALE_QUALITY, "0");

    IntPtr tex2 = SDL_CreateTexture(renderer, SDL_PIXELFORMAT_RGBA8888, (int)SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET, 500, 500);
    SDL_SetRenderTarget(renderer, tex2);

    SDL_RenderClear(renderer);

    SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

    SDL_Rect rect2 = new SDL_Rect();
    rect2.x = 50;
    rect2.y = 50;
    rect2.w = 400;
    rect2.h = 400;

    SDL_RenderFillRect(renderer, ref rect2);

    SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);

    SDL_SetRenderTarget(renderer, IntPtr.Zero);

    SDL_FRect destrect2;
    destrect2.x = 1280 - 550;
    destrect2.y = 720 / 2 - 250;
    destrect2.w = 500;
    destrect2.h = 500;

    SDL_RenderCopyExF(renderer, tex2, IntPtr.Zero, ref destrect2, angle, IntPtr.Zero, SDL_RendererFlip.SDL_FLIP_NONE);
    SDL_DestroyTexture(tex2);

    SDL_RenderPresent(renderer);
}