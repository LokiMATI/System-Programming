#include "pch.h"
#include "functions.h"
#include <cstdlib>;

int is_simple(int val) {
	for (int i = 2; i < val; i++) {
		if (val % i == 0) {
			return 0;
		}
	}
	return 1;
}

int is_simple_array(int* arr, int length) {
	int count{};
	for (int i = 0; i < length; i++) {
		if (is_simple(arr[i]) == 1) {
			count++;
		}
	}
	return count;
}

double calc_def(Point first, Point second) {
	double def_x = abs(first.x - second.x);
	double def_y = abs(first.y - second.y);
	double def = sqrt(pow(def_x, 2) + pow(def_y, 2));

	return def;
}