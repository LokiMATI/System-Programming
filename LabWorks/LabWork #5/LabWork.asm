%include "io64.inc"

extern scanf
extern printf
extern MessageBoxA

section .data
scan_template: db "%d %d",0
print_template: db "Result: %f",0
messageBox_title: db "Yes or no messagebox",0
messageBox_text: db "You wanna to go away?",0
first_leg: dq 0.0
second_leg: dq 0.0
hypotenuse: dq 0.0

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    start:
    sub rsp, 40
    lea rcx, template
    mov rdx, first_leg
    mov r8, second_leg
    call scanf
    add rsp, 40
    
    finit
    fild qword [first_leg]
    fmul ST0, ST0
    
    fild qword [second_leg]
    fmul ST0, ST0
    
    fadd
    fsqrt
    fst qword [hypotenuse]
    
    sub rsp, 40
    lea rcx, print_template
    mov rdx, [hypotenuse]
    call printf
    add rsp, 40
    NEWLINE
    
    sub rsp, 40
    mov rcx, 0
    lea rdx, messageBox_text
    lea r8, messageBox_title
    mov r9, 1
    call MessageBoxA
    add rsp, 40
    
    cmp rax, 2
    je start
    
    
    xor rax, rax
    ret
    