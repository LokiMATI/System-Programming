#pragma once

#ifdef LIBDLL_EXPORTS
#define LIBDLL_API __declspec(dllexport)
#else
#define LIBDLL_API __declspec(dllimport)
#endif // LIBDLL_EXPORTS

struct Point
{
	double x;
	double y;
};

extern "C" LIBDLL_API int is_simple(int);
extern "C" LIBDLL_API int is_simple_array(int*, int);
extern "C" LIBDLL_API double calc_def(Point, Point);