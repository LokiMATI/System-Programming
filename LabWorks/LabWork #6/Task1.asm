%include "io.inc"

section .data
    src_str db "Hello, World!", 0
    src_len equ $ - src_str - 1

section .bss
    dst_str resb src_len + 1

section .text
global main
main:
    mov ebp, esp; for correct debugging
    
    mov esi, src_str
    add esi, src_len - 1
    
    mov edi, dst_str
    mov ecx, src_len

reverse_loop:
    mov al, [esi]
    mov [edi], al
    dec esi
    inc edi 
    loop reverse_loop

    mov byte [edi], 0

    PRINT_STRING dst_str
    NEWLINE

    xor eax, eax
    ret
