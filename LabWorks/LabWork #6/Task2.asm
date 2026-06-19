%include "io.inc"

section .data
    s1 db "Hello World", 0

section .text
global main
main:
    sub esp, 264
    lea edi, [esp + 8]

    GET_DEC 4, [esp]
    GET_DEC 4, [esp + 4]

    mov esi, s1
    add esi, [esp]
    mov ecx, [esp + 4]
    jecxz end_copy

copy_loop:
    mov al, [esi]
    test al, al
    jz end_copy
    
    mov [edi], al
    inc esi
    inc edi
    loop copy_loop

end_copy:
    mov byte [edi], 0

    lea eax, [esp + 8]      ; Передаем адрес s2 из стека для вывода
    PRINT_STRING [eax]
    NEWLINE

    add esp, 264            ; Возвращаем стек в исходное состояние
    xor eax, eax
    ret
