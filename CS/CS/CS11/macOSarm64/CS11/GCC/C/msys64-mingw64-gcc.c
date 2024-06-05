// msys64-mingw64-gcc.c
// gcc -c -fPIC msys64-mingw64-gcc.c
// gcc -shared -o msys64-mingw64-gcc.so msys64-mingw64-gcc.o
#include <stdio.h>
int
c_code(int number) {
  return number * 10;
}