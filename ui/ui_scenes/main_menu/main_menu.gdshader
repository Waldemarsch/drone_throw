shader_type canvas_item;

// Переменные, которые можно будет настраивать в инспекторе
uniform float shine_progress : hint_range(-1.0, 2.0) = -0.5; // Позиция блика
uniform float shine_width : hint_range(0.0, 1.0) = 0.1;     // Ширина блика
uniform float shine_softness : hint_range(0.01, 1.0) = 0.05; // Мягкость краев
uniform vec4 shine_color : source_color = vec4(1.0, 1.0, 1.0, 0.8); // Цвет блика

void fragment() {
    // Берем оригинальный цвет пикселя из текстуры логотипа
    vec4 base_color = texture(TEXTURE, UV);
    
    // Вычисляем, где находится световая полоса
    float band = smoothstep(shine_progress - shine_width - shine_softness, shine_progress - shine_width, UV.x) - 
                 smoothstep(shine_progress + shine_width, shine_progress + shine_width + shine_softness, UV.x);
    
    // Смешиваем основной цвет с цветом блика
    vec4 final_color = mix(base_color, shine_color, band);
    
    // Применяем финальный цвет, сохраняя прозрачность оригинала
    COLOR = vec4(final_color.rgb, base_color.a);
}