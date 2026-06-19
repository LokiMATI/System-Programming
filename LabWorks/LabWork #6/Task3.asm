%include "io.inc"

section .text
global main
main:
    sub esp, 256
    mov edi, esp

    GET_STRING [edi], 256

    mov esi, edi

count_loop:
    cmp byte [esi], 0
    je end_count
    inc esi
    jmp count_loop

end_count:
    sub esi, edi

    PRINT_DEC 4, esi
    NEWLINE

    add esp, 256
    xor eax, eax
    ret
