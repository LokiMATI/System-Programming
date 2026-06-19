%include "io.inc"

section .data
    password db "qwert", 0
    pass_len equ $ - password - 1

    msg_ok   db "Access granted", 0
    msg_fail db "Incorrect password", 0

section .text
global main
main:
    sub esp, 256
    mov edi, esp

    GET_STRING [edi], 256

    mov esi, edi
count_len:
    cmp byte [esi], 0
    je check_start
    inc esi
    jmp count_len

check_start:
    sub esi, edi
    cmp esi, pass_len
    jne access_denied

    mov esi, password
    mov ecx, pass_len
    cld

repe cmpsb
    jne access_denied

    PRINT_STRING msg_ok
    jmp end_prog

access_denied:
    PRINT_STRING msg_fail

end_prog:
    NEWLINE
    add esp, 256
    xor eax, eax
    ret
