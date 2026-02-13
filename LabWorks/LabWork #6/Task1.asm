%include "io64.inc"

global main
 
section .data
string dw "Yes", 0
len equ $-string
elemSize equ 2
count equ len / elemSize
lastPosition equ count - elemSize

section .bss
reveseString resw count
 
section .text
main:
    mov rbp, rsp; for correct debugging
    mov rsi, string
    add rsi, lastPosition
    mov rdi, reveseString
    add rdi, lastPosition
    mov rcx, count
 
    std
    rep movsw
    
    PRINT_STRING [reveseString]
 
    