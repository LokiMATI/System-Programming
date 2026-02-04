%include "io64.inc"

section .data
sum: dq 0
percent: dq 0
hundred: dq 100
million: dq 1000000

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    
    PRINT_STRING 'Input start sum: '
    GET_UDEC 8, sum
    NEWLINE
    
    PRINT_STRING 'Input annual percentage: '
    GET_UDEC 8, percent
    NEWLINE

    FINIT
    FILD qword [million]
    FILD qword [percent]
    FILD qword [hundred]
    FDIV
    FLD1
    FADD
    
    FILD qword [sum]
      
    MOV ecx, 0     
    
    CMP rbx, rdx
    start_while:
    JAE end_while
        FMUL ST0, ST1
        INC ecx
        FCOM ST2
        FSTSW ax
        SAHF
        JMP start_while
    end_while:
    
    PRINT_STRING 'You need '
    PRINT_UDEC 4, ecx
    PRINT_STRING ' years'
    ret