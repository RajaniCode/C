// msys64-mingw64-gcc.c
// arch -x86_64 zsh
// gcc -c -fPIC msys64-mingw64-gcc.c
// gcc -shared -o msys64-mingw64-gcc.so msys64-mingw64-gcc.o

#include <stdio.h>

void 
c_code(void) {
  printf("C!\n");
}