; Объявляем внешние функции языка Си для ввода-вывода
extern _printf
extern _scanf
extern _get_str_len

section .data
    ; Форматы для ввода строки и вывода числа
    fmt_in  db "%255s", 0
    fmt_out db "%d", 10, 0

section .bss
    ; Буфер для строки (256 байт) в секции данных для надежности
    buffer resb 256

section .text
global _main
_main:
    push ebp
    mov ebp, esp

    ; 1. Ввод строки с клавиатуры через scanf
    push buffer
    push fmt_in
    call _scanf
    add esp, 8

    ; 2. Вызов вашей подпрограммы из файла strlen.asm
    push buffer
    call _get_str_len
    add esp, 4          ; В EAX вернулась длина строки

    ; 3. Вывод результата в консоль через printf
    push eax            ; Передаем длину строки
    push fmt_out        ; Передаем формат вывода
    call _printf
    add esp, 8

    ; Корректное завершение программы
    xor eax, eax
    mov esp, ebp
    pop ebp
    ret
