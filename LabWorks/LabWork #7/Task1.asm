%include "io64.inc"

section .text
global _start


_start:
    mov rcx, 4
    mov rdx, 2
    call pow
    PRINT_STRING "Result: "
    PRINT_UDEC 8, rax
    
    ret

pow:
    mov rax, rcx 
    mov rcx, rdx
    mov rdx, rax
    mov rax, 1
    cmp rcx, 0
    je loop_end
    
mainloop:
    mul rdx
    loop mainloop
    
loop_end:
    ret