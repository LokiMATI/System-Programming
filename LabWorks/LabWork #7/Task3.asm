%include "io.inc"

section .text
global main
main:
    mov ebp, esp

    sub esp, 16

    GET_DEC 4, [esp + 12]
    GET_DEC 4, [esp + 8]
    GET_DEC 4, [esp + 4]
    GET_DEC 4, [esp]

    push dword [esp]        ; x
    push dword [esp + 8]    ; c
    push dword [esp + 16]   ; b
    push dword [esp + 24]   ; a

    call calc_poly
    add esp, 16

    PRINT_DEC 4, eax
    NEWLINE

    add esp, 16
    xor eax, eax
    ret

calc_poly:
    push ebp
    mov ebp, esp

    push ebx

    sub esp, 8
    
    mov eax, [ebp + 12]
    imul eax, [ebp + 20]
    mov [ebp - 8], eax

    mov eax, [ebp + 20]
    imul eax, [ebp + 20]
    imul eax, [ebp + 8]
    mov [ebp - 12], eax

    mov eax, [ebp - 12]
    add eax, [ebp - 8]
    add eax, [ebp + 16]

    add esp, 8

    pop ebx

    mov esp, ebp
    pop ebp
    ret