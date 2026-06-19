global _get_str_len

section .text
_get_str_len:
    push ebp
    mov ebp, esp
    push esi

    mov esi, [ebp + 8]
    xor eax, eax

count_loop:
    mov cl, [esi]
    cmp cl, 0
    je end_count
    cmp cl, 10
    je end_count
    cmp cl, 13
    je end_count

    inc eax
    inc esi
    jmp count_loop

end_count:
    pop esi
    mov esp, ebp
    pop ebp
    ret
