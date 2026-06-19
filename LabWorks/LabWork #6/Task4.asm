%include "io.inc"

section .data
    arr dd 5, 12, 7, 12, 3, 12, 9, 12, 1
    len equ ($ - arr) / 4

section .text
global main
main:
    GET_DEC 4, eax
    
    mov esi, arr
    mov ecx, len
    xor edx, edx 

search_loop:
    cmp [esi], eax
    jne skip_inc
    inc edx

skip_inc:
    add esi, 4
    loop search_loop

    PRINT_DEC 4, edx
    NEWLINE

    xor eax, eax
    ret
